using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LibraryData.Entities;
using Lybrary.XML.Tests.FakeClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XMLReaderWriter.Library;
using System;
using System.Xml;

namespace Lybrary.XML.Tests
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
            
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                library = new LibraryReader<IElement>(stream, settings).ToList();
            }
            stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog2.xml", FileMode.Create);
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

            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                library = new LibraryReader<IElement>(stream, settings).ToList();
            }

            stream = new FileStream(@"D:\project\MentoringProgramTask6\AsyncTest.xml", FileMode.Create);
            using (stream)
            {
                var writer = new LibraryWriter<IElement>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
            }
        }

        [TestMethod]
        public async Task CanToWriteAsyncIdealCollectionToXmlFakeBaseClassTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionIdeal().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
                stream.Position = 0;
                result = await new StreamReader(stream).ReadToEndAsync();
            }
            Assert.AreEqual(new FakeXmlIdeal().XString, result);
        }

        [TestMethod]
        public void CanToWriteIdealCollectionToXmlFakeBaseClassTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionIdeal().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                writer.WriteXElements(stream, outSettings);
                stream.Position = 0;
                result = new StreamReader(stream).ReadToEnd();
            }

            var expectedString = new FakeXmlIdeal().XString;
            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        public async Task CanToWriteAsyncIdealCollectionToXmlFakeBaseInterfaceTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionIdealINterface().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<IBase>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
                stream.Position = 0;
                result = await new StreamReader(stream).ReadToEndAsync();
            }

            Assert.AreEqual(new FakeXmlIdeal().XString, result);
        }

        [TestMethod]
        public void CanToWriteIdealCollectionToXmlFakeBaseInterfaceTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionIdealINterface().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<IBase>(library, settings);
                writer.WriteXElements(stream, outSettings);
                stream.Position = 0;
                result = new StreamReader(stream).ReadToEnd();
            }

            var expectedString = new FakeXmlIdeal().XString;
            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        public async Task CanToWriteAsyncPoorCollectionToXmlFakeBaseClassTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionPoorObj().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
                stream.Position = 0;
                result = await new StreamReader(stream).ReadToEndAsync();
            }

            Assert.AreEqual(new FakeXmlPoor().XString, result);
        }

        [TestMethod]
        public void CanToWritePoorCollectionToXmlFakeBaseClassTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionPoorObj().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                writer.WriteXElements(stream, outSettings);
                stream.Position = 0;
                result = new StreamReader(stream).ReadToEnd();
            }

            var expectedString = new FakeXmlPoor().XString;
            Assert.AreEqual(expectedString.Length, result.Length);
            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        public async Task CanToWriteAsyncFailCollectionToXmlFakeBaseClassTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionFailObj().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
                stream.Position = 0;
                result = await new StreamReader(stream).ReadToEndAsync();
            }

            Assert.AreEqual(new FakeXmlFailObj().XString, result);
        }

        [TestMethod]
        public void CanToWriteFailCollectionToXmlFakeBaseClassTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionFailObj().ToList();
            var stream = new MemoryStream();
            string result;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                writer.WriteXElements(stream, outSettings);
                stream.Position = 0;
                result = new StreamReader(stream).ReadToEnd();
            }

            var expectedString = new FakeXmlFailObj().XString;
            Assert.AreEqual(expectedString, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Uncorrect or null parametr")]
        public void CanToThrowArgumentExceptionTest()
        {
            var outSettings = new OutputSettings();
            var settings = new LibrarySettings();
            var library = new FakeCollectionFailObj().ToList();
            var stream = new MemoryStream();
            settings.MainElementName = null;
            using (stream)
            {
                var writer = new LibraryWriter<BaseClass>(library, settings);
                writer.WriteXElements(stream, outSettings);
            }
        }
    }
}
