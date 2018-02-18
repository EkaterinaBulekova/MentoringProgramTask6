using System.ComponentModel.DataAnnotations;

namespace Lybrary.XML.Tests.FakeClasses
{
    internal class BaseClass 
    {
        [Required]
        public string Title { get; set; }

        public string Note { get; set; }
    }
}
