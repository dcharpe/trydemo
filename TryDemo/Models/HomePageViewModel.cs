using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TryDemo.Models
{
    public class HomePageViewModel
    {
        public IEnumerable<TEAM> ETeams { get; set; }
        public List<TEAM> Teams { get; set; }
        public int teamID { get; set; }
        public string teamName { get; set; }


        public IEnumerable<WSTREAM> EWStreams { get; set; }
        public List<WSTREAM> WStreams { get; set; }
        public int wstreamID { get; set; }

        public List<CATEGORY> Categories { get; set; }
        public List<SUBCATEGORY> Subcategories { get; set; }
    }
}