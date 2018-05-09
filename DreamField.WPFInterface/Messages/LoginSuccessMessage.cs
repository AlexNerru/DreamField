using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;

namespace DreamField.WPFInterface
{
    public class LoginSuccessMessage : MessageBase
    {
        public string UserName { get; set; }
        public LoginSuccessMessage(string userNmae) => UserName = userNmae;
    }
}
