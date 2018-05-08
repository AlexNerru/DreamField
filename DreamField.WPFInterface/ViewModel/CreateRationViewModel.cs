using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using DreamField.Model;
using DreamField.ServiceLayer;
using DreamField.BusinessLogic;
using DreamField.WPFInterface.Helpers;
using System.ComponentModel;
using System.Reflection;
using MaterialDesignThemes.Wpf;



namespace DreamField.WPFInterface.ViewModel
{
    public class CreateRationViewModel : ViewModelBase
    {
        private string _comment;
        private string _roughage;
        private string _juicy;
       

        ICustomFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;

        ISnackbarMessageQueue _messageQueue;


        public RelayCommand AddRationCommand { get; private set; }


        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                RaisePropertyChanged("Comment");
            }
        }

        public string Roughage
        {
            get => _roughage;
            set
            {
                _roughage = value;
                RaisePropertyChanged("Roughage");
            }
        }

        public string Juicy
        {
            get => _juicy;
            set
            {
                _juicy = value;
                RaisePropertyChanged("Juicy");
            }
        }


        ///TODO: Add Back Button
        public CreateRationViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;

            _messageQueue = new SnackbarMessageQueue();

            AddRationCommand = new RelayCommand(CreateRation);
        }

        private void CreateRation()
        {
            

        }
    }
}
