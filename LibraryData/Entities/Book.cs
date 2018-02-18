using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    /// <summary>
    /// Represents a book type in collection
    /// </summary>
    public class Book : IElement
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
        
        /// <summary>
        ///  Gets or sets authors of the book
        ///  </summary>
        [Required]
        public List<string> Authors { get; set; }

        /// <summary>
        /// Gets or sets a place of publishing (City)
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets a publishing house
        /// </summary>
        [Required]
        public string PublishingHouse { get; set; }

        /// <summary>
        /// Gets or sets a publishing year
        /// </summary>
        public int PublishingYear { get; set; }

        /// <summary>
        /// Gets or sets ISBN
        /// </summary>
        [Required]
        public string ISBN { get; set; }
    }
}
