﻿/* ExpressProfiler (aka SqlExpress Profiler)
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

namespace ExpressProfiler.Extensions;

using PoorMansTSqlFormatterLib;

/// <summary>
/// The extensions.
/// </summary>
internal static class TextExtensions
{
    /// <summary>
    /// The SQL formatting manager.
    /// </summary>
    private static readonly Lazy<SqlFormattingManager> SqlFormattingManager = new();

    /// <summary>
    /// Parse SQL.
    /// </summary>
    /// <param name="text">
    /// The text.
    /// </param>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string ParseSql(this string text)
    {
        const string SQL_PARSING_ERROR = "--WARNING! ERRORS ENCOUNTERED DURING SQL PARSING!";

        var sql = SqlFormattingManager.Value.Format(text);
        sql = sql.Replace(SQL_PARSING_ERROR, string.Empty);

        return sql;
    }
}