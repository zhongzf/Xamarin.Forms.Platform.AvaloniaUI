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
        /// <summary>
        /// The file read in bytes.
        /// </summary>
        private byte[] mFileBytes;

        /// <summary>
        /// The mime type.
        /// </summary>
        private string mMime;

        /// <summary>
        /// The completed flag.
        /// </summary>
        private bool mCompleted;

        /// <summary>
        /// The total bytes read.
        /// </summary>
        private int mTotalBytesRead;

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
            var u = new Uri(request.Url);
            var postData = request.PostData;
            if (postData != null)
            {
                var elements = postData.GetElements();
                if (elements?.Length > 0 && elements[0].ElementType == CefPostDataElementType.Bytes)
                {
                    var element = elements[0];
                    var bytes = element.GetBytes();
                    var postValue = Encoding.UTF8.GetString(bytes);
                    Console.WriteLine(postValue);
                }
                Console.WriteLine(elements?.Length);
            }
            var file = u.Authority + u.AbsolutePath;

            mTotalBytesRead = 0;
            mFileBytes = null;
            mCompleted = false;

            Task.Run(() =>
            {
                using (callback)
                {
                    try
                    {
                        mFileBytes = Encoding.UTF8.GetBytes("{ \"name\" : \"test\",  \"age\" : 10 }");
                        mMime = "application/json";
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception);
                    }
                    finally
                    {
                        callback.Continue();
                    }
                }
            });

            return true;
        }

        protected override bool ReadResponse(Stream response, int bytesToRead, out int bytesRead, CefCallback callback)
        {
            int currBytesRead = 0;

            try
            {
                if (mCompleted)
                {
                    bytesRead = 0;
                    mTotalBytesRead = 0;
                    mFileBytes = null;
                    return false;
                }
                else
                {
                    if (mFileBytes != null)
                    {
                        currBytesRead = Math.Min(mFileBytes.Length - mTotalBytesRead, bytesToRead);
                        response.Write(mFileBytes, mTotalBytesRead, currBytesRead);
                        mTotalBytesRead += currBytesRead;

                        if (mTotalBytesRead >= mFileBytes.Length)
                        {
                            mCompleted = true;
                        }
                    }
                    else
                    {
                        bytesRead = 0;
                        mCompleted = true;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            bytesRead = currBytesRead;
            return true;
        }
    }
}
