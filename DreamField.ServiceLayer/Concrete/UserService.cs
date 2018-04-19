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
        private UserManager _userManager;
        private IUnitOfWork _unitOfWork;

        public User LoggedUser { get; set; }

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = new UserManager(_unitOfWork);
        }

        public bool Login(string login, string password)
        {
            LoggedUser = _userManager.Login(new AuthData(login, password));
            return LoggedUser != null ? true : false;

        }


    }
}
