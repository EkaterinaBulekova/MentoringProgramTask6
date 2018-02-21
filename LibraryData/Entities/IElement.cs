using System.ComponentModel.DataAnnotations;

namespace LibraryData.Entities
{
    /// <summary>
    /// Represents base type of library collection's elements
    /// </summary>
    public interface IElement
    {
        /// <summary>
        /// Gets or sets element title
        /// </summary>
        [Required]
        string Title { get; set; }

        /// <summary>
        /// Gets or sets element page count
        /// </summary>
        [Required]
        int PageCount { get; set; }

        /// <summary>
        /// Gets or sets elements note
        /// </summary>
        string Note { get; set; }
    }
}
