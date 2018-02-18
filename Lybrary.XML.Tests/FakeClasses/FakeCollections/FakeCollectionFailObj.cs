using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lybrary.XML.Tests.FakeClasses
{
    internal class FakeCollectionFailObj : IEnumerable<BaseClass>
    {
        private readonly List<BaseClass> _collection;

        public FakeCollectionFailObj()
        {
            _collection = new List<BaseClass>
            {
                new FakeBook
                {
                    Authors = new List<string> { "Author1", "Author2" },
                    Title = "First book",
                },
                new FakePatent
                {
                    Inventors = new List<string> { "Inventor1", "Inventor2" },
                    Title = "First patent",
                    AppDate = DateTime.Today
                },
                new FakeBook
                {
                    Authors = new List<string> { "Author1", "Author2" },
                    Count = 56,
                    Date = DateTime.Today
                },
                new FakePatent
                {
                    Inventors = new List<string> { "Inventor1", "Inventor2" },
                    Title = "Second patent",
                    Number = 10,
                    AppDate = DateTime.Today
                },
                new FakeBook
                {
                    Authors = new List<string> { "Author1", "Author2" },
                    Title = "Third book",
                    Count = 51,
                    Date = DateTime.Today
                },
                new FakePatent
                {
                    Inventors = new List<string> { "Inventor1", "Inventor2" },
                    Title = "Third patent",
                    Number = 10,
                    AppDate = DateTime.Today
                }
            };
        }

        public IEnumerator<BaseClass> GetEnumerator()
        {
            foreach (var item in _collection)
            {
                yield return item;
            }

        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
