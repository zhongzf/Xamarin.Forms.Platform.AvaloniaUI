using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Platform.AvaloniaUI.Controls;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class FormsApplicationPage : FormsWindow
    {
        protected Application Application { get; private set; }
        protected Platform Platform { get; private set; }

        public FormsApplicationPage()
        {
            this.Initialized += FormsApplicationPage_Initialized;
            this.Activated += FormsApplicationPage_Activated;
            this.LayoutUpdated += FormsApplicationPage_LayoutUpdated;
            this.PositionChanged += FormsApplicationPage_PositionChanged;
            this.GotFocus += FormsApplicationPage_GotFocus;
            //MessagingCenter.Send(this, WPFDeviceInfo.BWPorientationChangedName, this.ToDeviceOrientation());
            //SizeChanged += OnOrientationChanged;

            this.ContentLoader = new FormsContentLoader();
        }

        private void FormsApplicationPage_GotFocus(object sender, Avalonia.Input.GotFocusEventArgs e)
        {
        }

        private void FormsApplicationPage_PositionChanged(object sender, Avalonia.Controls.PointEventArgs e)
        {
        }

        private void FormsApplicationPage_LayoutUpdated(object sender, EventArgs e)
        {
        }

        private void FormsApplicationPage_Initialized(object sender, EventArgs e)
        {
        }

        private void FormsApplicationPage_Activated(object sender, EventArgs e)
        {
            SetMainPage();
        }

        public void LoadApplication(Application application)
        {
            Application.Current = application;
            application.PropertyChanged += Application_PropertyChanged;
            Application = application;
            application.SendStart();
        }

        private void Application_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MainPage" && IsActive)
            {
                SetMainPage();
            }
        }


        private void SetMainPage()
        {
            if (Platform == null)
            {
                Platform = new Platform(this);
            }
            Platform.SetPage(Application.MainPage);
        }
    }
}
