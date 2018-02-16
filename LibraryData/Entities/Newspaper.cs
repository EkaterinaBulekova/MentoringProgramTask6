using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    /// <summary>
    /// Represents a newspaper type of library
    /// </summary>
    public class Newspaper : BaseElement
    {
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
        /// Gets or sets number of newspaper
        /// </summary>
        [Required]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets date of newspaper's publishing
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets ISBN
        /// </summary>
        [Required]
        public string ISSN { get; set; }
    }
}
