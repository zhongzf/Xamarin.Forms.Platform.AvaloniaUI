using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.AvaloniaUI;

namespace Xamarin.Forms
{
    public static class Forms
    {
        public static bool IsInitialized { get; private set; }

        public static void Init(IEnumerable<Assembly> rendererAssemblies = null)
        {
            if (IsInitialized)
                return;

            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

            Log.Listeners.Add(new DelegateLogListener((c, m) => Console.WriteLine("[{0}] {1}", m, c)));
            Registrar.ExtraAssemblies = rendererAssemblies?.ToArray();

            Device.SetTargetIdiom(TargetIdiom.Desktop);
            Device.PlatformServices = new AvaloniaPlatformServices();
            Device.Info = new AvaloniaUIDeviceInfo();
            ExpressionSearch.Default = new AvaloniaUIExpressionSearch();

            Registrar.RegisterAll(new[] { typeof(ExportRendererAttribute), typeof(ExportCellAttribute), typeof(ExportImageSourceHandlerAttribute) });

            Ticker.SetDefault(new AvaloniaUITicker());
            Device.SetIdiom(TargetIdiom.Desktop);

            IsInitialized = true;
        }
    }
}
