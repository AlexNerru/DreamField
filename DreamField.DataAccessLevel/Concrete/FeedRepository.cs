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
    }
}
