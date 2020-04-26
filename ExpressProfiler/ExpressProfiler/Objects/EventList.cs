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

// Traceutils assembly
// writen by Locky, 2009.

namespace ExpressProfiler.Objects
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class CEvent
    {
        // ReSharper disable UnaccessedField.Global
        // ReSharper disable FieldCanBeMadeReadOnly.Global
        // ReSharper disable InconsistentNaming
        // ReSharper disable MemberCanBePrivate.Global
        [XmlAttribute]
        public long EventClass;

        [XmlAttribute]
        public long DatabaseID;

        [XmlAttribute]
        public long ObjectID;

        [XmlAttribute]
        public long RowCounts;

        public string TextData;

        [XmlAttribute]
        public string DatabaseName;

        [XmlAttribute]
        public string ObjectName;

        [XmlAttribute]
        public long Count;

        [XmlAttribute]
        public long CPU;

        [XmlAttribute]
        public long Reads;

        [XmlAttribute]
        public long Writes;

        [XmlAttribute]
        public long Duration;

        [XmlAttribute]
        public long SPID;

        [XmlAttribute]
        public long NestLevel;

        // needed for serialization
        // ReSharper disable UnusedMember.Global
        /// <summary>
        /// Initializes a new instance of the <see cref="CEvent"/> class.
        /// </summary>
        public CEvent()
        {
        }

        // ReSharper restore UnusedMember.Global
        public CEvent(long aDatabaseID, string aDatabaseName, long aObjectID, string aObjectName, string aTextData)
        {
            this.DatabaseID = aDatabaseID;
            this.DatabaseName = aDatabaseName;
            this.ObjectID = aObjectID;
            this.ObjectName = aObjectName;
            this.TextData = aTextData;
        }

        public CEvent(
            long eventClass,
            long spid,
            long nestLevel,
            long aDatabaseID,
            string aDatabaseName,
            long aObjectID,
            string aObjectName,
            string aTextData,
            long duration,
            long reads,
            long writes,
            long cpu)
        {
            this.EventClass = eventClass;
            this.DatabaseID = aDatabaseID;
            this.DatabaseName = aDatabaseName;
            this.ObjectID = aObjectID;
            this.ObjectName = aObjectName;
            this.TextData = aTextData;
            this.Duration = duration;
            this.Reads = reads;
            this.Writes = writes;
            this.CPU = cpu;
            this.SPID = spid;
            this.NestLevel = nestLevel;
        }

        /// <summary>
        /// The get key.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetKey() => $"({this.DatabaseID}).({this.ObjectID}).({this.ObjectName}).({this.TextData})";
    }
}