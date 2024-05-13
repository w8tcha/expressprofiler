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

namespace ExpressProfiler.Objects;

/// <summary>
/// The c event.
/// </summary>
[Serializable]
public class CEvent
{
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

    /// <summary>
    /// Initializes a new instance of the <see cref="CEvent"/> class.
    /// </summary>
    public CEvent()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CEvent"/> class.
    /// </summary>
    /// <param name="aDatabaseID">
    /// The a database id.
    /// </param>
    /// <param name="aDatabaseName">
    /// The a database name.
    /// </param>
    /// <param name="aObjectID">
    /// The a object id.
    /// </param>
    /// <param name="aObjectName">
    /// The a object name.
    /// </param>
    /// <param name="aTextData">
    /// The a text data.
    /// </param>
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