using System;
using System.Globalization;
using System.Windows.Data;

namespace Lib.Converters
{
    public class StringToProperties : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string data = (string)value;
            if (data != null)
                return (data.Length >= 1) ? data[0] + "." : "";
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
