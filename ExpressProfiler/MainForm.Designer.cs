namespace ExpressProfiler
{
    using System.ComponentModel;
    using System.Windows.Forms;

    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.slEPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbScroll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbStart = new System.Windows.Forms.ToolStripSplitButton();
            this.tbRun = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRunWithFilters = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPause = new System.Windows.Forms.ToolStripButton();
            this.tbStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tbFilterEvents = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.edServer = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbAuth = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.edUser = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.edPassword = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbStayOnTop = new System.Windows.Forms.ToolStripButton();
            this.tbTransparent = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.reTextData = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyAllToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.copyToXlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnRun = new System.Windows.Forms.ToolStripMenuItem();
            this.mnRunWithFilters = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopTraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.extractAllEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractSelectedEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllForExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.saveAllEventsToExcelXmlFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findNextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.clearTraceWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.keepSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterCapturedEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearCapturedFiltersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slEPS});
            this.statusStrip1.Location = new System.Drawing.Point(0, 763);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1468, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // slEPS
            // 
            this.slEPS.Name = "slEPS";
            this.slEPS.Size = new System.Drawing.Size(0, 15);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbClear,
            this.toolStripSeparator5,
            this.tbScroll,
            this.toolStripSeparator1,
            this.tbStart,
            this.tbPause,
            this.tbStop,
            this.toolStripSeparator7,
            this.tbFilterEvents,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.edServer,
            this.toolStripSeparator4,
            this.tbAuth,
            this.toolStripLabel2,
            this.edUser,
            this.toolStripLabel3,
            this.edPassword,
            this.toolStripSeparator3,
            this.tbStayOnTop,
            this.tbTransparent});
            this.toolStrip1.Location = new System.Drawing.Point(0, 33);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1468, 33);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbClear
            // 
            this.tbClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbClear.Image = global::ExpressProfiler.Properties.Resources.imClear;
            this.tbClear.ImageTransparentColor = System.Drawing.Color.Silver;
            this.tbClear.Name = "tbClear";
            this.tbClear.Size = new System.Drawing.Size(34, 28);
            this.tbClear.Text = "Clear trace";
            this.tbClear.ToolTipText = "Clear trace\r\nCtrl+Shift+Del";
            this.tbClear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 33);
            // 
            // tbScroll
            // 
            this.tbScroll.Checked = true;
            this.tbScroll.CheckOnClick = true;
            this.tbScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tbScroll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbScroll.Image = global::ExpressProfiler.Properties.Resources.imScroll;
            this.tbScroll.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbScroll.Name = "tbScroll";
            this.tbScroll.Size = new System.Drawing.Size(34, 28);
            this.tbScroll.Text = "Auto scroll window";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 33);
            // 
            // tbStart
            // 
            this.tbStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStart.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbRun,
            this.tbRunWithFilters});
            this.tbStart.Image = global::ExpressProfiler.Properties.Resources.imStart;
            this.tbStart.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbStart.Name = "tbStart";
            this.tbStart.Size = new System.Drawing.Size(45, 28);
            this.tbStart.Text = "Start trace";
            // 
            // tbRun
            // 
            this.tbRun.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.tbRun.Name = "tbRun";
            this.tbRun.Size = new System.Drawing.Size(270, 34);
            this.tbRun.Text = "Run";
            this.tbRun.Click += new System.EventHandler(this.Start_Click);
            // 
            // tbRunWithFilters
            // 
            this.tbRunWithFilters.Name = "tbRunWithFilters";
            this.tbRunWithFilters.Size = new System.Drawing.Size(270, 34);
            this.tbRunWithFilters.Text = "Run with filters";
            this.tbRunWithFilters.Click += new System.EventHandler(this.RunWithFilters_Click);
            // 
            // tbPause
            // 
            this.tbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPause.Image = global::ExpressProfiler.Properties.Resources.imPause;
            this.tbPause.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbPause.Name = "tbPause";
            this.tbPause.Size = new System.Drawing.Size(34, 28);
            this.tbPause.Text = "Pause trace";
            this.tbPause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // tbStop
            // 
            this.tbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStop.Image = global::ExpressProfiler.Properties.Resources.imStop;
            this.tbStop.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbStop.Name = "tbStop";
            this.tbStop.Size = new System.Drawing.Size(34, 28);
            this.tbStop.Text = "Stop trace";
            this.tbStop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 33);
            // 
            // tbFilterEvents
            // 
            this.tbFilterEvents.CheckOnClick = true;
            this.tbFilterEvents.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbFilterEvents.Image = global::ExpressProfiler.Properties.Resources.filter;
            this.tbFilterEvents.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tbFilterEvents.Name = "tbFilterEvents";
            this.tbFilterEvents.Size = new System.Drawing.Size(34, 28);
            this.tbFilterEvents.Text = "Filter Captured Events ";
            this.tbFilterEvents.Click += new System.EventHandler(this.FilterEvents_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 33);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(61, 28);
            this.toolStripLabel1.Text = "Server";
            // 
            // edServer
            // 
            this.edServer.Name = "edServer";
            this.edServer.Size = new System.Drawing.Size(148, 33);
            this.edServer.TextChanged += new System.EventHandler(this.Server_TextChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 33);
            // 
            // tbAuth
            // 
            this.tbAuth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tbAuth.Items.AddRange(new object[] {
            "Windows auth",
            "SQL Server auth"});
            this.tbAuth.Name = "tbAuth";
            this.tbAuth.Size = new System.Drawing.Size(180, 33);
            this.tbAuth.SelectedIndexChanged += new System.EventHandler(this.Auth_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(47, 28);
            this.toolStripLabel2.Text = "User";
            // 
            // edUser
            // 
            this.edUser.Name = "edUser";
            this.edUser.Size = new System.Drawing.Size(148, 33);
            this.edUser.TextChanged += new System.EventHandler(this.User_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(87, 28);
            this.toolStripLabel3.Text = "Password";
            // 
            // edPassword
            // 
            this.edPassword.Name = "edPassword";
            this.edPassword.Size = new System.Drawing.Size(148, 33);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 33);
            // 
            // tbStayOnTop
            // 
            this.tbStayOnTop.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbStayOnTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStayOnTop.Image = ((System.Drawing.Image)(resources.GetObject("tbStayOnTop.Image")));
            this.tbStayOnTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbStayOnTop.Name = "tbStayOnTop";
            this.tbStayOnTop.Size = new System.Drawing.Size(34, 28);
            this.tbStayOnTop.Text = "Stay on top";
            this.tbStayOnTop.Click += new System.EventHandler(this.StayOnTop_Click);
            // 
            // tbTransparent
            // 
            this.tbTransparent.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbTransparent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbTransparent.Image = ((System.Drawing.Image)(resources.GetObject("tbTransparent.Image")));
            this.tbTransparent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbTransparent.Name = "tbTransparent";
            this.tbTransparent.Size = new System.Drawing.Size(34, 28);
            this.tbTransparent.Text = "Transparent";
            this.tbTransparent.Click += new System.EventHandler(this.ToolStripButton1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 66);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reTextData);
            this.splitContainer1.Size = new System.Drawing.Size(1468, 697);
            this.splitContainer1.SplitterDistance = 445;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 4;
            // 
            // reTextData
            // 
            this.reTextData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reTextData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reTextData.Location = new System.Drawing.Point(0, 0);
            this.reTextData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.reTextData.Name = "reTextData";
            this.reTextData.ReadOnly = true;
            this.reTextData.Size = new System.Drawing.Size(1468, 246);
            this.reTextData.TabIndex = 4;
            this.reTextData.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAllToClipboardToolStripMenuItem,
            this.copySelectedToClipboardToolStripMenuItem,
            this.toolStripMenuItem1,
            this.copyToXlsToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(354, 106);
            // 
            // copyAllToClipboardToolStripMenuItem
            // 
            this.copyAllToClipboardToolStripMenuItem.Name = "copyAllToClipboardToolStripMenuItem";
            this.copyAllToClipboardToolStripMenuItem.Size = new System.Drawing.Size(353, 32);
            this.copyAllToClipboardToolStripMenuItem.Text = "Copy all events to clipboard";
            this.copyAllToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyAllToClipboardToolStripMenuItem_Click);
            // 
            // copySelectedToClipboardToolStripMenuItem
            // 
            this.copySelectedToClipboardToolStripMenuItem.Name = "copySelectedToClipboardToolStripMenuItem";
            this.copySelectedToClipboardToolStripMenuItem.Size = new System.Drawing.Size(353, 32);
            this.copySelectedToClipboardToolStripMenuItem.Text = "Copy selected events to clipboard";
            this.copySelectedToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopySelectedToClipboardToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(350, 6);
            // 
            // copyToXlsToolStripMenuItem
            // 
            this.copyToXlsToolStripMenuItem.Name = "copyToXlsToolStripMenuItem";
            this.copyToXlsToolStripMenuItem.Size = new System.Drawing.Size(353, 32);
            this.copyToXlsToolStripMenuItem.Text = "Copy all for Excel";
            this.copyToXlsToolStripMenuItem.Click += new System.EventHandler(this.CopyToXlsToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.mnAbout,
            this.viewToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.recentToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1468, 33);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startTraceToolStripMenuItem,
            this.pauseTraceToolStripMenuItem,
            this.stopTraceToolStripMenuItem,
            this.toolStripMenuItem3,
            this.extractAllEventsToolStripMenuItem,
            this.extractSelectedEventsToolStripMenuItem,
            this.copyAllForExcelToolStripMenuItem,
            this.toolStripSeparator6,
            this.saveAllEventsToExcelXmlFileToolStripMenuItem,
            this.toolStripMenuItem5,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // startTraceToolStripMenuItem
            // 
            this.startTraceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnRun,
            this.mnRunWithFilters});
            this.startTraceToolStripMenuItem.Image = global::ExpressProfiler.Properties.Resources.imStart;
            this.startTraceToolStripMenuItem.Name = "startTraceToolStripMenuItem";
            this.startTraceToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.startTraceToolStripMenuItem.Text = "&Start trace";
            // 
            // mnRun
            // 
            this.mnRun.Name = "mnRun";
            this.mnRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.mnRun.Size = new System.Drawing.Size(231, 34);
            this.mnRun.Text = "Run";
            this.mnRun.Click += new System.EventHandler(this.Start_Click);
            // 
            // mnRunWithFilters
            // 
            this.mnRunWithFilters.Name = "mnRunWithFilters";
            this.mnRunWithFilters.Size = new System.Drawing.Size(231, 34);
            this.mnRunWithFilters.Text = "Run with filters";
            this.mnRunWithFilters.Click += new System.EventHandler(this.RunWithFilters_Click);
            // 
            // pauseTraceToolStripMenuItem
            // 
            this.pauseTraceToolStripMenuItem.Image = global::ExpressProfiler.Properties.Resources.imPause;
            this.pauseTraceToolStripMenuItem.Name = "pauseTraceToolStripMenuItem";
            this.pauseTraceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.pauseTraceToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.pauseTraceToolStripMenuItem.Text = "&Pause trace";
            this.pauseTraceToolStripMenuItem.Click += new System.EventHandler(this.PauseTraceToolStripMenuItem_Click);
            // 
            // stopTraceToolStripMenuItem
            // 
            this.stopTraceToolStripMenuItem.Image = global::ExpressProfiler.Properties.Resources.imStop;
            this.stopTraceToolStripMenuItem.Name = "stopTraceToolStripMenuItem";
            this.stopTraceToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.stopTraceToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.stopTraceToolStripMenuItem.Text = "S&top trace";
            this.stopTraceToolStripMenuItem.Click += new System.EventHandler(this.StopTraceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(488, 6);
            // 
            // extractAllEventsToolStripMenuItem
            // 
            this.extractAllEventsToolStripMenuItem.Name = "extractAllEventsToolStripMenuItem";
            this.extractAllEventsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Insert)));
            this.extractAllEventsToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.extractAllEventsToolStripMenuItem.Text = "Copy all events to clipboard";
            this.extractAllEventsToolStripMenuItem.Click += new System.EventHandler(this.ExtractAllEventsToolStripMenuItem_Click);
            // 
            // extractSelectedEventsToolStripMenuItem
            // 
            this.extractSelectedEventsToolStripMenuItem.Name = "extractSelectedEventsToolStripMenuItem";
            this.extractSelectedEventsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.Insert)));
            this.extractSelectedEventsToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.extractSelectedEventsToolStripMenuItem.Text = "Copy selected events to clipboard";
            this.extractSelectedEventsToolStripMenuItem.Click += new System.EventHandler(this.ExtractSelectedEventsToolStripMenuItem_Click);
            // 
            // copyAllForExcelToolStripMenuItem
            // 
            this.copyAllForExcelToolStripMenuItem.Name = "copyAllForExcelToolStripMenuItem";
            this.copyAllForExcelToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.copyAllForExcelToolStripMenuItem.Text = "Copy all for Excel";
            this.copyAllForExcelToolStripMenuItem.Click += new System.EventHandler(this.CopyToXlsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(488, 6);
            // 
            // saveAllEventsToExcelXmlFileToolStripMenuItem
            // 
            this.saveAllEventsToExcelXmlFileToolStripMenuItem.Name = "saveAllEventsToExcelXmlFileToolStripMenuItem";
            this.saveAllEventsToExcelXmlFileToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.saveAllEventsToExcelXmlFileToolStripMenuItem.Text = "Save all events to Excel Xml File";
            this.saveAllEventsToExcelXmlFileToolStripMenuItem.Click += new System.EventHandler(this.SaveAllEventsToExcelXmlFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(488, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem,
            this.findToolStripMenuItem,
            this.findNextToolStripMenuItem,
            this.toolStripMenuItem4,
            this.clearTraceWindowToolStripMenuItem,
            this.deleteSelectedToolStripMenuItem,
            this.keepSelectedToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(58, 29);
            this.editToolStripMenuItem.Text = "&Edit";
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(393, 34);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.SelectAllToolStripMenuItem_Click);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.findToolStripMenuItem.Size = new System.Drawing.Size(393, 34);
            this.findToolStripMenuItem.Text = "Find...";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.FindToolStripMenuItem_Click);
            // 
            // findNextToolStripMenuItem
            // 
            this.findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            this.findNextToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.findNextToolStripMenuItem.Size = new System.Drawing.Size(393, 34);
            this.findNextToolStripMenuItem.Text = "Find next";
            this.findNextToolStripMenuItem.Click += new System.EventHandler(this.FindNextToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(390, 6);
            // 
            // clearTraceWindowToolStripMenuItem
            // 
            this.clearTraceWindowToolStripMenuItem.Image = global::ExpressProfiler.Properties.Resources.imClear;
            this.clearTraceWindowToolStripMenuItem.Name = "clearTraceWindowToolStripMenuItem";
            this.clearTraceWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Delete)));
            this.clearTraceWindowToolStripMenuItem.Size = new System.Drawing.Size(393, 34);
            this.clearTraceWindowToolStripMenuItem.Text = "Clear Trace Window";
            this.clearTraceWindowToolStripMenuItem.Click += new System.EventHandler(this.ClearTraceWindowToolStripMenuItem_Click);
            // 
            // deleteSelectedToolStripMenuItem
            // 
            this.deleteSelectedToolStripMenuItem.Name = "deleteSelectedToolStripMenuItem";
            this.deleteSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.deleteSelectedToolStripMenuItem.Size = new System.Drawing.Size(393, 34);
            this.deleteSelectedToolStripMenuItem.Text = "Delete selected";
            this.deleteSelectedToolStripMenuItem.Click += new System.EventHandler(this.DeleteSelectedToolStripMenuItem_Click);
            // 
            // keepSelectedToolStripMenuItem
            // 
            this.keepSelectedToolStripMenuItem.Name = "keepSelectedToolStripMenuItem";
            this.keepSelectedToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
            this.keepSelectedToolStripMenuItem.Size = new System.Drawing.Size(393, 34);
            this.keepSelectedToolStripMenuItem.Text = "Keep selected";
            this.keepSelectedToolStripMenuItem.Click += new System.EventHandler(this.KeepSelectedToolStripMenuItem_Click);
            // 
            // mnAbout
            // 
            this.mnAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnAbout.Name = "mnAbout";
            this.mnAbout.Size = new System.Drawing.Size(78, 29);
            this.mnAbout.Text = "About";
            this.mnAbout.Click += new System.EventHandler(this.About_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stayOnTopToolStripMenuItem,
            this.transparentToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(65, 29);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // stayOnTopToolStripMenuItem
            // 
            this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
            this.stayOnTopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.stayOnTopToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.stayOnTopToolStripMenuItem.Text = "Stay on top";
            this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.StayOnTopToolStripMenuItem_Click);
            // 
            // transparentToolStripMenuItem
            // 
            this.transparentToolStripMenuItem.Name = "transparentToolStripMenuItem";
            this.transparentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.transparentToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.transparentToolStripMenuItem.Text = "Transparent";
            this.transparentToolStripMenuItem.Click += new System.EventHandler(this.TransparentToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterCapturedEventsToolStripMenuItem,
            this.clearCapturedFiltersToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(66, 29);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // filterCapturedEventsToolStripMenuItem
            // 
            this.filterCapturedEventsToolStripMenuItem.Name = "filterCapturedEventsToolStripMenuItem";
            this.filterCapturedEventsToolStripMenuItem.Size = new System.Drawing.Size(286, 34);
            this.filterCapturedEventsToolStripMenuItem.Text = "Filter Captured Events";
            this.filterCapturedEventsToolStripMenuItem.Click += new System.EventHandler(this.FilterCapturedEventsToolStripMenuItem_Click);
            // 
            // clearCapturedFiltersToolStripMenuItem
            // 
            this.clearCapturedFiltersToolStripMenuItem.Name = "clearCapturedFiltersToolStripMenuItem";
            this.clearCapturedFiltersToolStripMenuItem.Size = new System.Drawing.Size(286, 34);
            this.clearCapturedFiltersToolStripMenuItem.Text = "Clear Captured Filters";
            this.clearCapturedFiltersToolStripMenuItem.Click += new System.EventHandler(this.ClearCapturedFiltersToolStripMenuItem_Click);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectConnectionToolStripMenuItem});
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(80, 29);
            this.recentToolStripMenuItem.Text = "Recent";
            // 
            // selectConnectionToolStripMenuItem
            // 
            this.selectConnectionToolStripMenuItem.Name = "selectConnectionToolStripMenuItem";
            this.selectConnectionToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.selectConnectionToolStripMenuItem.Text = "Select Connection";
            this.selectConnectionToolStripMenuItem.Click += new System.EventHandler(this.SelectConnectionToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 785);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Express Profiler v1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolStripButton tbStop;
        private SplitContainer splitContainer1;
        private RichTextBox reTextData;
        private ToolStripButton tbScroll;
        private ToolStripButton tbPause;
        private ToolStripSeparator toolStripSeparator2;
        private Timer timer1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox edServer;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox edUser;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox edPassword;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripComboBox tbAuth;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton tbClear;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem copyAllToClipboardToolStripMenuItem;
        private ToolStripMenuItem copySelectedToClipboardToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem startTraceToolStripMenuItem;
        private ToolStripMenuItem pauseTraceToolStripMenuItem;
        private ToolStripMenuItem stopTraceToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem clearTraceWindowToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem extractAllEventsToolStripMenuItem;
        private ToolStripMenuItem extractSelectedEventsToolStripMenuItem;
        private ToolStripMenuItem findToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem findNextToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem5;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripSplitButton tbStart;
        private ToolStripMenuItem tbRun;
        private ToolStripMenuItem tbRunWithFilters;
        private ToolStripMenuItem mnRun;
        private ToolStripMenuItem mnRunWithFilters;
        private ToolStripStatusLabel slEPS;
        private ToolStripMenuItem copyToXlsToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnAbout;
        private ToolStripMenuItem copyAllForExcelToolStripMenuItem;
        private ToolStripButton tbStayOnTop;
        private ToolStripButton tbTransparent;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem stayOnTopToolStripMenuItem;
        private ToolStripMenuItem transparentToolStripMenuItem;
        private ToolStripMenuItem deleteSelectedToolStripMenuItem;
        private ToolStripMenuItem keepSelectedToolStripMenuItem;
		private ToolStripMenuItem filterToolStripMenuItem;
		private ToolStripMenuItem filterCapturedEventsToolStripMenuItem;
		private ToolStripMenuItem clearCapturedFiltersToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator6;
		private ToolStripMenuItem saveAllEventsToExcelXmlFileToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator7;
		private ToolStripButton tbFilterEvents;
        private ToolStripMenuItem recentToolStripMenuItem;
        private ToolStripMenuItem selectConnectionToolStripMenuItem;
    }
}

