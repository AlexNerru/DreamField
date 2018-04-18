using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;
using System.Data.Entity;

namespace DreamField.DataAccessLevel.Concrete
{
    class FeedRepository : Generics.GenericRepository<Feed>, IFeedRepository
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

    }
}
