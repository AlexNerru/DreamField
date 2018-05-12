
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using DreamField.WPFInterface;
using DreamField.ServiceLayer;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using DreamField.DataAccessLevel.Interfaces;
using DreamField.DataAccessLevel.Generics;
using DreamField.ServiceLayer.Concrete;


namespace DreamField.WPFInterface.ViewModel
{ 
    /// <summary>
    /// Provides IOC registration of viewmodels and services
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
           
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SetupNavigation();

            SimpleIoc.Default.Register<IUnitOfWork, UnitOfWork>();

            SimpleIoc.Default.Register<IFeedService, FeedService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<IRationService, RationService>();
            

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<FeedsViewModel>();
            SimpleIoc.Default.Register<AddRationStatsViewModel>();
            SimpleIoc.Default.Register<RationsViewModel>();
            SimpleIoc.Default.Register<CreateRationViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public FeedsViewModel Feeds => ServiceLocator.Current.GetInstance<FeedsViewModel>();

        public LoginViewModel Login => ServiceLocator.Current.GetInstance<LoginViewModel>();

        public AddRationStatsViewModel AddRationStats => ServiceLocator.Current.GetInstance<AddRationStatsViewModel>();

        public RationsViewModel Rations => ServiceLocator.Current.GetInstance<RationsViewModel>();

        public CreateRationViewModel CreateRation => ServiceLocator.Current.GetInstance<CreateRationViewModel>();

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Empty", new Uri("../Pages/EmptyContentPage.xaml", UriKind.Relative));
            navigationService.Configure("Feeds", new Uri("../Pages/FeedsPage.xaml", UriKind.Relative));
            navigationService.Configure("AddRationStats", new Uri("../Pages/Rations/AddRationStatsPage.xaml", UriKind.Relative));
            navigationService.Configure("AllRations", new Uri("../Pages/Rations/AllRationsPage.xaml", UriKind.Relative));
            navigationService.Configure("CreateRation", new Uri("../Pages/Rations/CreateRationPage.xaml", UriKind.Relative));
            navigationService.Configure("Settings", new Uri("../Pages/SettingsPage.xaml", UriKind.Relative));
            navigationService.Configure("Login", new Uri("../Pages/LoginPage.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<ICustomFrameNavigationService>(() => navigationService);
        }

        public static void Cleanup()
        {
            if (SimpleIoc.Default.IsRegistered<MainViewModel>())
                SimpleIoc.Default.Unregister<MainViewModel>();
            if (SimpleIoc.Default.IsRegistered<FeedsViewModel>())
                SimpleIoc.Default.Unregister<FeedsViewModel>();
            if (SimpleIoc.Default.IsRegistered<LoginViewModel>())
                SimpleIoc.Default.Unregister<LoginViewModel>();
            if (SimpleIoc.Default.IsRegistered<AddRationStatsViewModel>())
                SimpleIoc.Default.Unregister<AddRationStatsViewModel>();
            if (SimpleIoc.Default.IsRegistered<RationsViewModel>())
                SimpleIoc.Default.Unregister<RationsViewModel>();
            if (SimpleIoc.Default.IsRegistered<CreateRationViewModel>())
                SimpleIoc.Default.Unregister<CreateRationViewModel>();
        }
    }
}