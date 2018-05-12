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
using DreamField.ServiceLayer.Dto;
using GalaSoft.MvvmLight.Messaging;
using DreamField.WPFInterface.Messages;

namespace DreamField.WPFInterface.ViewModel
{
    public class RationsViewModel : ViewModelBase
    {
        private readonly IRationService _rationService;
        private readonly ICustomFrameNavigationService _navigationService;
        private readonly IUserService _userService;

        public RelayCommand AddRationCommand { get; private set; }
        public ObservableCollection<RationDto> Rations { get; set; }

        public RationsViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            AddRationCommand = new RelayCommand(AddRation);
            Rations = new ObservableCollection<RationDto>(_rationService.GetAllRations(_userService.LoggedUser.Id));
            MessengerInstance.Register<RationCreatedMessage>(this, RationCreatedMessageHandler);
        }

        private void AddRation() => _navigationService.NavigateTo("CreateRation");

        private void RationCreatedMessageHandler(RationCreatedMessage message)
            => Rations = new ObservableCollection<RationDto>(_rationService.GetAllRations(_userService.LoggedUser.Id));
    }
}
