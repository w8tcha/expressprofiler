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
    using System.Collections;
    using System.Windows.Forms;

    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>  
        private readonly NumberCaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// The first object compare.
        /// </summary>
        private readonly ImageTextComparer FirstObjectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            this.SortColumn = 0;

            // Initialize the sort order to 'none'
            // OrderOfSort = SortOrder.None;
            this.Order = SortOrder.Ascending;

            // Initialize my implementation of CaseInsensitiveComparer object
            this.ObjectCompare = new NumberCaseInsensitiveComparer();
            this.FirstObjectCompare = new ImageTextComparer();
        }

        /// <summary>
        /// This method is inherited from the IComparer interface.
        /// It compares the two objects passed\
        /// using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal,
        /// negative if 'x' is less than 'y' and positive
        /// if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult;

            // Cast the objects to be compared to ListViewItem objects
            var listviewX = (ListViewItem)x;
            var listviewY = (ListViewItem)y;
            if (this.SortColumn == 0)
            {
                compareResult = this.FirstObjectCompare.Compare(x, y);
            }
            else
            {
                // Compare the two items
                compareResult = this.ObjectCompare.Compare(
                    listviewX.SubItems[this.SortColumn].Text,
                    listviewY.SubItems[this.SortColumn].Text);
            }

            return this.Order switch
            {
                // Calculate correct return value based on object comparison
                SortOrder.Ascending =>

                // Ascending sort is selected,
                // return normal result of compare operation
                compareResult,
                SortOrder.Descending =>

                // Descending sort is selected,
                // return negative result of compare operation
                -compareResult,
                _ => 0
            };
        }

        /// <summary>
        /// Gets or sets the number of the column to which
        /// to apply the sorting operation (Defaults to '0').
        /// </summary>
        private int SortColumn { get; }

        /// <summary>
        /// Gets or sets the order of sorting to apply
        /// (for example, 'Ascending' or 'Descending').
        /// </summary>
        private SortOrder Order { get; }
    }
}