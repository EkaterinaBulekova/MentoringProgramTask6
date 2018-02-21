using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lybrary.XML.Tests.FakeClasses.FakeEntities
{
    internal class FakePatent : BaseClass, IBase
    {
        public List<string> Inventors { get; set; }

        public int Number { get; set; }

        [Required]
        public DateTime AppDate { get; set; }
    }
}
