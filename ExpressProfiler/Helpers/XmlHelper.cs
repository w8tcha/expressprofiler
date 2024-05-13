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

namespace ExpressProfiler.Helpers;

/// <summary>
/// The xml helper.
/// </summary>
public static class XmlHelper
{
    /// <summary>
    /// The deserialize xml.
    /// </summary>
    /// <param name="xmlString">
    /// The xml string.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="T"/>.
    /// </returns>
    public static T DeserializeXml<T>(string xmlString)
    {
        var serializer = new XmlSerializer(typeof(T));

        using TextReader reader = new StringReader(xmlString);
        return (T)serializer.Deserialize(reader);
    }

    /// <summary>
    /// The serialize xml.
    /// </summary>
    /// <param name="objectToSerialize">
    /// The object to serialize.
    /// </param>
    /// <typeparam name="T">
    /// </typeparam>
    /// <returns>
    /// The <see cref="string"/>.
    /// </returns>
    public static string SerializeXml<T>(T objectToSerialize)
    {
        var utf8NoBom = new UTF8Encoding(false);
        var serializationType = typeof(T);

        if (serializationType == typeof(object) && objectToSerialize != null)
        {
            serializationType = objectToSerialize.GetType();
        }

        var ns = new XmlSerializerNamespaces();
        ns.Add(string.Empty, string.Empty);

        using var memoryStream = new MemoryStream();
        var xs = new XmlSerializer(serializationType);
        using var xmlTextWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Encoding = utf8NoBom });
        xs.Serialize(xmlTextWriter, objectToSerialize, ns);

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }

    /// <summary>
    /// The write xml.
    /// </summary>
    /// <param name="folderPath">
    /// The folder path.
    /// </param>
    /// <param name="fileName">
    /// The file name.
    /// </param>
    /// <param name="content">
    /// The content.
    /// </param>
    public static void WriteXml(string folderPath, string fileName, string content)
    {
        if (fileName == null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        if (!Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"the directory '{folderPath}' does not exists");
        }

        var fullPath = Path.Combine(folderPath, fileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }

        File.WriteAllText(fullPath, content);
    }
}