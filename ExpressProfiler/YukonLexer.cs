/* ExpressProfiler (aka SqlExpress Profiler)
   https://github.com/OleksiiKovalov/expressprofiler
 * Copyright (C) Oleksii Kovalov
 * based on the sample application for demonstrating Sql Server
 * Profiling written by Locky, 2009.
 *
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

namespace ExpressProfiler;

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

    private readonly SqlTokens m_Tokens = new();

    private const string IdentifierStr = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_#$";

    private readonly char[] m_IdentifiersArray = IdentifierStr.ToCharArray();

    private const string HexDigits = "1234567890abcdefABCDEF";

    private const string NumberStr = "1234567890.-";

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
            var forecolor = this.TokenId switch
                {
                    TokenKind.tkKey => Color.Blue,
                    TokenKind.tkFunction => Color.Fuchsia,
                    TokenKind.tkGreyKeyword => Color.Gray,
                    TokenKind.tkFuKeyword => Color.Fuchsia,
                    TokenKind.tkDatatype => Color.Blue,
                    TokenKind.tkNumber => Color.Red,
                    TokenKind.tkString => Color.Red,
                    TokenKind.tkComment => Color.DarkGreen,
                    _ => Color.Black
                };

            sb.ForeColor = forecolor;
            if (this.Token == Environment.NewLine || this.Token is "\r" or "\n")
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
        if (this.GetChar(this.m_Run) == '\x0A')
        {
            this.m_Run++;
        }
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
                    while (!(c is '\x00' or '\x0A' or '\x0D'));

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
            if (this.m_Run <= 0 && this.Range == SqlRange.rsString && this.GetChar(this.m_Run) == '\x27')
            {
                return;
            }

            this.Range = SqlRange.rsString;
            char c;
            do
            {
                this.m_Run++;
                c = this.GetChar(this.m_Run);
            }
            while (!(c is '\x00' or '\x0A' or '\x0D' or '\x27'));

            if (this.GetChar(this.m_Run) != '\x27')
            {
                return;
            }

            this.m_Run++;
            this.Range = SqlRange.rsUnknown;
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
        switch (chr)
        {
            case >= 'A' and <= 'Z' or >= 'a' and <= 'z' or '_' or '#':
                this.IdentProc();
                return;
            case >= '0' and <= '9':
                this.NumberProc();
                return;
            case <= '\x09' or >= '\x0B' and <= '\x0C' or >= '\x0E' and <= '\x20':
                this.SpaceProc();
                return;
            default:
                this.UnknownProc();
                break;
        }
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
        while (!(c is > '\x20' or '\x00' or '\x0A' or '\x0D'));
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
            while (HexDigits.Contains(this.GetChar(this.m_Run)))
            {
                this.m_Run++;
            }

            return;
        }

        this.m_Run++;
        this.TokenId = TokenKind.tkNumber;
        while (NumberStr.Contains(this.GetChar(this.m_Run)))
        {
            if (this.GetChar(this.m_Run) == '.' && this.GetChar(this.m_Run + 1) == '.')
            {
                break;
            }

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
        if (this.GetChar(this.m_Run) == '=')
        {
            this.m_Run++;
        }
    }

    private void KeyHash(int pos)
    {
        this.m_StringLen = 0;
        while (Array.BinarySearch(this.m_IdentifiersArray, this.GetChar(pos)) >= 0)
        {
            this.m_StringLen++;
            pos++;
        }
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
            while (IdentifierStr.Contains(this.GetChar(this.m_Run)))
            {
                this.m_Run++;
            }
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
            while (IdentifierStr.Contains(this.GetChar(i)));

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
        if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '=')
        {
            this.m_Run++;
        }
    }

    private void OrSymbolProc()
    {
        this.TokenId = TokenKind.tkSymbol;
        this.m_Run++;
        if (this.GetChar(this.m_Run) == '=' || this.GetChar(this.m_Run) == '|')
        {
            this.m_Run++;
        }
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
            while (!(c is '\x00' or '\x0A' or '\x0D'));
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