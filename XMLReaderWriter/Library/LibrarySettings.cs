using System;
using System.Collections.Generic;

namespace XMLReaderWriter.Library
{
    /// <summary>
    /// Represents settings for Library elements and types 
    /// </summary>
    public class LibrarySettings
    {
        /// <summary>
        /// Gets or sets name of main XML element in library files
        /// </summary>
        public string MainElementName { get; set; }

        /// <summary>
        /// Gets or sets name of regular XML elements in library files
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// Create instance of LibrarySettings type with default values
        /// </summary>
        public LibrarySettings()
        {
            MainElementName = "Catalog";
            ElementName = "Element";
        }
    }
}
