using Avalonia;
using Avalonia.Media;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class FormsTabbedPage : FormsMultiPage
	{
        public static readonly AvaloniaProperty BarBackgroundColorProperty;// = AvaloniaProperty.Register("BarBackgroundColor", typeof(Brush), typeof(FormsTabbedPage));
        public static readonly AvaloniaProperty BarTextColorProperty;// = AvaloniaProperty.Register("BarTextColor", typeof(Brush), typeof(FormsTabbedPage));

		public Brush BarBackgroundColor
		{
			get { return (Brush)GetValue(BarBackgroundColorProperty); }
			set { SetValue(BarBackgroundColorProperty, value); }
		}

		public Brush BarTextColor
		{
			get { return (Brush)GetValue(BarTextColorProperty); }
			set { SetValue(BarTextColorProperty, value); }
		}

		public FormsTabbedPage()
		{
			//this.DefaultStyleKey = typeof(FormsTabbedPage);
		}
	}
}
