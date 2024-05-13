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

using System.Collections.Generic;

/// <summary>
/// The rtf builder.
/// </summary>
public class RTFBuilder
{
    /// <summary>
    /// The m_ sb.
    /// </summary>
    private readonly StringBuilder m_Sb = new();

    private readonly List<Color> m_Colortable = [];

    private readonly StringCollection m_Fonttable = [];

    private Color m_Forecolor;

    /// <summary>
    /// Sets the fore color.
    /// </summary>
    public Color ForeColor
    {
        set
        {
            if (!this.m_Colortable.Contains(value))
            {
                this.m_Colortable.Add(value);
            }

            if (value != this.m_Forecolor)
            {
                this.m_Sb.Append($"\\cf{this.m_Colortable.IndexOf(value) + 1} ");
            }

            this.m_Forecolor = value;
        }
    }

    /// <summary>
    /// The m_ backcolor.
    /// </summary>
    private Color m_Backcolor;

    public Color BackColor
    {
        set
        {
            if (!this.m_Colortable.Contains(value))
            {
                this.m_Colortable.Add(value);
            }

            if (value != this.m_Backcolor)
            {
                this.m_Sb.Append($"\\highlight{this.m_Colortable.IndexOf(value) + 1} ");
            }

            this.m_Backcolor = value;
        }
    }
    public RTFBuilder()
    {
        this.ForeColor = Color.FromKnownColor(KnownColor.WindowText);
        this.BackColor = Color.FromKnownColor(KnownColor.Window);
        this.m_DefaultFontSize = 20F;
    }

    public void AppendLine()
    {
        this.m_Sb.AppendLine("\\line");
    }

    public void Append(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        value = CheckChar(value);

        if (value.Contains(Environment.NewLine))
        {
            var lines = value.Split([Environment.NewLine], StringSplitOptions.None);

            foreach (var line in lines)
            {
                this.m_Sb.Append(line);
                this.m_Sb.Append("\\line ");
            }
        }
        else if (value.Contains('\n'))
        {
            // Don't fail on non-Windows Environment.NewLine 
            var lines = value.Split(["\n"], StringSplitOptions.None);
            foreach (var line in lines)
            {
                this.m_Sb.Append(line);
                this.m_Sb.Append("\\line ");
            }
        }
        else if (value.Contains('\r'))
        {
            // A sole carriage return should also be treated as Environment.NewLine
            var lines = value.Split(["\n"], StringSplitOptions.None);
            foreach (var line in lines)
            {
                this.m_Sb.Append(line);
                this.m_Sb.Append("\\line ");
            }
        }
        else
        {
            this.m_Sb.Append(value);
        }
    }

    private static readonly char[] Slashable = ['{', '}', '\\'];

    private readonly float m_DefaultFontSize;

    private static string CheckChar(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return value;
        }

        if (value.IndexOfAny(Slashable) >= 0)
        {
            value = value.Replace("{", "\\{").Replace("}", "\\}").Replace("\\", "\\\\");
        }

        var replaceuni = value.Any(t => t > 255);

        if (!replaceuni)
        {
            return value;
        }

        var sb = new StringBuilder();
        foreach (var t in value)
        {
            if (t <= 255)
            {
                sb.Append(t);
            }
            else
            {
                sb.Append("\\u");
                sb.Append((int)t);
                sb.Append('?');
            }
        }

        value = sb.ToString();

        return value;
    }

    public new string ToString()
    {
        var result = new StringBuilder();
        result.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang3081");
        result.Append("{\\fonttbl");
        for (var i = 0; i < this.m_Fonttable.Count; i++)
        {
            try
            {
                result.AppendFormat(this.m_Fonttable[i], i);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        result.AppendLine("}");
        result.Append("{\\colortbl ;");
        foreach (var item in this.m_Colortable)
        {
            result.AppendFormat("\\red{0}\\green{1}\\blue{2};", item.R, item.G, item.B);
        }

        result.AppendLine("}");
        result.Append("\\viewkind4\\uc1\\pard\\plain\\f0");
        result.AppendFormat("\\fs{0} ", this.m_DefaultFontSize);
        result.AppendLine();
        result.Append(this.m_Sb);
        result.Append('}');
        return result.ToString();
    }
}