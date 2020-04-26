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

namespace ExpressProfiler.Constants
{
    /// <summary>
    /// The profiler event columns.
    /// </summary>
    public static class ProfilerEventColumns
    {
        public static readonly string[] ColumnNames =
            {
                "Dumy", "TextData", "BinaryData", "DatabaseID", "TransactionID", "LineNumber", "NTUserName",
                "NTDomainName", "HostName", "ClientProcessID", "ApplicationName", "LoginName", "SPID", "Duration",
                "StartTime", "EndTime", "Reads", "Writes", "CPU", "Permissions", "Severity", "EventSubClass",
                "ObjectID", "Success", "IndexID", "IntegerData", "ServerName", "EventClass", "ObjectType", "NestLevel",
                "State", "Error", "Mode", "Handle", "ObjectName", "DatabaseName", "FileName", "OwnerName", "RoleName",
                "TargetUserName", "DBUserName", "LoginSid", "TargetLoginName", "TargetLoginSid", "ColumnPermissions",
                "LinkedServerName", "ProviderName", "MethodName", "RowCounts", "RequestID", "XactSequence",
                "EventSequence", "BigintData1", "BigintData2", "GUID", "IntegerData2", "ObjectID2", "Type", "OwnerID",
                "ParentName", "IsSystem", "Offset", "SourceDatabaseID", "SqlHandle", "SessionLoginName", "PlanHandle"
            };

        public static readonly ProfilerColumnDataType[] ProfilerColumnDataTypes =
        {
            /*dummy*/ ProfilerColumnDataType.String

            /*TextData*/,
            ProfilerColumnDataType.String

            /*BinaryData*/,
            ProfilerColumnDataType.Byte

            /*DatabaseID*/,
            ProfilerColumnDataType.Int

            /*TransactionID*/,
            ProfilerColumnDataType.Long

            /*LineNumber*/,
            ProfilerColumnDataType.Int

            /*NTUserName*/,
            ProfilerColumnDataType.String

            /*NTDomainName*/,
            ProfilerColumnDataType.String

            /*HostName*/,
            ProfilerColumnDataType.String

            /*ClientProcessID*/,
            ProfilerColumnDataType.Int

            /*ApplicationName*/,
            ProfilerColumnDataType.String

            /*LoginName*/,
            ProfilerColumnDataType.String

            /*SPID*/,
            ProfilerColumnDataType.Int

            /*Duration*/,
            ProfilerColumnDataType.Long

            /*StartTime*/,
            ProfilerColumnDataType.DateTime

            /*EndTime*/,
            ProfilerColumnDataType.DateTime

            /*Reads*/,
            ProfilerColumnDataType.Long

            /*Writes*/,
            ProfilerColumnDataType.Long

            /*CPU*/,
            ProfilerColumnDataType.Int

            /*Permissions*/,
            ProfilerColumnDataType.Long

            /*Severity*/,
            ProfilerColumnDataType.Int

            /*EventSubClass*/,
            ProfilerColumnDataType.Int

            /*ObjectID*/,
            ProfilerColumnDataType.Int

            /*Success*/,
            ProfilerColumnDataType.Int

            /*IndexID*/,
            ProfilerColumnDataType.Int

            /*IntegerData*/,
            ProfilerColumnDataType.Int

            /*ServerName*/,
            ProfilerColumnDataType.String

            /*EventClass*/,
            ProfilerColumnDataType.Int

            /*ObjectType*/,
            ProfilerColumnDataType.Int

            /*NestLevel*/,
            ProfilerColumnDataType.Int

            /*State*/,
            ProfilerColumnDataType.Int

            /*Error*/,
            ProfilerColumnDataType.Int

            /*Mode*/,
            ProfilerColumnDataType.Int

            /*Handle*/,
            ProfilerColumnDataType.Int

            /*ObjectName*/,
            ProfilerColumnDataType.String

            /*DatabaseName*/,
            ProfilerColumnDataType.String

            /*FileName*/,
            ProfilerColumnDataType.String

            /*OwnerName*/,
            ProfilerColumnDataType.String

            /*RoleName*/,
            ProfilerColumnDataType.String

            /*TargetUserName*/,
            ProfilerColumnDataType.String

            /*DBUserName*/,
            ProfilerColumnDataType.String

            /*LoginSid*/,
            ProfilerColumnDataType.Byte

            /*TargetLoginName*/,
            ProfilerColumnDataType.String

            /*TargetLoginSid*/,
            ProfilerColumnDataType.Byte

            /*ColumnPermissions*/,
            ProfilerColumnDataType.Int

            /*LinkedServerName*/,
            ProfilerColumnDataType.String

            /*ProviderName*/,
            ProfilerColumnDataType.String

            /*MethodName*/,
            ProfilerColumnDataType.String

            /*RowCounts*/,
            ProfilerColumnDataType.Long

            /*RequestID*/,
            ProfilerColumnDataType.Int

            /*XactSequence*/,
            ProfilerColumnDataType.Long

            /*EventSequence*/,
            ProfilerColumnDataType.Long

            /*BigintData1*/,
            ProfilerColumnDataType.Long

            /*BigintData2*/,
            ProfilerColumnDataType.Long

            /*GUID*/,
            ProfilerColumnDataType.Guid

            /*IntegerData2*/,
            ProfilerColumnDataType.Int

            /*ObjectID2*/,
            ProfilerColumnDataType.Long

            /*Type*/,
            ProfilerColumnDataType.Int

            /*OwnerID*/,
            ProfilerColumnDataType.Int

            /*ParentName*/,
            ProfilerColumnDataType.String

            /*IsSystem*/,
            ProfilerColumnDataType.Int

            /*Offset*/,
            ProfilerColumnDataType.Int

            /*SourceDatabaseID*/,
            ProfilerColumnDataType.Int

            /*SqlHandle*/,
            ProfilerColumnDataType.Byte

            /*SessionLoginName*/,
            ProfilerColumnDataType.String

            /*PlanHandle*/,
            ProfilerColumnDataType.Byte
        };

        /*
        select 'public const int '+Name + '= '+cast(trace_column_id as varchar)+';'
        from sys.trace_columns
        order by trace_column_id
         */
        public const int TextData = 1;

        public const int BinaryData = 2;

        public const int DatabaseID = 3;

        public const int TransactionID = 4;

        public const int LineNumber = 5;

        public const int NTUserName = 6;

        public const int NTDomainName = 7;

        public const int HostName = 8;

        public const int ClientProcessID = 9;

        public const int ApplicationName = 10;

        public const int LoginName = 11;

        public const int SPID = 12;

        public const int Duration = 13;

        public const int StartTime = 14;

        public const int EndTime = 15;

        public const int Reads = 16;

        public const int Writes = 17;

        public const int CPU = 18;

        public const int Permissions = 19;

        public const int Severity = 20;

        public const int EventSubClass = 21;

        public const int ObjectID = 22;

        public const int Success = 23;

        public const int IndexID = 24;

        public const int IntegerData = 25;

        public const int ServerName = 26;

        public const int EventClass = 27;

        public const int ObjectType = 28;

        public const int NestLevel = 29;

        public const int State = 30;

        public const int Error = 31;

        public const int Mode = 32;

        public const int Handle = 33;

        public const int ObjectName = 34;

        public const int DatabaseName = 35;

        public const int FileName = 36;

        public const int OwnerName = 37;

        public const int RoleName = 38;

        public const int TargetUserName = 39;

        public const int DBUserName = 40;

        public const int LoginSid = 41;

        public const int TargetLoginName = 42;

        public const int TargetLoginSid = 43;

        public const int ColumnPermissions = 44;

        public const int LinkedServerName = 45;

        public const int ProviderName = 46;

        public const int MethodName = 47;

        public const int RowCounts = 48;

        public const int RequestID = 49;

        public const int XactSequence = 50;

        public const int EventSequence = 51;

        public const int BigintData1 = 52;

        public const int BigintData2 = 53;

        public const int GUID = 54;

        public const int IntegerData2 = 55;

        public const int ObjectID2 = 56;

        public const int Type = 57;

        public const int OwnerID = 58;

        public const int ParentName = 59;

        public const int IsSystem = 60;

        public const int Offset = 61;

        public const int SourceDatabaseID = 62;

        public const int SqlHandle = 63;

        public const int SessionLoginName = 64;

        public const int PlanHandle = 65;
    }
}