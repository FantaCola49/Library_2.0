using System;
using System.Globalization;
using System.Windows.Data;

namespace Lib.Converters
{
    class FormatJanre : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is Books) && ((Books)value).Genres != null)
                return ((Books)value).Genres.Name;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
