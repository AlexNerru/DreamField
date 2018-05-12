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
using DreamField.WPFInterface.Helpers.Singletons;
using DreamField.WPFInterface.Helpers.Validators;
using MaterialDesignThemes.Wpf;
using GalaSoft.MvvmLight.Messaging;
using DreamField.WPFInterface.Messages;

namespace DreamField.WPFInterface.ViewModel
{
    public class AddRationStatsViewModel : ViewModelBase
    {
        private string _weight;
        private string _weightIncrement;
        private string _protein;
        private string _fat;
        private string _dryFeed;
        private string _dailyMilk;
        private string _dayDistanse;
        private string _lactationDay;
        private string _pregnancyDay;

        private readonly NumericPositivValidator _rationValidator;

        ICustomFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;


        public RelayCommand AddRationStatsCommand { get; private set; }

        #region Binding Properties

        public string Weight
        {
            get => _weight; 
            set
            {
                _weight = value;
                RaisePropertyChanged("Weight");
            }
        }

        public string WeightIncrement
        {
            get => _weightIncrement;
            set
            {
                _weightIncrement = value;
                RaisePropertyChanged("WeightIncrement");
            }
        }

        public string Protein
        {
            get => _protein; 
            set 
            {
                _protein = value;
                RaisePropertyChanged("Protein");
            }
        }

        public string Fat
        {
            get => _fat;
            set
            {
                _fat = value;
                RaisePropertyChanged("Fat");
            }
        }

        public string DryFeed
        {
            get => _dryFeed; 
            set
            {
                _dryFeed = value;
                RaisePropertyChanged("DryFeed");
            }
        }

        public string DailyMilk
        {
            get => _dailyMilk;
            set
            {
                _dailyMilk = value;
                RaisePropertyChanged("DailyMilk");
            }
        }

        public string DayDistance
        {
            get => _dayDistanse;
            set
            {
                _dayDistanse = value;
                RaisePropertyChanged("DayDistance");
            }
        }

        public string LactationDay
        {
            get => _lactationDay;
            set
            {
                _lactationDay = value;
                RaisePropertyChanged("LactationDay");
            }
        }

        public string PregnancyDay
        {
            get => _pregnancyDay; 
            set
            {
                _pregnancyDay = value;
                RaisePropertyChanged("PregnancyDay");
            }
        }

        public ISnackbarMessageQueue MessageQueue { get; set; }

        #endregion


        ///TODO: Add Back Button
        public AddRationStatsViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _rationValidator = new NumericPositivValidator();
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            MessageQueue = new SnackbarMessageQueue();

            AddRationStatsCommand = new RelayCommand(AddStatsRation);
        }

        private void AddStatsRation()
        {
            if (_rationValidator.Validate(this).IsValid)
            {
                CurrentRationSingleton rationSingleton = CurrentRationSingleton.Source;
                
                //TODO:Add fields for this
                RationStatsDto dto = new RationStatsDto(double.Parse(Weight), double.Parse(WeightIncrement), 24, 28,
                                    double.Parse(DryFeed), 80, double.Parse(Fat), int.Parse(PregnancyDay), double.Parse(DailyMilk),
                                        double.Parse(DayDistance), double.Parse(Protein), 4.85, 20, true, int.Parse(LactationDay));
                Norm norm = _rationService.Create(dto);

                Ration ration = _rationService.Add(rationSingleton.RationId, norm);

                ration = _rationService.Calculate(rationSingleton.RationId);

                Messenger.Default.Send(new RationCreatedMessage());

                _navigationService.NavigateTo("AllRations");
            }
            else
                Task.Factory.StartNew(() => MessageQueue.Enqueue("Ошибка данных"));
        }

    }
}
