using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DreamField.ServiceLayer;
using DreamField.WPFInterface.ViewModel;
using DreamField.BusinessLogic;
using DreamField.DataAccessLevel;
using DreamField.Model;
using System.Data.Entity;

namespace DreamField.WPFInterface.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //RationService rtSer;
        //DbContext context;
        public MainWindow()
        {
            InitializeComponent();
            //context = new DreamFieldEntities();
            //rtSer = new RationService(context);
            //AddRationGrid.Visibility = Visibility.Hidden;
            //LactatingGrid.Visibility = Visibility.Hidden;
        }

        private void AddRationButton_Click(object sender, RoutedEventArgs e)
        {
            //AddRationGrid.Visibility = Visibility.Visible;
            //FeedingGrid.Visibility = Visibility.Hidden;

        }

        private void AnimalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //AnimalStateBox.Items.Clear();
            //AnimalStateBox.Visibility = Visibility.Visible;
            //foreach (var item in Enum.GetNames(typeof(CowStates)))
            //    AnimalStateBox.Items.Add(item);
        }

        
        private void AnimalStateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LactatingGrid.Visibility = Visibility.Visible;
            
        }

        private void CalculateRationButton_Click(object sender, RoutedEventArgs e)
        {
            //rtSer.rationCreator = new RationsLogic(context);
            //int id = rtSer.rationCreator.AddRationToDb(1, 1, 1, "comment");
            //double weight = double.Parse(WeightBox.Text);
            //double weightIncrement = double.Parse(WeightIncrementBox.Text);
            //double fat = double.Parse(FatBox.Text);
            //double protein = double.Parse(ProteinBox.Text);
            //double dry = double.Parse(DryFeedBox.Text);
            //double distance = double.Parse(DayDistancrBox.Text);
            //double milk = double.Parse(DailyMilkBox.Text);
            //int lact = int.Parse(LactationDayBox.Text);
            //int prenanc = int.Parse(PregnancyBox.Text);
            //CowDTO dto = new CowDTO(id, weight, weightIncrement, 18, 38.5,dry,60,fat,prenanc,milk,distance,protein,4.85,100,true,lact);
            //NormIndexGeneral nig = rtSer.rationCreator.CreateNorm(dto);
        }

        private void FeedingModule_Click(object sender, RoutedEventArgs e)
        {
            //NothingChoosenGrid.Visibility = Visibility.Hidden;
            
            
        }

        private void SettingsModule_Click(object sender, RoutedEventArgs e)
        {
            //NothingChoosenGrid.Visibility = Visibility.Hidden;
            
        }

    }
}
