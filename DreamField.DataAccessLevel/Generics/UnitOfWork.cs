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

        IFeedRepository feedRepository;
        IFarmRepository farmRepository;
        INormRepository normRepository;
        IRationRepository rationRepository;
        IUserRepository userRepository;

        public UnitOfWork ()
        {
            _context = new DreamFieldEntities();

            feedRepository = new FeedRepository(_context);
            farmRepository = new FarmRepository(_context);
            normRepository = new NormRepository(_context);
            rationRepository = new RationRepository(_context);
            userRepository = new UserRepository(_context);
            
        }

        public IFeedRepository FeedRepository => feedRepository;

        public IFarmRepository FarmRepository => farmRepository;

        public INormRepository NormRepository => normRepository;

        public IRationRepository RationRepository => rationRepository;

        public IUserRepository UserRepository => userRepository;

        public void SaveChanges() => _context.SaveChanges();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
