using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Entities;
using XMLReaderWriter.Library;
using Lybrary.XML.Tests.FakeClasses;

namespace Lybrary.XML.Tests
{
    [TestClass]
    public class LibraryReaderTests
    {
        [TestMethod]
        public void CanToEnumerateAndGetBooksTest()
        {
            List<Book> books;
            var settings = new LibrarySettings();
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                books = new LibraryReader<IElement>(stream, settings).Where(_ => _ is Book).Cast<Book>().ToList();
            }

            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count);
            Assert.AreEqual("First Book", books[0].Title);
        }

        [TestMethod]
        public void CanToEnumerateAndGetNewspapersTest()
        {
            List<Newspaper> newspapers;
            var settings = new LibrarySettings();
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                newspapers = new LibraryReader<IElement>(stream, settings).Where(_ => _ is Newspaper).Cast<Newspaper>().ToList();
            }

            Assert.IsNotNull(newspapers);
            Assert.AreEqual(2, newspapers.Count);
            Assert.AreEqual(new DateTime(2018,1,24), newspapers[1].Date);
        }

        [TestMethod]
        public void CanToEnumerateAndGetPatentTest()
        {
            var settings = new LibrarySettings();
            List<Patent> patents;
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                patents = new LibraryReader<IElement>(stream, settings).Where(_ => _ is Patent).Cast<Patent>().ToList();
            }

            Assert.IsNotNull(patents);
            Assert.AreEqual(2, patents.Count);
            Assert.AreEqual(new DateTime(2017, 1, 24), patents[1].ApplicationDate);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllTypesElementTest()
        {
            List<IElement> elements;
            var settings = new LibrarySettings();
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                elements = new LibraryReader<IElement>(stream, settings).ToList();
            }

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }


        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesElementBaseClassTest()
        {
            var settings = new LibrarySettings();
            List<BaseClass> elements;
            var stream = new MemoryStream();
            using (stream)
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(new FakeXmlIdeal().XString);
                writer.Flush();
                stream.Position = 0;
                elements = new LibraryReader<BaseClass>(stream, settings).ToList();
            }

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesElementBaseInterfaceTest()
        {
            var settings = new LibrarySettings();
            List<IBase> elements;
            var stream = new MemoryStream();
            using (stream)
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(new FakeXmlIdeal().XString);
                writer.Flush();
                stream.Position = 0;
                elements = new LibraryReader<IBase>(stream, settings).ToList();
            }

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesPoorXmlBaseClassTest()
        {
            var settings = new LibrarySettings();
            List<BaseClass> elements;
            var stream = new MemoryStream();
            using (stream)
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(new FakeXmlPoor().XString);
                writer.Flush();
                stream.Position = 0;
                elements = new LibraryReader<BaseClass>(stream, settings).ToList();
            }

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
            Assert.IsNull(elements[0].Note);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllFakeTypesPoorXmlBaseInterfaceTest()
        {
            var settings = new LibrarySettings();
            List<IBase> elements;
            var stream = new MemoryStream();
            using (stream)
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(new FakeXmlPoor().XString);
                writer.Flush();
                stream.Position = 0;
                elements = new LibraryReader<IBase>(stream, settings).ToList();
            }

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
            Assert.IsNull(((FakeBook)elements[0]).Note);
        }
    }
}