//using OpenTK;
//using OpenTK.Graphics;
//using System;
//using Avalonia.Threading;
//using System.ComponentModel;
//using Avalonia.Forms;
//using Avalonia.Forms.Integration;

//namespace Xamarin.Forms.Platform.AvaloniaUI
//{
//    public class OpenGLViewRenderer : ViewRenderer<OpenGLView, WindowsFormsHost>
//    {
//        private GLControl _glControl;
//        private DispatcherTimer _timer;
//        private Action<Rectangle> _action;
//        private bool _hasRenderLoop;
//        private bool _disposed;

//        public Action<Rectangle> Action
//        {
//            get { return _action; }
//            set { _action = value; }
//        }

//        public bool HasRenderLoop
//        {
//            get { return _hasRenderLoop; }
//            set { _hasRenderLoop = value; }
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (!_disposed && disposing)
//            {
//                _disposed = true;

//                if (Element != null)
//                    ((IOpenGlVieAController)Element).DisplayRequested -= Render;

//                if(_glControl != null)
//                    _glControl.Paint -= OnPaint;

//                if(_timer != null)
//                    _timer.Tick -= OnTick;
//            }

//            base.Dispose(disposing);
//        }

//        protected override void OnElementChanged(ElementChangedEventArgs<OpenGLView> e)
//        {
//            if (e.OldElement != null)
//                ((IOpenGlVieAController)e.OldElement).DisplayRequested -= Render;

//            if (e.NewElement != null)
//            {
//                var windowsFormsHost = new WindowsFormsHost();
//                _glControl = new GLControl(new GraphicsMode(32, 24), 2, 0, GraphicsContextFlags.Default);
//                _glControl.MakeCurrent();
//                _glControl.Dock = DockStyle.Fill;

//                _glControl.Paint += OnPaint;

//                windowsFormsHost.Child = _glControl;
//                SetNativeControl(windowsFormsHost);

//                _timer = new DispatcherTimer();
//                _timer.Interval = TimeSpan.FromMilliseconds(16);
//                _timer.Tick += OnTick;
//                _timer.Start();

//                ((IOpenGlVieAController)e.NewElement).DisplayRequested += Render;

//                SetRenderMode();
//                SetupRenderAction();
//            }

//            base.OnElementChanged(e);
//        }

//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);

//            if (e.PropertyName == OpenGLView.HasRenderLoopProperty.PropertyName)
//            {
//                SetRenderMode();
//                SetupRenderAction();
//            }
//        }

//        public void Render(object sender, EventArgs eventArgs)
//        {
//            if (HasRenderLoop)
//                return;

//            SetupRenderAction();
//        }

//        private void SetRenderMode()
//        {
//            HasRenderLoop = Element.HasRenderLoop;
//        }

//        private void SetupRenderAction()
//        {
//            var model = Element;
//            var onDisplay = model.OnDisplay;

//            Action = onDisplay;
//        }

//        private void OnPaint(object sender, PaintEventArgs e)
//        {
//            if(_glControl == null)
//            {
//                return;
//            }

//            _glControl.MakeCurrent();
//            Action.Invoke(new Rectangle(0, 0, _glControl.Width, _glControl.Height));
//            _glControl.SwapBuffers();
//        }

//        private void OnTick(object sender, EventArgs e)
//        {
//            if (!HasRenderLoop)
//                return;

//            _glControl.Invalidate();
//        }
//    }
//}