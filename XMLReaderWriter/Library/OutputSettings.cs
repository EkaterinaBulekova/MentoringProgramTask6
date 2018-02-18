namespace XMLReaderWriter.Library
{
    /// <summary>
    /// Represents settings for right output attribute in main element of new library's catalog
    /// </summary>
    public class OutputSettings
    {
        /// <summary>
        /// Gets or sets catalog's name attribute's value
        /// </summary>
        public string CatalogName { get; set; }

        /// <summary>
        /// Gets or sets catalog's number attribute's value
        /// </summary>
        public int CatalogNumber { get; set; }

        /// <summary>
        /// Create instance of OutputSettings type with default values
        /// </summary>
        public OutputSettings()
        {
            CatalogName = "First catalog";
            CatalogNumber = 1;
        }
    }
}
