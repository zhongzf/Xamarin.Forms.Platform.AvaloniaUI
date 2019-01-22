using Xilium.CefGlue;

namespace Xamarin.Forms.Platform.AvaloniaUI.Handlers
{
    /// <summary>
    /// The CefGlue resource scheme handler factory.
    /// </summary>
    public class CefGlueResourceSchemeHandlerFactory : CustomizedSchemeHandlerFactory
    {
        public override string SchemeName => "local";
        public override string DomainName => "";

        /// <summary>
        /// The create.
        /// </summary>
        /// <param name="browser">
        /// The browser.
        /// </param>
        /// <param name="frame">
        /// The frame.
        /// </param>
        /// <param name="schemeName">
        /// The scheme name.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CefResourceHandler"/>.
        /// </returns>
        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            return new CefGlueResourceSchemeHandler();
        }
    }
}
