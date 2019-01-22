using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace Xamarin.Forms.Platform.AvaloniaUI.Handlers
{
    public class MethodCallSchemeHandlerFactory : CustomizedSchemeHandlerFactory
    {
        public override string SchemeName => "avalonia";
        public override string DomainName => "demo";

        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new MethodCallRequestResourceHandler();
        }
    }
}
