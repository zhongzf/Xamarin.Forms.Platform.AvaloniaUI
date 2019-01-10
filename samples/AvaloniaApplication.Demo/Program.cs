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
            BuildAvaloniaApp()
                .ConfigureCefGlue(args)
                .Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .UseSkia()
                .LogToDebug();
    }
}
