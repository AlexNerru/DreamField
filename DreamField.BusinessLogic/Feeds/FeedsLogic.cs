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
        private IGenericUnitOfWork _unitOfWork;
        
        public FeedsLogic(DbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public IEnumerable<Feed> GetAllFarmFeeds(int farmId) => _unitOfWork.Repository<Feed>().GetAll().Where(feed => feed.farm_id == farmId);

    }
}
