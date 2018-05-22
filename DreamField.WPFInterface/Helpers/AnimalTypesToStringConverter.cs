using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DreamField.Model;

namespace DreamField.WPFInterface
{
    public class AnimalTypesToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                switch ((int)value)
                {
                    case 0:
                        return "Корова";
                    case 1:
                        return "Свинья";
                    default:
                        return "Лошадь";
                }
                //AnimalTypes animalTypes = (AnimalTypes)value;
                
                //return animalTypes.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) 
            => throw new NotImplementedException();
    }
}
