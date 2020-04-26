/* ExpressProfiler (aka SqlExpress Profiler)
   https://github.com/OleksiiKovalov/expressprofiler
 * Copyright (C) Oleksii Kovalov
 * Forked by Ingo Herbote
 * https://github.com/w8tcha/expressprofiler
 * 
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at

 * https://www.apache.org/licenses/LICENSE-2.0

 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

//Traceutils assembly
//writen by Locky, 2009.

namespace ExpressProfiler
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class YukonLexer
    {
        public enum TokenKind
        {
            tkComment,

            tkDatatype,

            tkFunction,

            tkIdentifier,

            tkKey,

            tkNull,

            tkNumber,

            tkSpace,

            tkString,

            tkSymbol,

            tkUnknown,

            tkVariable,

            tkGreyKeyword,

            tkFuKeyword
        }

        private enum SqlRange
        {
            rsUnknown,

            rsComment,

            rsString
        }

        private readonly Sqltokens m_Tokens = new Sqltokens();

        const string IdentifierStr = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_#$";

        private readonly char[] m_IdentifiersArray = IdentifierStr.ToCharArray();

        const string HexDigits = "1234567890abcdefABCDEF";

        const string NumberStr = "1234567890.-";

        private int m_StringLen;

        private int m_TokenPos;

        private string m_Line;

        private int m_Run;

        private TokenKind TokenId { get; set; }

        private string Token { get; set; } = string.Empty;

        private SqlRange Range { get; set; }

        private char GetChar(int idx)
        {
            return idx >= this.m_Line.Length ? '\x00' : this.m_Line[idx];
        }

        public string StandardSql(string sql)
        {
            var result = new StringBuilder();
            this.Line = sql;
            while (this.TokenId != TokenKind.tkNull)
            {
                switch (this.TokenId)
                {
                    case TokenKind.tkNumber:
                    case TokenKind.tkString:
                        result.Append("<??>");
                        break;
                    default:
                        result.Append(this.Token);
                        break;
                }

                this.Next();
            }

            return result.ToString();
        }

        public YukonLexer()
        {
            Array.Sort(this.m_IdentifiersArray);
        }

        public void FillRichEdit(RichTextBox rich, string value)
        {
            rich.Text = string.Empty;
            this.Line = value;

            var sb = new RTFBuilder { BackColor = rich.BackColor };
            while (this.TokenId != TokenKind.tkNull)
            {
                Color forecolor;
                switch (this.TokenId)
                {
                    case TokenKind.tkKey:
                        forecolor = Color.Blue;
                        break;
                    case TokenKind.tkFunction:
                        forecolor = Color.Fuchsia;
                        break;
                    case TokenKind.tkGreyKeyword:
                        forecolor = Color.Gray;
                        break;
                    case TokenKind.tkFuKeyword:
                        forecolor = Color.Fuchsia;
                        break;
                    case TokenKind.tkDatatype:
                        forecolor = Color.Blue;
                        break;
                    case TokenKind.tkNumber:
                        forecolor = Color.Red;
                        break;
                    case TokenKind.tkString:
                        forecolor = Color.Red;
                        break;
                    case TokenKind.tkComment:
                        forecolor = Color.DarkGreen;
                        break;
                    default:
                        forecolor = Color.Black;
                        break;
                }

                sb.ForeColor = forecolor;
                if (this.Token == Environment.NewLine || this.Token == "\r" || this.Token == "\n")
                {
                    sb.AppendLine();
                }
                else
                {
                    sb.Append(this.Token);
                }

                this.Next();
            }

            rich.Rtf = sb.ToString();
        }

        private string Line
        {
            set
            {
                this.Range = SqlRange.rsUnknown;
                this.m_Line = value;
                this.m_Run = 0;
                this.Next();
            }
        }

        private void NullProc()
        {
            this.TokenId = TokenKind.tkNull;
        }

        // ReSharper disable InconsistentNaming
        private void LFProc()
        {
            this.TokenId = TokenKind.tkSpace;
            this.m_Run++;
        }

        private void CRProc()
        {
            this.TokenId = TokenKind.tkSpace;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '\x0A') this.m_Run++;
        }

        // ReSharper restore InconsistentNaming
        private void AnsiCProc()
        {
            switch (this.GetChar(this.m_Run))
            {
                case '\x00':
                    this.NullProc();
                    break;
                case '\x0A':
                    this.LFProc();
                    break;
                case '\x0D':
                    this.CRProc();
                    break;

                default:
                    {
                        this.TokenId = TokenKind.tkComment;
                        char c;
                        do
                        {
                            if (this.GetChar(this.m_Run) == '*' && this.GetChar(this.m_Run + 1) == '/')
                            {
                                this.Range = SqlRange.rsUnknown;
                                this.m_Run += 2;
                                break;
                            }

                            this.m_Run++;
                            c = this.GetChar(this.m_Run);
                        }
                        while (!(c == '\x00' || c == '\x0A' || c == '\x0D'));

                        break;
                    }
            }
        }

        private void AsciiCharProc()
        {
            if (this.GetChar(this.m_Run) == '\x00')
            {
                this.NullProc();
            }
            else
            {
                this.TokenId = TokenKind.tkString;
                if (this.m_Run > 0 || this.Range != SqlRange.rsString || this.GetChar(this.m_Run) != '\x27')
                {
                    this.Range = SqlRange.rsString;
                    char c;
                    do
                    {
                        this.m_Run++;
                        c = this.GetChar(this.m_Run);
                    }
                    while (!(c == '\x00' || c == '\x0A' || c == '\x0D' || c == '\x27'));

                    if (this.GetChar(this.m_Run) == '\x27')
                    {
                        this.m_Run++;
                        this.Range = SqlRange.rsUnknown;
                    }
                }
            }
        }

        private void DoProcTable(char chr)
        {
            switch (chr)
            {
                case '\x00':
                    this.NullProc();
                    break;
                case '\x0A':
                    this.LFProc();
                    break;
                case '\x0D':
                    this.CRProc();
                    break;
                case '\x27':
                    this.AsciiCharProc();
                    break;

                case '=':
                    this.EqualProc();
                    break;
                case '>':
                    this.GreaterProc();
                    break;
                case '<':
                    this.LowerProc();
                    break;
                case '-':
                    this.MinusProc();
                    break;
                case '|':
                    this.OrSymbolProc();
                    break;
                case '+':
                    this.PlusProc();
                    break;
                case '/':
                    this.SlashProc();
                    break;
                case '&':
                    this.AndSymbolProc();
                    break;
                case '\x22':
                    this.QuoteProc();
                    break;
                case ':':
                case '@':
                    this.VariableProc();
                    break;
                case '^':
                case '%':
                case '*':
                case '!':
                    this.SymbolAssignProc();
                    break;
                case '{':
                case '}':
                case '.':
                case ',':
                case ';':
                case '?':
                case '(':
                case ')':
                case ']':
                case '~':
                    this.SymbolProc();
                    break;
                case '[':
                    this.BracketProc();
                    break;
                default:
                    this.DoInsideProc(chr);
                    break;
            }
        }

        private void DoInsideProc(char chr)
        {
            if (chr >= 'A' && chr <= 'Z' || chr >= 'a' && chr <= 'z' || chr == '_' || chr == '#')
            {
                this.IdentProc();
                return;
            }

            if (chr >= '0' && chr <= '9')
            {
                this.NumberProc();
                return;
            }

            if (chr >= '\x00' && chr <= '\x09' || chr >= '\x0B' && chr <= '\x0C'
                                               || chr >= '\x0E' && chr <= '\x20')
            {
                this.SpaceProc();
                return;
            }

            this.UnknownProc();
        }

        private void SpaceProc()
        {
            this.TokenId = TokenKind.tkSpace;
            char c;
            do
            {
                this.m_Run++;
                c = this.GetChar(this.m_Run);
            }
            while (!(c > '\x20' || c == '\x00' || c == '\x0A' || c == '\x0D'));
        }

        private void UnknownProc()
        {
            this.m_Run++;
            this.TokenId = TokenKind.tkUnknown;
        }

        private void NumberProc()
        {
            this.TokenId = TokenKind.tkNumber;
            if (this.GetChar(this.m_Run) == '0'
                && (this.GetChar(this.m_Run + 1) == 'X' || this.GetChar(this.m_Run + 1) == 'x'))
            {
                this.m_Run += 2;
                while (HexDigits.IndexOf(this.GetChar(this.m_Run)) != -1) this.m_Run++;
                return;
            }

            this.m_Run++;
            this.TokenId = TokenKind.tkNumber;
            while (NumberStr.IndexOf(this.GetChar(this.m_Run)) != -1)
            {
                if (this.GetChar(this.m_Run) == '.' && this.GetChar(this.m_Run + 1) == '.') break;
                this.m_Run++;
            }
        }

        private void QuoteProc()
        {
            this.TokenId = TokenKind.tkIdentifier;
            this.m_Run++;
            while (!(this.GetChar(this.m_Run) == '\x00' || this.GetChar(this.m_Run) == '\x0A'
                                                        || this.GetChar(this.m_Run) == '\x0D'))
            {
                if (this.GetChar(this.m_Run) == '\x22')
                {
                    this.m_Run++;
                    break;
                }

                this.m_Run++;
            }
        }

        private void BracketProc()
        {
            this.TokenId = TokenKind.tkIdentifier;
            this.m_Run++;
            while (!(this.GetChar(this.m_Run) == '\x00' || this.GetChar(this.m_Run) == '\x0A'
                                                        || this.GetChar(this.m_Run) == '\x0D'))
            {
                if (this.GetChar(this.m_Run) == ']')
                {
                    this.m_Run++;
                    break;
                }

                this.m_Run++;
            }
        }

        private void SymbolProc()
        {
            this.m_Run++;
            this.TokenId = TokenKind.tkSymbol;
        }

        private void SymbolAssignProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '=') this.m_Run++;
        }

        private void KeyHash(int pos)
        {
            this.m_StringLen = 0;
            while (Array.BinarySearch(this.m_IdentifiersArray, this.GetChar(pos)) >= 0)
            {
                this.m_StringLen++;
                pos++;
            }

            return;
        }

        private TokenKind IdentKind()
        {
            this.KeyHash(this.m_Run);
            return this.m_Tokens[
                this.m_Line.Substring(this.m_TokenPos, this.m_Run + this.m_StringLen - this.m_TokenPos)];
        }

        private void IdentProc()
        {
            this.TokenId = this.IdentKind();
            this.m_Run += this.m_StringLen;
            if (this.TokenId == TokenKind.tkComment)
            {
                while (!(this.GetChar(this.m_Run) == '\x00' || this.GetChar(this.m_Run) == '\x0A'
                                                            || this.GetChar(this.m_Run) == '\x0D'))
                {
                    this.m_Run++;
                }
            }
            else
            {
                while (IdentifierStr.IndexOf(this.GetChar(this.m_Run)) != -1) this.m_Run++;
            }
        }

        private void VariableProc()
        {
            if (this.GetChar(this.m_Run) == '@' && this.GetChar(this.m_Run + 1) == '@')
            {
                this.m_Run += 2;
                this.IdentProc();
            }
            else
            {
                this.TokenId = TokenKind.tkVariable;
                var i = this.m_Run;
                do
                {
                    i++;
                }
                while (IdentifierStr.IndexOf(this.GetChar(i)) != -1);

                this.m_Run = i;
            }
        }

        private void AndSymbolProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '&') this.m_Run++;
        }

        private void SlashProc()
        {
            this.m_Run++;
            switch (this.GetChar(this.m_Run))
            {
                case '*':
                    {
                        this.Range = SqlRange.rsComment;
                        this.TokenId = TokenKind.tkComment;
                        do
                        {
                            this.m_Run++;
                            if (this.GetChar(this.m_Run) != '*' || this.GetChar(this.m_Run + 1) != '/')
                            {
                                continue;
                            }

                            this.Range = SqlRange.rsUnknown;
                            this.m_Run += 2;
                            break;
                        }
                        while (!(this.GetChar(this.m_Run) == '\x00' || this.GetChar(this.m_Run) == '\x0D'
                                                                    || this.GetChar(this.m_Run) == '\x0A'));
                    }

                    break;
                case '=':
                    this.m_Run++;
                    this.TokenId = TokenKind.tkSymbol;
                    break;
                default:
                    this.TokenId = TokenKind.tkSymbol;
                    break;
            }
        }

        private void PlusProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '=') this.m_Run++;
        }

        private void OrSymbolProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '|') this.m_Run++;
        }

        private void MinusProc()
        {
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '-')
            {
                this.TokenId = TokenKind.tkComment;
                char c;
                do
                {
                    this.m_Run++;
                    c = this.GetChar(this.m_Run);
                }
                while (!(c == '\x00' || c == '\x0A' || c == '\x0D'));
            }
            else
            {
                this.TokenId = TokenKind.tkSymbol;
            }
        }

        private void LowerProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            switch (this.GetChar(this.m_Run))
            {
                case '=':
                    this.m_Run++;
                    break;
                case '<':
                    this.m_Run++;
                    if (this.GetChar(this.m_Run) == '=')
                    {
                        this.m_Run++;
                    }
                    break;
            }
        }

        private void GreaterProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '>')
            {
                this.m_Run++;
            }
        }

        private void EqualProc()
        {
            this.TokenId = TokenKind.tkSymbol;
            this.m_Run++;
            if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '>')
            {
                this.m_Run++;
            }
        }

        private void Next()
        {
            this.m_TokenPos = this.m_Run;
            switch (this.Range)
            {
                case SqlRange.rsComment:
                    this.AnsiCProc();
                    break;
                case SqlRange.rsString:
                    this.AsciiCharProc();
                    break;
                default:
                    this.DoProcTable(this.GetChar(this.m_Run));
                    break;
            }

            this.Token = this.m_Line.Substring(this.m_TokenPos, this.m_Run - this.m_TokenPos);
        }
    }
}