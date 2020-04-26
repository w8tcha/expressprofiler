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
    using System.Windows.Forms;

    /// <summary>
    /// The find form.
    /// </summary>
    public partial class FindForm : Form
    {
        private readonly MainForm m_mainForm;

        public FindForm(MainForm f)
        {
            this.InitializeComponent();

            this.m_mainForm = f;

            // Set the control values to the last find performed.
            this.edPattern.Text = this.m_mainForm.lastpattern;
            this.chkCase.Checked = this.m_mainForm.matchCase;
            this.chkWholeWord.Checked = this.m_mainForm.wholeWord;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            this.DoFind(true);
        }

        private void btnFindPrevious_Click(object sender, EventArgs e)
        {
            this.DoFind(false);
        }

        private void DoFind(bool forwards)
        {
            this.m_mainForm.lastpattern = this.edPattern.Text;
            this.m_mainForm.matchCase = this.chkCase.Checked;
            this.m_mainForm.wholeWord = this.chkWholeWord.Checked;
            this.m_mainForm.PerformFind(forwards, this.chkWrapAround.Checked);
        }

        private void edPattern_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
