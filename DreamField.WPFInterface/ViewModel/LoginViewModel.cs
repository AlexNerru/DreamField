using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using DreamField.Model;
using DreamField.ServiceLayer;
using GalaSoft.MvvmLight.Messaging;


namespace DreamField.WPFInterface
{
    public class LoginViewModel : ViewModelBase
    {
        private IUserService _userService;
        public RelayCommand<Window> LoginCommand { get; private set; }
        private string _login;
        private string _password;

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }



        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand<Window>(this.LoginUser);
        }

        //TODO: Check if user logged in
        //TODO: Check wrong password
        private void LoginUser(Window window)
        {
            if (_userService.Login(Login, Password))
            {
                Messenger.Default.Send(new LoginSuccessMessage(_userService.LoggedUser.Name));
                Window loginWindow = GetWindowRef("Login_Window");
                loginWindow.Close();
                Window mainWindow = Application.Current.MainWindow;
                mainWindow.Show();
            }
        }

        public static Window GetWindowRef(string WindowName)
        {
            Window retVal = null;
            foreach (Window window in Application.Current.Windows)
                if (window.Name.Trim().ToLower() == WindowName.Trim().ToLower())
                {
                    retVal = window;
                    break;
                }
            return retVal;
        }



    }
}
