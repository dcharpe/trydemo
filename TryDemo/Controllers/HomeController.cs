using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryDemo.Models;
using TryDemo.Services;

namespace TryDemo.Controllers
{
    public class HomeController : Controller
    {

        private readonly TeamsServices _teamServices;
        private readonly WStreamsServices _wstreamServices;
        private readonly CategoryServices _categoryServices;
        //private readonly SubCategoryServices _subCategoryServices;


        public HomeController()
        {
            _teamServices = new TeamsServices();
            _wstreamServices = new WStreamsServices();
            _categoryServices = new CategoryServices();
            //_subCategoryServices = new SubCategoryServices();
        }


        //public ActionResult Index()
        //{
        //    var teams = _teamServices.GetAllTeams();
        //    var wstreams = _wstreamServices.GetAllWStreams();
        //    var categories = _categoryServices.GetAllCategories();
        //    //var subCategories = _subCategoryServices.GetAllSubCategories();

        //    var model = new HomePageViewModel
        //    {
        //        Teams = teams,
        //        WStreams = wstreams,
        //        Categories = categories,
        //        //Subcategories = subcategories
        //    };

        //    return View(model);
        //}

        public ActionResult Index()
        {
            var teams = _teamServices.GetAllTeams();
            var wstreams = _wstreamServices.GetAllWStreams();
            var categories = _categoryServices.GetAllCategories();
            List<SelectListItem> tmNames = new List<SelectListItem>();
            HomePageViewModel modal = new HomePageViewModel();

            List<TEAM> teamList = _teamServices.GetAllTeams().ToList();
            teamList.ForEach(x =>
            {
                tmNames.Add(new SelectListItem { Text = x.teamName, Value = x.teamID.ToString() });
            });
            var model = new HomePageViewModel
            {
                Teams = teams,
                WStreams = wstreams,
                Categories = categories,
                //Subcategories = subcategories
            };

            return View(model);
        }
        public ActionResult Changes(string teamID)
        {

            int wstreamsID;
            List<SelectListItem> wsNames = new List<SelectListItem>();
            if(!string.IsNullOrEmpty(teamID))
            {
                wstreamsID = Convert.ToInt32(teamID);
                List<WSTREAM> wstreamList = _wstreamServices.GetWStreamsList(wstreamsID);
                wstreamList.ForEach(x =>
                {
                    wsNames.Add(new SelectListItem { Text = x.wstreamName, Value = x.wstreamID.ToString() });
                });
            }

          
            return Json(wsNames, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult About()
        {
            return View();
        }
        

    }
}