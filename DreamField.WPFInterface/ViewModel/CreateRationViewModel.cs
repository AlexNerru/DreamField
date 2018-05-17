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
using System.Linq;
using System.Reflection;
using DreamField.WPFInterface.Helpers.Singletons;
using DreamField.WPFInterface.Helpers.Validators;
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
        private string _consentrates;

        #endregion

        ICustomFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;

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

        public string Consentrates
        {
            get => _consentrates;
            set
            {
                _consentrates = value;
                RaisePropertyChanged("Consentrates");
            }
        }

        public ISnackbarMessageQueue MessageQueue { get; set; }

        #endregion


        ///TODO: Add Back Button
        public CreateRationViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;

            MessageQueue = new SnackbarMessageQueue();

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
                rationStructure.concentrates = double.Parse(Consentrates);
                _rationService.Add(ration.Id, rationStructure);

                CurrentRationSingleton rationSingleton = CurrentRationSingleton.Source;
                rationSingleton.RationId = ration.Id;

                _navigationService.NavigateTo("AddRationStats");
            }
            else
                Task.Factory.StartNew(() => MessageQueue.Enqueue(validator.Validate(this).Errors.FirstOrDefault()));

        }
    }
}
