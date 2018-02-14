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

namespace DreamField.WPFInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RationService rtSer;
        public MainWindow()
        {
            InitializeComponent();
            rtSer = new RationService();
        }

        private void addRationButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Бла бла");

            

        }

        
    }
}
