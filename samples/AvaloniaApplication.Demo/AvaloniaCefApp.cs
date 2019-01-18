using CefGlue.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public class AvaloniaCefApp : CefApp
    {
        private readonly IServiceProvider _serviceProvider;

        public AvaloniaCefApp(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            if (string.IsNullOrEmpty(processType))
            {
                commandLine.AppendSwitch("disable-gpu");
                commandLine.AppendSwitch("disable-software-rasterizer");
                commandLine.AppendSwitch("disable-gpu-compositing");
                commandLine.AppendSwitch("enable-begin-frame-scheduling");
                commandLine.AppendSwitch("disable-smooth-scrolling");
            }
        }

        private CefBrowserProcessHandler _browserProcessHandler;

        protected override CefBrowserProcessHandler GetBrowserProcessHandler()
        {
            if (_browserProcessHandler == null)
            {
                _browserProcessHandler = new AvaloniaCefBrowserProcessHandler();
            }

            return _browserProcessHandler;
        }

        protected override CefRenderProcessHandler GetRenderProcessHandler()
        {
            return new AvaloniaCefRenderProcessHandler();
        }

        /// <summary>
        /// Check if scheme is a standard type.
        /// </summary>
        /// <param name="scheme">
        /// The scheme.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsStandardScheme(string scheme)
        {
            if (string.IsNullOrEmpty(scheme))
            {
                return false;
            }

            switch (scheme.ToLower())
            {
                case "http":
                case "https":
                case "file":
                case "ftp":
                case "about":
                case "data":
                    return true;
            }

            return false;
        }


        protected override void OnRegisterCustomSchemes(CefSchemeRegistrar registrar)
        {
            foreach (var item in _serviceProvider.GetServices<CustomizedSchemeHandlerFactory>())
            {
                bool isStandardScheme = IsStandardScheme(item.SchemeName);
                if (!isStandardScheme)
                {
                    registrar.AddCustomScheme(item.SchemeName, true, false, false, false, true, false);
                }
            }
        }
    }
}
