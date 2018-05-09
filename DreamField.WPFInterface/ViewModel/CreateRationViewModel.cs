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
using GalaSoft.MvvmLight.Messaging;
using DreamField.WPFInterface.Messages;



namespace DreamField.WPFInterface.ViewModel
{
    public class CreateRationViewModel : ViewModelBase
    {
        #region private fields

        private string _comment;
        private string _roughage;
        private string _juicy;

        #endregion


        ICustomFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;

        ISnackbarMessageQueue _messageQueue;

        CreateRationValidator validator = new CreateRationValidator();


        public RelayCommand CreateRationCommand { get; private set; }

        #region Binding Properties

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

        #endregion


        ///TODO: Add Back Button
        public CreateRationViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;

            _messageQueue = new SnackbarMessageQueue();

            CreateRationCommand = new RelayCommand(CreateRation);
        }

        private void CreateRation()
        {
            if (validator.Validate(this).IsValid)
            {
                //TODO: Add farms and animal type change
                Ration ration = _rationService.Create(_userService.LoggedUser.Id, 1, AnimalTypes.Cow, Comment);

                RationStructure rationStructure = new RationStructure();
                rationStructure.roughage = double.Parse(Roughage);
                rationStructure.juicy_food = double.Parse(Juicy);
                rationStructure.concentrates = 1 - (rationStructure.roughage + rationStructure.juicy_food);
                _rationService.Add(ration.Id, rationStructure);

                CurrentRationSingleton rationSingleton = CurrentRationSingleton.Source;
                rationSingleton.RationId = ration.Id;

                _navigationService.NavigateTo("AddRationStats");
            }
            else
                Task.Factory.StartNew(() => _messageQueue.Enqueue("Ошибка данных"));

        }
    }
}
