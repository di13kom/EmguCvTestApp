using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EmguWpfApp
{
    public class SyncConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            bool retVal = !(bool)value;
            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //throw new NotImplementedException();
            return (bool)values[0] == true ? values[1] : values[2];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] obj = new object[3];
            obj[0] = (bool)true;
            obj[1] = (bool)true;
            obj[2] = (bool)true;
            return obj;
            //throw new NotImplementedException();
        }
    }
}
