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
    public class AddRationViewModel : ViewModelBase
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
        private readonly AddRationValidator _rationValidator;

        ICustomFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;

        ISnackbarMessageQueue _messageQueue;


        public RelayCommand AddRationCommand { get; private set; }


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

        

        ///TODO: Add Back Button
        public AddRationViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _rationValidator = new AddRationValidator();
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            _messageQueue = new SnackbarMessageQueue();

            AddRationCommand = new RelayCommand(CreateRation);
        }

        private void CreateRation()
        {
            if (_rationValidator.Validate(this).IsValid)
            {
                Ration ration = _rationService.Create(_userService.LoggedUser.Id, 1, 0);
                
                //TODO: Refactor for service validation
                CowDTO dto = new CowDTO(double.Parse(Weight), double.Parse(WeightIncrement), 24, 28,
                                    double.Parse(DryFeed), 80, double.Parse(Fat), int.Parse(PregnancyDay), double.Parse(DailyMilk),
                                        double.Parse(DayDistance), double.Parse(Protein), 4.85, 20, true, int.Parse(LactationDay));
                Norm norm = _rationService.CalculateNorm(ration, dto);
                _rationService.Calculate(norm, new RationStructure(0.25, 0.5, 0.25));

                foreach (var item in ration.RationFeeds)
                    System.Console.WriteLine($"{item.Feed.name} {item.amount}");
            }
            else
                Task.Factory.StartNew(() => _messageQueue.Enqueue("Ошибка данных"));

        }
    }
}
