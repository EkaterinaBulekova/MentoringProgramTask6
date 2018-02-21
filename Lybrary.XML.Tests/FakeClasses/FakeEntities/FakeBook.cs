using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lybrary.XML.Tests.FakeClasses.FakeEntities
{
    internal class FakeBook : BaseClass, IBase
    {
        public List<string> Authors { get; set; }

        [Required]
        public int Count { get; set; }

        public DateTime Date { get; set; }
    }
}
