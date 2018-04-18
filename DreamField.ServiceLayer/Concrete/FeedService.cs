using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;
using DreamField.DataAccessLevel;
using System.Data.Entity;
using DreamField.Model;

namespace DreamField.ServiceLayer
{
    public class FeedService : IFeedService
    {
     
        private FeedsLogic _feedsLogic;

        public FeedService()
        {
            _feedsLogic = new FeedsLogic();
        }

        public IEnumerable<Feed> GetAllFamsFeedsByID(int farmId) => _feedsLogic.GetAllFarmFeeds(farmId);
        
    }
}
