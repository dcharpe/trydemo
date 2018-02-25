using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TryDemo.Models
{
    public class CascadingModel
    {
        public CascadingModel()
        {
            this.TEAMS = new List<SelectListItem>();
            this.WSTREAMS = new List<SelectListItem>();
            this.CATEGORIES = new List<SelectListItem>();
            this.SUBCATEGORIES = new List<SelectListItem>();
            this.AGOALS = new List<SelectListItem>();
        }

        public List<SelectListItem> TEAMS { get; set; }
        public List<SelectListItem> WSTREAMS { get; set; }
        public List<SelectListItem> CATEGORIES { get; set; }
        public List<SelectListItem> SUBCATEGORIES { get; set; }
        public List<SelectListItem> AGOALS { get; set; }

        public int teamID { get; set; }
        public int wstreamID { get; set; }
        public int categID { get; set; }
        public int subcategID { get; set; }
        public int agoalID { get; set; }
    }
}