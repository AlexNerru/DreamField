using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamField.Model;
using DreamField.BusinessLogic;

namespace DreamField.ServiceLayer
{
    public class UserService : IUserService
    {
        private UserManager _userManager;

        public User LoggedUser { get; set; }

        public UserService() => _userManager = new UserManager();

        public bool Login (string login, string password)
        {
            LoggedUser = _userManager.Login(new AuthData(login, password));
            if (LoggedUser != null)
                return true;
            else
                return false;
        }
    }
}
