using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Entities;
using XMLReaderWriter.Library;
using Lybrary.XML.Tests.FakeClasses.FakeCollections;
using Lybrary.XML.Tests.FakeClasses.FakeEntities;
using Lybrary.XML.Tests.FakeClasses.FakeXmls;
using Lybrary.XML.Tests.Resources;

namespace Lybrary.XML.Tests.Tests
{
    [TestClass]
    public class LibraryReaderTests
    {
        [TestMethod]
        public void CanToEnumerateAndGetBooksTest()
        {
            var books = ReadXmlFile<Book>();

            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count);
            Assert.AreEqual("First Book", books[0].Title);
        }

        [TestMethod]
        public void CanToEnumerateAndGetNewspapersTest()
        {
            var newspapers = ReadXmlFile<Newspaper>();

            Assert.IsNotNull(newspapers);
            Assert.AreEqual(2, newspapers.Count);
            Assert.AreEqual(new DateTime(2018,1,24), newspapers[1].Date);
        }

        [TestMethod]
        public void CanToEnumerateAndGetPatentTest()
        {
            var patents = ReadXmlFile<Patent>();

            Assert.IsNotNull(patents);
            Assert.AreEqual(2, patents.Count);
            Assert.AreEqual(new DateTime(2017, 1, 24), patents[1].ApplicationDate);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllTypesElementTest()
        {
            List<IElement> elements = ReadXmlFile<IElement>();

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }


        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesElementBaseClassTest()
        {
            var elements = ReadXmlString<BaseClass>(new FakeXmlIdeal().XString);

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesElementBaseInterfaceTest()
        {
            var elements = ReadXmlString<IBase>(new FakeXmlIdeal().XString);

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesPoorXmlBaseClassTest()
        {
            var elements = ReadXmlString<BaseClass>(new FakeXmlPoor().XString);

            var expCollection = new FakeCollectionPoorObj().ToList();
            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
            Assert.IsNull(elements[0].Note);
            Assert.AreEqual(((FakeBook)expCollection[0]).Count, ((FakeBook)elements[0]).Count);
            Assert.AreEqual(((FakeBook)expCollection[0]).Date, ((FakeBook)elements[0]).Date);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesPoorXmlBaseInterfaceTest()
        {
            var elements = ReadXmlString<IBase>(new FakeXmlPoor().XString);

            var expCollection = new FakeCollectionPoorObj().ToList();
            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
            Assert.IsNull(((FakeBook)elements[0]).Note);
            Assert.AreEqual(((FakeBook)expCollection[0]).Count, ((FakeBook)elements[0]).Count );
            Assert.AreEqual(((FakeBook)expCollection[0]).Date, ((FakeBook)elements[0]).Date);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesFailXmlBaseClassTest()
        {
            var elements = ReadXmlString<BaseClass>(new FakeXmlFail().XString);

            var expCollection = new FakeCollectionFail().ToList();
            Assert.IsNotNull(elements);
            Assert.AreEqual(4, elements.Count);
            Assert.AreEqual(((FakePatent)expCollection[0]).Number, ((FakePatent)elements[0]).Number);
            Assert.AreEqual(((FakePatent)expCollection[0]).AppDate, ((FakePatent)elements[0]).AppDate);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesFailXmlBaseInterfaceTest()
        {
            var elements = ReadXmlString<IBase>(new FakeXmlFail().XString);

            var expCollection = new FakeCollectionFail().ToList();
            Assert.IsNotNull(elements);
            Assert.AreEqual(4, elements.Count);
            Assert.AreEqual(((FakePatent)expCollection[0]).Number, ((FakePatent)elements[0]).Number);
            Assert.AreEqual(((FakePatent)expCollection[0]).AppDate, ((FakePatent)elements[0]).AppDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Uncorrect or null start(main) Element parametr")]
        public void CanToThrowArgumentExceptionTest()
        {
            var elements = ReadXmlString<BaseClass>(new FakeXmlIdeal().XString, true);
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException),
            "Uncorrect format of XML data")]
        public void CanToThrowXmlExceptionTest()
        {
            var elements = ReadXmlString<BaseClass>(new FakeXmlCrash().XString);
        }

        private List<T> ReadXmlFile<T>() where T : class
        {
            List<T> elements;
            var settings = new LibrarySettings();
            var stream = new FileStream(PathStrings.FileToRead, FileMode.Open);
            using (stream)
            {
                elements = new LibraryReader<IElement>(stream, settings).Where(_ => _ is T).Cast<T>().ToList();
            }

            return elements;
        }

        private List<T> ReadXmlString<T>(string xmlString, bool withFailSettings = false) where T : class
        {
            var settings = new LibrarySettings();
            if (withFailSettings) settings.MainElementName = null;
            List<T> elements;
            var stream = new MemoryStream();
            using (stream)
            {
                var writer = new StreamWriter(stream);
                writer.Write(xmlString);
                writer.Flush();
                stream.Position = 0;
                elements = new LibraryReader<T>(stream, settings).ToList();
            }
            return elements;
        }
    }
}