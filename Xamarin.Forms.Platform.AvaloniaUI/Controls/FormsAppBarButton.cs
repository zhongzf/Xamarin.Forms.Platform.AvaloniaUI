using Avalonia;
using Avalonia.Controls;


namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
    public class FormsAppBarButton : Avalonia.Controls.Button
    {
        public static readonly AvaloniaProperty IconProperty;// = AvaloniaProperty.Register("Icon", typeof(FormsElementIcon), typeof(FormsAppBarButton));
        public static readonly AvaloniaProperty LabelProperty;// = AvaloniaProperty.Register("Label", typeof(string), typeof(FormsAppBarButton));

        public FormsElementIcon Icon
        {
            get { return (FormsElementIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public FormsAppBarButton()
        {
        }
    }
}
