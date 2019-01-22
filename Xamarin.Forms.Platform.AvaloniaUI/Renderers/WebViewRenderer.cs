using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using CefGlue.Avalonia;
using Xamarin.Forms.Internals;
using Xilium.CefGlue;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class WebViewRenderer : ViewRenderer<WebView, AvaloniaCefBrowser>, IWebViewDelegate
    {
        WebNavigationEvent _eventState;
        bool _updating;

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            if (e.OldElement != null) // Clear old element event
            {
                e.OldElement.EvalRequested -= OnEvalRequested;
                e.OldElement.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
                e.OldElement.GoBackRequested -= OnGoBackRequested;
                e.OldElement.GoForwardRequested -= OnGoForwardRequested;
                e.OldElement.ReloadRequested -= OnReloadRequested;
            }

            if (e.NewElement != null)
            {
                if (Control == null) // construct and SetNativeControl and suscribe control event
                {
                    SetNativeControl(new AvaloniaCefBrowser());
                    Control.LoadStart += Control_LoadStart;
                    Control.LoadEnd += Control_LoadEnd;
                    Control.BrowserCreated += Control_BrowserCreated;
                }

                // Update control property 
                Load();

                // Suscribe element event
                Element.EvalRequested += OnEvalRequested;
                Element.EvaluateJavaScriptRequested += OnEvaluateJavaScriptRequested;
                Element.GoBackRequested += OnGoBackRequested;
                Element.GoForwardRequested += OnGoForwardRequested;
                Element.ReloadRequested += OnReloadRequested;
            }

            base.OnElementChanged(e);
        }


        private void Control_BrowserCreated(object sender, BrowserCreatedEventArgs e)
        {
            Platform.OnBrowserCreated(sender, e);
        }

        private void Control_LoadStart(object sender, LoadStartEventArgs e)
        {
            if (e.Frame.Url == null) return;

            string url = e.Frame.Url;
            var args = new WebNavigatingEventArgs(_eventState, new UrlWebViewSource { Url = url }, url);

            Element.SendNavigating(args);

            //navigatingEventArgs.Cancel = args.Cancel;

            // reset in this case because this is the last event we will get
            if (args.Cancel)
                _eventState = WebNavigationEvent.NewPage;
        }

        private void Control_LoadEnd(object sender, LoadEndEventArgs e)
        {
            string url = e.Frame.Url;
            SendNavigated(new UrlWebViewSource { Url = url }, _eventState, WebNavigationResult.Success);
            UpdateCanGoBackForward();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == WebView.SourceProperty.PropertyName)
            {
                if (!_updating)
                    Load();
            }
        }

        void Load()
        {
            if (Element.Source != null)
                Element.Source.Load(this);

            UpdateCanGoBackForward();
        }

        public void LoadHtml(string html, string baseUrl)
        {
            if (html == null)
                return;

            Control.Browser.GetMainFrame().LoadString(html, baseUrl);
        }

        public void LoadUrl(string url)
        {
            if (url == null)
                return;

            //Control.Source = new Uri(url, UriKind.RelativeOrAbsolute);
            if (Control.Browser != null && Control.Browser?.GetMainFrame() != null)
            {
                Control.Browser.GetMainFrame().LoadUrl(url);
            }
            else
            {
                Control.StartUrl = url;
            }
        }


        void OnEvalRequested(object sender, EvalRequested eventArg)
        {
            //Control.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Control.InvokeScript("eval", eventArg.Script)));
            //Control.Browser.GetMainFrame().ExecuteJavaScript(eventArg.Script, Control.StartUrl, 0);
            //var context = frame.V8Context;
            //context.Enter();
            //context.TryEval(eventArg.Script, string.Empty, 0, out CefV8Value returnValue, out CefV8Exception exception);
            //context.Exit();
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                var browser = Control.Browser;
                var frame = browser.GetMainFrame();
                frame.ExecuteJavaScript(eventArg.Script, frame.Url, 0);
            });
        }

        async Task<string> OnEvaluateJavaScriptRequested(string script)
        {
            var tcr = new TaskCompletionSource<string>();
            var task = tcr.Task;

            Device.BeginInvokeOnMainThread(() =>
            {
                // TODO: 
                //tcr.SetResult((string)Control.InvokeScript("eval", new[] { script }));
            });

            return await task.ConfigureAwait(false);
        }

        void OnGoBackRequested(object sender, EventArgs eventArgs)
        {
            if (Control.Browser.CanGoBack)
            {
                _eventState = WebNavigationEvent.Back;
                Control.Browser.GoBack();
            }
            UpdateCanGoBackForward();
        }

        void OnGoForwardRequested(object sender, EventArgs eventArgs)
        {
            if (Control.Browser.CanGoForward)
            {
                _eventState = WebNavigationEvent.Forward;
                Control.Browser.GoForward();
            }
            UpdateCanGoBackForward();
        }

        void OnReloadRequested(object sender, EventArgs eventArgs)
        {
            Control.Browser.Reload();
        }

        void SendNavigated(UrlWebViewSource source, WebNavigationEvent evnt, WebNavigationResult result)
        {
            Console.WriteLine("SendNavigated : " + source.Url);
            _updating = true;
            ((IElementController)Element).SetValueFromRenderer(WebView.SourceProperty, source);
            _updating = false;

            Element.SendNavigated(new WebNavigatedEventArgs(evnt, source, source.Url, result));

            UpdateCanGoBackForward();
            _eventState = WebNavigationEvent.NewPage;
        }

        void UpdateCanGoBackForward()
        {
            ((IWebViewController)Element).CanGoBack = Control.Browser?.CanGoBack ?? false;
            ((IWebViewController)Element).CanGoForward = Control.Browser?.CanGoForward ?? false;
        }


        bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (Control != null)
                {
                    Control.BrowserCreated -= Control_BrowserCreated;
                    Control.LoadStart -= Control_LoadStart;
                    Control.LoadEnd -= Control_LoadEnd;
                    Control.StartUrl = null;
                    Control.Browser.Dispose();
                }

                if (Element != null)
                {
                    Element.EvalRequested -= OnEvalRequested;
                    Element.EvaluateJavaScriptRequested -= OnEvaluateJavaScriptRequested;
                    Element.GoBackRequested -= OnGoBackRequested;
                    Element.GoForwardRequested -= OnGoForwardRequested;
                    Element.ReloadRequested -= OnReloadRequested;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
