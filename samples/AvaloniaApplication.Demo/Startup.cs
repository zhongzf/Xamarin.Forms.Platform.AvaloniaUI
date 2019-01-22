using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.AvaloniaUI.Handlers;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CustomizedSchemeHandlerFactory, MethodCallSchemeHandlerFactory>();
            services.AddSingleton<CustomizedSchemeHandlerFactory, CefGlueResourceSchemeHandlerFactory>();
        }

        public void Configure(IServiceProvider serviceProvider)
        {
        }
    }
}
