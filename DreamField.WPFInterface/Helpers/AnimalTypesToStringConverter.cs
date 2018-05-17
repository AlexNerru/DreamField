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
                if ((int)value == 0)
                    return "Корова";
                else if ((int) value == 1)
                    return "Свинья";
                else
                    return "Лошадь";
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
