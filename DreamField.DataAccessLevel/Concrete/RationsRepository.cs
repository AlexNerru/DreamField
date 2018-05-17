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
    public class RationRepository: GenericRepository<Ration>, IRationRepository
    {
        public RationRepository(DbContext context): base(context) { }

        //rework
        public IEnumerable<Ration> GetAllRationsByAnimalType(AnimalTypes animal)
        {
            return _entities.Where(p => p.Animal == (int)animal);
        }

        public IEnumerable<Ration> GetUserRations(int userId) => _entities.Where(ration => ration.Author_id == userId);
    }
}
