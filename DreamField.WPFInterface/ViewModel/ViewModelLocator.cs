
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using CommonServiceLocator;
using DreamField.WPFInterface;
using DreamField.ServiceLayer;
using GalaSoft.MvvmLight.Views;
using System;


namespace DreamField.WPFInterface.ViewModel
{ 
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SetupNavigation();
            SimpleIoc.Default.Register<IFeedService, FeedService>();
            SimpleIoc.Default.Register<IUserService, UserService>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<FeedsViewModel>();
        }

        public MainViewModel Main
        {
            get => ServiceLocator.Current.GetInstance<MainViewModel>();
        }

        public FeedsViewModel Feeds
        {
            get => ServiceLocator.Current.GetInstance<FeedsViewModel>();
        }

        public LoginViewModel Login
        {
            get => ServiceLocator.Current.GetInstance<LoginViewModel>();
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Empty", new Uri("../Pages/EmptyContentPage.xaml", UriKind.Relative));
            navigationService.Configure("Feeds", new Uri("../Pages/FeedsPage.xaml", UriKind.Relative));
            navigationService.Configure("Rations", new Uri("../Pages/RationsPage.xaml", UriKind.Relative));
            navigationService.Configure("Settings", new Uri("../Pages/SettingsPage.xaml", UriKind.Relative));
            navigationService.Configure("Login", new Uri("../Pages/LoginPage.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }

        public static void Cleanup()
        {
            if (SimpleIoc.Default.IsRegistered<MainViewModel>())
                SimpleIoc.Default.Unregister<MainViewModel>();
            if (SimpleIoc.Default.IsRegistered<FeedsViewModel>())
                SimpleIoc.Default.Unregister<FeedsViewModel>();
            if (SimpleIoc.Default.IsRegistered<LoginViewModel>())
                SimpleIoc.Default.Unregister<LoginViewModel>();
            // TODO Clear the ViewModels
        }
    }
}