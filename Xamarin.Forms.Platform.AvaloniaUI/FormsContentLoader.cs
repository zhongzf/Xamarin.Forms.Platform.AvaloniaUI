using Avalonia.Controls;
using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Xamarin.Forms.Platform.AvaloniaUI.Interfaces;
using Avalonia.Threading;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class FormsContentLoader : IContentLoader
    {
        public Task<object> LoadContentAsync(Control parent, object oldContent, object newContent, CancellationToken cancellationToken)
        {
            VisualElement element = oldContent as VisualElement;
            if (element != null)
            {
                element.Cleanup(); // Cleanup old content
            }

            if (!Dispatcher.UIThread.CheckAccess())
            {
                throw new InvalidOperationException("UIThreadRequired");
            }

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            return Task.Factory.StartNew(() => LoadContent(parent, newContent), cancellationToken, TaskCreationOptions.None, scheduler);
        }

        protected virtual object LoadContent(Control parent, object page)
        {
            VisualElement visualElement = page as VisualElement;
            if (visualElement != null)
            {
                var renderer = CreateOrResizeContent(parent, visualElement);
                return renderer;
            }
            return null;
        }

        public void OnSizeContentChanged(Control parent, object page)
        {
            VisualElement visualElement = page as VisualElement;
            if (visualElement != null)
            {
                CreateOrResizeContent(parent, visualElement);
            }
        }

        private object CreateOrResizeContent(Control parent, VisualElement visualElement)
        {
            var renderer = Platform.GetOrCreateRenderer(visualElement);

            //if (Debugger.IsAttached)
            //	Console.WriteLine("Page type : " + visualElement.GetType() + " (" + (visualElement as Page).Title + ") -- Parent type : " + visualElement.Parent.GetType() + " -- " + parent.ActualHeight + "H*" + parent.ActualWidth + "W");

            var actualRect = new Rectangle(0, 0, parent.Bounds.Width, parent.Bounds.Height);
            visualElement.Layout(actualRect);

            // ControlTemplate adds an additional layer through which to send sizing changes.
            var contentPage = visualElement as ContentPage;
            if (contentPage?.ControlTemplate != null)
            {
                contentPage.Content?.Layout(actualRect);
            }
            else
            {
                var contentView = visualElement as ContentView;
                if (contentView?.ControlTemplate != null)
                {
                    contentView.Content?.Layout(actualRect);
                }
            }

            IPageController pageController = visualElement.RealParent as IPageController;
            if (pageController != null)
            {
                pageController.ContainerArea = actualRect;
            }

            return renderer.GetNativeElement();
        }
    }
}
