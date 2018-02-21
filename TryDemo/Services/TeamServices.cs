using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TryDemo.Models;

namespace TryDemo.Services
{
    public class TeamsServices : IDisposable
    {
        private readonly mastermodelEntities _dbContext;

        public TeamsServices()
        {
            _dbContext = new mastermodelEntities(); //STILL HAVE TO DISPOSE
            
        }

        public List<TEAM> GetAllTeams()
        {
            
            return _dbContext.TEAMs.ToList();
        }

        public TEAM GetTeamById(int id)
        {
            return _dbContext.TEAMs.SingleOrDefault(t => t.teamID == id);
        }

        public void Dispose()
        {
            //Cleanup Resources
            _dbContext.Dispose();
        }
    }
}