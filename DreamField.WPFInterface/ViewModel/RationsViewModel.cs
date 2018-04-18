using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using DreamField.Model;
using DreamField.ServiceLayer;

namespace DreamField.WPFInterface.ViewModel
{
    public class RationsViewModel:ViewModelBase
    {
        IRationService _rationService;
        ICustomFrameNavigationService _navigationService;

        public RelayCommand AddRationCommand { get; private set; }

        public RationsViewModel(ICustomFrameNavigationService navigationService, IRationService rationService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            AddRationCommand = new RelayCommand(AddRation);
        }

        private void AddRation()
        {
            _navigationService.NavigateTo("AddRation");
        }
    }
}
