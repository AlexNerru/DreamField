using System;

namespace DreamField.DataAccessLevel.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<T> Repository<T>() where T : class;
       
        void SaveChanges();
    }
}
