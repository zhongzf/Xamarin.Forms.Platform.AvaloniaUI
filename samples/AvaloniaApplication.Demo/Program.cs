using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using CefGlue.Avalonia;

namespace AvaloniaApplication.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            BuildAvaloniaApp(args)
                .Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp(string[] args)
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseSkia()
                .ConfigureCefGlue(args)
                .LogToDebug();
    }
}
