using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class AvaloniaUIDeviceInfo : DeviceInfo
    {
        internal const string BWPorientationChangedName = "Xamarin.AvaloniaUI.OrientationChanged";

        readonly double _scalingFactor;

        public AvaloniaUIDeviceInfo()
        {
            MessagingCenter.Subscribe(this, BWPorientationChangedName, (FormsApplicationPage page, DeviceOrientation orientation) => { CurrentOrientation = orientation; });

            var content = Avalonia.Application.Current.MainWindow;

            // Scaling Factor for Windows Phone 8 is relative to WVGA: https://msdn.microsoft.com/en-us/library/windows/apps/jj206974(v=vs.105).aspx
            //_scalingFactor = content.ScaleFactor / 100d;
            //PixelScreenSize = new Size(content.ActualWidth * _scalingFactor, content.ActualHeight * _scalingFactor);
            PixelScreenSize = new Size(content?.Screens.Primary.Bounds.Width ?? 0, content?.Screens.Primary.Bounds.Height ?? 0);
            ScaledScreenSize = new Size(content?.Screens.Primary.Bounds.Width ?? 0, content?.Screens.Primary.Bounds.Height ?? 0);
        }

        public override Size PixelScreenSize { get; }

        public override Size ScaledScreenSize { get; }

        public override double ScalingFactor
        {
            get { return _scalingFactor; }
        }

        protected override void Dispose(bool disposing)
        {
            MessagingCenter.Unsubscribe<FormsApplicationPage, DeviceOrientation>(this, BWPorientationChangedName);
            base.Dispose(disposing);
        }
    }
}
