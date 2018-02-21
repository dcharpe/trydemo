using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TryDemo.Models
{
    public class HomePageViewModel
    {
        public List<TEAM> Teams { get; set; }
        public List<WSTREAM> WStreams { get; set; }
        public List<CATEGORY> Categories { get; set; }
        public List<SUBCATEGORY> Subcategories { get; set; }
    }
}