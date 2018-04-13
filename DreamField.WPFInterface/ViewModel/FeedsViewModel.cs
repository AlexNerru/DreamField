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
    public class FeedsViewModel:ViewModelBase
    {
        IFeedService _feedService;

        public ObservableCollection<Feed> Feeds { get; set; }

        public string Title { get; set; }

        public FeedsViewModel(IFeedService feedService)
        {
            Title = "sdfsdfsdfs";
            _feedService = feedService;
            Feeds = new ObservableCollection<Feed>(_feedService.GetAllFamsFeedsByID(1));
        }
    }
}
