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
/// The find form.
/// </summary>
public partial class FindForm : Form
{
    /// <summary>
    /// The MainForm.
    /// </summary>
    private readonly MainForm mainForm;

    /// <summary>
    /// Initializes a new instance of the <see cref="FindForm"/> class.
    /// </summary>
    /// <param name="f">
    /// The f.
    /// </param>
    public FindForm(MainForm f)
    {
        this.InitializeComponent();

        this.mainForm = f;

        // Set the control values to the last find performed.
        this.edPattern.Text = this.mainForm.LastPattern;
        this.chkCase.Checked = this.mainForm.matchCase;
        this.chkWholeWord.Checked = this.mainForm.wholeWord;
    }

    /// <summary>
    /// Find Next
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void btnFindNext_Click(object sender, EventArgs e)
    {
        this.DoFind(true);
    }

    /// <summary>
    /// Find Previous
    /// </summary>
    /// <param name="sender">
    /// The sender.
    /// </param>
    /// <param name="e">
    /// The e.
    /// </param>
    private void btnFindPrevious_Click(object sender, EventArgs e)
    {
        this.DoFind(false);
    }

    /// <summary>
    /// The do find.
    /// </summary>
    /// <param name="forwards">
    /// The forwards.
    /// </param>
    private void DoFind(bool forwards)
    {
        this.mainForm.LastPattern = this.edPattern.Text;
        this.mainForm.matchCase = this.chkCase.Checked;
        this.mainForm.wholeWord = this.chkWholeWord.Checked;
        this.mainForm.PerformFind(forwards, this.chkWrapAround.Checked);
    }
}