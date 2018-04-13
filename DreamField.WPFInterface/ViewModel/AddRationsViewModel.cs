using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using DreamField.Model;
using DreamField.ServiceLayer;

namespace DreamField.WPFInterface.ViewModel
{
    public class AddRationViewModel:ViewModelBase
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
        IFrameNavigationService _navigationService;
        IRationService _rationService;
        IUserService _userService;


        public string Weight
        {
            get { return _weight; }
            set { _weight = value;
                RaisePropertyChanged("Weight");
            }
        }

        public string WeightIncrement
        {
            get { return _weightIncrement; }
            set { _weightIncrement = value;
                RaisePropertyChanged("WeightIncrement");
            }
        }

        public string Protein
        {
            get { return _protein; }
            set { _protein = value;
                RaisePropertyChanged("Protein");
            }
        }

        public string Fat
        {
            get { return _fat; }
            set { _fat = value;
                RaisePropertyChanged("Fat");
            }
        }

        public string DryFeed
        {
            get { return _dryFeed; }
            set { _dryFeed = value;
                RaisePropertyChanged("DryFeed");
            }
        }

        public string DailyMilk
        {
            get { return _dailyMilk; }
            set { _dailyMilk = value;
                RaisePropertyChanged("DailyMilk");
            }
        }

        public string DayDistance
        {
            get { return _dayDistanse; }
            set { _dayDistanse = value;
                RaisePropertyChanged("DayDistance");
            }
        }

        public string LactationDay
        {
            get { return _lactationDay; }
            set { _lactationDay = value;
                RaisePropertyChanged("LactationDay");
            }
        }

        public string PregnancyDay
        {
            get { return _pregnancyDay; }
            set { _pregnancyDay = value;
                RaisePropertyChanged("PregnancyDay");
            }
        }

        ///TODO: Add Back Button
        public AddRationViewModel(IFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
        }








    }
}
