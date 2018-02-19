using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;
using DreamField.DataAccessLevel.Generics;
using DreamField.DataAccessLevel.Interfaces;
using System.Data.Entity;

namespace DreamField.DataAccessLevel.Concrete
{
    public class NormRepository : GenericRepository<NormIndexGeneral>, INormRepository
    {
        public NormRepository(DbContext context) : base(context) { }

        

    }
}
