using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TryDemo.Models;

namespace TryDemo.Services
{
    public class SubcategoryServices
    {
        private readonly mastermodelEntities _dbContext;

        public SubcategoryServices()
        {
            _dbContext = new mastermodelEntities(); //STILL HAVE TO DISPOSE
        }

        public List<SUBCATEGORY> GetAllSubCategories()
        {

            return _dbContext.SUBCATEGORies.ToList();
        }

        public List<SUBCATEGORY> GetSubCategoriesList(int categID)
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            return _dbContext.SUBCATEGORies.Where(x => x.categID == categID).ToList();
        }
        public SUBCATEGORY GetSubCategoryById(int id)
        {
            return _dbContext.SUBCATEGORies.SingleOrDefault(t => t.subcategID == id);
        }

        public void Dispose()
        {
            //Cleanup Resources
            _dbContext.Dispose();
        }
    }
}