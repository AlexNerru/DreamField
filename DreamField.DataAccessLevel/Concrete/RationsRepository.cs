using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Generics;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;

namespace DreamField.DataAccessLevel.Concrete
{
    class RationRepository:GenericRepository<Ration>, IRationRepository
    {
        public RationRepository(DbContext context): base(context) { }

        public IEnumerable<Ration> GetAllRationsAnimalType(AnimalTypes animal)
        {
            return _entities.Where(p => p.animal == animal);
        }

    }
}
