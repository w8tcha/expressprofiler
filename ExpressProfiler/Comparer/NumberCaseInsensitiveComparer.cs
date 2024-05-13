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

namespace ExpressProfiler.Comparer;

/// <summary>
/// The number case insensitive comparer.
/// </summary>
public class NumberCaseInsensitiveComparer : CaseInsensitiveComparer
{
    /// <summary>
    /// The compare.
    /// </summary>
    /// <param name="x">
    /// The x.
    /// </param>
    /// <param name="y">
    /// The y.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public new int Compare(object x, object y)
    {
        // in case x,y are strings and actually number,
        // convert them to int and use the base.Compare for comparison
        if (x is string s && IsWholeNumber(s) && y is string s1 && IsWholeNumber(s1))
        {
            return base.Compare(Convert.ToInt32(x), Convert.ToInt32(y));
        }

        return base.Compare(x, y);
    }

    /// <summary>
    /// The is whole number.
    /// </summary>
    /// <param name="strNumber">
    /// The string number.
    /// </param>
    /// <returns>
    /// The <see cref="bool"/>.
    /// </returns>
    private static bool IsWholeNumber(string strNumber)
    {
        // use a regular expression to find out if string is actually a number
        var objNotWholePattern = new Regex("[^0-9]");
        return !objNotWholePattern.IsMatch(strNumber);
    }
}