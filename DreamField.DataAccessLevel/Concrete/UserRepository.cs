using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Generics;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel.Interfaces;

namespace DreamField.DataAccessLevel.Concrete
{
    internal class UserRepository: Generics.GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context) { }
    }
}
