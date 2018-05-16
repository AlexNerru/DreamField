using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;
using DreamField.BusinessLogic;
using DreamField.DataAccessLevel.Interfaces;

namespace DreamField.ServiceLayer
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;

        public User LoggedUser { get; set; }

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Login(string login, string password)
        {
            User userData = _unitOfWork.UserRepository.GetUserByLoginPassword(login, password);
            if (userData != null)
            {
                LoggedUser = userData;
                return true;
            }
            return false;
        }

    }
}
