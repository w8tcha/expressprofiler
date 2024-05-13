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

/// <summary>
/// The recent connections form.
/// </summary>
public partial class RecentConnectionsForm : Form
{
    /// <summary>
    /// The main form.
    /// </summary>
    private readonly MainForm mainForm;

    /// <summary>
    /// The recent connection.
    /// </summary>
    private RecentConnection recentConnection;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecentConnectionsForm"/> class.
    /// </summary>
    /// <param name="form">
    /// The form.
    /// </param>
    public RecentConnectionsForm(MainForm form)
        : this()
    {
        this.mainForm = form;
        this.LoadConnections();
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="RecentConnectionsForm"/> class from being created.
    /// </summary>
    private RecentConnectionsForm()
    {
        this.InitializeComponent();
        this.ConnectEvents();
    }

    /// <summary>
    /// Gets a value indicating whether connection selected.
    /// </summary>
    public bool ConnectionSelected { get; private set; }

    /// <summary>
    /// The connect events.
    /// </summary>
    private void ConnectEvents()
    {
        this.txtSearch.KeyUp += this.TxtSearch_KeyUp;
        this.dgvConnections.DoubleClick += this.DgvConnections_DoubleClick;
    }

    /// <summary>
    /// The convert UTC date to local time.
    /// </summary>
    private void ConvertUTCDateToLocalTime()
    {
        this.recentConnection?.Connections?.ForEach(
            c => c.CreationDate = string.IsNullOrEmpty(c.CreationDate)
                                      ? string.Empty
                                      : DateTime.Parse(c.CreationDate).ToLocalTime().ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// The dgv connections_ double click.
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void DgvConnections_DoubleClick(object sender, EventArgs e)
    {
        if (sender is not DataGridView dataGridView)
        {
            return;
        }

        if (dataGridView.CurrentRow.DataBoundItem is Connection currentRow)
        {
            this.mainForm.recentServerName = currentRow.DataSource;
            this.mainForm.recentUserName = string.IsNullOrEmpty(currentRow.IntegratedSecurity)
                                               ? currentRow.UserId
                                               : string.Empty;
            this.mainForm.recentUserPassword = string.IsNullOrEmpty(currentRow.IntegratedSecurity)
                                                   ? Cryptography.Decrypt(currentRow.Password)
                                                   : string.Empty;
            this.mainForm.recent_auth = string.IsNullOrEmpty(currentRow.IntegratedSecurity) ? 1 : 0;
            this.ConnectionSelected = true;
            this.Close();
        }
        else
        {
            this.ConnectionSelected = false;
        }
    }

    /// <summary>
    /// The load connections.
    /// </summary>
    private void LoadConnections()
    {
        this.recentConnection = this.mainForm.ReadRecentConnections();
        this.ConvertUTCDateToLocalTime();
        this.connectionBindingSource.DataSource = this.recentConnection?.Connections;
    }

    /// <summary>
    /// The search connection.
    /// </summary>
    /// <param name="searchTerm">
    /// The search term.
    /// </param>
    /// <returns>
    /// The <see cref="List"/>.
    /// </returns>
    private List<Connection> SearchConnection(string searchTerm)
    {
        var items = new List<Connection>();

        if (this.recentConnection?.Connections == null)
        {
            return items;
        }

        this.recentConnection.Connections.ForEach(connection =>
            {
                if (connection.DataSource.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    connection.UserId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                {
                    items.Add(connection);
                }
            });

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

        if (e.KeyCode is Keys.Back or Keys.Delete && this.txtSearch.Text.Length == 0)
        {
            this.LoadConnections();
        }
    }
}