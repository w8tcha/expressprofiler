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

namespace ExpressProfiler.Constants;

/// <summary>
/// The profiler events.
/// </summary>
public static class ProfilerEvents
{
    /*
    select 'public static class '+replace(name,' ','')+'
    {
    }

    ' 
    from sys.trace_categories
    order by category_id


    select '/ *'+sc.name+'* / '+'public const int '+replace(replace(ev.name,' ',''),':','')+' = '+cast(trace_event_id as varchar)+';'
    from	sys.trace_categories sc inner join sys.trace_events ev on sc.category_id = ev.category_id
    order by sc.category_id,ev.trace_event_id
        */
    // ReSharper disable RedundantExplicitArraySize
    public static readonly string[] Names =
    [
        "", "", "", "", "", "", "", "", "", "", "RPC:Completed",
                                                    "RPC:Starting", "SQL:BatchCompleted", "SQL:BatchStarting",
                                                    "Audit Login", "Audit Logout", "Attention",
                                                    "ExistingConnection", "Audit Server Starts And Stops",
                                                    "DTCTransaction", "Audit Login Failed", "EventLog", "ErrorLog",
                                                    "Lock:Released", "Lock:Acquired", "Lock:Deadlock",
                                                    "Lock:Cancel", "Lock:Timeout",
                                                    "Degree of Parallelism (7.0 Insert)", "", "", "", "",
                                                    "Exception", "SP:CacheMiss", "SP:CacheInsert", "SP:CacheRemove",
                                                    "SP:Recompile", "SP:CacheHit", "Deprecated", "SQL:StmtStarting",
                                                    "SQL:StmtCompleted", "SP:Starting", "SP:Completed",
                                                    "SP:StmtStarting", "SP:StmtCompleted", "Object:Created",
                                                    "Object:Deleted", "", "", "SQLTransaction", "Scan:Started",
                                                    "Scan:Stopped", "CursorOpen", "TransactionLog", "Hash Warning",
                                                    "", "", "Auto Stats", "Lock:Deadlock Chain", "Lock:Escalation",
                                                    "OLEDB Errors", "", "", "", "", "", "Execution Warnings",
                                                    "Showplan Text (Unencoded)", "Sort Warnings", "CursorPrepare",
                                                    "Prepare SQL", "Exec Prepared SQL", "Unprepare SQL",
                                                    "CursorExecute", "CursorRecompile", "CursorImplicitConversion",
                                                    "CursorUnprepare", "CursorClose", "Missing Column Statistics",
                                                    "Missing Join Predicate", "Server Memory Change",
                                                    "UserConfigurable:0", "UserConfigurable:1",
                                                    "UserConfigurable:2", "UserConfigurable:3",
                                                    "UserConfigurable:4", "UserConfigurable:5",
                                                    "UserConfigurable:6", "UserConfigurable:7",
                                                    "UserConfigurable:8", "UserConfigurable:9",
                                                    "Data File Auto Grow", "Log File Auto Grow",
                                                    "Data File Auto Shrink", "Log File Auto Shrink",
                                                    "Showplan Text", "Showplan All", "Showplan Statistics Profile",
                                                    "", "RPC Output Parameter", "",
                                                    "Audit Database Scope GDR Event",
                                                    "Audit Schema Object GDR Event", "Audit Addlogin Event",
                                                    "Audit Login GDR Event", "Audit Login Change Property Event",
                                                    "Audit Login Change Password Event",
                                                    "Audit Add Login to Server Role Event",
                                                    "Audit Add DB User Event", "Audit Add Member to DB Role Event",
                                                    "Audit Add Role Event", "Audit App Role Change Password Event",
                                                    "Audit Statement Permission Event",
                                                    "Audit Schema Object Access Event",
                                                    "Audit Backup/Restore Event", "Audit DBCC Event",
                                                    "Audit Change Audit Event",
                                                    "Audit Object Derived Permission Event", "OLEDB Call Event",
                                                    "OLEDB QueryInterface Event", "OLEDB DataRead Event",
                                                    "Showplan XML", "SQL:FullTextQuery", "Broker:Conversation",
                                                    "Deprecation Announcement", "Deprecation Final Support",
                                                    "Exchange Spill Event", "Audit Database Management Event",
                                                    "Audit Database Object Management Event",
                                                    "Audit Database Principal Management Event",
                                                    "Audit Schema Object Management Event",
                                                    "Audit Server Principal Impersonation Event",
                                                    "Audit Database Principal Impersonation Event",
                                                    "Audit Server Object Take Ownership Event",
                                                    "Audit Database Object Take Ownership Event",
                                                    "Broker:Conversation Group", "Blocked process report",
                                                    "Broker:Connection", "Broker:Forwarded Message Sent",
                                                    "Broker:Forwarded Message Dropped", "Broker:Message Classify",
                                                    "Broker:Transmission", "Broker:Queue Disabled",
                                                    "Broker:Mirrored Route State Changed", "",
                                                    "Showplan XML Statistics Profile", "", "Deadlock graph",
                                                    "Broker:Remote Message Acknowledgement", "Trace File Close", "",
                                                    "Audit Change Database Owner",
                                                    "Audit Schema Object Take Ownership Event", "",
                                                    "FT:Crawl Started", "FT:Crawl Stopped", "FT:Crawl Aborted",
                                                    "Audit Broker Conversation", "Audit Broker Login",
                                                    "Broker:Message Undeliverable", "Broker:Corrupted Message",
                                                    "User Error Message", "Broker:Activation", "Object:Altered",
                                                    "Performance statistics", "SQL:StmtRecompile",
                                                    "Database Mirroring State Change",
                                                    "Showplan XML For Query Compile",
                                                    "Showplan All For Query Compile",
                                                    "Audit Server Scope GDR Event", "Audit Server Object GDR Event",
                                                    "Audit Database Object GDR Event",
                                                    "Audit Server Operation Event", "",
                                                    "Audit Server Alter Trace Event",
                                                    "Audit Server Object Management Event",
                                                    "Audit Server Principal Management Event",
                                                    "Audit Database Operation Event", "",
                                                    "Audit Database Object Access Event", "TM: Begin Tran starting",
                                                    "TM: Begin Tran completed", "TM: Promote Tran starting",
                                                    "TM: Promote Tran completed", "TM: Commit Tran starting",
                                                    "TM: Commit Tran completed", "TM: Rollback Tran starting",
                                                    "TM: Rollback Tran completed", "Lock:Timeout (timeout > 0)",
                                                    "Progress Report: Online Index Operation",
                                                    "TM: Save Tran starting", "TM: Save Tran completed",
                                                    "Background Job Error", "OLEDB Provider Information",
                                                    "Mount Tape", "Assembly Load", "", "XQuery Static Type",
                                                    "QN: Subscription", "QN: Parameter table", "QN: Template"
    ];

    public static class Cursors
    {
        /*Cursors*/
        public const int CursorOpen = 53;

        /*Cursors*/
        public const int CursorPrepare = 70;

        /*Cursors*/
        public const int CursorExecute = 74;

        /*Cursors*/
        public const int CursorRecompile = 75;

        /*Cursors*/
        public const int CursorImplicitConversion = 76;

        /*Cursors*/
        public const int CursorUnprepare = 77;

        /*Cursors*/
        public const int CursorClose = 78;
    }

    public static class Database
    {
        /*Database*/
        public const int DataFileAutoGrow = 92;

        /*Database*/
        public const int LogFileAutoGrow = 93;

        /*Database*/
        public const int DataFileAutoShrink = 94;

        /*Database*/
        public const int LogFileAutoShrink = 95;

        /*Database*/
        public const int DatabaseMirroringStateChange = 167;
    }

    public static class ErrorsAndWarnings
    {
        /*Errors and Warnings*/
        public const int Attention = 16;

        /*Errors and Warnings*/
        public const int EventLog = 21;

        /*Errors and Warnings*/
        public const int ErrorLog = 22;

        /*Errors and Warnings*/
        public const int Exception = 33;

        /*Errors and Warnings*/
        public const int HashWarning = 55;

        /*Errors and Warnings*/
        public const int ExecutionWarnings = 67;

        /*Errors and Warnings*/
        public const int SortWarnings = 69;

        /*Errors and Warnings*/
        public const int MissingColumnStatistics = 79;

        /*Errors and Warnings*/
        public const int MissingJoinPredicate = 80;

        /*Errors and Warnings*/
        public const int ExchangeSpillEvent = 127;

        /*Errors and Warnings*/
        public const int Blockedprocessreport = 137;

        /*Errors and Warnings*/
        public const int UserErrorMessage = 162;

        /*Errors and Warnings*/
        public const int BackgroundJobError = 193;
    }

    public static class Locks
    {
        /*Locks*/
        public const int LockReleased = 23;

        /*Locks*/
        public const int LockAcquired = 24;

        /*Locks*/
        public const int LockDeadlock = 25;

        /*Locks*/
        public const int LockCancel = 26;

        /*Locks*/
        public const int LockTimeout = 27;

        /*Locks*/
        public const int LockDeadlockChain = 59;

        /*Locks*/
        public const int LockEscalation = 60;

        /*Locks*/
        public const int Deadlockgraph = 148;

        /*Locks*/
        public const int LockTimeout100 = 189;
    }

    public static class Objects
    {
        /*Objects*/
        public const int ObjectCreated = 46;

        /*Objects*/
        public const int ObjectDeleted = 47;

        /*Objects*/
        public const int ObjectAltered = 164;
    }

    public static class Performance
    {
        /*Performance*/
        public const int DegreeofParallelism70Insert = 28;

        /*Performance*/
        public const int AutoStats = 58;

        /*Performance*/
        public const int ShowplanTextUnencoded = 68;

        /*Performance*/
        public const int ShowplanText = 96;

        /*Performance*/
        public const int ShowplanAll = 97;

        /*Performance*/
        public const int ShowplanStatisticsProfile = 98;

        /*Performance*/
        public const int ShowplanXML = 122;

        /*Performance*/
        public const int SQLFullTextQuery = 123;

        /*Performance*/
        public const int ShowplanXMLStatisticsProfile = 146;

        /*Performance*/
        public const int Performancestatistics = 165;

        /*Performance*/
        public const int ShowplanXMLForQueryCompile = 168;

        /*Performance*/
        public const int ShowplanAllForQueryCompile = 169;
    }

    public static class Scans
    {
        /*Scans*/
        public const int ScanStarted = 51;

        /*Scans*/
        public const int ScanStopped = 52;
    }

    public static class SecurityAudit
    {
        /*Security Audit*/
        public const int AuditLogin = 14;

        /*Security Audit*/
        public const int AuditLogout = 15;

        /*Security Audit*/
        public const int AuditServerStartsAndStops = 18;

        /*Security Audit*/
        public const int AuditLoginFailed = 20;

        /*Security Audit*/
        public const int AuditDatabaseScopeGDREvent = 102;

        /*Security Audit*/
        public const int AuditSchemaObjectGDREvent = 103;

        /*Security Audit*/
        public const int AuditAddloginEvent = 104;

        /*Security Audit*/
        public const int AuditLoginGDREvent = 105;

        /*Security Audit*/
        public const int AuditLoginChangePropertyEvent = 106;

        /*Security Audit*/
        public const int AuditLoginChangePasswordEvent = 107;

        /*Security Audit*/
        public const int AuditAddLogintoServerRoleEvent = 108;

        /*Security Audit*/
        public const int AuditAddDBUserEvent = 109;

        /*Security Audit*/
        public const int AuditAddMembertoDBRoleEvent = 110;

        /*Security Audit*/
        public const int AuditAddRoleEvent = 111;

        /*Security Audit*/
        public const int AuditAppRoleChangePasswordEvent = 112;

        /*Security Audit*/
        public const int AuditStatementPermissionEvent = 113;

        /*Security Audit*/
        public const int AuditSchemaObjectAccessEvent = 114;

        /*Security Audit*/
        public const int AuditBackupRestoreEvent = 115;

        /*Security Audit*/
        public const int AuditDBCCEvent = 116;

        /*Security Audit*/
        public const int AuditChangeAuditEvent = 117;

        /*Security Audit*/
        public const int AuditObjectDerivedPermissionEvent = 118;

        /*Security Audit*/
        public const int AuditDatabaseManagementEvent = 128;

        /*Security Audit*/
        public const int AuditDatabaseObjectManagementEvent = 129;

        /*Security Audit*/
        public const int AuditDatabasePrincipalManagementEvent = 130;

        /*Security Audit*/
        public const int AuditSchemaObjectManagementEvent = 131;

        /*Security Audit*/
        public const int AuditServerPrincipalImpersonationEvent = 132;

        /*Security Audit*/
        public const int AuditDatabasePrincipalImpersonationEvent = 133;

        /*Security Audit*/
        public const int AuditServerObjectTakeOwnershipEvent = 134;

        /*Security Audit*/
        public const int AuditDatabaseObjectTakeOwnershipEvent = 135;

        /*Security Audit*/
        public const int AuditChangeDatabaseOwner = 152;

        /*Security Audit*/
        public const int AuditSchemaObjectTakeOwnershipEvent = 153;

        /*Security Audit*/
        public const int AuditBrokerConversation = 158;

        /*Security Audit*/
        public const int AuditBrokerLogin = 159;

        /*Security Audit*/
        public const int AuditServerScopeGDREvent = 170;

        /*Security Audit*/
        public const int AuditServerObjectGDREvent = 171;

        /*Security Audit*/
        public const int AuditDatabaseObjectGDREvent = 172;

        /*Security Audit*/
        public const int AuditServerOperationEvent = 173;

        /*Security Audit*/
        public const int AuditServerAlterTraceEvent = 175;

        /*Security Audit*/
        public const int AuditServerObjectManagementEvent = 176;

        /*Security Audit*/
        public const int AuditServerPrincipalManagementEvent = 177;

        /*Security Audit*/
        public const int AuditDatabaseOperationEvent = 178;

        /*Security Audit*/
        public const int AuditDatabaseObjectAccessEvent = 180;
    }

    public static class Server
    {
        /*Server*/
        public const int ServerMemoryChange = 81;

        /*Server*/
        public const int TraceFileClose = 150;

        /*Server*/
        public const int MountTape = 195;
    }

    public static class Sessions
    {
        /*Sessions*/
        public const int ExistingConnection = 17;
    }

    public static class StoredProcedures
    {
        /*Stored Procedures*/
        public const int RPCCompleted = 10;

        /*Stored Procedures*/
        public const int RPCStarting = 11;

        /*Stored Procedures*/
        public const int SPCacheMiss = 34;

        /*Stored Procedures*/
        public const int SPCacheInsert = 35;

        /*Stored Procedures*/
        public const int SPCacheRemove = 36;

        /*Stored Procedures*/
        public const int SPRecompile = 37;

        /*Stored Procedures*/
        public const int SPCacheHit = 38;

        /*Stored Procedures*/
        public const int Deprecated = 39;

        /*Stored Procedures*/
        public const int SPStarting = 42;

        /*Stored Procedures*/
        public const int SPCompleted = 43;

        /*Stored Procedures*/
        public const int SPStmtStarting = 44;

        /*Stored Procedures*/
        public const int SPStmtCompleted = 45;

        /*Stored Procedures*/
        public const int RPCOutputParameter = 100;
    }

    public static class Transactions
    {
        /*Transactions*/
        public const int DTCTransaction = 19;

        /*Transactions*/
        public const int SQLTransaction = 50;

        /*Transactions*/
        public const int TransactionLog = 54;

        /*Transactions*/
        public const int TMBeginTranstarting = 181;

        /*Transactions*/
        public const int TMBeginTrancompleted = 182;

        /*Transactions*/
        public const int TMPromoteTranstarting = 183;

        /*Transactions*/
        public const int TMPromoteTrancompleted = 184;

        /*Transactions*/
        public const int TMCommitTranstarting = 185;

        /*Transactions*/
        public const int TMCommitTrancompleted = 186;

        /*Transactions*/
        public const int TMRollbackTranstarting = 187;

        /*Transactions*/
        public const int TMRollbackTrancompleted = 188;

        /*Transactions*/
        public const int TMSaveTranstarting = 191;

        /*Transactions*/
        public const int TMSaveTrancompleted = 192;
    }

    public static class TSQL
    {
        /*TSQL*/
        public const int SQLBatchCompleted = 12;

        /*TSQL*/
        public const int SQLBatchStarting = 13;

        /*TSQL*/
        public const int SQLStmtStarting = 40;

        /*TSQL*/
        public const int SQLStmtCompleted = 41;

        /*TSQL*/
        public const int PrepareSQL = 71;

        /*TSQL*/
        public const int ExecPreparedSQL = 72;

        /*TSQL*/
        public const int UnprepareSQL = 73;

        /*TSQL*/
        public const int SQLStmtRecompile = 166;

        /*TSQL*/
        public const int XQueryStaticType = 198;
    }

    public static class Userconfigurable
    {
        /*User configurable*/
        public const int UserConfigurable0 = 82;

        /*User configurable*/
        public const int UserConfigurable1 = 83;

        /*User configurable*/
        public const int UserConfigurable2 = 84;

        /*User configurable*/
        public const int UserConfigurable3 = 85;

        /*User configurable*/
        public const int UserConfigurable4 = 86;

        /*User configurable*/
        public const int UserConfigurable5 = 87;

        /*User configurable*/
        public const int UserConfigurable6 = 88;

        /*User configurable*/
        public const int UserConfigurable7 = 89;

        /*User configurable*/
        public const int UserConfigurable8 = 90;

        /*User configurable*/
        public const int UserConfigurable9 = 91;
    }

    public static class OLEDB
    {
        /*OLEDB*/
        public const int OLEDBErrors = 61;

        /*OLEDB*/
        public const int OLEDBCallEvent = 119;

        /*OLEDB*/
        public const int OLEDBQueryInterfaceEvent = 120;

        /*OLEDB*/
        public const int OLEDBDataReadEvent = 121;

        /*OLEDB*/
        public const int OLEDBProviderInformation = 194;
    }

    public static class Broker
    {
        /*Broker*/
        public const int BrokerConversation = 124;

        /*Broker*/
        public const int BrokerConversationGroup = 136;

        /*Broker*/
        public const int BrokerConnection = 138;

        /*Broker*/
        public const int BrokerForwardedMessageSent = 139;

        /*Broker*/
        public const int BrokerForwardedMessageDropped = 140;

        /*Broker*/
        public const int BrokerMessageClassify = 141;

        /*Broker*/
        public const int BrokerTransmission = 142;

        /*Broker*/
        public const int BrokerQueueDisabled = 143;

        /*Broker*/
        public const int BrokerMirroredRouteStateChanged = 144;

        /*Broker*/
        public const int BrokerRemoteMessageAcknowledgement = 149;

        /*Broker*/
        public const int BrokerMessageUndeliverable = 160;

        /*Broker*/
        public const int BrokerCorruptedMessage = 161;

        /*Broker*/
        public const int BrokerActivation = 163;
    }

    public static class Fulltext
    {
        /*Full text*/
        public const int FTCrawlStarted = 155;

        /*Full text*/
        public const int FTCrawlStopped = 156;

        /*Full text*/
        public const int FTCrawlAborted = 157;
    }

    public static class Deprecation
    {
        /*Deprecation*/
        public const int DeprecationAnnouncement = 125;

        /*Deprecation*/
        public const int DeprecationFinalSupport = 126;
    }

    public static class ProgressReport
    {
        /*Progress Report*/
        public const int ProgressReportOnlineIndexOperation = 190;
    }

    public static class CLR
    {
        /*CLR*/
        public const int AssemblyLoad = 196;
    }

    public static class QueryNotifications
    {
        /*Query Notifications*/
        public const int QNSubscription = 199;

        /*Query Notifications*/
        public const int QNParametertable = 200;

        /*Query Notifications*/
        public const int QNTemplate = 201;

        /*Query Notifications*/
        public const int QNDynamics = 202;
    }
}