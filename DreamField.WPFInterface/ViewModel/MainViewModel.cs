using GalaSoft.MvvmLight;
using DreamField.WPFInterface;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using DreamField.ServiceLayer;
using System;

namespace DreamField.WPFInterface.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private string _userName;

        public string Title { get; set; }
        IFrameNavigationService _navigationService;
        IUserService _userService;
        public RelayCommand OpenRationsCommand { get; private set; }
        public RelayCommand CloseWindowCommand { get; private set; }
        public RelayCommand EmptyCommand { get; private set; }
        public RelayCommand SettingsCommand { get; private set; }
        public RelayCommand FeedsCommand { get; private set; }
        public RelayCommand LoginCommand { get; private set; }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value;
                RaisePropertyChanged("UserName");
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IFrameNavigationService navigationService, IUserService userService)
        {
            _navigationService = navigationService;
            _userService = userService;
            if (IsInDesignMode)
                Title = "DreamField (Design Mode)";
            else
                Title = "DreamField";

            CloseWindowCommand = new RelayCommand(CloseWindow);
            OpenRationsCommand = new RelayCommand(OpenRations);
            EmptyCommand = new RelayCommand(EmptyPage);
            FeedsCommand = new RelayCommand(FeedsPage);
            LoginCommand = new RelayCommand(LoginPage);
            MessengerInstance.Register<LoginSuccessMessage>(this, OnLoginSuccessMessage);
        }

        private void CloseWindow() => Application.Current.MainWindow.Close();
        
        private void OpenRations() => _navigationService.NavigateTo("Rations");

        private void EmptyPage() => _navigationService.NavigateTo("Empty");

        private void FeedsPage() => _navigationService.NavigateTo("Feeds");

        private void LoginPage() => _navigationService.NavigateTo("Login");

        private void OnLoginSuccessMessage(LoginSuccessMessage message)
        {
            this.UserName = message.UserName;
            _navigationService.NavigateTo("Empty");
        }
    }
}