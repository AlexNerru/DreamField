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

        public FeedService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Feed> GetAllFarmsFeedsByID(int farmId) => _unitOfWork.FeedRepository.GetFarmFeeds(farmId);


        
    }
}
