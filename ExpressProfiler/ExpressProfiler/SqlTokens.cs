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
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The sqltokens.
    /// </summary>
    class Sqltokens
    {
        #region Keywords

        private const string Keywords = "ADD,ALTER,AS,ASC,AUTHORIZATION,BACKUP," + "BEGIN,BREAK,BROWSE,BULK,BY,CASCADE,"
                                                                                 + "CHECK,CHECKPOINT,CLOSE,CLUSTERED,COLLATE,"
                                                                                 + "COLUMN,COMMIT,COMPUTE,CONSTRAINT,CONTAINS,CONTAINSTABLE,"
                                                                                 + "CONTINUE,CREATE,CURRENT,CURSOR,DATABASE,"
                                                                                 + "DBCC,DEALLOCATE,DECLARE,DEFAULT,DELETE,DENY,DESC,DISK,"
                                                                                 + "DISTINCT,DISTRIBUTED,DOUBLE,DROP,DUMMY,DUMP,ELSE,END,"
                                                                                 + "ERRLVL,ESCAPE,EXCEPT,EXEC,EXECUTE,EXIT,FETCH,FILE,"
                                                                                 + "FILLFACTOR,FOR,FOREIGN,FORMSOF,FREETEXT,FREETEXTTABLE,FROM,FULL,"
                                                                                 + "FUNCTION,GOTO,GRANT,GROUP,HAVING,HOLDLOCK,IDENTITY,"
                                                                                 + "IDENTITYCOL,IDENTITY_INSERT,IF,INFLECTIONAL,INDEX,INNER,INSERT,"
                                                                                 + "INTERSECT,INTO,IS,ISABOUT,KEY,KILL,LINENO,LOAD,"
                                                                                 + "NATIONAL,NOCHECK,NONCLUSTERED,OF,OFF,"
                                                                                 + "OFFSETS,ON,OPEN,OPENDATASOURCE,OPENQUERY,OPENROWSET,OPENXML,"
                                                                                 + "OPTION,ORDER,OVER,PERCENT,PLAN,PRECISION,"
                                                                                 + "PRIMARY,PRINT,PROC,PROCEDURE,PUBLIC,RAISERROR,READ,"
                                                                                 + "READTEXT,RECONFIGURE,REFERENCES,REPLICATION,RESTORE,"
                                                                                 + "RESTRICT,RETURN,REVOKE,ROLLBACK,ROWCOUNT,ROWGUIDCOL,"
                                                                                 + "RULE,SAVE,SCHEMA,SELECT,SET,SETUSER,SHUTDOWN,"
                                                                                 + "STATISTICS,TABLE,TEXTSIZE,THEN,TO,TOP,TRAN,TRANSACTION,"
                                                                                 + "TRIGGER,TRUNCATE,TSEQUAL,UNION,UNIQUE,UPDATE,UPDATETEXT,"
                                                                                 + "USE,VALUES,VARYING,VIEW,WAITFOR,WEIGHT,WHEN,WHERE,WHILE,"
                                                                                 + "WITH,WRITETEXT,CURRENT_DATE,CURRENT_TIME"
                                                                                 + ",OUT,NEXT,PRIOR,RETURNS,ABSOLUTE,ACTION,PARTIAL,FALSE"
                                                                                 + ",PREPARE,FIRST,PRIVILEGES,AT,GLOBAL,RELATIVE,ROWS,HOUR,MIN,MAX"
                                                                                 + ",SCROLL,SECOND,SECTION,SIZE,INSENSITIVE,CONNECT,CONNECTION"
                                                                                 + ",ISOLATION,LEVEL,LOCAL,DATE,MINUTE,TRANSLATION"
                                                                                 + ",TRUE,NO,ONLY,WORK,OUTPUT"
                                                                                 + ",ABSOLUTE,ACTION,FREE,PRIOR,PRIVILEGES,AFTER,GLOBAL"
                                                                                 + ",HOUR,RELATIVE,IGNORE,AT,RETURNS,ROLLUP,ROWS,SCROLL"
                                                                                 + ",ISOLATION,SECOND,SECTION,SEQUENCE,LAST,SIZE,LEVEL"
                                                                                 + ",CONNECT,CONNECTION,LOCAL,CUBE,MINUTE,MODIFY,STATIC"
                                                                                 + ",DATE,TEMPORARY,TIME,NEXT,NO,TRANSLATION,TRUE,ONLY"
                                                                                 + ",OUT,DYNAMIC,OUTPUT,PARTIAL,WORK,FALSE,FIRST,PREPARE,GROUPING,FORMAT,INIT,STATS"
                                                                                 + "FORMAT,INIT,STATS,NOCOUNT,FORWARD_ONLY,KEEPFIXED,FORCE,KEEP,MERGE,HASH,LOOP,maxdop,nolock"
                                                                                 + ",updlock,tablock,tablockx,paglock,readcommitted,readpast,readuncommitted,repeatableread,rowlock,serializable,xlock"
                                                                                 + ",delay";

        #endregion

        #region Functions

        private const string Functions = "@@CONNECTIONS,@@CPU_BUSY,@@CURSOR_ROWS,@@DATEFIRST,@@DBTS,@@ERROR,"
                                         + "@@FETCH_STATUS,@@IDENTITY,@@IDLE,@@IO_BUSY,@@LANGID,@@LANGUAGE,"
                                         + "@@LOCK_TIMEOUT,@@MAX_CONNECTIONS,@@MAX_PRECISION,@@NESTLEVEL,@@OPTIONS,"
                                         + "@@PACKET_ERRORS,@@PACK_RECEIVED,@@PACK_SENT,@@PROCID,@@REMSERVER,"
                                         + "@@ROWCOUNT,@@SERVERNAME,@@SERVICENAME,@@SPID,@@TEXTSIZE,@@TIMETICKS,"
                                         + "@@TOTAL_ERRORS,@@TOTAL_READ,@@TOTAL_WRITE,@@TRANCOUNT,@@VERSION,"
                                         + "ABS,ACOS,APP_NAME,ASCII,ASIN,ATAN,ATN2,AVG,BINARY_CHECKSUM,CAST,"
                                         + "CEILING,CHARINDEX,CHECKSUM,CHECKSUM_AGG,COLLATIONPROPERTY,"
                                         + "COLUMNPROPERTY,COL_LENGTH,COL_NAME,COS,COT,COUNT," + "COUNT_BIG,"
                                         + "CURSOR_STATUS,DATABASEPROPERTY,DATABASEPROPERTYEX,"
                                         + "DATALENGTH,DATEADD,DATEDIFF,DATENAME,DATEPART,DAY,DB_ID,DB_NAME,DEGREES,"
                                         + "DIFFERENCE,EXP,FILEGROUPPROPERTY,FILEGROUP_ID,FILEGROUP_NAME,FILEPROPERTY,"
                                         + "FILE_ID,FILE_NAME,FLOOR" + ""
                                         + "FORMATMESSAGE,FULLTEXTCATALOGPROPERTY,FULLTEXTSERVICEPROPERTY,"
                                         + "GETANSINULL,GETDATE,GETUTCDATE,HAS_DBACCESS,HOST_ID,HOST_NAME,"
                                         + "IDENT_CURRENT,IDENT_INCR,IDENT_SEED,INDEXKEY_PROPERTY,INDEXPROPERTY,"
                                         + "INDEX_COL,ISDATE,ISNULL,ISNUMERIC,IS_MEMBER,IS_SRVROLEMEMBER,LEN,LOG,"
                                         + "LOG10,LOWER,LTRIM,MONTH,NEWID,OBJECTPROPERTY,OBJECT_ID,"
                                         + "OBJECT_NAME,PARSENAME,PATINDEX,"
                                         + "PERMISSIONS,PI,POWER,QUOTENAME,RADIANS,RAND,REPLACE,REPLICATE,REVERSE,"
                                         + "ROUND,ROWCOUNT_BIG,RTRIM,SCOPE_IDENTITY,SERVERPROPERTY,SESSIONPROPERTY,"
                                         + "SIGN,SIN,SOUNDEX,SPACE,SQL_VARIANT_PROPERTY,SQRT,SQUARE,"
                                         + "STATS_DATE,STDEV,STDEVP,STR,STUFF,SUBSTRING,SUM,SUSER_SID,SUSER_SNAME,"
                                         + "TAN,TEXTPTR,TEXTVALID,TYPEPROPERTY,UNICODE,UPPER,"
                                         + "USER_ID,USER_NAME,VAR,VARP,YEAR";

        #endregion

        #region Types

        private const string Types = "bigint,binary,bit,char,character,datetime," + "dec,decimal,float,image,int,"
                                                                                  + "integer,money,nchar,ntext,nvarchar,real,"
                                                                                  + "rowversion,smalldatetime,smallint,smallmoney,"
                                                                                  + "sql_variant,sysname,text,timestamp,tinyint,uniqueidentifier,"
                                                                                  + "varbinary,varchar,NUMERIC";

        #endregion

        private const string Greykeywords = "AND,EXISTS,ALL,ANY,BETWEEN,IN,SOME,JOIN,CROSS,OR,NULL,OUTER,NOT,LIKE";

        private const string Fukeywords =
            "CASE,RIGHT,COALESCE,SESSION_USER,CONVERT,SYSTEM_USER,LEFT,CURRENT_TIMESTAMP,CURRENT_USER,NULLIF,USER";

        private readonly Dictionary<string, YukonLexer.TokenKind> m_Words =
            new Dictionary<string, YukonLexer.TokenKind>();

        public YukonLexer.TokenKind this[string token]
        {
            get
            {
                token = token.ToLower();
                return this.m_Words.ContainsKey(token) ? this.m_Words[token] : YukonLexer.TokenKind.tkUnknown;
            }
        }

        private void AddTokens(string tokens, YukonLexer.TokenKind tokenkind)
        {
            var curtoken = new StringBuilder();
            foreach (var t in tokens)
            {
                if (t == ',')
                {
                    var s = curtoken.ToString().ToLower();
                    if (!this.m_Words.ContainsKey(s)) this.m_Words.Add(s, tokenkind);
                    curtoken = new StringBuilder();
                }
                else
                {
                    curtoken.Append(t);
                }
            }

            if (curtoken.Length != 0)
            {
                this.m_Words.Add(curtoken.ToString(), tokenkind);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sqltokens"/> class.
        /// </summary>
        public Sqltokens()
        {
            this.AddTokens(Keywords, YukonLexer.TokenKind.tkKey);
            this.AddTokens(Functions, YukonLexer.TokenKind.tkFunction);
            this.AddTokens(Types, YukonLexer.TokenKind.tkDatatype);
            this.AddTokens(Greykeywords, YukonLexer.TokenKind.tkGreyKeyword);
            this.AddTokens(Fukeywords, YukonLexer.TokenKind.tkFuKeyword);
        }
    }
}