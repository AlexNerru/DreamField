﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using DreamField.Model;
using DreamField.ServiceLayer;
using GalaSoft.MvvmLight.Messaging;
using DreamField.WPFInterface.Messages;

namespace DreamField.WPFInterface.ViewModel
{
    public class RationsViewModel : ViewModelBase
    {
        IRationService _rationService;
        ICustomFrameNavigationService _navigationService;
        IUserService _userService;

        public RelayCommand AddRationCommand { get; private set; }
        public ObservableCollection<Ration> Rations { get; set; }

        public RationsViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            AddRationCommand = new RelayCommand(AddRation);
            Rations = new ObservableCollection<Ration>(_rationService.GetAllRations(_userService.LoggedUser.Id));
            MessengerInstance.Register<RationCreatedMessage>(this, RationCreatedMessageHandler);
        }

        private void AddRation() => _navigationService.NavigateTo("CreateRation");

        private void RationCreatedMessageHandler(RationCreatedMessage message)
            => Rations = new ObservableCollection<Ration>(_rationService.GetAllRations(_userService.LoggedUser.Id));
    }
}
