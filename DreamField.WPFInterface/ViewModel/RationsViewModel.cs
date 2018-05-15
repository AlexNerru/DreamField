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

        private ObservableCollection<RationInfoDto> _rations;

        public RelayCommand AddRationCommand { get; private set; }
        public RelayCommand<int> DeleteRationCommand { get; private set; }
        public ObservableCollection<RationInfoDto> Rations
        {
            get => _rations;
            set
            {
                _rations = value;
                RaisePropertyChanged("Rations");
            }
        }

        public RationsViewModel(ICustomFrameNavigationService navigationService,
            IRationService rationService, IUserService userService)
        {
            _navigationService = navigationService;
            _rationService = rationService;
            _userService = userService;
            AddRationCommand = new RelayCommand(AddRation);
            DeleteRationCommand = new RelayCommand<int>(DeleteRation);
            Rations = new ObservableCollection<RationInfoDto>(_rationService.GetAllRations(_userService.LoggedUser.Id));
            MessengerInstance.Register<UpdateRationsMessage>(this, RationCreatedMessageHandler);
        }

        private void AddRation() => _navigationService.NavigateTo("CreateRation");

        private void DeleteRation(int rationId)
        {
            _rationService.DeleteRation(new RationDeleteDto { Id = rationId });
            Rations.Remove(Rations.FirstOrDefault(ration => ration.Id==rationId));
        }

        //Переделать сообщение на перенос типа Ration
        private void RationCreatedMessageHandler(UpdateRationsMessage message)
            => Rations = new ObservableCollection<RationInfoDto>(_rationService.GetAllRations(_userService.LoggedUser.Id));
    }
}
