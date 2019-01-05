using System;
using System.Globalization;
using Xamarin.Forms.Platform.AvaloniaUI.Enums;

namespace Xamarin.Forms.Platform.AvaloniaUI.Converters
{
	public class SymbolToValueConverter : Avalonia.Data.Converters.IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Symbol symbol)
				return Char.ConvertFromUtf32((int)symbol);

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
