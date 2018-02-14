using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.DataAccessLevel.Interfaces
{
    public interface IGenericUnitOfWork:IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
       
        void SaveChanges();
    }
}
