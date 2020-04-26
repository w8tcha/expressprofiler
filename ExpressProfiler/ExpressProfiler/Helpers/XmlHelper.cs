namespace ExpressProfiler.Helpers
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

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

            using (TextReader reader = new StringReader(xmlString))
            {
                return (T)serializer.Deserialize(reader);
            }
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

            using (var memoryStream = new MemoryStream())
            {
                var xs = new XmlSerializer(serializationType);
                using (var xmlTextWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Encoding = utf8NoBom }))
                {
                    xs.Serialize(xmlTextWriter, objectToSerialize, ns);

                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }
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
}