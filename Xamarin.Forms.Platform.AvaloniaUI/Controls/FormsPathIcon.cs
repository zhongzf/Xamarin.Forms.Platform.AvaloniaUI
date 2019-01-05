using Avalonia;
using Avalonia.Media;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class FormsPathIcon : FormsElementIcon
	{
        public static readonly AvaloniaProperty DataProperty;// = AvaloniaProperty.Register("Data", typeof(Geometry), typeof(FormsPathIcon));

		public Geometry Data
		{
			get { return (Geometry)GetValue(DataProperty); }
			set { SetValue(DataProperty, value); }
		}

		public FormsPathIcon()
		{
			//this.DefaultStyleKey = typeof(FormsPathIcon);
		}
	}
}
