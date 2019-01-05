using System;
using Avalonia;
using Avalonia.Layout;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
	internal static class AlignmentExtensions
	{
		internal static Avalonia.Media.TextAlignment ToNativeTextAlignment(this TextAlignment alignment)
		{
			switch (alignment)
			{
				case TextAlignment.Center:
					return Avalonia.Media.TextAlignment.Center;
				case TextAlignment.End:
					return Avalonia.Media.TextAlignment.Right;
				default:
					return Avalonia.Media.TextAlignment.Left;
			}
		}

		internal static VerticalAlignment ToNativeVerticalAlignment(this LayoutOptions alignment)
		{
			switch (alignment.Alignment)
			{
				case LayoutAlignment.Start:
					return VerticalAlignment.Top;
				case LayoutAlignment.Center:
					return VerticalAlignment.Center;
				case LayoutAlignment.End:
					return VerticalAlignment.Bottom;
				case LayoutAlignment.Fill:
					return VerticalAlignment.Stretch;
				default:
					return VerticalAlignment.Stretch;
			}
		}

		internal static HorizontalAlignment ToNativeHorizontalAlignment(this LayoutOptions alignment)
		{
			switch (alignment.Alignment)
			{
				case LayoutAlignment.Start:
					return HorizontalAlignment.Left;
				case LayoutAlignment.Center:
					return HorizontalAlignment.Center;
				case LayoutAlignment.End:
					return HorizontalAlignment.Right;
				case LayoutAlignment.Fill:
					return HorizontalAlignment.Stretch;
				default:
					return HorizontalAlignment.Stretch;
			}
		}
	}
}
