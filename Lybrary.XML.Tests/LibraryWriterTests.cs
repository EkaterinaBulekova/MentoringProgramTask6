using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryData.Entities;
using XMLReaderWriter.Library;
using System;

namespace Lybrary.XML.Tests
{
    [TestClass]
    public class LibraryWriterTests
    {
        [TestMethod]
        public void CanToWriteTest()
        {
            var outSettings = new OutputSettings{CatalogName = "First catalog", CatalogNumber = 1};

            var settings = new LibrarySettings
            {
                MainElementName = "Catalog",
                ElementName = "Element",
                LibraryTypes = new List<Type> { typeof(Book), typeof(Newspaper), typeof(Patent) }
            };
            List<BaseElement> library;
            
            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                library = new LibraryReader<BaseElement>(stream, settings).ToList();
            }
            stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog2.xml", FileMode.Create);
            using (stream)
            {
                var writer = new LibraryWriter<BaseElement>(library, settings);
                writer.WriteXElements(stream, outSettings);
            }
        }

        [TestMethod]
        public async Task CanToWriteAsyncTest()
        {
            var outSettings = new OutputSettings { CatalogName = "First catalog", CatalogNumber = 1 };

            var settings = new LibrarySettings
            {
                MainElementName = "Catalog",
                ElementName = "Element",
                LibraryTypes = new List<Type> { typeof(Book), typeof(Newspaper), typeof(Patent) }
            };
            List<BaseElement> library;

            var stream = new FileStream(@"D:\project\MentoringProgramTask6\LibraryCatalog1.xml", FileMode.Open);
            using (stream)
            {
                library = new LibraryReader<BaseElement>(stream, settings).ToList();
            }

            stream = new FileStream(@"D:\project\MentoringProgramTask6\AsyncTest.xml", FileMode.Create);
            using (stream)
            {
                var writer = new LibraryWriter<BaseElement>(library, settings);
                await writer.WriteXElementsAsync(stream, outSettings);
            }
        }
    }
}
