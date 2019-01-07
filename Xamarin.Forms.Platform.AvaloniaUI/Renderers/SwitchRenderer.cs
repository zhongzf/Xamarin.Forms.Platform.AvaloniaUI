using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
	public class SwitchRenderer : ViewRenderer<Switch, CheckBox>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
		{
			if (e.NewElement != null)
			{
				if (Control == null) // construct and SetNativeControl and suscribe control event
				{
					SetNativeControl(new CheckBox());
                    // TODO: 
                    //Control.Checked += OnNativeToggled;
                    //Control.Unchecked += OnNativeToggled;
                }

                // Update control property 
                UpdateIsToggled();
			}

			base.OnElementChanged(e);
		}
		
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Switch.IsToggledProperty.PropertyName)
			{
				UpdateIsToggled();
			}
		}

		void UpdateIsToggled()
		{
			Control.IsChecked = Element.IsToggled;
		}

		void OnNativeToggled(object sender, RoutedEventArgs e)
		{
			((IElementController)Element).SetValueFromRenderer(Switch.IsToggledProperty, Control.IsChecked);
		}

		bool _isDisposed;

		protected override void Dispose(bool disposing)
		{
			if (_isDisposed)
				return;

			if (disposing)
			{
				if (Control != null)
				{
					//Control.Checked -= OnNativeToggled;
					//Control.Unchecked -= OnNativeToggled;
				}
			}

			_isDisposed = true;
			base.Dispose(disposing);
		}
	}
}