using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;

namespace DreamField.DataAccessLevel.Interfaces
{
    public interface IFeedRepository : IRepository<Feed>
    {
        IEnumerable<Feed> GetFeedsByType(Farm farm, FeedTypes type);

        IEnumerable<Feed> GetFarmFeeds(int farmId);
    }
}
