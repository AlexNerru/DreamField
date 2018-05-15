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
    /// <summary>
    /// Represents unit of work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork ()
        {
            _context = new DreamFieldEntities();

            FeedRepository = new FeedRepository(_context);
            FarmRepository = new FarmRepository(_context);
            RationRepository = new RationRepository(_context);
            UserRepository = new UserRepository(_context);
            
        }

        public IFeedRepository FeedRepository { get; }

        public IFarmRepository FarmRepository { get; }

        public IRationRepository RationRepository { get; }

        public IUserRepository UserRepository { get; }

        public void SaveChanges() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
