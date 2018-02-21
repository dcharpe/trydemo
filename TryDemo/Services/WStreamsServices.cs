using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TryDemo.Models;

namespace TryDemo.Services
{
    public class WStreamsServices : IDisposable
    {
        private readonly mastermodelEntities _dbContext;

        public WStreamsServices()
        {
            _dbContext = new mastermodelEntities(); //STILL HAVE TO DISPOSE
        }

        public List<WSTREAM> GetAllWStreams()
        {
            return _dbContext.WSTREAMs.ToList();
        }

        public List<WSTREAM> GetWStreamsList(int teamID)
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;
           
            return _dbContext.WSTREAMs.Where(x => x.teamID == teamID).ToList();
        }
        public WSTREAM GetWStreamsById(int id)
        {
            _dbContext.Configuration.ProxyCreationEnabled = false;
            return _dbContext.WSTREAMs.SingleOrDefault(t => t.wstreamID == id);
        }

        public void Dispose()
        {
            //Cleanup Resources
            _dbContext.Dispose();
        }
    }
}