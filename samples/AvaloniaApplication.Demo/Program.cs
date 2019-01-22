using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Logging.Serilog;
using CefGlue.Avalonia;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var startup = new Startup();
            var serviceCollection = new ServiceCollection();
            startup.ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            startup.Configure(serviceProvider);

            // TODO:
            var appBuilder = BuildAvaloniaApp(args, serviceProvider);
            appBuilder.Start<MainWindow>();
        }

        public static AppBuilder BuildAvaloniaApp(string[] args, IServiceProvider serviceProvider)
                => AppBuilder.Configure<App>()
                    .UsePlatformDetect()
                    .UseSkia()
                    .ConfigureCefGlueAvalonia(args, serviceProvider)
                    .ConfigureCefGlue(args)
                    .LogToDebug();
    }
}
