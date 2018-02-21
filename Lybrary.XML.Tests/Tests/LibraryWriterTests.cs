using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryData.Entities;
using Lybrary.XML.Tests.FakeClasses.FakeCollections;
using Lybrary.XML.Tests.FakeClasses.FakeEntities;
using Lybrary.XML.Tests.FakeClasses.FakeXmls;
using Lybrary.XML.Tests.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLReaderWriter.Library;

namespace Lybrary.XML.Tests.Tests
{
    [TestClass]
    public class LibraryWriterTests
    {
        [TestMethod]
        public void CanToWriteRealCollectioToRealFileTest()
        {
            List<IElement> library;
            var settings = new LibrarySettings();
            var outSettings = new OutputSettings();
            
            var stream = new FileStream(PathStrings.FileToRead, FileMode.Open);
            using (stream)
            {
                library = new LibraryReader<IElement>(stream, settings).ToList();
            }
            stream = new FileStream(PathStrings.FileToWrite, FileMode.Create);
            using (stream)
            {
                var writer = new LibraryWriter<IElement>(library, settings);
                writer.WriteXElements(stream, outSettings);
            }
        }

        [TestMethod]
        public async Task CanToWriteAsyncRealCollectioToRealFileTest()
        {
            List<IElement> library;
            var settings = new LibrarySettings();
            var outSettings = new OutputSettings();

            var stream = new FileStream(PathStrings.FileToRead, FileMode.Open);
            using (stream)
            {
                library = new LibraryReader<IElement>(stream, settings).ToList();
            }

            stream = new FileStream(PathStrings.FileToAsyncWrite, FileMode.Create);
            using (stream)
            {
                var writer = new LibraryWriter<IElement>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
            }
        }

        [TestMethod]
        public async Task CanToWriteAsyncIdealCollectionToXmlFakeBaseClassTestAsync()
        {
            var actualString = await WriteXmlStringAsync(new FakeCollectionIdeal().ToList());
            var expectedString = new FakeXmlIdeal().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void CanToWriteIdealCollectionToXmlFakeBaseClassTest()
        {
            var actualString = WriteXmlString(new FakeCollectionIdeal().ToList());
            var expectedString = new FakeXmlIdeal().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public async Task CanToWriteAsyncIdealCollectionToXmlFakeBaseInterfaceTest()
        {
            var actualString = await WriteXmlStringAsync(new FakeCollectionIdealINterface().ToList());
            var expectedString = new FakeXmlIdeal().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void CanToWriteIdealCollectionToXmlFakeBaseInterfaceTest()
        {
            var actualString = WriteXmlString(new FakeCollectionIdealINterface().ToList());
            var expectedString = new FakeXmlIdeal().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public async Task CanToWriteAsyncPoorCollectionToXmlFakeBaseClassTest()
        {
            var actualString = await WriteXmlStringAsync(new FakeCollectionPoorObj().ToList());
            var expectedString = new FakeXmlPoor().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void CanToWritePoorCollectionToXmlFakeBaseClassTest()
        {
            var actualString = WriteXmlString(new FakeCollectionPoorObj().ToList());
            var expectedString = new FakeXmlPoor().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public async Task CanToWriteAsyncFailCollectionToXmlFakeBaseClassTest()
        {
            var actualString = await WriteXmlStringAsync(new FakeCollectionFailObj().ToList());
            var expectedString = new FakeXmlFailObj().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        public void CanToWriteFailCollectionToXmlFakeBaseClassTest()
        {
            var actualString = WriteXmlString(new FakeCollectionFailObj().ToList());
            var expectedString = new FakeXmlFailObj().XString;

            Assert.AreEqual(expectedString, actualString);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Uncorrect or null parametr")]
        public void CanToThrowArgumentExceptionTest()
        {
            WriteXmlString(new FakeCollectionFailObj().ToList(), true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Uncorrect or null parametr")]
        public async Task CanToThrowArgumentExceptionAsyncTest()
        {
            await WriteXmlStringAsync(new FakeCollectionFailObj().ToList(), true);
        }

        private async Task<string> WriteXmlStringAsync<T>(List<T> collection, bool withFailSettings = false) where T : class 
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            if (withFailSettings) settings.MainElementName = null;
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<T>(collection, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
                stream.Position = 0;
                result = await new StreamReader(stream).ReadToEndAsync();
            }

            return result;
        }

        private string WriteXmlString<T>(List<T> collecton, bool withFailSettings = false) where T : class
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            if (withFailSettings) settings.MainElementName = null;
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<T>(collecton, settings);
                writer.WriteXElements(stream, outSettings);
                stream.Position = 0;
                result = new StreamReader(stream).ReadToEnd();
            }

            return result;
        }
    }
}
