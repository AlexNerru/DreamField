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

        IFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;

        ISnackbarMessageQueue _messageQueue;

        public ISnackbarMessageQueue MessageQueue { get; set; }

        public RelayCommand AddRationCommand { get; private set; }


        public string Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                RaisePropertyChanged("Weight");
            }
        }

        public string WeightIncrement
        {
            get { return _weightIncrement; }
            set
            {
                _weightIncrement = value;
                RaisePropertyChanged("WeightIncrement");
            }
        }

        public string Protein
        {
            get { return _protein; }
            set
            {
                _protein = value;
                RaisePropertyChanged("Protein");
            }
        }

        public string Fat
        {
            get { return _fat; }
            set
            {
                _fat = value;
                RaisePropertyChanged("Fat");
            }
        }

        public string DryFeed
        {
            get { return _dryFeed; }
            set
            {
                _dryFeed = value;
                RaisePropertyChanged("DryFeed");
            }
        }

        public string DailyMilk
        {
            get { return _dailyMilk; }
            set
            {
                _dailyMilk = value;
                RaisePropertyChanged("DailyMilk");
            }
        }

        public string DayDistance
        {
            get { return _dayDistanse; }
            set
            {
                _dayDistanse = value;
                RaisePropertyChanged("DayDistance");
            }
        }

        public string LactationDay
        {
            get { return _lactationDay; }
            set
            {
                _lactationDay = value;
                RaisePropertyChanged("LactationDay");
            }
        }

        public string PregnancyDay
        {
            get { return _pregnancyDay; }
            set
            {
                _pregnancyDay = value;
                RaisePropertyChanged("PregnancyDay");
            }
        }

        

        ///TODO: Add Back Button
        public AddRationViewModel(IFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _rationValidator = new AddRationValidator();
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            MessageQueue = new SnackbarMessageQueue();

            AddRationCommand = new RelayCommand(CreateRation);
        }

        private void CreateRation()
        {
            int rationId = 0;
            
            if (_rationValidator.Validate(this).IsValid)
            {
                rationId = _rationService.CreateRation(_userService.LoggedUser.Id, 1, 1);
                //TODO: Refactor for service validation
                CowDTO dto = new CowDTO(rationId, double.Parse(Weight), double.Parse(WeightIncrement), 24, 28,
                                    30, 80, double.Parse(Fat), int.Parse(PregnancyDay), double.Parse(DailyMilk),
                                        double.Parse(DayDistance), double.Parse(Protein), 4.85, 20, true, int.Parse(LactationDay));
                _rationService.CalculateNorm(dto);
            }
            else
                Task.Factory.StartNew(() => MessageQueue.Enqueue("Ошибка данных"));

        }













    }
}
