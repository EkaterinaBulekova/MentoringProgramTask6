using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XMLReaderWriter.Helpers;
using XMLReaderWriter.LibraryLogger;
using XMLReaderWriter.Resources;

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
        private int _current;


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
            _current = 0;
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
                    Logger.Log(ReaderExcMessages.XMLException, e);
                    throw new XmlException(ReaderExcMessages.XMLException, e);
                }
                catch (ArgumentException e)
                {
                    Logger.Log(ReaderExcMessages.ArgumentException, e);
                    throw new ArgumentException(ReaderExcMessages.ArgumentException, e);
                }
                catch (InvalidOperationException e)
                {
                    Logger.Log(ReaderExcMessages.InvalidOperationException, e);
                    throw new InvalidOperationException(ReaderExcMessages.InvalidOperationException, e);
                }

                while (reader.ReadToNextSibling(_element)) 
                {
                    if (XNode.ReadFrom(reader) is XElement element) yield return element;
                    else Logger.Log(ReaderExcMessages.InvalidXmlElement);
                }

                reader.Close();
            }
        }

        /// <summary>
        /// Supports a simple iteration over a generic collection of base type.
        /// </summary>
        /// <returns>IEnumerator of base type</returns>
        public IEnumerator<T> GetEnumerator() => GetXElements()
            .Select(_ =>
            {
                var element = _.ParseTo<T>();
                _current++;
                if(element==null)
                    Logger.Log(ReaderExcMessages.UncorrectXmlElement + _element + ReaderExcMessages.Index + _current);
                return element;
            })
            .Where(_ => _ != null).GetEnumerator();

        /// <summary>
        /// Supports a simple iteration over a generic collection.
        /// </summary>
        /// <returns>Inumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
