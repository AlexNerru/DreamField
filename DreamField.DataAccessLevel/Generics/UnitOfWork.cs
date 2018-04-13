using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using System.Collections;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Concrete;

namespace DreamField.DataAccessLevel.Generics
{
    public class UnitOfWork : IGenericUnitOfWork
    {
        private DbContext context;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>() {};

        public UnitOfWork (DbContext context)
        {
            this.context = context;
            repositories.Add(typeof(Ration), new RationRepository(context));
            repositories.Add(typeof(NormIndexGeneral), new NormRepository(context));
            repositories.Add(typeof(User), new UserRepository(context));
            repositories.Add(typeof(Farm), new FarmRepository(context));
            repositories.Add(typeof(Feed), new FeedRepository(context));
        }
        

        
        public IRepository<T> Repository<T>() where T : class
        {
              return repositories[typeof(T)] as IRepository<T>;
        }

        public void SaveChanges() => context.SaveChanges();

        public void Dispose() => context.Dispose();
    }
}
