using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using System.Collections;
using System.Data.Entity;

namespace DreamField.DataAccessLevel.Generics
{
    public class UnitOfWork : IGenericUnitOfWork
    {
        private DbContext context;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public UnitOfWork (DbContext context)
        {
            this.context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }

            IRepository<T> repo = new GenericRepository<T>(context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
