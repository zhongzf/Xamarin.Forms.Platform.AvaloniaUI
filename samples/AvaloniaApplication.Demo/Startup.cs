﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<CustomizedSchemeHandlerFactory, MethodCallSchemeHandlerFactory>();
        }

        public void Configure(IServiceProvider serviceProvider)
        {
        }
    }
}
