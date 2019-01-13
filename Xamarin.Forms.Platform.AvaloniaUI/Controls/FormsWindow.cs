using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Platform.AvaloniaUI.Interfaces;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
    public class FormsWindow : Window
    {
        FormsAppBar topAppBar;
        FormsAppBar bottomAppBar;
        Avalonia.Controls.Button previousButton;
        Avalonia.Controls.Button previousModalButton;
        Avalonia.Controls.Button hamburgerButton;

        public static readonly DirectProperty<FormsWindow, object> StartupPageProperty = AvaloniaProperty.RegisterDirect<FormsWindow, object>(nameof(StartupPage), o => o.StartupPage, (o, v) => o.StartupPage = v);
        public static readonly DirectProperty<FormsWindow, IContentLoader> ContentLoaderProperty = AvaloniaProperty.RegisterDirect<FormsWindow, IContentLoader>(nameof(ContentLoader), o => o.ContentLoader, (o, v) => o.ContentLoader = v);
        public static readonly AvaloniaProperty HasBackButtonProperty;//= AvaloniaProperty.Register("HasBackButton", typeof(bool), typeof(FormsWindow));
        public static readonly AvaloniaProperty HasBackButtonModalProperty;//= AvaloniaProperty.Register("HasBackButtonModal", typeof(bool), typeof(FormsWindow));
        public static readonly AvaloniaProperty HasNavigationBarProperty;//= AvaloniaProperty.Register("HasNavigationBar", typeof(bool), typeof(FormsWindow));
        public static readonly AvaloniaProperty BackButtonTitleProperty;//= AvaloniaProperty.Register("BackButtonTitle", typeof(string), typeof(FormsWindow));
        public static readonly AvaloniaProperty CurrentTitleProperty;//= AvaloniaProperty.Register("CurrentTitle", typeof(string), typeof(FormsWindow));
        public static readonly AvaloniaProperty CurrentModalPageProperty;//= AvaloniaProperty.Register("CurrentModalPage", typeof(object), typeof(FormsWindow));
        public static readonly AvaloniaProperty CurrentNavigationPageProperty;//= AvaloniaProperty.Register("CurrentNavigationPage", typeof(FormsNavigationPage), typeof(FormsWindow));
        public static readonly AvaloniaProperty CurrentMasterDetailPageProperty;//= AvaloniaProperty.Register("CurrentMasterDetailPage", typeof(FormsMasterDetailPage), typeof(FormsWindow));
        public static readonly AvaloniaProperty CurrentContentDialogProperty;//= AvaloniaProperty.Register("CurrentContentDialog", typeof(FormsContentDialog), typeof(FormsWindow));
        public static readonly AvaloniaProperty TitleBarBackgroundColorProperty;//= AvaloniaProperty.Register("TitleBarBackgroundColor", typeof(Brush), typeof(FormsWindow));
        public static readonly AvaloniaProperty TitleBarTextColorProperty;//= AvaloniaProperty.Register("TitleBarTextColor", typeof(Brush), typeof(FormsWindow));

        public Brush TitleBarBackgroundColor
        {
            get { return (Brush)GetValue(TitleBarBackgroundColorProperty); }
            private set { SetValue(TitleBarBackgroundColorProperty, value); }
        }

        public Brush TitleBarTextColor
        {
            get { return (Brush)GetValue(TitleBarTextColorProperty); }
            private set { SetValue(TitleBarTextColorProperty, value); }
        }

        public FormsContentDialog CurrentContentDialog
        {
            get { return (FormsContentDialog)GetValue(CurrentContentDialogProperty); }
            set { SetValue(CurrentContentDialogProperty, value); }
        }

        public string CurrentTitle
        {
            get { return (string)GetValue(CurrentTitleProperty); }
            private set { SetValue(CurrentTitleProperty, value); }
        }

        public FormsNavigationPage CurrentNavigationPage
        {
            get { return (FormsNavigationPage)GetValue(CurrentNavigationPageProperty); }
            private set { SetValue(CurrentNavigationPageProperty, value); }
        }

        public FormsMasterDetailPage CurrentMasterDetailPage
        {
            get { return (FormsMasterDetailPage)GetValue(CurrentMasterDetailPageProperty); }
            private set { SetValue(CurrentMasterDetailPageProperty, value); }
        }

        public bool HasBackButton
        {
            get { return (bool)GetValue(HasBackButtonProperty); }
            private set { SetValue(HasBackButtonProperty, value); }
        }

        public bool HasBackButtonModal
        {
            get { return (bool)GetValue(HasBackButtonModalProperty); }
            private set { SetValue(HasBackButtonModalProperty, value); }
        }

        public bool HasNavigationBar
        {
            get { return (bool)GetValue(HasNavigationBarProperty); }
            private set { SetValue(HasNavigationBarProperty, value); }
        }

        public string BackButtonTitle
        {
            get { return (string)GetValue(BackButtonTitleProperty); }
            private set { SetValue(BackButtonTitleProperty, value); }
        }

        public object CurrentModalPage
        {
            get { return (object)GetValue(CurrentModalPageProperty); }
            private set { SetValue(CurrentModalPageProperty, value); }
        }

        private IContentLoader _contentLoader;
        public IContentLoader ContentLoader
        {
            get { return _contentLoader; }
            set { SetAndRaise(ContentLoaderProperty, ref _contentLoader, value); }
        }

        private object _startupPage;
        public object StartupPage
        {
            get { return _startupPage; }
            set { SetAndRaise(StartupPageProperty, ref _startupPage, value); }
        }

        private CancellationTokenSource tokenStartupPage;


        public FormsWindow()
        {
            //this.DefaultStyleKey = typeof(FormsWindow);
            this.Activated += (sender, e) => Appearing();
            this.Closing += (sender, e) => Disappearing();
            StartupPageProperty.Changed.AddClassHandler<FormsWindow>(x => x.OnStartupPageChanged);

            this.LayoutUpdated += FormsWindow_LayoutUpdated;
        }

        private void FormsWindow_LayoutUpdated(object sender, EventArgs e)
        {
            if (this.Content is Control)
            {
                var bounds = this.Bounds;
                var control = this.Content as Control;
                Rect childFinal = new Rect(bounds.X, bounds.Y, Math.Max(0, bounds.Width), Math.Max(0, bounds.Height));
                //control.Arrange(childFinal);
                control.Width = childFinal.Width;
                control.Height = childFinal.Height;
            }
        }

        protected virtual void OnStartupPageChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var oldValue = e.OldValue;
            var newValue = e.NewValue;
            if (newValue != null && newValue.Equals(oldValue)) return;

            var localTokenStartupPage = new CancellationTokenSource();
            this.tokenStartupPage = localTokenStartupPage;

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = this.ContentLoader.LoadContentAsync(this, oldValue, newValue, this.tokenStartupPage.Token);

            task.ContinueWith(t =>
            {
                try
                {
                    if (t.IsFaulted || t.IsCanceled || localTokenStartupPage.IsCancellationRequested)
                    {
                        this.Content = null;
                    }
                    else
                    {
                        this.Content = t.Result;

                    }
                }
                finally
                {
                    if (this.tokenStartupPage == localTokenStartupPage)
                    {
                        this.tokenStartupPage = null;
                    }
                    localTokenStartupPage.Dispose();
                }
            }, scheduler);
            return;
        }


        protected virtual void Appearing()
        {

        }

        protected virtual void Disappearing()
        {

        }

        //public override void OnApplyTemplate()
        //{
        //    base.OnApplyTemplate();
        //    topAppBar = Template.FindName("PART_TopAppBar", this) as FormsAppBar;
        //    bottomAppBar = Template.FindName("PART_BottomAppBar", this) as FormsAppBar;
        //    previousButton = Template.FindName("PART_Previous", this) as Avalonia.Controls.Button;
        //    if (previousButton != null)
        //    {
        //        previousButton.Click += PreviousButton_Click;
        //    }
        //    previousModalButton = Template.FindName("PART_Previous_Modal", this) as Avalonia.Controls.Button;
        //    if (previousButton != null)
        //    {
        //        previousModalButton.Click += PreviousModalButton_Click;
        //    }
        //    hamburgerButton = Template.FindName("PART_Hamburger", this) as Avalonia.Controls.Button;
        //    if (hamburgerButton != null)
        //    {
        //        hamburgerButton.Click += HamburgerButton_Click;
        //    }
        //}

        private void PreviousModalButton_Click(object sender, RoutedEventArgs e)
        {
            OnBackSystemButtonPressed();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMasterDetailPage != null)
            {
                CurrentMasterDetailPage.IsPresented = !CurrentMasterDetailPage.IsPresented;
            }
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentNavigationPage != null && CurrentNavigationPage.StackDepth > 1)
            {
                CurrentNavigationPage.OnBackButtonPressed();
            }
        }

        private static void OnContentLoaderChanged(AvaloniaObject o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
                throw new ArgumentNullException("ContentLoader");
        }

        public void SynchronizeAppBar()
        {
            //IEnumerable<FormsPage> childrens = this.FindVisualChildren<FormsPage>();

            //CurrentTitle = childrens.FirstOrDefault()?.GetTitle();
            //HasNavigationBar = childrens.FirstOrDefault()?.GetHasNavigationBar() ?? false;
            //CurrentNavigationPage = childrens.OfType<FormsNavigationPage>()?.FirstOrDefault();
            //CurrentMasterDetailPage = childrens.OfType<FormsMasterDetailPage>()?.FirstOrDefault();
            //var page = childrens.FirstOrDefault();
            //if (page != null)
            //{
            //    TitleBarBackgroundColor = page.GetTitleBarBackgroundColor();
            //    TitleBarTextColor = page.GetTitleBarTextColor();
            //}
            //else
            //{
            //    ClearValue(TitleBarBackgroundColorProperty);
            //    ClearValue(TitleBarTextColorProperty);
            //}

            //hamburgerButton.Visibility = CurrentMasterDetailPage != null ? Visibility.Visible : Visibility.Collapsed;

            //if (CurrentNavigationPage != null)
            //{
            //    HasBackButton = CurrentNavigationPage.GetHasBackButton();
            //    BackButtonTitle = CurrentNavigationPage.GetBackButtonTitle();

            //}
            //else
            //{
            //    HasBackButton = false;
            //    BackButtonTitle = "";
            //}
        }

        public void SynchronizeToolbarCommands()
        {
            //IEnumerable<FormsPage> childrens = this.FindVisualChildren<FormsPage>();

            //var page = childrens.FirstOrDefault();
            //if (page == null) return;

            //topAppBar.PrimaryCommands = page.GetPrimaryTopBarCommands().OrderBy(ti => ti.GetValue(FrameworkElementAttached.PriorityProperty));
            //topAppBar.SecondaryCommands = page.GetSecondaryTopBarCommands().OrderBy(ti => ti.GetValue(FrameworkElementAttached.PriorityProperty));
            //bottomAppBar.PrimaryCommands = page.GetPrimaryBottomBarCommands().OrderBy(ti => ti.GetValue(FrameworkElementAttached.PriorityProperty));
            //bottomAppBar.SecondaryCommands = page.GetSecondaryBottomBarCommands().OrderBy(ti => ti.GetValue(FrameworkElementAttached.PriorityProperty));
            //bottomAppBar.Content = childrens.LastOrDefault(x => x.ContentBottomBar != null)?.ContentBottomBar;

            //topAppBar.Reset();
            //bottomAppBar.Reset();
        }

        public void ShowContentDialog(FormsContentDialog contentDialog)
        {
            this.CurrentContentDialog = contentDialog;
        }

        public void HideContentDialog()
        {
            this.CurrentContentDialog = null;
        }

        public ObservableCollection<object> InternalChildren { get; } = new ObservableCollection<object>();


        public void PushModal(object page)
        {
            PushModal(page, true);
        }

        public void PushModal(object page, bool animated)
        {
            InternalChildren.Add(page);
            this.CurrentModalPage = InternalChildren.Last();
            this.HasBackButtonModal = true;
        }

        public object PopModal()
        {
            return PopModal(true);
        }

        public object PopModal(bool animated)
        {
            if (InternalChildren.Count < 1)
                return null;

            var modal = InternalChildren.Last();

            if (InternalChildren.Remove(modal))
            {
                /*if (LightContentControl != null)
				{
					LightContentControl.Transition = animated ? TransitionType.Right : TransitionType.Normal;
				}*/
                CurrentModalPage = InternalChildren.LastOrDefault();
            }

            this.HasBackButtonModal = InternalChildren.Count >= 1;

            return modal;
        }

        public virtual void OnBackSystemButtonPressed()
        {
            PopModal();
        }
    }
}
