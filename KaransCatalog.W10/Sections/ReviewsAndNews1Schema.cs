using System;
using AppStudio.DataProviders;

namespace KaransCatalog.Sections
{
    /// <summary>
    /// Implementation of the ReviewsAndNews1Schema class.
    /// </summary>
    public class ReviewsAndNews1Schema : SchemaBase
    {

        public string Device { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }
    }
}
