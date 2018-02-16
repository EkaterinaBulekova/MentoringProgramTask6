using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Entities;
using XMLReaderWriter.Library;

namespace Lybrary.XML.Tests
{
    [TestClass]
    public class LibraryReaderTests
    {
        [TestMethod]
        public void CanToEnumerateAndGetBooksTest()
        {
            List<Book> books;
            var settings = new LibrarySettings
            {
                MainElementName = "Catalog",
                ElementName = "Element",
                LibraryTypes = new List<Type> {typeof(Book), typeof(Newspaper), typeof(Patent)}
            };
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                books = new LibraryReader<BaseElement>(stream, settings).Where(_ => _ is Book).Cast<Book>().ToList();
            }

            Assert.IsNotNull(books);
            Assert.AreEqual(2, books.Count);
            Assert.AreEqual("First Book", books[0].Title);
        }

        [TestMethod]
        public void CanToEnumerateAndGetNewspapersTest()
        {
            var settings = new LibrarySettings
            {
                MainElementName = "Catalog",
                ElementName = "Element",
                LibraryTypes = new List<Type> { typeof(Book), typeof(Newspaper), typeof(Patent) }
            };
            List<Newspaper> newspapers;
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                newspapers = new LibraryReader<BaseElement>(stream, settings).Where(_ => _ is Newspaper).Cast<Newspaper>().ToList();
            }

            Assert.IsNotNull(newspapers);
            Assert.AreEqual(2, newspapers.Count);
            Assert.AreEqual(new DateTime(2018,1,24), newspapers[1].Date);
        }

        [TestMethod]
        public void CanToEnumerateAndGetPatentTest()
        {
            var settings = new LibrarySettings
            {
                MainElementName = "Catalog",
                ElementName = "Element",
                LibraryTypes = new List<Type> { typeof(Book), typeof(Newspaper), typeof(Patent) }
            };
            List<Patent> patents;
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                patents = new LibraryReader<BaseElement>(stream, settings).Where(_ => _ is Patent).Cast<Patent>().ToList();
            }

            Assert.IsNotNull(patents);
            Assert.AreEqual(2, patents.Count);
            Assert.AreEqual(new DateTime(2017, 1, 24), patents[1].ApplicationDate);
        }

        [TestMethod]
        public void CanToEnumerateAndGetAllTypesElementTest()
        {
            var settings = new LibrarySettings
            {
                MainElementName = "Catalog",
                ElementName = "Element",
                LibraryTypes = new List<Type> { typeof(Book), typeof(Newspaper), typeof(Patent) }
            };
            List<BaseElement> elements;
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                elements = new LibraryReader<BaseElement>(stream, settings).ToList();
            }

            Assert.IsNotNull(elements);
            Assert.AreEqual(6, elements.Count);
        }
    }
}