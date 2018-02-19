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
using DreamField.BusinessLogic;
using DreamField.DataAccessLevel;
using DreamField.Model;
using System.Data.Entity;

namespace DreamField.WPFInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RationService rtSer;
        DbContext context;
        public MainWindow()
        {
            InitializeComponent();
            context = new DreamFieldEntities();
            rtSer = new RationService(context);
            AddRationGrid.Visibility = Visibility.Hidden;
            LactatingGrid.Visibility = Visibility.Hidden;
        }

        private void AddRationButton_Click(object sender, RoutedEventArgs e)
        {
            AddRationGrid.Visibility = Visibility.Visible;
            FeedingGrid.Visibility = Visibility.Hidden;

        }

        private void AnimalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AnimalStateBox.Items.Clear();
            AnimalStateBox.Visibility = Visibility.Visible;
            foreach (var item in Enum.GetNames(typeof(CowStates)))
                AnimalStateBox.Items.Add(item);
        }

        private void AddTatijgjonButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(context.Set<Ration>().ToList()[0].Comment);
            //MessageBox.Show("Бла бла");
            //MilkCowFactorial mcf = new MilkCowFactorial(1, 2, 3, 4, 5, 6);
            //NormIndexLactating nil = mcf.CreateNorm();
            //Console.WriteLine("hgjgjhgjjlhjh" + nil.AcidDetergentFiber);
            //context.Set<NormIndexLactating>().Add(nil);
            //context.SaveChanges();
        }

        private void AnimalStateBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LactatingGrid.Visibility = Visibility.Visible;
            
        }

        private void CalculateRationButton_Click(object sender, RoutedEventArgs e)
        {
            rtSer.rationCreator = new RationCreator(context);
            NormIndexLactating nil = rtSer.rationCreator.CreateRation(new LactatingDTO(2.25, 2.56, 24, 8));
            MessageBox.Show(nil.ExcahngeEnergy.ToString());
        }

        private void FeedingModule_Click(object sender, RoutedEventArgs e)
        {
            NothingChoosenGrid.Visibility = Visibility.Hidden;
            FeedingGrid.Visibility = Visibility.Visible;
            SettingsGrid.Visibility = Visibility.Hidden;
            
        }

        private void SettingsModule_Click(object sender, RoutedEventArgs e)
        {
            NothingChoosenGrid.Visibility = Visibility.Hidden;
            FeedingGrid.Visibility = Visibility.Hidden;
            SettingsGrid.Visibility = Visibility.Visible;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
