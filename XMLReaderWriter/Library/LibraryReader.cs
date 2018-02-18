using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XMLReaderWriter.Helpers;

namespace XMLReaderWriter.Library
{
    /// <summary>
    /// Represents generic collection of T type that genereted dinamicly 
    /// from input stream of XML data
    /// </summary>
    /// <typeparam name="T">Base class of library's elements</typeparam>
    public class LibraryReader<T> : IEnumerable<T> where T : class
    {
        private readonly Stream _inputStream;
        private readonly string _mainElement;
        private readonly string _element;


        /// <summary>
        /// Create instance of collection
        /// </summary>
        /// <param name="inputStream">Input stream</param>
        /// <param name="settings">Settings of collection</param>
        public LibraryReader(Stream inputStream, LibrarySettings settings)
        {
            _inputStream = inputStream;
            _mainElement = settings.MainElementName;
            _element = settings.ElementName;
        }

        /// <summary>
        /// Gets and enumerate xml elements from input stream 
        /// </summary>
        /// <returns>IEnumerable of XElement</returns>
        private IEnumerable<XElement> GetXElements()
        {
            using (var reader = XmlReader.Create(_inputStream))
            {
                try
                {
                    reader.ReadToFollowing(_mainElement);
                    reader.ReadStartElement();
                }
                catch (XmlException e)
                {
                    throw new XmlException("Uncorrect format of XML data", e);
                }
                catch (ArgumentException e)
                {
                    throw new ArgumentException("Uncorrect or null start(main) Element parametr", e);
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException("Invalid operation to read XML", e);
                }

                while (reader.ReadToNextSibling(_element)) 
                {
                    if (XNode.ReadFrom(reader) is XElement element) yield return element;
                }

                reader.Close();
            }
        }

        /// <summary>
        /// Supports a simple iteration over a generic collection of base type.
        /// </summary>
        /// <returns>IEnumerator of base type</returns>
        public IEnumerator<T> GetEnumerator() => GetXElements()
            .Select(_ => _.ParseTo<T>())
            .Where(_ => _ != null).GetEnumerator();

        /// <summary>
        /// Supports a simple iteration over a generic collection.
        /// </summary>
        /// <returns>Inumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
