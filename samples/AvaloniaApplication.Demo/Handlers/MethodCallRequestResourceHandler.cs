using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xilium.CefGlue;

namespace Xamarin.Forms.Platform.AvaloniaUI.Handlers
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
            responseLength = -1;
            redirectUrl = null;

            try
            {
                var headers = response.GetHeaderMap();
                headers.Add("Access-Control-Allow-Origin", "*");
                response.SetHeaderMap(headers);

                response.Status = (int)HttpStatusCode.OK;
                response.MimeType = "application/json";
                response.StatusText = "OK";
            }
            catch (Exception exception)
            {
                response.Status = (int)HttpStatusCode.BadRequest;
                response.MimeType = "text/plain";
                response.StatusText = "Resource loading error.";

                Debug.WriteLine(exception);
            }
        }

        protected override bool ProcessRequest(CefRequest request, CefCallback callback)
        {
            Task.Run(() =>
            {
                using (callback)
                {
                    callback.Continue();
                }
            });
            return true;
        }

        protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        {
            var bytes = Encoding.UTF8.GetBytes("{ \"name\": \"C#\" }");
            response.Write(bytes, 0, bytes.Length);
            bytesRead = bytes.Length;
            return true;
        }
    }
}
