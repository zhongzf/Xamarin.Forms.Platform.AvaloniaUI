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
            args = args.Concat(new string[] { "disable-devtools_F12" }).ToArray();
            var appBuilder = BuildAvaloniaApp(args, serviceProvider);
            RegisterSchemeHandlers(serviceProvider);
            appBuilder.Start<MainWindow>();
        }

        private static void RegisterSchemeHandlers(IServiceProvider serviceProvider)
        {
            var schemeHandlerFactories = serviceProvider.GetServices<CustomizedSchemeHandlerFactory>();
            foreach (var schemeHandlerFactory in schemeHandlerFactories)
            {
                CefRuntime.RegisterSchemeHandlerFactory(schemeHandlerFactory.SchemeName, schemeHandlerFactory.DomainName, schemeHandlerFactory);
            }
        }

        public static AppBuilder BuildAvaloniaApp(string[] args, IServiceProvider serviceProvider)
                => AppBuilder.Configure<App>()
                    .UsePlatformDetect()
                    .UseSkia()
                    .ConfigureAvaloniaCefGlue(args, serviceProvider)
                    .LogToDebug();
    }
}
