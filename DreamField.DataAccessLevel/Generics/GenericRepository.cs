using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using System.Data.Entity;
using System.Linq.Expressions;


namespace DreamField.DataAccessLevel.Generics
{
    class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public TEntity GetById(int id) => _entities.Find(id);
        
        public IEnumerable<TEntity> GetAll() => _entities.ToList();
        
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) => _entities.Where(predicate);
        
        public void Add(TEntity entity) => _entities.Add(entity);
   
        public void AddRange(IEnumerable<TEntity> entities) => _entities.AddRange(entities);

        public void Update (TEntity entity) => _context.Entry(entity).State = EntityState.Modified;
  
        public void Delete(TEntity entity) => _entities.Remove(entity);
        
        public void DeleteRange(IEnumerable<TEntity> entities) => _entities.RemoveRange(entities);
       
    }
}
