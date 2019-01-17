using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public class MethodCallRequestResourceHandler : CefResourceHandler
    {
        protected override void Cancel()
        {
        }

        protected override bool CanGetCookie(CefCookie cookie)
        {
            return true;
        }

        protected override bool CanSetCookie(CefCookie cookie)
        {
            return true;
        }

        protected override void GetResponseHeaders(CefResponse response, out long responseLength, out string redirectUrl)
        {
            responseLength = 0;
            redirectUrl = null;
        }

        protected override bool ProcessRequest(CefRequest request, CefCallback callback)
        {
            return true;
        }

        protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        {
            bytesRead = 0;
            return true;
        }
    }
}
