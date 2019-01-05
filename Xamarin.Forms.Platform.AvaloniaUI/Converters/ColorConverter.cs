using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
	public sealed class ColorConverter : IMultiValueConverter
	{
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
			Control framework = values[0] as Control;
			AvaloniaProperty dp = parameter as AvaloniaProperty;

			if (values.Count() > 1 && framework != null && values[1] is Color && dp != null)
			{
				return framework.UpdateDependencyColor(dp, (Color)values[1]);
			}
			return Color.Transparent.ToBrush();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
