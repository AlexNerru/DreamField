using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.BusinessLogic;
using DreamField.DataAccessLevel;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;

namespace DreamField.ServiceLayer
{
    public class FeedService : IFeedService
    {
        private IUnitOfWork _unitOfWork;
        private FeedsLogic _feedsLogic;

        public FeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _feedsLogic = new FeedsLogic(_unitOfWork);
        }

        public IEnumerable<Feed> GetAllFarmsFeedsByID(int farmId) => _feedsLogic.GetAllFarmFeeds(farmId);


        
    }
}
