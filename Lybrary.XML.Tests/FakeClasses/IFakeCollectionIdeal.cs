using System;
using System.Collections;
using System.Collections.Generic;

namespace Lybrary.XML.Tests.FakeClasses
{
    internal class FakeCollectionIdealINterface : IEnumerable<IBase>
    {
        private readonly List<IBase> _collection;

        public FakeCollectionIdealINterface() => _collection = new List<IBase>
            {
                new FakeBook
                {
                    Authors = new List<string> { "Author1", "Author2" },
                    Title = "First book",
                    Count = 51,
                    Date = DateTime.Today,
                    Note = "some book"
                },
                new FakePatent
                {
                    Inventors = new List<string> { "Inventor1", "Inventor2" },
                    Title = "First patent",
                    Number = 10,
                    AppDate = DateTime.Today,
                    Note = "some patent"
                },
                new FakeBook
                {
                    Authors = new List<string> { "Author1", "Author2" },
                    Title = "Second book",
                    Count = 56,
                    Date = DateTime.Today,
                    Note = "some book"
                },
                new FakePatent
                {
                    Inventors = new List<string> { "Inventor1", "Inventor2" },
                    Title = "Second patent",
                    Number = 10,
                    AppDate = DateTime.Today,
                    Note = "some patent"
                },
                new FakeBook
                {
                    Authors = new List<string> { "Author1", "Author2" },
                    Title = "Third book",
                    Count = 51,
                    Date = DateTime.Today,
                    Note = "some book"
                },
                new FakePatent
                {
                    Inventors = new List<string> { "Inventor1", "Inventor2" },
                    Title = "Third patent",
                    Number = 10,
                    AppDate = DateTime.Today,
                    Note = "some patent"
                }
            };

        public IEnumerator<IBase> GetEnumerator()
        {
            foreach (var item in _collection)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
