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

namespace ExpressProfiler.Comparer;

/// <summary>
/// The image text comparer.
/// </summary>
public class ImageTextComparer : IComparer
{
    /// <summary>
    /// The object compare.
    /// </summary>
    private readonly NumberCaseInsensitiveComparer objectCompare;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageTextComparer"/> class.
    /// </summary>
    public ImageTextComparer()
    {
        // Initialize the CaseInsensitiveComparer object
        this.objectCompare = new NumberCaseInsensitiveComparer();
    }

    /// <summary>
    /// The compare.
    /// </summary>
    /// <param name="x">
    /// The x.
    /// </param>
    /// <param name="y">
    /// The y.
    /// </param>
    /// <returns>
    /// The <see cref="int"/>.
    /// </returns>
    public int Compare(object x, object y)
    {
        // int compareResult;

        // Cast the objects to be compared to ListViewItem objects
        var listViewX = (ListViewItem)x;
        var image1 = listViewX.ImageIndex;
        var listViewY = (ListViewItem)y;
        var image2 = listViewY.ImageIndex;
            
        if (image1 < image2)
        {
            return -1;
        }

        return image1 == image2 ? this.objectCompare.Compare(listViewX.Text, listViewY.Text) : 1;
    }
}