using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class FormsAppBar : ContentControl
	{
		ToggleButton btnMore;

        public static readonly AvaloniaProperty PrimaryCommandsProperty; //= AvaloniaProperty.Register("PrimaryCommands", typeof(IEnumerable<Control>), typeof(FormsAppBar), new PropertyMetadata(new List<Control>()));
        public static readonly AvaloniaProperty SecondaryCommandsProperty; //= AvaloniaProperty.Register("SecondaryCommands", typeof(IEnumerable<Control>), typeof(FormsAppBar), new PropertyMetadata(new List<Control>()));

        public IEnumerable<Control> PrimaryCommands
        {
            get { return (IEnumerable<Control>)GetValue(PrimaryCommandsProperty); }
            set { SetValue(PrimaryCommandsProperty, value); }
        }

        public IEnumerable<Control> SecondaryCommands
        {
            get { return (IEnumerable<Control>)GetValue(SecondaryCommandsProperty); }
            set { SetValue(SecondaryCommandsProperty, value); }
        }

        public void Reset()
		{
			if (btnMore != null)
			{
				btnMore.IsChecked = false;
			}
		}
	}
}
