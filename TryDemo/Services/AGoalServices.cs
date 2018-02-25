using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TryDemo.Models;

namespace TryDemo.Services
{
    public class AGoalServices
    {
        private readonly mastermodelEntities _dbContext;

        public AGoalServices()
        {
            _dbContext = new mastermodelEntities(); //STILL HAVE TO DISPOSE
        }

        public List<AGOAL> GetAllSubCategories()
        {

            return _dbContext.AGOALs.ToList();
        }

        public List<AGOAL> GetAGoalsList(int subcategID)
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;

            return _dbContext.AGOALs.Where(x => x.subcategID == subcategID).ToList();
        }
        public AGOAL GetAGoalsById(int id)
        {
            return _dbContext.AGOALs.SingleOrDefault(t => t.agoalID == id);
        }

        public void Dispose()
        {
            //Cleanup Resources
            _dbContext.Dispose();
        }
    }
}