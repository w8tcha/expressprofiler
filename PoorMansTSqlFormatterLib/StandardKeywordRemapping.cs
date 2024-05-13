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
    static class StandardKeywordRemapping
    {
        public static Dictionary<string, string> Instance { get; private set;  }
        static StandardKeywordRemapping()
        {
            Instance = new Dictionary<string, string>
            {
                { "PROC", "PROCEDURE" },
                { "LEFT OUTER JOIN", "LEFT JOIN" },
                { "RIGHT OUTER JOIN", "RIGHT JOIN" },
                { "FULL OUTER JOIN", "FULL JOIN" },
                { "JOIN", "INNER JOIN" },
                //TODO: This is now wrong in MERGE statements... we now need a scope-limitation strategy :(
                //Instance.Add("INSERT", "INSERT INTO");
                { "TRAN", "TRANSACTION" },
                { "BEGIN TRAN", "BEGIN TRANSACTION" },
                { "COMMIT TRAN", "COMMIT TRANSACTION" },
                { "ROLLBACK TRAN", "ROLLBACK TRANSACTION" },
                { "BINARY VARYING", "VARBINARY" },
                { "CHAR VARYING", "VARCHAR" },
                { "CHARACTER", "CHAR" },
                { "CHARACTER VARYING", "VARCHAR" },
                { "DEC", "DECIMAL" },
                { "DOUBLE PRECISION", "FLOAT" },
                { "INTEGER", "INT" },
                { "NATIONAL CHARACTER", "NCHAR" },
                { "NATIONAL CHAR", "NCHAR" },
                { "NATIONAL CHARACTER VARYING", "NVARCHAR" },
                { "NATIONAL CHAR VARYING", "NVARCHAR" },
                { "NATIONAL TEXT", "NTEXT" },
                { "OUT", "OUTPUT" }
            };
			//TODO: This is wrong when a TIMESTAMP column is unnamed; ROWVERSION does not auto-name. Due to context-sensitivity, this mapping is disabled for now.
            // REF: http://msdn.microsoft.com/en-us/library/ms182776.aspx
            //Instance.Add("TIMESTAMP", "ROWVERSION");
        }
    }
}
