using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.AvaloniaUI.Controls;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class Platform : BindableObject, INavigation
    {
        readonly FormsApplicationPage _page;
        Page Page { get; set; }

        internal static readonly BindableProperty RendererProperty = BindableProperty.CreateAttached("Renderer", typeof(IVisualElementRenderer), typeof(Platform), default(IVisualElementRenderer));

        internal Platform(FormsApplicationPage page)
        {
            _page = page;


            var busyCount = 0;
            MessagingCenter.Subscribe(this, Page.BusySetSignalName, (Page sender, bool enabled) =>
            {
                busyCount = Math.Max(0, enabled ? busyCount + 1 : busyCount - 1);
            });

            MessagingCenter.Subscribe<Page, AlertArguments>(this, Page.AlertSignalName, OnPageAlert);
            MessagingCenter.Subscribe<Page, ActionSheetArguments>(this, Page.ActionSheetSignalName, OnPageActionSheet);

        }

        async void OnPageAlert(Page sender, AlertArguments options)
        {
            string content = options.Message ?? options.Title ?? string.Empty;

            FormsContentDialog dialog = new FormsContentDialog();

            if (options.Message == null || options.Title == null)
                dialog.Content = content;
            else
            {
                dialog.Title = options.Title;
                dialog.Content = options.Message;
            }

            if (options.Accept != null)
            {
                dialog.IsPrimaryButtonEnabled = true;
                dialog.PrimaryButtonText = options.Accept;
            }

            if (options.Cancel != null)
            {
                dialog.IsSecondaryButtonEnabled = true;
                dialog.SecondaryButtonText = options.Cancel;
            }

            var dialogResult = await dialog.ShowAsync();

            //options.SetResult(dialogResult == LightContentDialogResult.Primary);
        }

        async void OnPageActionSheet(Page sender, ActionSheetArguments options)
        {
            //var list = new Avalonia.Controls.ListView
            //{
            //    Style = (Avalonia.Styling.Style)Avalonia.Application.Current.Resources["ActionSheetList"],
            //    ItemsSource = options.Buttons
            //};

            var dialog = new FormsContentDialog
            {
                //Content = list,
            };

            if (options.Title != null)
                dialog.Title = options.Title;

            //list.SelectionChanged += (s, e) =>
            //{
            //    if (list.SelectedItem != null)
            //    {
            //        dialog.Hide();
            //        options.SetResult((string)list.SelectedItem);
            //    }
            //};

            /*_page.KeyDown += (window, e) =>
			 {
				 if (e.Key == Avalonia.Input.Key.Escape)
				 {
					 dialog.Hide();
					 options.SetResult(LightContentDialogResult.None.ToString());
				 }
			 };*/

            if (options.Cancel != null)
            {
                dialog.IsSecondaryButtonEnabled = true;
                dialog.SecondaryButtonText = options.Cancel;
            }

            if (options.Destruction != null)
            {
                dialog.IsPrimaryButtonEnabled = true;
                dialog.PrimaryButtonText = options.Destruction;
            }

            //LightContentDialogResult result = await dialog.ShowAsync();
            //if (result == LightContentDialogResult.Secondary)
            //    options.SetResult(options.Cancel);
            //else if (result == LightContentDialogResult.Primary)
            //    options.SetResult(options.Destruction);

        }


        public IReadOnlyList<Page> ModalStack => throw new NotImplementedException();

        public IReadOnlyList<Page> NavigationStack => throw new NotImplementedException();

        public void InsertPageBefore(Page page, Page before)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Page> PopModalAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync()
        {
            throw new NotImplementedException();
        }

        public Task PopToRootAsync(bool animated)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task PushAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page)
        {
            throw new NotImplementedException();
        }

        public Task PushModalAsync(Page page, bool animated)
        {
            throw new NotImplementedException();
        }

        public void RemovePage(Page page)
        {
            throw new NotImplementedException();
        }

        internal void SetPage(Page mainPage)
        {
            if (mainPage == null)
                return;

            Page = mainPage;
            _page.StartupPage = Page;
            Application.Current.NavigationProxy.Inner = this;
        }


        public static SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
        {
            if (widthConstraint > 0 && heightConstraint > 0 && GetRenderer(view) != null)
            {
                IVisualElementRenderer element = GetRenderer(view);
                return element.GetDesiredSize(widthConstraint, heightConstraint);
            }

            return new SizeRequest();
        }

        public static IVisualElementRenderer GetOrCreateRenderer(VisualElement element)
        {
            if (GetRenderer(element) == null)
                SetRenderer(element, CreateRenderer(element));

            return GetRenderer(element);
        }

        public static IVisualElementRenderer CreateRenderer(VisualElement element)
        {
            IVisualElementRenderer result = Registrar.Registered.GetHandlerForObject<IVisualElementRenderer>(element) ?? new DefaultViewRenderer();
            result.SetElement(element);
            return result;
        }

        public static IVisualElementRenderer GetRenderer(VisualElement self)
        {
            return (IVisualElementRenderer)self.GetValue(RendererProperty);
        }

        public static void SetRenderer(VisualElement self, IVisualElementRenderer renderer)
        {
            self.SetValue(RendererProperty, renderer);
            self.IsPlatformEnabled = renderer != null;
        }
    }
}
