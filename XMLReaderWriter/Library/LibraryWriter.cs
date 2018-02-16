using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using XMLReaderWriter.Helpers;

namespace XMLReaderWriter.Library
{
    /// <summary>
    /// Represents a writer that provides forward-only way to generate
    /// streams that contain XML data from generic collection.
    /// </summary>
    /// <typeparam name="T">Base type of source collection's items</typeparam>
    public class LibraryWriter<T> where T: class 
    {
        private readonly IEnumerable<T> _source;
        private readonly string _mainElement;
        private readonly string _element;
        private readonly List<Type> _elementTypes;

        /// <summary>
        /// Create instance of Library writer to write a library catalog to stream
        /// </summary>
        /// <param name="source">Generic collection</param>
        /// <param name="settings">Settings of collection</param>
        public LibraryWriter(IEnumerable<T> source, LibrarySettings settings)
        {
            _source = source;
            _mainElement = settings.MainElementName;
            _element = settings.ElementName;
            _elementTypes = settings.LibraryTypes;
        }

        /// <summary>
        ///  Writes asyncronly object from lybrary to streams that contain XML data
        /// </summary>
        /// <param name="outputStream">Stream to output</param>
        /// <param name="outSettings">Output settings</param>
        /// <returns>Task</returns>
        public async Task WriteXElementsAsync(Stream outputStream, OutputSettings outSettings)
        {
            var settings = new XmlWriterSettings {Async = true, Indent = true};

            using (var writer = XmlWriter.Create(outputStream, settings))
            {
                await writer.WriteStartElementAsync(null, _mainElement, null);
                await writer.WriteAttributeStringAsync(null, "date", null,
                    XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local));
                await writer.WriteAttributeStringAsync(null, "name", null, outSettings.CatalogName);
                await writer.WriteAttributeStringAsync(null, "number", null, XmlConvert.ToString(outSettings.CatalogNumber));
                foreach (var item in _source)
                {
                    item.ParseToXElement(_element, _elementTypes)?.WriteTo(writer);
                }

                await writer.WriteEndElementAsync();
                await writer.FlushAsync();
            }
        }

        /// <summary>
        ///  Writes syncronly object from lybrary to streams that contain XML data
        /// </summary>
        /// <param name="outputStream">Stream to output</param>
        /// <param name="outSettings">Output settings</param>
        public void WriteXElements(Stream outputStream, OutputSettings outSettings)
        {
            var settings = new XmlWriterSettings { Indent = true };

            using (var writer = XmlWriter.Create(outputStream, settings))
            {
                writer.WriteStartElement(_mainElement);
                writer.WriteAttributeString("", "date", "",
                    XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local));
                writer.WriteAttributeString("", "name", "", outSettings.CatalogName);
                writer.WriteAttributeString("", "number", "", XmlConvert.ToString(outSettings.CatalogNumber));
                foreach (var item in _source)
                {
                    item.ParseToXElement(_element, _elementTypes)?.WriteTo(writer);
                }

                writer.WriteEndElement();
                writer.Close();
            }
        }
    }
}
