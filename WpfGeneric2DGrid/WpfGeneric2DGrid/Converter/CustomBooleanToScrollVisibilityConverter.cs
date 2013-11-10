using System;
using System.Globalization;
using System.Windows.Data;
using WpfGeneric2DGrid.Model;

namespace WpfGeneric2DGrid.Converter
{
    public class ProductDetailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var prodConfig = value as Configuration;
            return string.Format(" ({0},{1},{2})", prodConfig.ScreenSize, prodConfig.RAM, prodConfig.Processor);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
