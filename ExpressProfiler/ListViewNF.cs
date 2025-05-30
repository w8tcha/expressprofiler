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

/// <summary>
/// The list view.
/// </summary>
public class ListViewNF : ListView
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListViewNF"/> class.
    /// </summary>
    public ListViewNF()
    {
        // Activate double buffering
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

        // Enable the OnNotifyMessage event so we get a chance to filter out
        // Windows messages before they get to the form's WndProc
        this.SetStyle(ControlStyles.EnableNotifyMessage, true);

        this.SortOrder = SortOrder.Ascending;
    }

    /// <summary>
    /// Gets the sort order.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public SortOrder SortOrder { get; private set; }

    /// <summary>
    /// The toggle sort order.
    /// </summary>
    public void ToggleSortOrder()
    {
        if (this.SortOrder == SortOrder.Ascending)
        {
            this.SortOrder = SortOrder.Descending;
            return;
        }

        this.SortOrder = SortOrder.Ascending;
    }

    /// <summary>
    /// The on notify message.
    /// </summary>
    /// <param name="m">
    /// The m.
    /// </param>
    protected override void OnNotifyMessage(Message m)
    {
        // Filter out the WM_ERASEBKGND message
        if (m.Msg != 0x14)
        {
            base.OnNotifyMessage(m);
        }
    }
}