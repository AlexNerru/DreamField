using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;

namespace DreamField.DataAccessLevel.Concrete
{
    class FarmRepository : Generics.GenericRepository<Farm>, IFarmRepository
    {
        public FarmRepository(DbContext context) : base(context) { }
    }
}
