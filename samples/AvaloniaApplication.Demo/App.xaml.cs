using Avalonia;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication.Demo
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
