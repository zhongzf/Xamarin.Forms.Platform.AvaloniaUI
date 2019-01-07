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
            Avalonia.Application.Current.OnExit += CurrentApplication_OnExit;

            MessagingCenter.Send(this, AvaloniaUIDeviceInfo.BWPorientationChangedName, this.ToDeviceOrientation());
            this.LayoutUpdated += (sender, e) =>
            {
                MessagingCenter.Send(this, AvaloniaUIDeviceInfo.BWPorientationChangedName, this.ToDeviceOrientation());
            };

            this.ContentLoader = new FormsContentLoader();
        }

        public void LoadApplication(Application application)
        {
            Application.Current = application;
            application.PropertyChanged += Application_PropertyChanged;
            Application = application;
            application.SendStart();
        }

        protected override void Appearing()
        {
            base.Appearing();
            SetMainPage();
        }

        private void Application_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "MainPage" && IsActive)
            {
                SetMainPage();
            }
        }

        protected virtual void SetMainPage()
        {
            if (Platform == null)
            {
                Platform = new Platform(this);
            }
            Platform.SetPage(Application.MainPage);
        }

        // TODO: 
        // when app gets tombstoned, user press back past first page
        //void OnClosing(object sender, ExitEventArgs e)
        //{
        //    Application.SendSleep();
        //}

        //void OnLaunching(object sender, StartupEventArgs e)
        //{
        //    Application.SendStart();
        //}


        void OnActivated(object sender, System.EventArgs e)
        {
            // TODO : figure out consistency of get this to fire
            // Check whether tombstoned (terminated, but OS retains information about navigation state and state dictionarys) or dormant
            Application.SendResume();
        }

        void OnDeactivated(object sender, System.EventArgs e)
        {
            Application.SendSleep();
        }

        private void CurrentApplication_OnExit(object sender, EventArgs e)
        {
        }
    }
}
