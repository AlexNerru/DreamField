using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Concrete;

namespace DreamField.DataAccessLevel.Generics
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context;
        private Dictionary<Type, object> repositories = new Dictionary<Type, object>() {};

        private Dictionary<Type, object> interfaces = new Dictionary<Type, object>() { };

        IFeedRepository feedRepository;

        public UnitOfWork ()
        {
            this._context = new DreamFieldEntities();
            //TODO: think about good dependecy injection
            repositories.Add(typeof(Ration), new RationRepository(_context));
            repositories.Add(typeof(Norm), new NormRepository(_context));
            repositories.Add(typeof(User), new UserRepository(_context));
            repositories.Add(typeof(Farm), new FarmRepository(_context));
            repositories.Add(typeof(Feed), new FeedRepository(_context));

            
            
        }
        
        public IRepository<T> Repository<T>() where T : class => repositories[typeof(T)] as IRepository<T>;

        public IFeedRepository FeedRepository => feedRepository;

        public void SaveChanges() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
