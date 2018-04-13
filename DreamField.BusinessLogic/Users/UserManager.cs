using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.DataAccessLevel.Generics;
using System.Data.Entity;
using DreamField.Model;
using DreamField.DataAccessLevel;

namespace DreamField.BusinessLogic
{
    public class UserManager
    {
        private IGenericUnitOfWork _unitOfWork;

        public UserManager() => _unitOfWork = new UnitOfWork(new DreamFieldEntities());

        public UserManager(IGenericUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public User Login(AuthData data)
        {
            if (this.isUserValid(data))
                return _unitOfWork.Repository<User>()
                    .Find(user => user.Login == data.Login && user.Password == data.Password).First();
            else
                return null;
        }

        private bool isUserValid(AuthData data) => _unitOfWork.Repository<User>()
                                                    .GetAll()
                                                    .Any(savedUser => savedUser.Login == data.Login 
                                                                        && savedUser.Password == data.Password);



 
}
}
