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

namespace ExpressProfiler.Comparer
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class TextDataComparer : IComparer<ListViewItem>
    {
        private int CheckedColumn { get; set; }

        private SortOrder SortOrder { get; set; }

        public TextDataComparer(int checkedColumn, SortOrder sortOrder)
        {
            this.CheckedColumn = checkedColumn;
            this.SortOrder = sortOrder;
        }

        public int Compare(ListViewItem x, ListViewItem y)
        {
            return this.SortOrder == SortOrder.Descending ? this.CompareDescending(x, y) : this.CompareAscending(x, y);
        }

        private int CompareAscending(ListViewItem x, ListViewItem y)
        {
            switch (x.SubItems[this.CheckedColumn])
            {
                case null when y.SubItems[this.CheckedColumn] == null:
                    return 0;
                case null:
                    return -1;
                default:
                    {
                        if (y.SubItems[this.CheckedColumn] == null)
                        {
                            return 1;
                        }

                        var xIsInt = int.TryParse(
                            x.SubItems[this.CheckedColumn].Text.Replace(",", string.Empty),
                            out var xAsInt);

                        var yIsInt = int.TryParse(
                            y.SubItems[this.CheckedColumn].Text.Replace(",", string.Empty),
                            out var yAsInt);

                        if (!xIsInt || !yIsInt)
                        {
                            return string.CompareOrdinal(
                                x.SubItems[this.CheckedColumn].Text,
                                y.SubItems[this.CheckedColumn].Text);
                        }

                        if (xAsInt < yAsInt)
                        {
                            return -1;
                        }

                        return xAsInt > yAsInt ? 1 : 0;
                    }
            }
        }

        private int CompareDescending(ListViewItem x, ListViewItem y)
        {
            switch (x.SubItems[this.CheckedColumn])
            {
                case null when y.SubItems[this.CheckedColumn] == null:
                    return 0;
                case null:
                    return 1;
                default:
                    {
                        if (y.SubItems[this.CheckedColumn] == null)
                        {
                            return -1;
                        }

                        var xIsInt = int.TryParse(
                            x.SubItems[this.CheckedColumn].Text.Replace(",", string.Empty),
                            out var xAsInt);

                        var yIsInt = int.TryParse(
                            y.SubItems[this.CheckedColumn].Text.Replace(",", string.Empty),
                            out var yAsInt);

                        if (!xIsInt || !yIsInt)
                        {
                            return string.CompareOrdinal(
                                y.SubItems[this.CheckedColumn].Text,
                                x.SubItems[this.CheckedColumn].Text);

                        }

                        if (xAsInt > yAsInt)
                        {
                            return -1;
                        }

                        return xAsInt < yAsInt ? 1 : 0;

                    }
            }
        }
    }
}