using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TryDemo.Models;
using TryDemo.Services;
using System.Data;
using System.Text;

namespace TryDemo.Controllers
{
    public class HomeController : Controller
    {

        private readonly TeamsServices _teamServices;
        private readonly WStreamsServices _wstreamServices;
        private readonly CategoryServices _categoryServices;
        private readonly SubcategoryServices _subCategoryServices;
        private readonly AGoalServices _agoalServices;


        public HomeController()
        {
            _teamServices = new TeamsServices();
            _wstreamServices = new WStreamsServices();
            _categoryServices = new CategoryServices();
            _subCategoryServices = new SubcategoryServices();
            _agoalServices = new AGoalServices();
        }

        public ActionResult Index()
        {
            var teams = _teamServices.GetAllTeams();
            var wstreams = _wstreamServices.GetAllWStreams();
            var categories = _categoryServices.GetAllCategories();
            var subcategories = _subCategoryServices.GetAllSubCategories();

            List<SelectListItem> tmNames = new List<SelectListItem>();
            HomePageViewModel modal = new HomePageViewModel();

            List<TEAM> teamList = _teamServices.GetAllTeams().ToList();
            teamList.ForEach(x =>
            {
                tmNames.Add(new SelectListItem { Text = x.teamName, Value = x.teamID.ToString() });
            });

            List<SelectListItem> wsNames = new List<SelectListItem>();
            List<WSTREAM> wstreamList = _wstreamServices.GetAllWStreams().ToList();
            wstreamList.ForEach(x =>
            {
                wsNames.Add(new SelectListItem { Text = x.wstreamName, Value = x.wstreamID.ToString() });
            });

            var model = new HomePageViewModel
            {
                Teams = teams,
                WStreams = wstreams,
                Categories = categories,
                Subcategories = subcategories
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

        public ActionResult Changing(string wstreamID)
        {

            int categoryID;
            List<SelectListItem> cyNames = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(wstreamID))
            {
                categoryID = Convert.ToInt32(wstreamID);
                List<CATEGORY> categoryList = _categoryServices.GetCategoriesList(categoryID);
                categoryList.ForEach(x =>
                {
                    cyNames.Add(new SelectListItem { Text = x.categName, Value = x.categID.ToString() });
                });
            }

            return Json(cyNames, JsonRequestBehavior.AllowGet); ;
        }

        public ActionResult About()
        {
            return View();
        }

        public static DataTable GetDatabaseSummary()
        {
            DataTable dt = new DataTable("GoalSummary");
            //string query = "Select Vehicletype,str(count(Vehicletype)* 100.0 / (Select Count(*) From VehicleMaster), 5,1) as percentage ";
            //query += "from VehicleMaster group by Vehicletype";
            string query = "SELECT COUNT(*) FROM AGOAL";
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            SqlConnection con = new SqlConnection();
            //con.ConnectionString = "Data Source=.;" + "Initial Catalog=Transport;" + "Persist Security Info=True;";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(dt);
            con.Close();
            return dt;
        }

        [HttpGet]
        public JsonResult GoalSummary()
        {
            List<ChartModel> lstSummary = new List<ChartModel>();

            foreach (DataRow dr in GetDatabaseSummary().Rows)
            {
                ChartModel summary = new ChartModel();
                summary.agoalID = Convert.ToInt32(dr[0]);
                summary.agoalValue = Convert.ToInt32(dr[1]);
                lstSummary.Add(summary);

            }
            return Json(lstSummary.ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Contact()
        {
            CascadingModel model = new CascadingModel();
            model.TEAMS = PopulateDropDown("SELECT teamID, teamName FROM TEAM", "teamName", "teamID");
            return View(model);
        }

        [HttpPost]
        public JsonResult AjaxMethod(string type, int value)
        {
            CascadingModel model = new CascadingModel();
            switch (type)
            {
                case "teamID":
                    model.WSTREAMS = PopulateDropDown("SELECT wstreamID, wstreamName FROM WSTREAM WHERE teamID = " + value, "wstreamName", "wstreamID");
                    break;
                case "wstreamID":
                    model.CATEGORIES = PopulateDropDown("SELECT categID, categName FROM CATEGORY WHERE wstreamID = " + value, "categName", "categID");
                    break;
                case "categID":
                    model.SUBCATEGORIES = PopulateDropDown("SELECT subcategID, subcategName FROM SUBCATEGORY WHERE categID = " + value, "subcategName", "subcategID");
                    break;
                case "subcategID":
                    model.AGOALS = PopulateDropDown("SELECT agoalID, agoalValue FROM AGOAL WHERE subcategID = " + value, "agoalValue", "agoalID");
                    break;

            }
            return Json(model);
        }

        [HttpPost]
        public ActionResult Contact(int teamID, int wstreamID, int categID, int subcategID)
        {
            CascadingModel model = new CascadingModel();
            model.TEAMS = PopulateDropDown("SELECT teamID, teamName FROM TEAM", "teamName", "teamID");
            model.WSTREAMS = PopulateDropDown("SELECT wstreamID, wstreamName FROM WSTREAM WHERE teamID = " + teamID, "wstreamName", "wstreamID");
            model.CATEGORIES = PopulateDropDown("SELECT categID, categName FROM CATEGORY WHERE wstreamID = " + wstreamID, "categName", "categID");
            model.SUBCATEGORIES = PopulateDropDown("SELECT subcategID, subcategName FROM SUBCATEGORY WHERE categID = " + categID, "subcategName", "subcategID");
            model.AGOALS = PopulateDropDown("SELECT agoalID, agoalValue FROM AGOAL WHERE subcategID = " + subcategID, "agoalValue", "agoalID");
            return View(model);
        }

        private static List<SelectListItem> PopulateDropDown(string query, string textColumn, string valueColumn)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
            // Constring
            // mastermodelEntities
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Text = sdr[textColumn].ToString(),
                                Value = sdr[valueColumn].ToString()
                            });
                        }
                    }
                    con.Close();
                }
            }

            return items;
        }
    }

}
