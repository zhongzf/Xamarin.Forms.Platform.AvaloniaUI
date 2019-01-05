using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public sealed class CollapseWhenEmptyConverter : Avalonia.Data.Converters.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //var length = 0;

            //string s = value as string;
            //if (s != null)
            //	length = s.Length;

            //if (value is int)
            //	length = (int)value;

            //return length > 0 ? Visibility.Visible : Visibility.Collapsed;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
