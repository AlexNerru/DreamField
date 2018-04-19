using System.Collections.Generic;
using DreamField.Model;

namespace DreamField.ServiceLayer
{
    public interface IFeedService
    {
        IEnumerable<Feed> GetAllFarmsFeedsByID(int farmId);
    }
}