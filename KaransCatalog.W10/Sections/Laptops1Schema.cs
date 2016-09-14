using System;
using AppStudio.DataProviders;

namespace KaransCatalog.Sections
{
    /// <summary>
    /// Implementation of the Laptops1Schema class.
    /// </summary>
    public class Laptops1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
