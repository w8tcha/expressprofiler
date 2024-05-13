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

namespace ExpressProfiler.Extensions;

using System.Runtime.InteropServices;

/// <summary>
/// Taken From http://stackoverflow.com/questions/254129/how-to-i-display-a-sort-arrow-in-the-header-of-a-list-view-column-using-c
/// Then, you can call the extension method like such:
/// myListView.SetSortIcon(0, SortOrder.Ascending);
/// The list view extensions.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ListViewExtensions
{
    /// <summary>
    /// The lv m_ first.
    /// </summary>
    private const int LVM_FIRST = 0x1000;

    /// <summary>
    /// The lv m_ getheader.
    /// </summary>
    private const int LVM_GETHEADER = LVM_FIRST + 31;

    /// <summary>
    /// The hd m_ first.
    /// </summary>
    private const int HDM_FIRST = 0x1200;

    /// <summary>
    /// The hd m_ getitem.
    /// </summary>
    private const int HDM_GETITEM = HDM_FIRST + 11;

    /// <summary>
    /// The hd m_ setitem.
    /// </summary>
    private const int HDM_SETITEM = HDM_FIRST + 12;

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, ref HDITEM lParam);

    /// <summary>
    /// The set sort icon.
    /// </summary>
    /// <param name="listViewControl">
    /// The list view control.
    /// </param>
    /// <param name="columnIndex">
    /// The column index.
    /// </param>
    /// <param name="order">
    /// The order.
    /// </param>
    /// <exception cref="Win32Exception">
    /// </exception>
    public static void SetSortIcon(this ListView listViewControl, int columnIndex, SortOrder order)
    {
        var columnHeader = SendMessage(listViewControl.Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero);
        for (var columnNumber = 0; columnNumber <= listViewControl.Columns.Count - 1; columnNumber++)
        {
            var columnPtr = new IntPtr(columnNumber);
            var item = new HDITEM { mask = HDITEM.Mask.Format };

            if (SendMessage(columnHeader, HDM_GETITEM, columnPtr, ref item) == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            if (order != SortOrder.None && columnNumber == columnIndex)
            {
                switch (order)
                {
                    case SortOrder.Ascending:
                        item.fmt &= ~HDITEM.Format.SortDown;
                        item.fmt |= HDITEM.Format.SortUp;
                        break;
                    case SortOrder.Descending:
                        item.fmt &= ~HDITEM.Format.SortUp;
                        item.fmt |= HDITEM.Format.SortDown;
                        break;
                }
            }
            else
            {
                item.fmt &= ~HDITEM.Format.SortDown & ~HDITEM.Format.SortUp;
            }

            if (SendMessage(columnHeader, HDM_SETITEM, columnPtr, ref item) == IntPtr.Zero)
            {
                throw new Win32Exception();
            }
        }
    }

    /// <summary>
    /// The hditem.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct HDITEM
    {
        /// <summary>
        /// The mask.
        /// </summary>
        public Mask mask;

        /// <summary>
        /// The cxy.
        /// </summary>
        private readonly int cxy;

        [MarshalAs(UnmanagedType.LPTStr)]
        private readonly string pszText;

        private readonly IntPtr hbm;

        private readonly int cchTextMax;

        public Format fmt;

        private readonly IntPtr lParam;

        // _WIN32_IE >= 0x0300 
        private readonly int iImage;

        private readonly int iOrder;

        // _WIN32_IE >= 0x0500
        private readonly uint type;

        private readonly IntPtr pvFilter;

        // _WIN32_WINNT >= 0x0600
        private readonly uint state;

        /// <summary>
        /// The mask.
        /// </summary>
        [Flags]
        public enum Mask
        {
            /// <summary>
            /// The HDI FORMAT
            /// </summary>
            Format = 0x4
        }

        /// <summary>
        /// The format.
        /// </summary>
        [Flags]
        public enum Format
        {
            /// <summary>
            /// The sort down.
            /// </summary>
            SortDown = 0x200, // HDF_SORTDOWN

            /// <summary>
            /// The sort up.
            /// </summary>
            SortUp = 0x400 // HDF_SORTUP
        }
    }
}