using System;
using AppStudio.DataProviders;

namespace KaransCatalog.Sections
{
    /// <summary>
    /// Implementation of the Xbox1Schema class.
    /// </summary>
    public class Xbox1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }
    }
}
