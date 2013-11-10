using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfGenericGrid.Converter
{
    public class CustomBooleanToScrollVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Auto" : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
