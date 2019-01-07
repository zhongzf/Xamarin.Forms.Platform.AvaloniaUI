using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public sealed class ViewToRendererConverter : Avalonia.Data.Converters.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            VisualElement visualElement = value as VisualElement;
            if (visualElement != null)
            {
                var Control = Platform.GetOrCreateRenderer(visualElement)?.GetNativeElement();

                if (Control != null)
                {
                    Control.AttachedToLogicalTree += (sender, e) =>
                    {
                        visualElement.Layout(new Rectangle(0, 0, Control.Bounds.Width, Control.Bounds.Height));
                    };

                    Control.LayoutUpdated += (sender, e) =>
                    {
                        visualElement.Layout(new Rectangle(0, 0, Control.Bounds.Width, Control.Bounds.Height));
                    };

                    return Control;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
