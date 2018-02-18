using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    /// <summary>
    /// Represents a patent type of library
    /// </summary>
    public class Patent : IElement
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
        /// Gets or sets inventors of the patent
        /// </summary>
        [Required]
        public List<string> Inventors { get; set; }

        /// <summary>
        /// Gets or sets country of invent
        /// </summary>
        [Required]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets registration number of patent
        /// </summary>
        [Required]
        public int RegistrationNumber { get; set; }

        /// <summary>
        /// Gets or sets application date
        /// </summary>
        public DateTime ApplicationDate { get; set; }

        /// <summary>
        /// Gets or sets publishing date
        /// </summary>
        public DateTime PublishingDate { get; set; }
    }
}
