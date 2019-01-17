using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public abstract class CustomizedSchemeHandlerFactory : CefSchemeHandlerFactory
    {
        public abstract string SchemeName { get; }
        public abstract string DomainName { get; }
    }
}
