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

using System.Collections.Generic;

[XmlRoot(ElementName = "Connection")]
public class Connection
{
    [XmlElement(ElementName = "ApplicationName")]
    public string ApplicationName { get; set; }
    [XmlElement(ElementName = "Catalog")]
    public string Catalog { get; set; }
    [XmlElement(ElementName = "CreationDate")]
    public string CreationDate { get; set; }
    [XmlElement(ElementName = "DataSource")]
    public string DataSource { get; set; }
    [XmlElement(ElementName = "IntegratedSecurity")]
    public string IntegratedSecurity { get; set; }
    [XmlElement(ElementName = "Password")]
    public string Password { get; set; }
    [XmlElement(ElementName = "UserId")]
    public string UserId { get; set; }
}

[XmlRoot(ElementName = "RecentConnection")]
public class RecentConnection
{
    [XmlElement(ElementName = "Connections")]
    public List<Connection> Connections { get; set; }

    /// <summary>
    /// The add.
    /// </summary>
    /// <param name="connection">
    /// The connection.
    /// </param>
    public void Add(Connection connection)
    {
        if (this.Connections == null)
        {
            return;
        }

        if (connection == null)
        {
            return;
        }

        if (this.Connections.Exists(c => c.DataSource == connection.DataSource && c.UserId == connection.UserId))
        {
            this.Connections.ForEach(c =>
                {
                    if (c.DataSource == connection.DataSource &&
                        c.UserId == connection.UserId)
                    {
                        c.Password = connection.Password;
                    }
                });
        }
        else
        {
            this.Connections.Add(connection);
        }
    }
}