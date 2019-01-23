using Avalonia.Controls;
using CefGlue.Avalonia;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.AvaloniaUI.Handlers;
using Xilium.CefGlue;
using Xamarin.Forms.Platform.AvaloniaUI;

namespace AvaloniaApplication.Demo
{
    public static class AvaloniaCefGlueApplicationExtensions
    {
        public static T ConfigureCefGlueAvalonia<T>(this T builder, string[] args, IServiceProvider serviceProvider) where T : AppBuilderBase<T>, new()
        {
            Platform.WebKitInitialized += AvaloniaCefBrowser_WebKitInitialized;
            Platform.RegisterCustomSchemes += (sender, e) => AvaloniaCefBrowser_RegisterCustomSchemes(sender, e, serviceProvider);
            Platform.BrowserCreated += (sender, e) => Platform_BrowserCreated(sender, e, serviceProvider);
            return builder;
        }

        private static void Platform_BrowserCreated(object sender, BrowserCreatedEventArgs e, IServiceProvider serviceProvider)
        {
            foreach (var item in serviceProvider.GetServices<CustomizedSchemeHandlerFactory>())
            {
                bool isStandardScheme = IsStandardScheme(item.SchemeName);
                if (!isStandardScheme)
                {
                    CefRuntime.RegisterSchemeHandlerFactory(item.SchemeName, item.DomainName, item);
                }
            }
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

        private static void AvaloniaCefBrowser_RegisterCustomSchemes(object sender, RegisterCustomSchemesEventArgs e, IServiceProvider serviceProvider)
        {
            var registrar = e.Registrar;
            foreach (var item in serviceProvider.GetServices<CustomizedSchemeHandlerFactory>())
            {
                bool isStandardScheme = IsStandardScheme(item.SchemeName);
                if (!isStandardScheme)
                {
                    registrar.AddCustomScheme(item.SchemeName, true, false, false, false, true);
                }
            }
        }

        private static void AvaloniaCefBrowser_WebKitInitialized(object sender, EventArgs e)
        {
            var cefV8Handler = new AvaloniaCefV8Handler();

            const string javascriptCode = @"function avalonia() { }

                if (!avalonia) avalonia = { };

                (function() {

                    avalonia.__defineGetter__('myParam',

                    function() {

                        native function GetMyParam();

                        return GetMyParam();

                    });

                    avalonia.__defineSetter__('myParam',

                    function(arg0) {

                        native function SetMyParam(arg0);

                        SetMyParam(arg0);

                    });

                    avalonia.myFunction = function(arg0, arg1, arg2) {

                        native function MyFunction(arg0, arg1, arg2);

                        return MyFunction(arg0, arg1, arg2);

                    };

                    avalonia.getMyParam = function() {

                        native function GetMyParam();

                        return GetMyParam();

                    };

                    avalonia.setMyParam = function(arg0) {

                        native function SetMyParam(arg0);

                        SetMyParam(arg0);

                    };

                })();";

            CefRuntime.RegisterExtension("avalonia", javascriptCode, cefV8Handler);
        }
    }
}
