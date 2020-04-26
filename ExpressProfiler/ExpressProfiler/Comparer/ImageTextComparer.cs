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
    /// The image text comparer.
    /// </summary>
    public class ImageTextComparer : IComparer
    {
        // private CaseInsensitiveComparer ObjectCompare;
        private NumberCaseInsensitiveComparer ObjectCompare;

        public ImageTextComparer()
        {
            // Initialize the CaseInsensitiveComparer object
            this.ObjectCompare = new NumberCaseInsensitiveComparer();
        }

        public int Compare(object x, object y)
        {
            // int compareResult;

            // Cast the objects to be compared to ListViewItem objects
            var listviewX = (ListViewItem)x;
            var image1 = listviewX.ImageIndex;
            var listviewY = (ListViewItem)y;
            var image2 = listviewY.ImageIndex;
            
            if (image1 < image2)
            {
                return -1;
            }

            return image1 == image2 ? this.ObjectCompare.Compare(listviewX.Text, listviewY.Text) : 1;
        }
    }
}