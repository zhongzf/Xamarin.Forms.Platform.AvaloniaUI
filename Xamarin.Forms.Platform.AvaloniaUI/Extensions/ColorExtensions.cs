using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;

namespace Xamarin.Forms.Platform.AvaloniaUI
{ 
	public static class ColorExtensions
	{
		public static Brush ToBrush(this Color color)
		{
			return new SolidColorBrush(color.ToMediaColor());
		}

		public static Avalonia.Media.Color ToMediaColor(this Color color)
		{
			return Avalonia.Media.Color.FromArgb((byte)(color.A * 255), (byte)(color.R * 255), (byte)(color.G * 255), (byte)(color.B * 255));
		}
	}
}
