using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    /// <summary>
    /// Represents base type of library collection's elements
    /// </summary>
    public class BaseElement
    {
        /// <summary>
        /// Gets or sets element title
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets element page count
        /// </summary>
        [Required]
        public int PageCount { get; set; }

        /// <summary>
        /// Gets or sets elements note
        /// </summary>
        public string Note { get; set; }
    }
}
