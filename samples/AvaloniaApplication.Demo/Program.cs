using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Logging.Serilog;
using CefGlue.Avalonia;
using System.Linq;

namespace AvaloniaApplication.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO:
            args = args.Concat(new string[] { "disable-devtools_F12" }).ToArray();
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
