using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Xamarin.Forms.Platform.AvaloniaUI.Interfaces;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
    public class FormsContentControl : ContentControl
    {
        private CancellationTokenSource tokenSource;

        public static readonly AvaloniaProperty SourceProperty;// = AvaloniaProperty.Register("Source", typeof(object), typeof(FormsContentControl), new PropertyMetadata(OnSourceChanged));
        public static readonly AvaloniaProperty ContentLoaderProperty;// = AvaloniaProperty.Register("ContentLoader", typeof(IContentLoader), typeof(FormsContentControl), new PropertyMetadata(new DefaultContentLoader(), OnContentLoaderChanged));

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public IContentLoader ContentLoader
        {
            get { return (IContentLoader)GetValue(ContentLoaderProperty); }
            set { SetValue(ContentLoaderProperty, value); }
        }

        public FormsContentControl()
        {
            //this.DefaultStyleKey = typeof(FormsContentControl);
            BoundsProperty.Changed.AddClassHandler<FormsContentControl>(x => x.LightContentControl_SizeChanged);
            this.LayoutUpdated += FormsContentControl_LayoutUpdated;
        }

        private void FormsContentControl_LayoutUpdated(object sender, EventArgs e)
        {
            this.ContentLoader.OnSizeContentChanged(this, Source);
        }

        private void LightContentControl_SizeChanged(AvaloniaPropertyChangedEventArgs e)
        {
            this.ContentLoader.OnSizeContentChanged(this, Source);
        }

        private static void OnContentLoaderChanged(AvaloniaObject o, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue == null)
            {
                throw new ArgumentNullException("ContentLoader");
            }
        }

        private static void OnSourceChanged(AvaloniaObject o, AvaloniaPropertyChangedEventArgs e)
        {
            ((FormsContentControl)o).OnSourceChanged(e.OldValue, e.NewValue);
        }

        private void OnSourceChanged(object oldValue, object newValue)
        {
            if (newValue != null && newValue.Equals(oldValue)) return;

            var localTokenSource = new CancellationTokenSource();
            this.tokenSource = localTokenSource;

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            var task = this.ContentLoader.LoadContentAsync(this, oldValue, newValue, this.tokenSource.Token);

            task.ContinueWith(t =>
            {
                try
                {
                    if (t.IsFaulted || t.IsCanceled || localTokenSource.IsCancellationRequested)
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
                    if (this.tokenSource == localTokenSource)
                    {
                        this.tokenSource = null;
                    }
                    localTokenSource.Dispose();
                }
            }, scheduler);
            return;
        }
    }
}
