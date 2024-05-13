/*
Poor Man's T-SQL Formatter - a small free Transact-SQL formatting 
library for .NET 8.0 and JS, written in C#. 
Copyright (C) 2011-2017 Tao Klerks

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;

namespace PoorMansTSqlFormatterLib
{
    static class ObfuscatingKeywordMapping
    {
        public static Dictionary<string, string> Instance { get; private set; }
        static ObfuscatingKeywordMapping()
        {
            Instance = new Dictionary<string, string>
            {
                { "PROCEDURE", "PROC" },
                { "LEFT OUTER JOIN", "LEFT JOIN" },
                { "RIGHT OUTER JOIN", "RIGHT JOIN" },
                { "FULL OUTER JOIN", "FULL JOIN" },
                { "INNER JOIN", "JOIN" },
                { "TRANSACTION", "TRAN" },
                { "BEGIN TRANSACTION", "BEGIN TRAN" },
                { "COMMIT TRANSACTION", "COMMIT TRAN" },
                { "ROLLBACK TRANSACTION", "ROLLBACK TRAN" },
                { "VARBINARY", "BINARY VARYING" },
                { "VARCHAR", "CHARACTER VARYING" },
                { "CHARACTER", "CHAR" },
                { "CHAR VARYING", "VARCHAR" },
                { "DECIMAL", "DEC" },
                { "FLOAT", "DOUBLE PRECISION" },
                { "INTEGER", "INT" },
                { "NCHAR", "NATIONAL CHARACTER" },
                { "NATIONAL CHAR", "NCHAR" },
                { "NVARCHAR", "NATIONAL CHARACTER VARYING" },
                { "NATIONAL CHAR VARYING", "NVARCHAR" },
                { "NTEXT", "NATIONAL TEXT" }
            };
        }
    }
}
