using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Xamarin.Forms.Platform.AvaloniaUI;

namespace AvaloniaApplication.Demo
{
    public class MainWindow : FormsApplicationPage
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            Xamarin.Forms.Forms.Init();
            LoadApplication(new Xamarin.Forms.Sample.App());
            //LoadApplication(new Xamarin.Forms.Controls.SimpleApp());
            //LoadApplication(new Xamarin.Forms.Controls.App());
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
