using System;
using System.Globalization;
using System.Windows.Data;

namespace Lib.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class DownHeight : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double data = (double)value;
            return data - 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
