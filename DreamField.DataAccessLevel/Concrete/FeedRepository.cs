using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.DataAccessLevel.Generics;
using System.Data.Entity;

namespace DreamField.DataAccessLevel.Concrete
{
    internal class FeedRepository : GenericRepository<Feed>, IFeedRepository
    {
        public FeedRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Gets all feeds owned by farm with provided type
        /// </summary>
        /// <param name="farm">Farm that own feeds</param>
        /// <param name="type">Type of feed <see cref="FeedTypes"/></param>
        /// <returns>IEnumerable of required feeds</returns>
        public IEnumerable<Feed> GetFeedsByType(Farm farm, FeedTypes type) => 
            _entities.Where((feed) => feed.farm_id == farm.id && feed.type == type);

        /// <summary>
        /// Gets all feeds belongs to farm
        /// </summary>
        /// <param name="farmId">Id of farm</param>
        /// <returns>IEnumerable of all feeds</returns>
        public IEnumerable<Feed> GetFarmFeeds(int farmId) => _entities.Where(feed => feed.farm_id == farmId);

    }
}
