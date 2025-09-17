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
using System.Data.SqlClient;

// ReSharper restore UnusedMember.Global
// ReSharper restore InconsistentNaming
public class ProfilerEvent
{
    internal readonly object[] m_Events = new object[65];

    internal ulong m_ColumnMask;

    // ReSharper disable UnusedMember.Global
    // ReSharper disable InconsistentNaming

    /*
select 'case ProfilerEventColumns.'+Name + ':
'
+
case when row_number() over(partition by Type_Name order by trace_column_id desc) = 1 then 

'		return Get'+
case Type_Name
                    when 'text' then 'String'
                    when 'int' then 'Int'
                    when 'bigint' then 'Long'
                    when 'nvarchar' then 'String'
                    when 'datetime' then 'DateTime'
                    when 'image' then 'Byte'
                    when 'uniqueidentifier' then 'Guid'
            end
+'(idx);
'
else '' end
from sys.trace_columns
order by Type_Name,trace_column_id         
     */
    public string GetFormattedData(int idx, string format)
    {
        switch (ProfilerEventColumns.ProfilerColumnDataTypes[idx])
        {
            case ProfilerColumnDataType.Long:
                return this.GetLong(idx).ToString(format);
            case ProfilerColumnDataType.DateTime:
                var d = this.GetDateTime(idx);
                return 1 == d.Year ? string.Empty : d.ToString(format);
            case ProfilerColumnDataType.Byte:
                return this.GetByte(idx).ToString();
            case ProfilerColumnDataType.Int:
                return this.GetInt(idx).ToString(format);
            case ProfilerColumnDataType.String:
                return this.GetString(idx);
            case ProfilerColumnDataType.Guid:
                return this.GetGuid(idx).ToString();
        }

        return null;
    }

    private int GetInt(int idx)
    {
        if (!this.ColumnIsSet(idx))
        {
            return 0;
        }

        return this.m_Events[idx] == null ? 0 : (int)this.m_Events[idx];
    }

    private long GetLong(int idx)
    {
        if (!this.ColumnIsSet(idx))
        {
            return 0;
        }

        return this.m_Events[idx] == null ? 0 : (long)this.m_Events[idx];
    }

    private string GetString(int idx)
    {
        if (!this.ColumnIsSet(idx))
        {
            return string.Empty;
        }

        return this.m_Events[idx] == null ? string.Empty : (string)this.m_Events[idx];
    }

    private byte[] GetByte(int idx)
    {
        return this.ColumnIsSet(idx) ? (byte[])this.m_Events[idx] : new byte[1];
    }

    private DateTime GetDateTime(int idx)
    {
        return this.ColumnIsSet(idx) ? (DateTime)this.m_Events[idx] : new DateTime(0);
    }

    private Guid GetGuid(int idx)
    {
        return this.ColumnIsSet(idx) ? (Guid)this.m_Events[idx] : Guid.Empty;
    }

    // ReSharper disable MemberCanBePrivate.Global
    public bool ColumnIsSet(int columnId)
    {
        // ReSharper restore MemberCanBePrivate.Global
        return (this.m_ColumnMask & (1UL << columnId)) != 0;
    }

    /*        
select 'public '+case Type_Name
                    when 'text' then 'string'
                    when 'int' then 'int'
                    when 'bigint' then 'long'
                    when 'nvarchar' then 'string'
                    when 'datetime' then 'DateTime'
                    when 'image' then 'byte[]'
                    when 'uniqueidentifier' then 'GUID'
            end
+' '+Name + '{get{ return Get'+
case Type_Name
                    when 'text' then 'String'
                    when 'int' then 'Int'
                    when 'bigint' then 'Long'
                    when 'nvarchar' then 'String'
                    when 'datetime' then 'DateTime'
                    when 'image' then 'Byte'
                    when 'uniqueidentifier' then 'Guid'
            end
+'(ProfilerEventColumns.'+Name+');}}'
from sys.trace_columns
order by trace_column_id

     * 
     */
    public string TextData => this.GetString(ProfilerEventColumns.TextData);

    public byte[] BinaryData => this.GetByte(ProfilerEventColumns.BinaryData);

    public int DatabaseID => this.GetInt(ProfilerEventColumns.DatabaseID);

    public long TransactionID => this.GetLong(ProfilerEventColumns.TransactionID);

    public int LineNumber => this.GetInt(ProfilerEventColumns.LineNumber);

    public string NTUserName => this.GetString(ProfilerEventColumns.NTUserName);

    public string NTDomainName => this.GetString(ProfilerEventColumns.NTDomainName);

    public string HostName => this.GetString(ProfilerEventColumns.HostName);

    public int ClientProcessID => this.GetInt(ProfilerEventColumns.ClientProcessID);

    public string ApplicationName => this.GetString(ProfilerEventColumns.ApplicationName);

    public string LoginName => this.GetString(ProfilerEventColumns.LoginName);

    public int SPID => this.GetInt(ProfilerEventColumns.SPID);

    public long Duration => this.GetLong(ProfilerEventColumns.Duration);

    public DateTime StartTime => this.GetDateTime(ProfilerEventColumns.StartTime);

    public DateTime EndTime => this.GetDateTime(ProfilerEventColumns.EndTime);

    public long Reads => this.GetLong(ProfilerEventColumns.Reads);

    public long Writes => this.GetLong(ProfilerEventColumns.Writes);

    public int CPU => this.GetInt(ProfilerEventColumns.CPU);

    public long Permissions => this.GetLong(ProfilerEventColumns.Permissions);

    public int Severity => this.GetInt(ProfilerEventColumns.Severity);

    public int EventSubClass => this.GetInt(ProfilerEventColumns.EventSubClass);

    public int ObjectID => this.GetInt(ProfilerEventColumns.ObjectID);

    public int Success => this.GetInt(ProfilerEventColumns.Success);

    public int IndexID => this.GetInt(ProfilerEventColumns.IndexID);

    public int IntegerData => this.GetInt(ProfilerEventColumns.IntegerData);

    public string ServerName => this.GetString(ProfilerEventColumns.ServerName);

    public int EventClass => this.GetInt(ProfilerEventColumns.EventClass);

    public int ObjectType => this.GetInt(ProfilerEventColumns.ObjectType);

    public int NestLevel => this.GetInt(ProfilerEventColumns.NestLevel);

    public int State => this.GetInt(ProfilerEventColumns.State);

    public int Error => this.GetInt(ProfilerEventColumns.Error);

    public int Mode => this.GetInt(ProfilerEventColumns.Mode);

    public int Handle => this.GetInt(ProfilerEventColumns.Handle);

    public string ObjectName => this.GetString(ProfilerEventColumns.ObjectName);

    public string DatabaseName => this.GetString(ProfilerEventColumns.DatabaseName);

    public string FileName => this.GetString(ProfilerEventColumns.FileName);

    public string OwnerName => this.GetString(ProfilerEventColumns.OwnerName);

    public string RoleName => this.GetString(ProfilerEventColumns.RoleName);

    public string TargetUserName => this.GetString(ProfilerEventColumns.TargetUserName);

    public string DBUserName => this.GetString(ProfilerEventColumns.DBUserName);

    public byte[] LoginSid => this.GetByte(ProfilerEventColumns.LoginSid);

    public string TargetLoginName => this.GetString(ProfilerEventColumns.TargetLoginName);

    public byte[] TargetLoginSid => this.GetByte(ProfilerEventColumns.TargetLoginSid);

    public int ColumnPermissions => this.GetInt(ProfilerEventColumns.ColumnPermissions);

    public string LinkedServerName => this.GetString(ProfilerEventColumns.LinkedServerName);

    public string ProviderName => this.GetString(ProfilerEventColumns.ProviderName);

    public string MethodName => this.GetString(ProfilerEventColumns.MethodName);

    public long RowCounts => this.GetLong(ProfilerEventColumns.RowCounts);

    public int RequestID => this.GetInt(ProfilerEventColumns.RequestID);

    public long XactSequence => this.GetLong(ProfilerEventColumns.XactSequence);

    public long EventSequence => this.GetLong(ProfilerEventColumns.EventSequence);

    public long BigintData1 => this.GetLong(ProfilerEventColumns.BigintData1);

    public long BigintData2 => this.GetLong(ProfilerEventColumns.BigintData2);

    public Guid GUID => this.GetGuid(ProfilerEventColumns.GUID);

    public int IntegerData2 => this.GetInt(ProfilerEventColumns.IntegerData2);

    public long ObjectID2 => this.GetLong(ProfilerEventColumns.ObjectID2);

    public int Type => this.GetInt(ProfilerEventColumns.Type);

    public int OwnerID => this.GetInt(ProfilerEventColumns.OwnerID);

    public string ParentName => this.GetString(ProfilerEventColumns.ParentName);

    public int IsSystem => this.GetInt(ProfilerEventColumns.IsSystem);

    public int Offset => this.GetInt(ProfilerEventColumns.Offset);

    public int SourceDatabaseID => this.GetInt(ProfilerEventColumns.SourceDatabaseID);

    public byte[] SqlHandle => this.GetByte(ProfilerEventColumns.SqlHandle);

    public string SessionLoginName => this.GetString(ProfilerEventColumns.SessionLoginName);

    public byte[] PlanHandle => this.GetByte(ProfilerEventColumns.PlanHandle);
}

// ReSharper restore InconsistentNaming
// ReSharper restore UnusedMember.Global
public class RawTraceReader
{
    private delegate void SetEventDelegate(ProfilerEvent evt, int columnid);

    private SqlDataReader m_Reader;

    private readonly byte[] m_B16 = new byte[16];

    private readonly byte[] m_B8 = new byte[8];

    private readonly byte[] m_B2 = new byte[2];

    private readonly byte[] m_B4 = new byte[4];

    private readonly SqlConnection m_Conn;

    /// <summary>
    /// Gets the trace id.
    /// </summary>
    private int TraceId { get; set; }

    private readonly SetEventDelegate[] m_Delegates = new SetEventDelegate[66];

    private bool Read()
    {
        this.TraceIsActive = this.m_Reader.Read();
        return this.TraceIsActive;
    }

    public bool TraceIsActive { get; private set; }

    public void Close()
    {
        this.m_Reader?.Close();

        this.TraceIsActive = false;
    }

    public RawTraceReader(SqlConnection con)
    {
        this.m_Conn = con;
        SetEventDelegate evtInt = this.SetIntColumn;
        SetEventDelegate evtLong = this.SetLongColumn;
        SetEventDelegate evtString = this.SetStringColumn;
        SetEventDelegate evtByte = this.SetByteColumn;
        SetEventDelegate evtDateTime = this.SetDateTimeColumn;
        SetEventDelegate evtGuid = SetGuidColumn;

        /*
                    select 'm_Delegates[ProfilerEventColumns.'+Name+'] = evt'+
                    case Type_Name
                                            when 'text' then 'String'
                                            when 'int' then 'Int'
                                            when 'bigint' then 'Long'
                                            when 'nvarchar' then 'String'
                                            when 'datetime' then 'DateTime'
                                            when 'image' then 'Byte'
                                            when 'uniqueidentifier' then 'Guid'
                                    end+';'
        
                    from sys.trace_columns
                    order by trace_column_id
         
                     */
        this.m_Delegates[ProfilerEventColumns.TextData] = evtString;
        this.m_Delegates[ProfilerEventColumns.BinaryData] = evtByte;
        this.m_Delegates[ProfilerEventColumns.DatabaseID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.TransactionID] = evtLong;
        this.m_Delegates[ProfilerEventColumns.LineNumber] = evtInt;
        this.m_Delegates[ProfilerEventColumns.NTUserName] = evtString;
        this.m_Delegates[ProfilerEventColumns.NTDomainName] = evtString;
        this.m_Delegates[ProfilerEventColumns.HostName] = evtString;
        this.m_Delegates[ProfilerEventColumns.ClientProcessID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ApplicationName] = evtString;
        this.m_Delegates[ProfilerEventColumns.LoginName] = evtString;
        this.m_Delegates[ProfilerEventColumns.SPID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Duration] = evtLong;
        this.m_Delegates[ProfilerEventColumns.StartTime] = evtDateTime;
        this.m_Delegates[ProfilerEventColumns.EndTime] = evtDateTime;
        this.m_Delegates[ProfilerEventColumns.Reads] = evtLong;
        this.m_Delegates[ProfilerEventColumns.Writes] = evtLong;
        this.m_Delegates[ProfilerEventColumns.CPU] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Permissions] = evtLong;
        this.m_Delegates[ProfilerEventColumns.Severity] = evtInt;
        this.m_Delegates[ProfilerEventColumns.EventSubClass] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ObjectID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Success] = evtInt;
        this.m_Delegates[ProfilerEventColumns.IndexID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.IntegerData] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ServerName] = evtString;
        this.m_Delegates[ProfilerEventColumns.EventClass] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ObjectType] = evtInt;
        this.m_Delegates[ProfilerEventColumns.NestLevel] = evtInt;
        this.m_Delegates[ProfilerEventColumns.State] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Error] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Mode] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Handle] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ObjectName] = evtString;
        this.m_Delegates[ProfilerEventColumns.DatabaseName] = evtString;
        this.m_Delegates[ProfilerEventColumns.FileName] = evtString;
        this.m_Delegates[ProfilerEventColumns.OwnerName] = evtString;
        this.m_Delegates[ProfilerEventColumns.RoleName] = evtString;
        this.m_Delegates[ProfilerEventColumns.TargetUserName] = evtString;
        this.m_Delegates[ProfilerEventColumns.DBUserName] = evtString;
        this.m_Delegates[ProfilerEventColumns.LoginSid] = evtByte;
        this.m_Delegates[ProfilerEventColumns.TargetLoginName] = evtString;
        this.m_Delegates[ProfilerEventColumns.TargetLoginSid] = evtByte;
        this.m_Delegates[ProfilerEventColumns.ColumnPermissions] = evtInt;
        this.m_Delegates[ProfilerEventColumns.LinkedServerName] = evtString;
        this.m_Delegates[ProfilerEventColumns.ProviderName] = evtString;
        this.m_Delegates[ProfilerEventColumns.MethodName] = evtString;
        this.m_Delegates[ProfilerEventColumns.RowCounts] = evtLong;
        this.m_Delegates[ProfilerEventColumns.RequestID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.XactSequence] = evtLong;
        this.m_Delegates[ProfilerEventColumns.EventSequence] = evtLong;
        this.m_Delegates[ProfilerEventColumns.BigintData1] = evtLong;
        this.m_Delegates[ProfilerEventColumns.BigintData2] = evtLong;
        this.m_Delegates[ProfilerEventColumns.GUID] = evtGuid;
        this.m_Delegates[ProfilerEventColumns.IntegerData2] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ObjectID2] = evtLong;
        this.m_Delegates[ProfilerEventColumns.Type] = evtInt;
        this.m_Delegates[ProfilerEventColumns.OwnerID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.ParentName] = evtString;
        this.m_Delegates[ProfilerEventColumns.IsSystem] = evtInt;
        this.m_Delegates[ProfilerEventColumns.Offset] = evtInt;
        this.m_Delegates[ProfilerEventColumns.SourceDatabaseID] = evtInt;
        this.m_Delegates[ProfilerEventColumns.SqlHandle] = evtByte;
        this.m_Delegates[ProfilerEventColumns.SessionLoginName] = evtString;
        this.m_Delegates[ProfilerEventColumns.PlanHandle] = evtByte;
    }

    private static void SetGuidColumn(ProfilerEvent evt, int columnid)
    {
        throw new NotImplementedException();
    }

    private void SetDateTimeColumn(ProfilerEvent evt, int columnid)
    {
        // 2 byte - year
        // 2 byte - month
        // 2 byte - ???
        // 2 byte - day
        // 2 byte - hour
        // 2 byte - min
        // 2 byte - sec
        // 2 byte - msec
        this.m_Reader.GetBytes(2, 0, this.m_B16, 0, 16);
        var year = this.m_B16[0] | this.m_B16[1] << 8;
        var month = this.m_B16[2] | this.m_B16[3] << 8;
        var day = this.m_B16[6] | this.m_B16[7] << 8;
        var hour = this.m_B16[8] | this.m_B16[9] << 8;
        var min = this.m_B16[10] | this.m_B16[11] << 8;
        var sec = this.m_B16[12] | this.m_B16[13] << 8;
        var msec = this.m_B16[14] | this.m_B16[15] << 8;
        evt.m_Events[columnid] = new DateTime(year, month, day, hour, min, sec, msec);
        evt.m_ColumnMask |= (ulong)1 << columnid;
    }

    private void SetByteColumn(ProfilerEvent evt, int columnid)
    {
        var b = new byte[(int)this.m_Reader[1]];
        evt.m_Events[columnid] = b;
        evt.m_ColumnMask |= 1UL << columnid;
    }

    private void SetStringColumn(ProfilerEvent evt, int columnid)
    {
        evt.m_Events[columnid] = Encoding.Unicode.GetString((byte[])this.m_Reader[2]);
        evt.m_ColumnMask |= 1UL << columnid;
    }

    private void SetIntColumn(ProfilerEvent evt, int columnid)
    {
        this.m_Reader.GetBytes(2, 0, this.m_B4, 0, 4);
        evt.m_Events[columnid] = ToInt32(this.m_B4);
        evt.m_ColumnMask |= 1UL << columnid;
    }

    private void SetLongColumn(ProfilerEvent evt, int columnid)
    {
        this.m_Reader.GetBytes(2, 0, this.m_B8, 0, 8);
        evt.m_Events[columnid] = ToInt64(this.m_B8);
        evt.m_ColumnMask |= 1UL << columnid;
    }

    private static long ToInt64(IReadOnlyList<byte> value)
    {
        var i1 = value[0] | (value[1] << 8) | (value[2] << 16) | (value[3] << 24);
        var i2 = value[4] | (value[5] << 8) | (value[6] << 16) | (value[7] << 24);
        return (uint)i1 | ((long)i2 << 32);
    }

    private static int ToInt32(IReadOnlyList<byte> value)
    {
        return value[0] | (value[1] << 8) | (value[2] << 16) | (value[3] << 24);
    }

    private static int ToInt16(IReadOnlyList<byte> value)
    {
        return value[0] | (value[1] << 8);
    }

    // ReSharper disable UnusedMember.Global
    public static string BuildEventSql(int traceid, int eventId, params int[] columns)
    {
        // ReSharper restore UnusedMember.Global
        var sb = new StringBuilder();
        foreach (var i in columns)
        {
            sb.Append($"\r\n exec sp_trace_setevent {traceid}, {eventId}, {i}, @on");
        }

        return sb.ToString();
    }

    public void SetEvent(int eventId, params int[] columns)
    {
        var cmd = new SqlCommand
                      {
                          Connection = this.m_Conn,
                          CommandText = "sp_trace_setevent",
                          CommandType = CommandType.StoredProcedure
                      };
        cmd.Parameters.Add("@traceid", SqlDbType.Int).Value = this.TraceId;
        cmd.Parameters.Add("@eventid", SqlDbType.Int).Value = eventId;
        var p = cmd.Parameters.Add("@columnid", SqlDbType.Int);
        cmd.Parameters.Add("@on", SqlDbType.Bit).Value = 1;
        foreach (var i in columns)
        {
            p.Value = i;
            cmd.ExecuteNonQuery();
        }
    }

    // ReSharper disable UnusedMember.Global
    public void SetFilter(int columnId, int logicalOperator, int comparisonOperator, long? value)
    {
        // ReSharper restore UnusedMember.Global
        var cmd = new SqlCommand
                      {
                          Connection = this.m_Conn,
                          CommandText = "sp_trace_setfilter",
                          CommandType = CommandType.StoredProcedure
                      };
        cmd.Parameters.Add("@traceid", SqlDbType.Int).Value = this.TraceId;
        cmd.Parameters.Add("@columnid", SqlDbType.Int).Value = columnId;
        cmd.Parameters.Add("@logical_operator", SqlDbType.Int).Value = logicalOperator;
        cmd.Parameters.Add("@comparison_operator", SqlDbType.Int).Value = comparisonOperator;
        if (value == null)
        {
            cmd.Parameters.Add("@value", SqlDbType.Int).Value = DBNull.Value;
        }
        else
        {
            switch (columnId)
            {
                case ProfilerEventColumns.BigintData1:
                case ProfilerEventColumns.BigintData2:
                case ProfilerEventColumns.Duration:
                case ProfilerEventColumns.EventSequence:
                case ProfilerEventColumns.ObjectID2:
                case ProfilerEventColumns.Permissions:
                case ProfilerEventColumns.Reads:
                case ProfilerEventColumns.RowCounts:
                case ProfilerEventColumns.TransactionID:
                case ProfilerEventColumns.Writes:
                case ProfilerEventColumns.XactSequence:
                    cmd.Parameters.Add("@value", SqlDbType.BigInt).Value = value;
                    break;
                case ProfilerEventColumns.ClientProcessID:
                case ProfilerEventColumns.ColumnPermissions:
                case ProfilerEventColumns.CPU:
                case ProfilerEventColumns.DatabaseID:
                case ProfilerEventColumns.Error:
                case ProfilerEventColumns.EventClass:
                case ProfilerEventColumns.EventSubClass:
                case ProfilerEventColumns.Handle:
                case ProfilerEventColumns.IndexID:
                case ProfilerEventColumns.IntegerData:
                case ProfilerEventColumns.IntegerData2:
                case ProfilerEventColumns.IsSystem:
                case ProfilerEventColumns.LineNumber:
                case ProfilerEventColumns.Mode:
                case ProfilerEventColumns.NestLevel:
                case ProfilerEventColumns.ObjectID:
                case ProfilerEventColumns.ObjectType:
                case ProfilerEventColumns.Offset:
                case ProfilerEventColumns.OwnerID:
                case ProfilerEventColumns.RequestID:
                case ProfilerEventColumns.Severity:
                case ProfilerEventColumns.SourceDatabaseID:
                case ProfilerEventColumns.SPID:
                case ProfilerEventColumns.State:
                case ProfilerEventColumns.Success:
                case ProfilerEventColumns.Type:
                    cmd.Parameters.Add("@value", SqlDbType.Int).Value = value;
                    break;
                default:
                    throw new Exception($"Unsupported column_id: {columnId}");
            }
        }

        cmd.ExecuteNonQuery();
    }

    public void SetFilter(int columnId, int logicalOperator, int comparisonOperator, string value)
    {
        var cmd = new SqlCommand
                      {
                          Connection = this.m_Conn,
                          CommandText = "sp_trace_setfilter",
                          CommandType = CommandType.StoredProcedure
                      };
        cmd.Parameters.Add("@traceid", SqlDbType.Int).Value = this.TraceId;
        cmd.Parameters.Add("@columnid", SqlDbType.Int).Value = columnId;
        cmd.Parameters.Add("@logical_operator", SqlDbType.Int).Value = logicalOperator;
        cmd.Parameters.Add("@comparison_operator", SqlDbType.Int).Value = comparisonOperator;
        if (value == null)
        {
            cmd.Parameters.Add("@value", SqlDbType.NVarChar).Value = DBNull.Value;
        }
        else
        {
            cmd.Parameters.Add("@value", SqlDbType.NVarChar, value.Length).Value = value;
        }

        cmd.ExecuteNonQuery();
    }

    public void CreateTrace()
    {
        var cmd = new SqlCommand
                      {
                          Connection = this.m_Conn, CommandText = "sp_trace_create", CommandType = CommandType.StoredProcedure
                      };
        cmd.Parameters.Add("@traceid", SqlDbType.Int).Direction = ParameterDirection.Output;
        cmd.Parameters.Add("@options", SqlDbType.Int).Value = 1;
        cmd.Parameters.Add("@trace_file", SqlDbType.NVarChar, 245).Value = DBNull.Value;
        cmd.Parameters.Add("@maxfilesize", SqlDbType.BigInt).Value = DBNull.Value;
        cmd.Parameters.Add("@stoptime", SqlDbType.DateTime).Value = DBNull.Value;
        cmd.Parameters.Add("@filecount", SqlDbType.Int).Value = DBNull.Value;
        cmd.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        cmd.ExecuteNonQuery();
        var result = (int)cmd.Parameters["@result"].Value;
        this.TraceId = result != 0 ? -result : (int)cmd.Parameters["@traceid"].Value;
    }

    private void ControlTrace(SqlConnection con, int status)
    {
        var cmd = new SqlCommand
                      {
                          Connection = con,
                          CommandText = "sp_trace_setstatus",
                          CommandType = CommandType.StoredProcedure,
                          CommandTimeout = 0
                      };
        cmd.Parameters.Add("@traceid", SqlDbType.Int).Value = this.TraceId;
        cmd.Parameters.Add("@status", SqlDbType.Int).Value = status;
        cmd.ExecuteNonQuery();
    }

    public void CloseTrace(SqlConnection con)
    {
        this.ControlTrace(con, 2);
    }

    public void StopTrace(SqlConnection con)
    {
        this.ControlTrace(con, 0);
    }

    public void StartTrace()
    {
        this.ControlTrace(this.m_Conn, 1);
        this.GetReader();
        this.Read();
    }

    private void GetReader()
    {
        var cmd = new SqlCommand
                      {
                          Connection = this.m_Conn,
                          CommandText = "sp_trace_getdata",
                          CommandType = CommandType.StoredProcedure,
                          CommandTimeout = 0
                      };
        cmd.Parameters.Add("@traceid", SqlDbType.Int).Value = this.TraceId;
        cmd.Parameters.Add("@records", SqlDbType.Int).Value = 0;
        this.m_Reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
    }

    public ProfilerEvent Next()
    {
        if (!this.TraceIsActive)
        {
            return null;
        }

        var columnid = (int)this.m_Reader[0];

        // skip to begin of new event
        while (columnid != 65526 && this.Read() && this.TraceIsActive)
        {
            columnid = (int)this.m_Reader[0];
        }

        // start of new event
        if (columnid != 65526)
        {
            return null;
        }

        if (!this.TraceIsActive)
        {
            return null;
        }

        // get potential event class
        this.m_Reader.GetBytes(2, 0, this.m_B2, 0, 2);
        var eventClass = ToInt16(this.m_B2);

        // we got new event
        if (eventClass is >= 0 and < 255)
        {
            var evt = new ProfilerEvent { m_Events = { [27] = eventClass } };
            evt.m_ColumnMask |= 1 << 27;
            while (this.Read())
            {
                columnid = (int)this.m_Reader[0];
                if (columnid > 64)
                {
                    return evt;
                }

                this.m_Delegates[columnid](evt, columnid);
            }
        }

        this.Read();
        return null;
    }
}