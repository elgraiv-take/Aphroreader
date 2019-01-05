using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Elgraiv.Aphroreader.Main
{
    public class ExpanderWidthConverter : IValueConverter
    {
        private static readonly GridLength s_expanded = new GridLength(200.0);
        private static readonly GridLength s_collapsed = new GridLength(28.0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool isExpanded)
            {
                return isExpanded ? s_expanded : s_collapsed;
            }
            return GridLength.Auto;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
