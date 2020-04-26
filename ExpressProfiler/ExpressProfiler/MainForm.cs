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

//sample application for demonstrating Sql Server Profiling
//writen by Locky, 2009.

namespace ExpressProfiler
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    using ExpressProfiler.Comparer;
    using ExpressProfiler.Constants;
    using ExpressProfiler.Extensions;
    using ExpressProfiler.Helpers;
    using ExpressProfiler.Objects;
    using ExpressProfiler.Properties;
    using ExpressProfiler.Utils;

    public partial class MainForm : Form
    {
        private const string versionString = "Express Profiler v2.3";

        private readonly string recentConnectionFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Express Profiler");

        private readonly string recentConnectionFileName = "RecentConnections.xml";

        private class PerfInfo
        {
            internal int m_count;

            internal readonly DateTime m_date = DateTime.Now;
        }

        private class PerfColumn
        {
            public string Caption;

            public int Column;

            public int Width;

            public string Format;

            public HorizontalAlignment Alignment = HorizontalAlignment.Left;
        }

        private enum ProfilingStateEnum
        {
            psStopped,

            psProfiling,

            psPaused
        }

        private RawTraceReader m_Rdr;

        private readonly YukonLexer m_Lex = new YukonLexer();

        private SqlConnection m_Conn;

        private readonly SqlCommand m_Cmd = new SqlCommand();

        private Thread m_Thr;

        private bool m_NeedStop = true;

        private ProfilingStateEnum m_ProfilingState;

        private int m_EventCount;

        private readonly ProfilerEvent m_EventStarted = new ProfilerEvent();

        private readonly ProfilerEvent m_EventStopped = new ProfilerEvent();

        private readonly ProfilerEvent m_EventPaused = new ProfilerEvent();

        private readonly List<ListViewItem> m_Cached = new List<ListViewItem>(1024);

        private readonly List<ListViewItem> m_CachedUnFiltered = new List<ListViewItem>(1024);

        private readonly Dictionary<string, ListViewItem> m_itembysql = new Dictionary<string, ListViewItem>();

        private string m_servername;

        private string m_username;

        private string m_userpassword = string.Empty;

        internal string lastpattern = string.Empty;

        private ListViewNF lvEvents;

        Queue<ProfilerEvent> m_events = new Queue<ProfilerEvent>(10);

        private bool m_autostart;

        private bool dontUpdateSource;

        private Exception m_profilerexception;

        private readonly Queue<PerfInfo> m_perf = new Queue<PerfInfo>();

        private PerfInfo m_first;

        private PerfInfo m_prev;

        private TraceProperties.TraceSettings m_currentsettings;

        private readonly List<PerfColumn> m_columns = new List<PerfColumn>();

        internal bool matchCase = false;

        internal bool wholeWord = false;

        internal string recent_servername = string.Empty;

        internal string recent__username = string.Empty;

        internal string recent_userpassword = string.Empty;

        internal int recent_auth = 0;

        public MainForm()
        {
            this.InitializeComponent();
            this.tbStart.DefaultItem = this.tbRun;
            this.Text = versionString;
            this.edPassword.TextBox.PasswordChar = '*';
            this.m_servername = Settings.Default.ServerName;
            this.m_username = Settings.Default.UserName;
            this.m_currentsettings = GetDefaultSettings();
            this.ParseCommandLine();
            this.InitLV();
            this.edServer.Text = this.m_servername;
            this.edUser.Text = this.m_username;
            this.edPassword.Text = this.m_userpassword;
            this.tbAuth.SelectedIndex = string.IsNullOrEmpty(this.m_username) ? 0 : 1;
            
            if (this.m_autostart)
            {
                this.RunProfiling(false);
            }

            this.UpdateButtons();
        }

        private static TraceProperties.TraceSettings GetDefaultSettings()
        {
            try
            {
                var x = new XmlSerializer(typeof(TraceProperties.TraceSettings));
                using (var sr = new StringReader(Settings.Default.TraceSettings))
                {
                    return (TraceProperties.TraceSettings)x.Deserialize(sr);
                }
            }
            catch (Exception)
            {
            }

            return TraceProperties.TraceSettings.GetDefaultSettings();
        }

        // DatabaseName = Filters.DatabaseName,

        // LoginName = Filters.LoginName,
        // HostName = Filters.HostName,
        // TextData = Filters.TextData,
        // ApplicationName = Filters.ApplicationName,
        private bool ParseFilterParam(IReadOnlyList<string> args, int idx)
        {
            var condition = idx + 1 < args.Count ? args[idx + 1] : string.Empty;
            var value = idx + 2 < args.Count ? args[idx + 2] : string.Empty;

            switch (args[idx].ToLower())
            {
                case "-cpu":
                    this.m_currentsettings.Filters.CPU = int.Parse(value);
                    this.m_currentsettings.Filters.CpuFilterCondition = TraceProperties.ParseIntCondition(condition);
                    break;
                case "-duration":
                    this.m_currentsettings.Filters.Duration = int.Parse(value);
                    this.m_currentsettings.Filters.DurationFilterCondition =
                        TraceProperties.ParseIntCondition(condition);
                    break;
                case "-reads":
                    this.m_currentsettings.Filters.Reads = int.Parse(value);
                    this.m_currentsettings.Filters.ReadsFilterCondition = TraceProperties.ParseIntCondition(condition);
                    break;
                case "-writes":
                    this.m_currentsettings.Filters.Writes = int.Parse(value);
                    this.m_currentsettings.Filters.WritesFilterCondition = TraceProperties.ParseIntCondition(condition);
                    break;
                case "-spid":
                    this.m_currentsettings.Filters.SPID = int.Parse(value);
                    this.m_currentsettings.Filters.SPIDFilterCondition = TraceProperties.ParseIntCondition(condition);
                    break;

                case "-databasename":
                    this.m_currentsettings.Filters.DatabaseName = value;
                    this.m_currentsettings.Filters.DatabaseNameFilterCondition =
                        TraceProperties.ParseStringCondition(condition);
                    break;
                case "-loginname":
                    this.m_currentsettings.Filters.LoginName = value;
                    this.m_currentsettings.Filters.LoginNameFilterCondition =
                        TraceProperties.ParseStringCondition(condition);
                    break;
                case "-hostname":
                    this.m_currentsettings.Filters.HostName = value;
                    this.m_currentsettings.Filters.HostNameFilterCondition =
                        TraceProperties.ParseStringCondition(condition);
                    break;
                case "-textdata":
                    this.m_currentsettings.Filters.TextData = value;
                    this.m_currentsettings.Filters.TextDataFilterCondition =
                        TraceProperties.ParseStringCondition(condition);
                    break;
                case "-applicationname":
                    this.m_currentsettings.Filters.ApplicationName = value;
                    this.m_currentsettings.Filters.ApplicationNameFilterCondition =
                        TraceProperties.ParseStringCondition(condition);
                    break;
            }

            return false;
        }

        private void ParseCommandLine()
        {
            try
            {
                var args = Environment.GetCommandLineArgs();
                var i = 1;
                while (i < args.Length)
                {
                    var ep = i + 1 < args.Length ? args[i + 1] : string.Empty;
                    switch (args[i].ToLower())
                    {
                        case "-s":
                        case "-server":
                            this.m_servername = ep;
                            i++;
                            break;
                        case "-u":
                        case "-user":
                            this.m_username = ep;
                            i++;
                            break;
                        case "-p":
                        case "-password":
                            this.m_userpassword = ep;
                            i++;
                            break;
                        case "-m":
                        case "-maxevents":
                            if (!int.TryParse(ep, out var m))
                            {
                                m = 1000;
                            }

                            this.m_currentsettings.Filters.MaximumEventCount = m;
                            break;
                        case "-d":
                        case "-duration":
                            if (int.TryParse(ep, out var d))
                            {
                                this.m_currentsettings.Filters.DurationFilterCondition =
                                    TraceProperties.IntFilterCondition.GreaterThan;
                                this.m_currentsettings.Filters.Duration = d;
                            }

                            break;
                        case "-start":
                            this.m_autostart = true;
                            break;
                        case "-batchcompleted":
                            this.m_currentsettings.EventsColumns.BatchCompleted = true;
                            break;
                        case "-batchstarting":
                            this.m_currentsettings.EventsColumns.BatchStarting = true;
                            break;
                        case "-existingconnection":
                            this.m_currentsettings.EventsColumns.ExistingConnection = true;
                            break;
                        case "-loginlogout":
                            this.m_currentsettings.EventsColumns.LoginLogout = true;
                            break;
                        case "-rpccompleted":
                            this.m_currentsettings.EventsColumns.RPCCompleted = true;
                            break;
                        case "-rpcstarting":
                            this.m_currentsettings.EventsColumns.RPCStarting = true;
                            break;
                        case "-spstmtcompleted":
                            this.m_currentsettings.EventsColumns.SPStmtCompleted = true;
                            break;
                        case "-spstmtstarting":
                            this.m_currentsettings.EventsColumns.SPStmtStarting = true;
                            break;
                        default:
                            if (this.ParseFilterParam(args, i))
                            {
                                i++;
                            }

                            break;
                    }

                    i++;
                }

                if (this.m_servername.Length == 0)
                {
                    this.m_servername = @"(local)";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbStart_Click(object sender, EventArgs e)
        {
            if (!TraceProperties.AtLeastOneEventSelected(this.m_currentsettings))
            {
                MessageBox.Show(
                    "You should select at least 1 event",
                    "Starting trace",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                this.RunProfiling(true);
            }
            else
            {
                this.RunProfiling(false);
            }
        }

        private void UpdateButtons()
        {
            this.tbStart.Enabled = this.m_ProfilingState == ProfilingStateEnum.psStopped ||
                                   this.m_ProfilingState == ProfilingStateEnum.psPaused;
            this.tbRun.Enabled = this.tbStart.Enabled;
            this.mnRun.Enabled = this.tbRun.Enabled;
            this.tbRunWithFilters.Enabled = ProfilingStateEnum.psStopped == this.m_ProfilingState;
            this.mnRunWithFilters.Enabled = this.tbRunWithFilters.Enabled;
            this.startTraceToolStripMenuItem.Enabled = this.tbStart.Enabled;
            this.tbStop.Enabled = this.m_ProfilingState == ProfilingStateEnum.psPaused ||
                                  this.m_ProfilingState == ProfilingStateEnum.psProfiling;
            this.stopTraceToolStripMenuItem.Enabled = this.tbStop.Enabled;
            this.tbPause.Enabled = this.m_ProfilingState == ProfilingStateEnum.psProfiling;
            this.pauseTraceToolStripMenuItem.Enabled = this.tbPause.Enabled;
            this.timer1.Enabled = this.m_ProfilingState == ProfilingStateEnum.psProfiling;
            this.edServer.Enabled = this.m_ProfilingState == ProfilingStateEnum.psStopped;
            this.tbAuth.Enabled = this.m_ProfilingState == ProfilingStateEnum.psStopped;
            this.edUser.Enabled = this.edServer.Enabled && this.tbAuth.SelectedIndex == 1;
            this.edPassword.Enabled = this.edServer.Enabled && this.tbAuth.SelectedIndex == 1;
        }

        private void InitLV()
        {
            this.lvEvents = new ListViewNF
            {
                Dock = DockStyle.Fill,
                Location = new Point(0, 0),
                Name = "lvEvents",
                Size = new Size(979, 297),
                TabIndex = 0,
                VirtualMode = true,
                UseCompatibleStateImageBehavior = false,
                BorderStyle = BorderStyle.None,
                FullRowSelect = true,
                View = View.Details,
                GridLines = true,
                HideSelection = false,
                MultiSelect = true,
                AllowColumnReorder = false
            };
            this.lvEvents.RetrieveVirtualItem += this.lvEvents_RetrieveVirtualItem;
            this.lvEvents.KeyDown += lvEvents_KeyDown;
            this.lvEvents.ItemSelectionChanged += this.listView1_ItemSelectionChanged_1;
            this.lvEvents.ColumnClick += this.lvEvents_ColumnClick;
            this.lvEvents.SelectedIndexChanged += this.lvEvents_SelectedIndexChanged;
            this.lvEvents.VirtualItemsSelectionRangeChanged += this.LvEventsOnVirtualItemsSelectionRangeChanged;
            this.lvEvents.ContextMenuStrip = this.contextMenuStrip1;
            this.splitContainer1.Panel1.Controls.Add(this.lvEvents);
            this.InitColumns();
            this.InitGridColumns();
        }

        private void InitColumns()
        {
            this.m_columns.Clear();
            this.m_columns.Add(
                new PerfColumn { Caption = "Event Class", Column = ProfilerEventColumns.EventClass, Width = 122 });
            this.m_columns.Add(
                new PerfColumn { Caption = "Text Data", Column = ProfilerEventColumns.TextData, Width = 255 });
            this.m_columns.Add(
                new PerfColumn { Caption = "Login Name", Column = ProfilerEventColumns.LoginName, Width = 79 });
            this.m_columns.Add(
                new PerfColumn
                {
                    Caption = "CPU",
                    Column = ProfilerEventColumns.CPU,
                    Width = 82,
                    Alignment = HorizontalAlignment.Right,
                    Format = "#,0"
                });
            this.m_columns.Add(
                new PerfColumn
                {
                    Caption = "Reads",
                    Column = ProfilerEventColumns.Reads,
                    Width = 78,
                    Alignment = HorizontalAlignment.Right,
                    Format = "#,0"
                });
            this.m_columns.Add(
                new PerfColumn
                {
                    Caption = "Writes",
                    Column = ProfilerEventColumns.Writes,
                    Width = 78,
                    Alignment = HorizontalAlignment.Right,
                    Format = "#,0"
                });
            this.m_columns.Add(
                new PerfColumn
                {
                    Caption = "Duration, ms",
                    Column = ProfilerEventColumns.Duration,
                    Width = 82,
                    Alignment = HorizontalAlignment.Right,
                    Format = "#,0"
                });
            this.m_columns.Add(
                new PerfColumn
                {
                    Caption = "SPID",
                    Column = ProfilerEventColumns.SPID,
                    Width = 50,
                    Alignment = HorizontalAlignment.Right
                });

            if (this.m_currentsettings.EventsColumns.StartTime)
            {
                this.m_columns.Add(
                    new PerfColumn
                    {
                        Caption = "Start time",
                        Column = ProfilerEventColumns.StartTime,
                        Width = 140,
                        Format = "yyyy-MM-dd hh:mm:ss.ffff"
                    });

            }

            if (this.m_currentsettings.EventsColumns.EndTime)
            {
                this.m_columns.Add(
                    new PerfColumn
                    {
                        Caption = "End time",
                        Column = ProfilerEventColumns.EndTime,
                        Width = 140,
                        Format = "yyyy-MM-dd hh:mm:ss.ffff"
                    });

            }

            if (this.m_currentsettings.EventsColumns.DatabaseName)
            {
                this.m_columns.Add(
                    new PerfColumn
                    {
                        Caption = "DatabaseName", Column = ProfilerEventColumns.DatabaseName, Width = 70
                    });
            }

            if (this.m_currentsettings.EventsColumns.ObjectName)
            {
                this.m_columns.Add(
                    new PerfColumn { Caption = "Object name", Column = ProfilerEventColumns.ObjectName, Width = 70 });
            }

            if (this.m_currentsettings.EventsColumns.ApplicationName)
            {
                this.m_columns.Add(
                    new PerfColumn
                    {
                        Caption = "Application name", Column = ProfilerEventColumns.ApplicationName, Width = 70
                    });
            }

            if (this.m_currentsettings.EventsColumns.HostName)
            {
                this.m_columns.Add(
                    new PerfColumn { Caption = "Host name", Column = ProfilerEventColumns.HostName, Width = 70 });

            }

            this.m_columns.Add(
                new PerfColumn { Caption = "#", Column = -1, Width = 53, Alignment = HorizontalAlignment.Right });
        }

        private void InitGridColumns()
        {
            this.InitColumns();
            this.lvEvents.BeginUpdate();
            try
            {
                this.lvEvents.Columns.Clear();
                foreach (var pc in this.m_columns)
                {
                    var l = this.lvEvents.Columns.Add(pc.Caption, pc.Width);
                    l.TextAlign = pc.Alignment;
                }
            }
            finally
            {
                this.lvEvents.EndUpdate();
            }
        }

        private void LvEventsOnVirtualItemsSelectionRangeChanged(
            object sender,
            ListViewVirtualItemsSelectionRangeChangedEventArgs listViewVirtualItemsSelectionRangeChangedEventArgs)
        {
            this.UpdateSourceBox();
        }

        void lvEvents_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateSourceBox();
        }

        void lvEvents_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lvEvents.ToggleSortOrder();
            this.lvEvents.SetSortIcon(e.Column, this.lvEvents.SortOrder);
            var comparer = new TextDataComparer(e.Column, this.lvEvents.SortOrder);
            this.m_Cached.Sort(comparer);
            this.UpdateSourceBox();
            this.ShowSelectedEvent();
        }

        private string GetEventCaption(ProfilerEvent evt)
        {
            if (evt == this.m_EventStarted)
            {
                return "Trace started";
            }

            if (evt == this.m_EventPaused)
            {
                return "Trace paused";
            }

            if (evt == this.m_EventStopped)
            {
                return "Trace stopped";
            }

            return ProfilerEvents.Names[evt.EventClass];
        }

        private static string GetFormattedValue(ProfilerEvent evt, int column, string format)
        {
            return ProfilerEventColumns.Duration == column
                ? (evt.Duration / 1000).ToString(format)
                : evt.GetFormattedData(column, format);
        }

        private void NewEventArrived(ProfilerEvent evt, bool last)
        {
            {
                var current = this.lvEvents.SelectedIndices.Count > 0
                    ? this.m_Cached[this.lvEvents.SelectedIndices[0]]
                    : null;
                this.m_EventCount++;
                var caption = this.GetEventCaption(evt);
                var lvi = new ListViewItem(caption);
                var items = new string[this.m_columns.Count];
                for (var i = 1; i < this.m_columns.Count; i++)
                {
                    var pc = this.m_columns[i];
                    items[i - 1] = pc.Column == -1
                        ? this.m_EventCount.ToString("#,0")
                        : GetFormattedValue(evt, pc.Column, pc.Format) ?? string.Empty;
                }

                lvi.SubItems.AddRange(items);
                lvi.Tag = evt;
                this.m_Cached.Add(lvi);
                if (!last)
                {
                    return;
                }

                this.lvEvents.VirtualListSize = this.m_Cached.Count;
                this.lvEvents.SelectedIndices.Clear();
                this.FocusLVI(
                    this.tbScroll.Checked ? this.lvEvents.Items[this.m_Cached.Count - 1] : current,
                    this.tbScroll.Checked);
                this.lvEvents.Invalidate(lvi.Bounds);
            }
        }

        private void FocusLVI(ListViewItem lvi, bool ensure)
        {
            if (null == lvi)
            {
                return;
            }

            lvi.Focused = true;
            lvi.Selected = true;
            this.listView1_ItemSelectionChanged_1(this.lvEvents, null);
            if (ensure)
            {
                this.lvEvents.EnsureVisible(this.lvEvents.Items.IndexOf(lvi));
            }
        }

        private void ProfilerThread(object state)
        {
            try
            {
                while (!this.m_NeedStop && this.m_Rdr.TraceIsActive)
                {
                    var evt = this.m_Rdr.Next();
                    if (evt == null)
                    {
                        continue;
                    }

                    lock (this)
                    {
                        this.m_events.Enqueue(evt);
                    }
                }
            }
            catch (Exception e)
            {
                lock (this)
                {
                    if (!this.m_NeedStop && this.m_Rdr.TraceIsActive)
                    {
                        this.m_profilerexception = e;
                    }
                }
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection
            {
                ConnectionString = this.tbAuth.SelectedIndex == 0
                    ? $@"Data Source = {this.edServer.Text}; Initial Catalog = master; Integrated Security=SSPI;Application Name=Express Profiler"
                    : $@"Data Source={this.edServer.Text};Initial Catalog=master;User Id={this.edUser.Text};Password='{this.edPassword.Text}';;Application Name=Express Profiler"
            };
        }

        private void StartProfiling()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.m_perf.Clear();
                this.m_first = null;
                this.m_prev = null;
                if (this.m_ProfilingState == ProfilingStateEnum.psPaused)
                {
                    this.ResumeProfiling();
                    return;
                }

                if (this.m_Conn != null && this.m_Conn.State == ConnectionState.Open)
                {
                    this.m_Conn.Close();
                }

                this.InitGridColumns();
                this.m_EventCount = 0;
                this.m_Conn = this.GetConnection();
                this.m_Conn.Open();
                this.m_Rdr = new RawTraceReader(this.m_Conn);

                this.m_Rdr.CreateTrace();
                if (true)
                {
                    if (this.m_currentsettings.EventsColumns.LoginLogout)
                    {
                        this.m_Rdr.SetEvent(
                            ProfilerEvents.SecurityAudit.AuditLogin,
                            ProfilerEventColumns.TextData,
                            ProfilerEventColumns.LoginName,
                            ProfilerEventColumns.SPID,
                            ProfilerEventColumns.StartTime,
                            ProfilerEventColumns.EndTime,
                            ProfilerEventColumns.HostName);
                        this.m_Rdr.SetEvent(
                            ProfilerEvents.SecurityAudit.AuditLogout,
                            ProfilerEventColumns.CPU,
                            ProfilerEventColumns.Reads,
                            ProfilerEventColumns.Writes,
                            ProfilerEventColumns.Duration,
                            ProfilerEventColumns.LoginName,
                            ProfilerEventColumns.SPID,
                            ProfilerEventColumns.StartTime,
                            ProfilerEventColumns.EndTime,
                            ProfilerEventColumns.ApplicationName,
                            ProfilerEventColumns.HostName);
                    }

                    if (this.m_currentsettings.EventsColumns.ExistingConnection)
                    {
                        this.m_Rdr.SetEvent(
                            ProfilerEvents.Sessions.ExistingConnection,
                            ProfilerEventColumns.TextData,
                            ProfilerEventColumns.SPID,
                            ProfilerEventColumns.StartTime,
                            ProfilerEventColumns.EndTime,
                            ProfilerEventColumns.ApplicationName,
                            ProfilerEventColumns.HostName);
                    }

                    if (this.m_currentsettings.EventsColumns.BatchCompleted)
                    {
                        this.m_Rdr.SetEvent(
                            ProfilerEvents.TSQL.SQLBatchCompleted,
                            ProfilerEventColumns.TextData,
                            ProfilerEventColumns.LoginName,
                            ProfilerEventColumns.CPU,
                            ProfilerEventColumns.Reads,
                            ProfilerEventColumns.Writes,
                            ProfilerEventColumns.Duration,
                            ProfilerEventColumns.SPID,
                            ProfilerEventColumns.StartTime,
                            ProfilerEventColumns.EndTime,
                            ProfilerEventColumns.DatabaseName,
                            ProfilerEventColumns.ApplicationName,
                            ProfilerEventColumns.HostName);
                    }

                    if (this.m_currentsettings.EventsColumns.BatchStarting)
                    {
                        this.m_Rdr.SetEvent(
                            ProfilerEvents.TSQL.SQLBatchStarting,
                            ProfilerEventColumns.TextData,
                            ProfilerEventColumns.LoginName,
                            ProfilerEventColumns.SPID,
                            ProfilerEventColumns.StartTime,
                            ProfilerEventColumns.EndTime,
                            ProfilerEventColumns.DatabaseName,
                            ProfilerEventColumns.ApplicationName,
                            ProfilerEventColumns.HostName);
                    }

                    if (this.m_currentsettings.EventsColumns.RPCStarting)
                    {
                        this.m_Rdr.SetEvent(
                            ProfilerEvents.StoredProcedures.RPCStarting,
                            ProfilerEventColumns.TextData,
                            ProfilerEventColumns.LoginName,
                            ProfilerEventColumns.SPID,
                            ProfilerEventColumns.StartTime,
                            ProfilerEventColumns.EndTime,
                            ProfilerEventColumns.DatabaseName,
                            ProfilerEventColumns.ObjectName,
                            ProfilerEventColumns.ApplicationName,
                            ProfilerEventColumns.HostName);
                    }
                }

                if (this.m_currentsettings.EventsColumns.RPCCompleted)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.StoredProcedures.RPCCompleted,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.Reads,
                        ProfilerEventColumns.Writes,
                        ProfilerEventColumns.Duration,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.EndTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ObjectName,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (this.m_currentsettings.EventsColumns.SPStmtCompleted)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.StoredProcedures.SPStmtCompleted,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.Reads,
                        ProfilerEventColumns.Writes,
                        ProfilerEventColumns.Duration,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.EndTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ObjectName,
                        ProfilerEventColumns.ObjectID,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (this.m_currentsettings.EventsColumns.SPStmtStarting)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.StoredProcedures.SPStmtStarting,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.Reads,
                        ProfilerEventColumns.Writes,
                        ProfilerEventColumns.Duration,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.EndTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ObjectName,
                        ProfilerEventColumns.ObjectID,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (this.m_currentsettings.EventsColumns.UserErrorMessage)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.ErrorsAndWarnings.UserErrorMessage,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (this.m_currentsettings.EventsColumns.BlockedProcessPeport)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.ErrorsAndWarnings.Blockedprocessreport,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (this.m_currentsettings.EventsColumns.SQLStmtStarting)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.TSQL.SQLStmtStarting,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.Reads,
                        ProfilerEventColumns.Writes,
                        ProfilerEventColumns.Duration,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.EndTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (this.m_currentsettings.EventsColumns.SQLStmtCompleted)
                {
                    this.m_Rdr.SetEvent(
                        ProfilerEvents.TSQL.SQLStmtCompleted,
                        ProfilerEventColumns.TextData,
                        ProfilerEventColumns.LoginName,
                        ProfilerEventColumns.CPU,
                        ProfilerEventColumns.Reads,
                        ProfilerEventColumns.Writes,
                        ProfilerEventColumns.Duration,
                        ProfilerEventColumns.SPID,
                        ProfilerEventColumns.StartTime,
                        ProfilerEventColumns.EndTime,
                        ProfilerEventColumns.DatabaseName,
                        ProfilerEventColumns.ApplicationName,
                        ProfilerEventColumns.HostName);
                }

                if (null != this.m_currentsettings.Filters.Duration)
                {
                    this.SetIntFilter(
                        this.m_currentsettings.Filters.Duration * 1000,
                        this.m_currentsettings.Filters.DurationFilterCondition,
                        ProfilerEventColumns.Duration);
                }

                this.SetIntFilter(
                    this.m_currentsettings.Filters.Reads,
                    this.m_currentsettings.Filters.ReadsFilterCondition,
                    ProfilerEventColumns.Reads);
                this.SetIntFilter(
                    this.m_currentsettings.Filters.Writes,
                    this.m_currentsettings.Filters.WritesFilterCondition,
                    ProfilerEventColumns.Writes);
                this.SetIntFilter(
                    this.m_currentsettings.Filters.CPU,
                    this.m_currentsettings.Filters.CpuFilterCondition,
                    ProfilerEventColumns.CPU);
                this.SetIntFilter(
                    this.m_currentsettings.Filters.SPID,
                    this.m_currentsettings.Filters.SPIDFilterCondition,
                    ProfilerEventColumns.SPID);

                this.SetStringFilter(
                    this.m_currentsettings.Filters.LoginName,
                    this.m_currentsettings.Filters.LoginNameFilterCondition,
                    ProfilerEventColumns.LoginName);
                this.SetStringFilter(
                    this.m_currentsettings.Filters.HostName,
                    this.m_currentsettings.Filters.HostNameFilterCondition,
                    ProfilerEventColumns.HostName);
                this.SetStringFilter(
                    this.m_currentsettings.Filters.DatabaseName,
                    this.m_currentsettings.Filters.DatabaseNameFilterCondition,
                    ProfilerEventColumns.DatabaseName);
                this.SetStringFilter(
                    this.m_currentsettings.Filters.TextData,
                    this.m_currentsettings.Filters.TextDataFilterCondition,
                    ProfilerEventColumns.TextData);
                this.SetStringFilter(
                    this.m_currentsettings.Filters.ApplicationName,
                    this.m_currentsettings.Filters.ApplicationNameFilterCondition,
                    ProfilerEventColumns.ApplicationName);

                this.m_Cmd.Connection = this.m_Conn;
                this.m_Cmd.CommandTimeout = 0;
                this.m_Rdr.SetFilter(
                    ProfilerEventColumns.ApplicationName,
                    LogicalOperators.AND,
                    ComparisonOperators.NotLike,
                    "Express Profiler");
                this.m_Cached.Clear();
                this.m_events.Clear();
                this.m_itembysql.Clear();
                this.lvEvents.VirtualListSize = 0;
                this.StartProfilerThread();
                this.m_servername = this.edServer.Text;
                this.m_username = this.edUser.Text;
                this.SaveDefaultSettings();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.UpdateButtons();
                this.Cursor = Cursors.Default;
            }
        }

        private void SaveDefaultSettings()
        {
            Settings.Default.ServerName = this.m_servername;
            Settings.Default.UserName = this.tbAuth.SelectedIndex == 0 ? string.Empty : this.m_username;
            Settings.Default.Save();
        }

        private void SetIntFilter(int? value, TraceProperties.IntFilterCondition condition, int column)
        {
            var com = new[]
            {
                ComparisonOperators.Equal, ComparisonOperators.NotEqual, ComparisonOperators.GreaterThan,
                ComparisonOperators.LessThan
            };

            if (null == value)
            {
                return;
            }

            long? v = value;
            this.m_Rdr.SetFilter(column, LogicalOperators.AND, com[(int)condition], v);
        }

        private void SetStringFilter(string value, TraceProperties.StringFilterCondition condition, int column)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.m_Rdr.SetFilter(
                    column,
                    LogicalOperators.AND,
                    condition == TraceProperties.StringFilterCondition.Like
                        ? ComparisonOperators.Like
                        : ComparisonOperators.NotLike,
                    value);
            }
        }

        private void StartProfilerThread()
        {
            this.m_Rdr?.Close();

            this.m_Rdr.StartTrace();
            this.m_Thr = new Thread(this.ProfilerThread) { IsBackground = true, Priority = ThreadPriority.Lowest };
            this.m_NeedStop = false;
            this.m_ProfilingState = ProfilingStateEnum.psProfiling;
            this.NewEventArrived(this.m_EventStarted, true);
            this.m_Thr.Start();
        }

        private void ResumeProfiling()
        {
            this.StartProfilerThread();
            this.UpdateButtons();
        }

        private void tbStop_Click(object sender, EventArgs e)
        {
            this.StopProfiling();
        }

        private void StopProfiling()
        {
            this.tbStop.Enabled = false;
            using (var cn = this.GetConnection())
            {
                cn.Open();
                this.m_Rdr.StopTrace(cn);
                this.m_Rdr.CloseTrace(cn);
                cn.Close();
            }

            this.m_NeedStop = true;
            if (this.m_Thr.IsAlive)
            {
                this.m_Thr.Abort();
            }

            this.m_Thr.Join();
            this.m_ProfilingState = ProfilingStateEnum.psStopped;
            this.NewEventArrived(this.m_EventStopped, true);
            this.UpdateButtons();
            this.SaveRecentConnection();
        }

        private void listView1_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this.UpdateSourceBox();
        }

        private void UpdateSourceBox()
        {
            if (this.dontUpdateSource)
            {
                return;
            }

            var sb = new StringBuilder();

            foreach (int i in this.lvEvents.SelectedIndices)
            {
                var lv = this.m_Cached[i];
                if (lv.SubItems[1].Text != string.Empty)
                {
                    sb.AppendFormat("{0}\r\ngo\r\n", lv.SubItems[1].Text.ParseSql());
                }
            }

            this.m_Lex.FillRichEdit(this.reTextData, sb.ToString());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.m_ProfilingState != ProfilingStateEnum.psPaused &&
                this.m_ProfilingState != ProfilingStateEnum.psProfiling)
            {
                return;
            }

            if (MessageBox.Show(
                    "There are traces still running. Are you sure you want to close the application?",
                    "ExpressProfiler",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.StopProfiling();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void lvEvents_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = this.m_Cached[e.ItemIndex];
        }

        private void tbPause_Click(object sender, EventArgs e)
        {
            this.PauseProfiling();
        }

        private void PauseProfiling()
        {
            using (var cn = this.GetConnection())
            {
                cn.Open();
                this.m_Rdr.StopTrace(cn);
                cn.Close();
            }

            this.m_ProfilingState = ProfilingStateEnum.psPaused;
            this.NewEventArrived(this.m_EventPaused, true);
            this.UpdateButtons();
        }

        private void SelectAllEvents(bool select)
        {
            lock (this.m_Cached)
            {
                this.lvEvents.BeginUpdate();
                this.dontUpdateSource = true;
                try
                {
                    foreach (var lv in this.m_Cached)
                    {
                        lv.Selected = select;
                    }
                }
                finally
                {
                    this.dontUpdateSource = false;
                    this.UpdateSourceBox();
                    this.lvEvents.EndUpdate();
                }
            }
        }

        private static void lvEvents_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Queue<ProfilerEvent> saved;
            Exception exc;
            lock (this)
            {
                saved = this.m_events;
                this.m_events = new Queue<ProfilerEvent>(10);
                exc = this.m_profilerexception;
                this.m_profilerexception = null;
            }

            if (null != exc)
            {
                using (var dlg = new ThreadExceptionDialog(exc))
                {
                    dlg.ShowDialog();
                }
            }

            lock (this.m_Cached)
            {
                while (0 != saved.Count)
                {
                    this.NewEventArrived(saved.Dequeue(), 0 == saved.Count);
                }

                if (this.m_Cached.Count > this.m_currentsettings.Filters.MaximumEventCount)
                {
                    while (this.m_Cached.Count > this.m_currentsettings.Filters.MaximumEventCount)
                    {
                        this.m_Cached.RemoveAt(0);
                    }

                    this.lvEvents.VirtualListSize = this.m_Cached.Count;
                    this.lvEvents.Invalidate();
                }

                if (null != this.m_prev && !(DateTime.Now.Subtract(this.m_prev.m_date).TotalSeconds >= 1))
                {
                    return;
                }

                var curr = new PerfInfo { m_count = this.m_EventCount };
                if (this.m_perf.Count >= 60)
                {
                    this.m_first = this.m_perf.Dequeue();
                }

                this.m_first ??= curr;

                this.m_prev ??= curr;

                var now = DateTime.Now;
                var d1 = now.Subtract(this.m_prev.m_date).TotalSeconds;
                var d2 = now.Subtract(this.m_first.m_date).TotalSeconds;
                this.slEPS.Text =
                    $"{(Math.Abs(d1 - 0) > 0.001 ? ((curr.m_count - this.m_prev.m_count) / d1).ToString("#,0.00") : string.Empty)} / {(Math.Abs(d2 - 0) > 0.001 ? ((curr.m_count - this.m_first.m_count) / d2).ToString("#,0.00") : string.Empty)} EPS(last/avg for {d2:0} second(s))";

                this.m_perf.Enqueue(curr);
                this.m_prev = curr;
            }
        }

        private void tbAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateButtons();
        }

        private void ClearTrace()
        {
            lock (this.lvEvents)
            {
                this.m_Cached.Clear();
                this.m_itembysql.Clear();
                this.lvEvents.VirtualListSize = 0;
                this.listView1_ItemSelectionChanged_1(this.lvEvents, null);
                this.lvEvents.Invalidate();
                this.m_EventCount = 0;
            }
        }

        private void tbClear_Click(object sender, EventArgs e)
        {
            this.ClearTrace();
        }

        private static void NewAttribute(XmlNode node, string name, string value)
        {
            var attr = node.OwnerDocument.CreateAttribute(name);
            attr.Value = value;
            node.Attributes.Append(attr);
        }

        private static void NewAttribute(XmlNode node, string name, string value, string namespaceURI)
        {
            var attr = node.OwnerDocument.CreateAttribute("ss", name, namespaceURI);
            attr.Value = value;
            node.Attributes.Append(attr);
        }

        private void copyAllToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyEventsToClipboard(false);
        }

        private void CopyEventsToClipboard(bool copySelected)
        {
            var doc = new XmlDocument();
            XmlNode root = doc.CreateElement("events");
            lock (this.m_Cached)
            {
                if (copySelected)
                {
                    foreach (int i in this.lvEvents.SelectedIndices)
                    {
                        CreateEventRow((ProfilerEvent)this.m_Cached[i].Tag, root);
                    }
                }
                else
                {
                    foreach (var i in this.m_Cached)
                    {
                        CreateEventRow((ProfilerEvent)i.Tag, root);
                    }
                }
            }

            doc.AppendChild(root);
            doc.PreserveWhitespace = true;
            using (var writer = new StringWriter())
            {
                var textWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented };
                doc.Save(textWriter);
                Clipboard.SetText(writer.ToString());
            }

            MessageBox.Show(
                "Event(s) data copied to clipboard",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private static void CreateEventRow(ProfilerEvent evt, XmlNode root)
        {
            XmlNode row = root.OwnerDocument.CreateElement("event");
            NewAttribute(row, "EventClass", evt.EventClass.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "CPU", evt.CPU.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "Reads", evt.Reads.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "Writes", evt.Writes.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "Duration", evt.Duration.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "SPID", evt.SPID.ToString(CultureInfo.InvariantCulture));
            NewAttribute(row, "LoginName", evt.LoginName);
            NewAttribute(row, "DatabaseName", evt.DatabaseName);
            NewAttribute(row, "ObjectName", evt.ObjectName);
            NewAttribute(row, "ApplicationName", evt.ApplicationName);
            NewAttribute(row, "HostName", evt.HostName);
            row.InnerText = evt.TextData;
            root.AppendChild(row);
        }

        private void copySelectedToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyEventsToClipboard(true);
        }

        private void clearTraceWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ClearTrace();
        }

        private void extractAllEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyEventsToClipboard(false);
        }

        private void extractSelectedEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyEventsToClipboard(true);
        }

        private void pauseTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.PauseProfiling();
        }

        private void stopTraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.StopProfiling();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoFind();
        }

        private void DoFind()
        {
            if (this.m_ProfilingState == ProfilingStateEnum.psProfiling)
            {
                MessageBox.Show(
                    "You cannot find when trace is running",
                    "ExpressProfiler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using (var f = new FindForm(this))
            {
                f.TopMost = this.TopMost;
                f.ShowDialog();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lvEvents.Focused && this.m_ProfilingState != ProfilingStateEnum.psProfiling)
            {
                this.SelectAllEvents(true);
            }
            else if (this.reTextData.Focused)
            {
                this.reTextData.SelectAll();
            }
        }

        // internal void PerformFind(bool forwards)
        // {
        // if(String.IsNullOrEmpty(lastpattern)) return;

        // if (forwards)
        // {
        // for (int i = lastpos = lvEvents.Items.IndexOf(lvEvents.FocusedItem) + 1; i < m_Cached.Count; i++)
        // {
        // if (FindText(i))
        // {
        // return;
        // }
        // }
        // }
        // else
        // {
        // for (int i = lastpos = lvEvents.Items.IndexOf(lvEvents.FocusedItem) - 1; i > 0; i--)
        // {
        // if (FindText(i))
        // {
        // return;
        // }
        // }
        // }
        // MessageBox.Show(String.Format("Failed to find \"{0}\". Searched to the end of data. ", lastpattern), "ExpressProfiler", MessageBoxButtons.OK, MessageBoxIcon.Information);
        // }
        internal void PerformFind(bool forwards, bool wrapAround)
        {
            if (string.IsNullOrEmpty(this.lastpattern))
            {
                return;
            }

            var lastpos = this.lvEvents.Items.IndexOf(this.lvEvents.FocusedItem);
            if (forwards)
            {
                for (var i = lastpos + 1; i < this.m_Cached.Count; i++)
                {
                    if (this.FindText(i))
                    {
                        return;
                    }
                }

                if (wrapAround)
                {
                    for (var i = 0; i < lastpos; i++)
                    {
                        if (this.FindText(i))
                        {
                            return;
                        }
                    }
                }
            }
            else
            {
                for (var i = lastpos - 1; i > 0; i--)
                {
                    if (this.FindText(i))
                    {
                        return;
                    }
                }

                if (wrapAround)
                {
                    for (var i = this.m_Cached.Count; i > lastpos; i--)
                    {
                        if (this.FindText(i))
                        {
                            return;
                        }
                    }
                }
            }

            MessageBox.Show(
                $"Failed to find \"{this.lastpattern}\". Searched to the end of data. ",
                "ExpressProfiler",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void ShowSelectedEvent()
        {
            var focusedIndex = this.lvEvents.Items.IndexOf(this.lvEvents.FocusedItem);
            if (focusedIndex <= -1 || focusedIndex >= this.m_Cached.Count)
            {
                return;
            }

            var lvi = this.m_Cached[focusedIndex];

            lvi.Focused = true;
            this.SelectAllEvents(false);
            this.FocusLVI(lvi, true);
        }

        private bool FindText(int i)
        {
            var lvi = this.m_Cached[i];
            var evt = (ProfilerEvent)lvi.Tag;
            var pattern = this.wholeWord ? "\\b" + this.lastpattern + "\\b" : this.lastpattern;
            if (!Regex.IsMatch(evt.TextData, pattern, this.matchCase ? RegexOptions.None : RegexOptions.IgnoreCase))
            {
                return false;
            }

            lvi.Focused = true;
            this.SelectAllEvents(false);
            this.FocusLVI(lvi, true);
            return true;

        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.m_ProfilingState == ProfilingStateEnum.psProfiling)
            {
                MessageBox.Show(
                    "You cannot find when trace is running",
                    "ExpressProfiler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            this.PerformFind(true, false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RunProfiling(bool showfilters)
        {
            if (showfilters)
            {
                var ts = this.m_currentsettings.GetCopy();
                using (var frm = new TraceProperties())
                {
                    frm.SetSettings(ts);
                    if (DialogResult.OK != frm.ShowDialog())
                    {
                        return;
                    }

                    this.m_currentsettings = frm.m_currentsettings.GetCopy();
                }
            }

            this.StartProfiling();
        }

        private void tbRunWithFilters_Click(object sender, EventArgs e)
        {
            this.RunProfiling(true);
        }

        private void copyToXlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CopyForExcel();
        }

        private void CopyForExcel()
        {
            var doc = new XmlDocument();
            var pi = doc.CreateProcessingInstruction("mso-application", "progid='Excel.Sheet'");
            doc.AppendChild(pi);
            const string urn = "urn:schemas-microsoft-com:office:spreadsheet";
            XmlNode root = doc.CreateElement("ss", "Workbook", urn);
            NewAttribute(root, "xmlns:ss", urn);
            doc.AppendChild(root);

            XmlNode styles = doc.CreateElement("ss", "Styles", urn);
            root.AppendChild(styles);
            XmlNode style = doc.CreateElement("ss", "Style", urn);
            styles.AppendChild(style);
            NewAttribute(style, "ID", "s62", urn);
            XmlNode font = doc.CreateElement("ss", "Font", urn);
            style.AppendChild(font);
            NewAttribute(font, "Bold", "1", urn);

            XmlNode worksheet = doc.CreateElement("ss", "Worksheet", urn);
            root.AppendChild(worksheet);
            NewAttribute(worksheet, "Name", "Sql Trace", urn);
            XmlNode table = doc.CreateElement("ss", "Table", urn);
            worksheet.AppendChild(table);
            NewAttribute(
                table,
                "ExpandedColumnCount",
                this.m_columns.Count.ToString(CultureInfo.InvariantCulture),
                urn);

            foreach (ColumnHeader lv in this.lvEvents.Columns)
            {
                XmlNode r = doc.CreateElement("ss", "Column", urn);
                NewAttribute(r, "AutoFitWidth", "0", urn);
                NewAttribute(r, "Width", lv.Width.ToString(CultureInfo.InvariantCulture), urn);
                table.AppendChild(r);
            }

            XmlNode row = doc.CreateElement("ss", "Row", urn);
            table.AppendChild(row);
            foreach (ColumnHeader lv in this.lvEvents.Columns)
            {
                XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                row.AppendChild(cell);
                NewAttribute(cell, "StyleID", "s62", urn);
                XmlNode data = doc.CreateElement("ss", "Data", urn);
                cell.AppendChild(data);
                NewAttribute(data, "Type", "String", urn);
                data.InnerText = lv.Text;
            }

            lock (this.m_Cached)
            {
                long rowNumber = 1;
                foreach (var lvi in this.m_Cached)
                {
                    row = doc.CreateElement("ss", "Row", urn);
                    table.AppendChild(row);
                    foreach (var pc in this.m_columns)
                    {
                        if (pc.Column != -1)
                        {
                            XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                            row.AppendChild(cell);
                            XmlNode data = doc.CreateElement("ss", "Data", urn);
                            cell.AppendChild(data);
                            string dataType;
                            switch (ProfilerEventColumns.ProfilerColumnDataTypes[pc.Column])
                            {
                                case ProfilerColumnDataType.Int:
                                case ProfilerColumnDataType.Long:
                                    dataType = "Number";
                                    break;
                                case ProfilerColumnDataType.DateTime:
                                    dataType = "String";
                                    break;
                                default:
                                    dataType = "String";
                                    break;
                            }

                            if (ProfilerEventColumns.EventClass == pc.Column)
                            {
                                dataType = "String";
                            }

                            NewAttribute(data, "Type", dataType, urn);
                            if (ProfilerEventColumns.EventClass == pc.Column)
                            {
                                data.InnerText = this.GetEventCaption((ProfilerEvent)lvi.Tag);
                            }
                            else
                            {
                                data.InnerText = pc.Column == -1
                                    ? string.Empty
                                    : GetFormattedValue(
                                        (ProfilerEvent)lvi.Tag,
                                        pc.Column,
                                        ProfilerEventColumns.ProfilerColumnDataTypes[pc.Column] ==
                                        ProfilerColumnDataType.DateTime
                                            ? pc.Format
                                            : string.Empty) ?? string.Empty;
                            }
                        }
                        else
                        {
                            // The export of the sequence number '#' is handled here.
                            XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                            row.AppendChild(cell);
                            XmlNode data = doc.CreateElement("ss", "Data", urn);
                            cell.AppendChild(data);
                            const string dataType = "Number";
                            NewAttribute(data, "Type", dataType, urn);
                            data.InnerText = rowNumber.ToString();
                        }
                    }

                    rowNumber++;
                }
            }

            using (var writer = new StringWriter())
            {
                var textWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented, Namespaces = true };
                doc.Save(textWriter);
                var xml = writer.ToString();
                var xmlStream = new MemoryStream();
                xmlStream.Write(Encoding.UTF8.GetBytes(xml), 0, xml.Length);
                Clipboard.SetData("XML Spreadsheet", xmlStream);
            }

            MessageBox.Show(
                "Event(s) data copied to clipboard",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void mnAbout_Click(object sender, EventArgs e)
        {
            var aboutMsgOrig =
                $"{versionString} nhttps://expressprofiler.codeplex.com/ \n Filter Icon: http://www.softicons.com/toolbar-icons/iconza-light-blue-icons-by-turbomilk/filter-icon";

            var aboutMsg = new StringBuilder();
            aboutMsg.AppendLine(versionString + "\nhttps://expressprofiler.codeplex.com/");
            aboutMsg.AppendLine();
            aboutMsg.AppendLine("Filter Icon Downloaded From:");
            aboutMsg.AppendLine(
                "    http://www.softicons.com/toolbar-icons/iconza-light-blue-icons-by-turbomilk/filter-icon");
            aboutMsg.AppendLine("    By Author Turbomilk:  	http://turbomilk.com/");
            aboutMsg.AppendLine("    Used under Creative Commons License: http://creativecommons.org/licenses/by/3.0/");

            MessageBox.Show(aboutMsg.ToString(), "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tbStayOnTop_Click(object sender, EventArgs e)
        {
            this.SetStayOnTop();
        }

        private void SetStayOnTop()
        {
            this.tbStayOnTop.Checked = !this.tbStayOnTop.Checked;
            this.TopMost = this.tbStayOnTop.Checked;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.SetTransparent();
        }

        private void SetTransparent()
        {
            this.tbTransparent.Checked = !this.tbTransparent.Checked;
            this.Opacity = this.tbTransparent.Checked ? 0.50 : 1;
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetStayOnTop();
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetTransparent();
        }

        private void deleteSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (var i = this.lvEvents.SelectedIndices.Count - 1; i >= 0; i--)
            {
                this.m_Cached.RemoveAt(this.lvEvents.SelectedIndices[i]);
            }

            this.lvEvents.VirtualListSize = this.m_Cached.Count;
            this.lvEvents.SelectedIndices.Clear();
        }

        private void keepSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (var i = this.m_Cached.Count - 1; i >= 0; i--)
            {
                if (!this.lvEvents.SelectedIndices.Contains(i))
                {
                    this.m_Cached.RemoveAt(i);
                }
            }

            this.lvEvents.VirtualListSize = this.m_Cached.Count;
            this.lvEvents.SelectedIndices.Clear();
        }

        private void SaveToExcelXmlFile()
        {
            var doc = new XmlDocument();
            var pi = doc.CreateProcessingInstruction("mso-application", "progid='Excel.Sheet'");
            doc.AppendChild(pi);
            const string urn = "urn:schemas-microsoft-com:office:spreadsheet";
            XmlNode root = doc.CreateElement("ss", "Workbook", urn);
            NewAttribute(root, "xmlns:ss", urn);
            doc.AppendChild(root);

            XmlNode styles = doc.CreateElement("ss", "Styles", urn);
            root.AppendChild(styles);
            XmlNode style = doc.CreateElement("ss", "Style", urn);
            styles.AppendChild(style);
            NewAttribute(style, "ID", "s62", urn);
            XmlNode font = doc.CreateElement("ss", "Font", urn);
            style.AppendChild(font);
            NewAttribute(font, "Bold", "1", urn);

            XmlNode worksheet = doc.CreateElement("ss", "Worksheet", urn);
            root.AppendChild(worksheet);
            NewAttribute(worksheet, "Name", "Sql Trace", urn);
            XmlNode table = doc.CreateElement("ss", "Table", urn);
            worksheet.AppendChild(table);
            NewAttribute(
                table,
                "ExpandedColumnCount",
                this.m_columns.Count.ToString(CultureInfo.InvariantCulture),
                urn);

            foreach (ColumnHeader lv in this.lvEvents.Columns)
            {
                XmlNode r = doc.CreateElement("ss", "Column", urn);
                NewAttribute(r, "AutoFitWidth", "0", urn);
                NewAttribute(r, "Width", lv.Width.ToString(CultureInfo.InvariantCulture), urn);
                table.AppendChild(r);
            }

            XmlNode row = doc.CreateElement("ss", "Row", urn);
            table.AppendChild(row);
            foreach (ColumnHeader lv in this.lvEvents.Columns)
            {
                XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                row.AppendChild(cell);
                NewAttribute(cell, "StyleID", "s62", urn);
                XmlNode data = doc.CreateElement("ss", "Data", urn);
                cell.AppendChild(data);
                NewAttribute(data, "Type", "String", urn);
                data.InnerText = lv.Text;
            }

            lock (this.m_Cached)
            {
                long rowNumber = 1;
                foreach (var lvi in this.m_Cached)
                {
                    row = doc.CreateElement("ss", "Row", urn);
                    table.AppendChild(row);
                    foreach (var pc in this.m_columns)
                    {
                        if (pc.Column != -1)
                        {
                            XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                            row.AppendChild(cell);
                            XmlNode data = doc.CreateElement("ss", "Data", urn);
                            cell.AppendChild(data);
                            string dataType;
                            switch (ProfilerEventColumns.ProfilerColumnDataTypes[pc.Column])
                            {
                                case ProfilerColumnDataType.Int:
                                case ProfilerColumnDataType.Long:
                                    dataType = "Number";
                                    break;
                                case ProfilerColumnDataType.DateTime:
                                    dataType = "String";
                                    break;
                                default:
                                    dataType = "String";
                                    break;
                            }

                            if (ProfilerEventColumns.EventClass == pc.Column)
                            {
                                dataType = "String";
                            }

                            NewAttribute(data, "Type", dataType, urn);
                            if (ProfilerEventColumns.EventClass == pc.Column)
                            {
                                data.InnerText = this.GetEventCaption((ProfilerEvent)lvi.Tag);
                            }
                            else
                            {
                                data.InnerText = pc.Column == -1
                                    ? string.Empty
                                    : GetFormattedValue(
                                        (ProfilerEvent)lvi.Tag,
                                        pc.Column,
                                        ProfilerEventColumns.ProfilerColumnDataTypes[pc.Column] ==
                                        ProfilerColumnDataType.DateTime
                                            ? pc.Format
                                            : string.Empty) ?? string.Empty;
                            }
                        }
                        else
                        {
                            // The export of the sequence number '#' is handled here.
                            XmlNode cell = doc.CreateElement("ss", "Cell", urn);
                            row.AppendChild(cell);
                            XmlNode data = doc.CreateElement("ss", "Data", urn);
                            cell.AppendChild(data);
                            const string dataType = "Number";
                            NewAttribute(data, "Type", dataType, urn);
                            data.InnerText = rowNumber.ToString();
                        }
                    }

                    rowNumber++;
                }
            }

            var sfd = new SaveFileDialog { Filter = "Excel XML|*.xml", Title = "Save the Excel XML FIle" };
            sfd.ShowDialog();

            if (string.IsNullOrEmpty(sfd.FileName))
            {
                return;
            }

            using (var writer = new StringWriter())
            {
                var textWriter = new XmlTextWriter(writer) { Formatting = Formatting.Indented, Namespaces = true };
                doc.Save(textWriter);
                var xml = writer.ToString();
                var xmlStream = new MemoryStream();
                xmlStream.Write(Encoding.UTF8.GetBytes(xml), 0, xml.Length);
                xmlStream.Position = 0;
                var fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write);
                xmlStream.WriteTo(fs);
                fs.Close();
                xmlStream.Close();
            }

            MessageBox.Show(
                $"File saved to: {sfd.FileName}",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void SetFilterEvents()
        {
            if (this.m_CachedUnFiltered.Count != 0)
            {
                return;
            }

            this.lvEvents.SelectedIndices.Clear();
            var ts = this.m_currentsettings.GetCopy();
            using (var frm = new TraceProperties())
            {
                frm.SetSettings(ts);

                if (DialogResult.OK != frm.ShowDialog())
                {
                    return;
                }

                ts = frm.m_currentsettings.GetCopy();

                this.m_CachedUnFiltered.AddRange(this.m_Cached);
                this.m_Cached.Clear();
                foreach (var lvi in this.m_CachedUnFiltered)
                {
                    if (frm.IsIncluded(lvi) && this.m_Cached.Count < ts.Filters.MaximumEventCount)
                    {
                        this.m_Cached.Add(lvi);
                    }
                }
            }

            this.lvEvents.VirtualListSize = this.m_Cached.Count;
            this.UpdateSourceBox();
            this.ShowSelectedEvent();
        }

        private void ClearFilterEvents()
        {
            if (this.m_CachedUnFiltered.Count <= 0)
            {
                return;
            }

            this.m_Cached.Clear();
            this.m_Cached.AddRange(this.m_CachedUnFiltered);
            this.m_CachedUnFiltered.Clear();
            this.lvEvents.VirtualListSize = this.m_Cached.Count;
            this.lvEvents.SelectedIndices.Clear();
            this.UpdateSourceBox();
            this.ShowSelectedEvent();
        }

        private void saveAllEventsToExcelXmlFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveToExcelXmlFile();
        }

        /// <summary>
        /// Persist the server string when it changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edServer_TextChanged(object sender, EventArgs e)
        {
            this.m_servername = this.edServer.Text;
            this.SaveDefaultSettings();
        }

        /// <summary>
        /// Persist the user name string when it changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void edUser_TextChanged(object sender, EventArgs e)
        {
            this.m_username = this.edUser.Text;
            this.SaveDefaultSettings();
        }

        private void filterCapturedEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SetFilterEvents();
        }

        private void clearCapturedFiltersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ClearFilterEvents();
        }

        private void tbFilterEvents_Click(object sender, EventArgs e)
        {
            var filterButton = (ToolStripButton)sender;
            if (filterButton.Checked)
            {
                this.SetFilterEvents();
            }
            else
            {
                this.ClearFilterEvents();
            }
        }

        private void SaveRecentConnection()
        {
            var recentConnections = this.ReadRecentConnections();

            if (recentConnections?.Connections == null)
            {
                recentConnections = new RecentConnection { Connections = new List<Connection>() };
            }

            var currentConnection = new Connection
            {
                ApplicationName = "Express Profiler",
                Catalog = "master",
                CreationDate = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture),
                DataSource = this.edServer.Text?.Trim(),
                IntegratedSecurity = this.tbAuth.SelectedIndex == 0 ? "SSPI" : string.Empty,
                Password = string.IsNullOrEmpty(this.edPassword.Text?.Trim())
                    ? string.Empty
                    : Cryptography.Encrypt(this.edPassword.Text.Trim()),
                UserId = this.edUser.Text?.Trim()
            };


            recentConnections.Add(currentConnection);

            try
            {
                var serialize = XmlHelper.SerializeXml(recentConnections);
                XmlHelper.WriteXml(this.recentConnectionFolderPath, this.recentConnectionFileName, serialize);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DoSearchConnection();
        }

        public RecentConnection ReadRecentConnections()
        {
            if (!Directory.Exists(this.recentConnectionFolderPath))
            {
                Directory.CreateDirectory(this.recentConnectionFolderPath);
            }

            var recentConnectionsFile = Path.Combine(this.recentConnectionFolderPath, this.recentConnectionFileName);

            if (!File.Exists(recentConnectionsFile))
            {
                return null;
            }

            RecentConnection recentConnection = null;
            try
            {
                recentConnection =
                    XmlHelper.DeserializeXml<RecentConnection>(TextFileHelper.ReadAllText(recentConnectionsFile));
            }
            catch
            {
            }

            return recentConnection;
        }

        private void DoSearchConnection()
        {
            if (this.m_ProfilingState == ProfilingStateEnum.psProfiling)
            {
                MessageBox.Show(
                    "You cannot search a recent connection when trace is running",
                    "ExpressProfiler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            try
            {
                using (var form = new RecentConnectionsForm(this))
                {
                    form.TopMost = this.TopMost;
                    form.ShowDialog();

                    if (!form.ConnectionSelected)
                    {
                        return;
                    }

                    this.edServer.Text = this.recent_servername;
                    this.edUser.Text = this.recent__username;
                    this.edPassword.Text = this.recent_userpassword;
                    this.tbAuth.SelectedIndex = this.recent_auth;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
