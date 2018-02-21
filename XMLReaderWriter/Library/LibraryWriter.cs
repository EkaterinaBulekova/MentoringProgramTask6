using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using XMLReaderWriter.Helpers;
using XMLReaderWriter.LibraryLogger;
using XMLReaderWriter.Resources;

namespace XMLReaderWriter.Library
{
    /// <summary>
    /// Represents a writer that provides forward-only way to generate
    /// streams that contain XML data from generic collection.
    /// </summary>
    /// <typeparam name="T">Base type of source collection's items</typeparam>
    public class LibraryWriter<T> where T: class
    {
        private const string DateAttr = "date";
        private const string NameAttr = "name";
        private const string NumberAttr = "number";
        private readonly IEnumerable<T> _source;
        private readonly string _mainElement;
        private readonly string _element;
        private int _current;

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
            _current = 0;
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
                try
                {
                    await writer.WriteStartElementAsync(null, _mainElement, null);
                    await writer.WriteAttributeStringAsync(null, DateAttr, null,
                        XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local));
                    await writer.WriteAttributeStringAsync(null, NameAttr, null, outSettings.CatalogName);
                    await writer.WriteAttributeStringAsync(null, NumberAttr, null,
                        XmlConvert.ToString(outSettings.CatalogNumber));
                    if (_source != null)
                        foreach (var item in _source)
                        {
                            _current++;
                            var xElement = item.ParseToXElement(_element);
                            if (xElement == null)
                                Logger.Log(WriterExcMessages.UncorrectObject + _element + WriterExcMessages.Index +
                                           _current);
                            else
                                xElement.WriteTo(writer);
                        }

                    await writer.WriteEndElementAsync();
                    await writer.FlushAsync();
                }
                catch (XmlException e)
                {
                    Logger.Log(ReaderExcMessages.XMLException, e);
                    throw new XmlException(WriterExcMessages.XMLException, e);
                }
                catch (ArgumentException e)
                {
                    Logger.Log(ReaderExcMessages.ArgumentException, e);
                    throw new ArgumentException(WriterExcMessages.ArgumentException, e);
                }
                catch (InvalidOperationException e)
                {
                    Logger.Log(ReaderExcMessages.InvalidOperationException, e);
                    throw new InvalidOperationException(WriterExcMessages.InvalidOperationException, e);
                }
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
                try
                {
                    writer.WriteStartElement(_mainElement);
                    writer.WriteAttributeString(string.Empty, DateAttr, string.Empty,
                        XmlConvert.ToString(DateTime.Today.Date, XmlDateTimeSerializationMode.Local));
                    writer.WriteAttributeString(string.Empty, NameAttr, string.Empty, outSettings.CatalogName);
                    writer.WriteAttributeString(string.Empty, NumberAttr, string.Empty, XmlConvert.ToString(outSettings.CatalogNumber));
                    if (_source != null)
                        foreach (var item in _source)
                        {
                            _current++;
                            var xElement = item.ParseToXElement(_element);
                            if (xElement == null)
                                Logger.Log(WriterExcMessages.UncorrectObject + _element + WriterExcMessages.Index +
                                           _current);
                            else
                                xElement.WriteTo(writer);
                        }

                    writer.WriteEndElement();
                    writer.Close();

                }
                catch (XmlException e)
                {
                    Logger.Log(ReaderExcMessages.XMLException, e);
                    throw new XmlException(WriterExcMessages.XMLException, e);
                }
                catch (ArgumentException e)
                {
                    Logger.Log(ReaderExcMessages.ArgumentException, e);
                    throw new ArgumentException(WriterExcMessages.ArgumentException, e);
                }
                catch (InvalidOperationException e)
                {
                    Logger.Log(ReaderExcMessages.InvalidOperationException, e);
                    throw new InvalidOperationException(WriterExcMessages.InvalidOperationException, e);
                }
            }
        }
    }
}
