using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.Model;


namespace DreamField.DataAccessLevel.Interfaces
{
    public interface IRationRepository:IRepository<Ration>
    {
        IEnumerable<Ration> GetAllRationsAnimalType(AnimalTypes animal);

        IEnumerable<Ration> GetUserRations(int userId);
    }
}
