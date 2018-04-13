using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamField.BusinessLogic
{
    public class AuthData
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public AuthData(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
