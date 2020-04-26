namespace ExpressProfiler.Helpers
{
    using System;
    using System.IO;

    /// <summary>
    /// The text file helper.
    /// </summary>
    public static class TextFileHelper
    {
        /// <summary>
        /// The read all text.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ReadAllText(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
}