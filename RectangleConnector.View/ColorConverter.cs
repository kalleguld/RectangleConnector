using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RectangleConnector.View
{
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sourceColorN = value as ViewModel.Color?;
            if (!sourceColorN.HasValue)
            {
                throw new NotImplementedException();
            }
            var sc = sourceColorN.Value;
            return new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(sc.R, sc.G, sc.B));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
