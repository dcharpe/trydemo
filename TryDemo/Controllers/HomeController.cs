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

       
        public ActionResult Index()
        {
            var teams = _teamServices.GetAllTeams();
            var wstreams = _wstreamServices.GetAllWStreams();
            var categories = _categoryServices.GetAllCategories();
            //var subCategories = _subCategoryServices.GetAllSubCategories();

            var model = new HomePageViewModel
            {
                Teams = teams,
                WStreams = wstreams,
                Categories = categories,
                //Subcategories = subcategories
            };

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
        public JsonResult ChangeCategory(int teamID)
        {
            List<WSTREAM> wstreamList = _wstreamServices.GetWStreamsList(teamID);
           
            return Json(wstreamList, JsonRequestBehavior.AllowGet);
        }

        
    }
}