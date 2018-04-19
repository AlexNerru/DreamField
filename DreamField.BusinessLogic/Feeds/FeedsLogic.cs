using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.DataAccessLevel.Generics;
using System.Data.Entity;
using DreamField.Model;

namespace DreamField.BusinessLogic
{
    public class FeedsLogic
    {
        private IUnitOfWork _unitOfWork;

        public FeedsLogic(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IEnumerable<Feed> GetAllFarmFeeds(int farmId) => _unitOfWork.FeedRepository.Find(feed => feed.farm_id == farmId);

    }
}
