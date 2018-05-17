using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DreamField.WPFInterface
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Task.Factory.StartNew(WordInizialiser.Inizialize);
        }
    }

    public static class WordInizialiser
    {

        public static Microsoft.Office.Interop.Word.Application Word { get; set; }

        public static void Inizialize()
        {
            Microsoft.Office.Interop.Word.Application winword =
                new Microsoft.Office.Interop.Word.Application
                {
                    ShowAnimation = false,
                    Visible = false
                };
            Word = winword;
        }
    }
}
