using System;
using AppStudio.DataProviders;

namespace KaransCatalog.Sections
{
    /// <summary>
    /// Implementation of the Devices1Schema class.
    /// </summary>
    public class Devices1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }
    }
}
