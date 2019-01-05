using Avalonia;
using Xamarin.Forms.Platform.AvaloniaUI.Enums;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class FormsSymbolIcon : FormsElementIcon
	{
        public static readonly AvaloniaProperty SymbolProperty;// = AvaloniaProperty.Register("Symbol", typeof(Symbol), typeof(FormsSymbolIcon));

		public Symbol Symbol
		{
			get { return (Symbol)GetValue(SymbolProperty); }
			set { SetValue(SymbolProperty, value); }
		}

		public FormsSymbolIcon()
		{
			//this.DefaultStyleKey = typeof(FormsSymbolIcon);
		}
	}
}
