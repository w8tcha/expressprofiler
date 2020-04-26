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

namespace ExpressProfiler
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows.Forms;

    using ExpressProfiler.Objects;
    using ExpressProfiler.Utils;

    /// <summary>
    /// The recent connections form.
    /// </summary>
    public partial class RecentConnectionsForm : Form
    {
        private readonly MainForm _mainForm;

        private RecentConnection _recentConnection;

        public RecentConnectionsForm(MainForm form)
            : this()
        {
            this._mainForm = form;
            this.LoadConnections();
        }

        private RecentConnectionsForm()
        {
            this.InitializeComponent();
            this.ConnectEvents();
        }

        public bool ConnectionSelected { get; private set; }

        private void ConnectEvents()
        {
            this.txtSearch.KeyUp += this.TxtSearch_KeyUp;
            this.dgvConnections.DoubleClick += this.DgvConnections_DoubleClick;
        }

        private void ConvertUTCDateToLocalTime()
        {
            this._recentConnection?.Connections?.ForEach(
                c => c.CreationDate = string.IsNullOrEmpty(c.CreationDate)
                    ? string.Empty
                    : DateTime.Parse(c.CreationDate).ToLocalTime().ToString(CultureInfo.InvariantCulture));
        }

        private void DgvConnections_DoubleClick(object sender, EventArgs e)
        {
            if (!(sender is DataGridView dataGridView))
            {
                return;
            }

            if (dataGridView.CurrentRow.DataBoundItem is Connection currentRow)
            {
                this._mainForm.recent_servername = currentRow.DataSource;
                this._mainForm.recent__username = string.IsNullOrEmpty(currentRow.IntegratedSecurity)
                    ? currentRow.UserId
                    : string.Empty;
                this._mainForm.recent_userpassword = string.IsNullOrEmpty(currentRow.IntegratedSecurity)
                    ? Cryptography.Decrypt(currentRow.Password)
                    : string.Empty;
                this._mainForm.recent_auth = string.IsNullOrEmpty(currentRow.IntegratedSecurity) ? 1 : 0;
                this.ConnectionSelected = true;
                this.Close();
            }
            else
            {
                this.ConnectionSelected = false;
            }
        }

        private void LoadConnections()
        {
            this._recentConnection = this._mainForm.ReadRecentConnections();
            this.ConvertUTCDateToLocalTime();
            this.connectionBindingSource.DataSource = this._recentConnection?.Connections;
        }

        private List<Connection> SearchConnection(string searchTerm)
        {
            var items = new List<Connection>();

            if (this._recentConnection?.Connections == null)
            {
                return items;
            }

            foreach (var iter in this._recentConnection.Connections)
            {
                if (iter.DataSource.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1 ||
                    iter.UserId.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) != -1)
                {
                    items.Add(iter);
                }
            }

            return items;
        }

        /// <summary>
        /// The txt search_ key up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || this.txtSearch.Text.Length > 3)
            {
                if (string.IsNullOrEmpty(this.txtSearch.Text.Trim()))
                {
                    return;
                }

                this.connectionBindingSource.DataSource = this.SearchConnection(this.txtSearch.Text);
            }

            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && this.txtSearch.Text.Length == 0)
            {
                this.LoadConnections();
            }
        }
    }
}