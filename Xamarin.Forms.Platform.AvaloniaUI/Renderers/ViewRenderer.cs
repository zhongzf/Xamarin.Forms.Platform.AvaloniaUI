using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using AControl = Avalonia.Controls.Primitives.TemplatedControl;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class DefaultViewRenderer : ViewRenderer<View, UserControl>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            SetNativeControl(new UserControl());
        }
    }

    public class ViewRenderer<TElement, TNativeElement> : IVisualElementRenderer, IEffectControlProvider
        where TElement : VisualElement where TNativeElement : Control
    {
        readonly List<EventHandler<VisualElementChangedEventArgs>> _elementChangedHandlers =
            new List<EventHandler<VisualElementChangedEventArgs>>();

        VisualElementTracker _tracker;
        AControl _AControl => Control as AControl;
        bool _disposed;

        IElementController ElementController => Element as IElementController;

        public TNativeElement Control { get; private set; }

        public TElement Element { get; private set; }

        protected virtual bool AutoTrack { get; set; } = true;

        protected VisualElementTracker Tracker
        {
            get { return _tracker; }
            set
            {
                if (_tracker == value)
                    return;

                if (_tracker != null)
                {
                    _tracker.Dispose();
                    _tracker.Updated -= HandleTrackerUpdated;
                }

                _tracker = value;

                if (_tracker != null)
                    _tracker.Updated += HandleTrackerUpdated;
            }
        }
        void IEffectControlProvider.RegisterEffect(Effect effect)
        {
            PlatformEffect platformEffect = effect as PlatformEffect;
            if (platformEffect != null)
                OnRegisterEffect(platformEffect);
        }

        VisualElement IVisualElementRenderer.Element
        {
            get { return Element; }
        }

        public Control GetNativeElement()
        {
            return Control;
        }

        event EventHandler<VisualElementChangedEventArgs> IVisualElementRenderer.ElementChanged
        {
            add { _elementChangedHandlers.Add(value); }
            remove { _elementChangedHandlers.Remove(value); }
        }

        public virtual SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
        {
            if (Control == null)
                return new SizeRequest();

            var constraint = new Avalonia.Size(widthConstraint, heightConstraint);

            if (Element.HeightRequest == -1)
                Control.Height = double.NaN;

            if (Element.WidthRequest == -1)
                Control.Width = double.NaN;

            Control.Measure(constraint);

            return new SizeRequest(new Size(Math.Ceiling(Control.DesiredSize.Width), Math.Ceiling(Control.DesiredSize.Height)));
        }

        public void SetElement(VisualElement element)
        {
            TElement oldElement = Element;
            Element = (TElement)element;

            if (oldElement != null)
            {
                oldElement.PropertyChanged -= OnElementPropertyChanged;
                oldElement.FocusChangeRequested -= OnModelFocusChangeRequested;
            }

            Element.PropertyChanged += OnElementPropertyChanged;
            Element.FocusChangeRequested += OnModelFocusChangeRequested;

            OnElementChanged(new ElementChangedEventArgs<TElement>(oldElement, Element));

            var controller = (IElementController)oldElement;
            if (controller != null && controller.EffectControlProvider == this)
                controller.EffectControlProvider = null;

            controller = element;
            if (controller != null)
                controller.EffectControlProvider = this;
        }

        public event EventHandler<ElementChangedEventArgs<TElement>> ElementChanged;

        protected virtual void OnElementChanged(ElementChangedEventArgs<TElement> e)
        {
            var args = new VisualElementChangedEventArgs(e.OldElement, e.NewElement);
            for (var i = 0; i < _elementChangedHandlers.Count; i++)
                _elementChangedHandlers[i](this, args);

            ElementChanged?.Invoke(this, e);
        }

        protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == VisualElement.IsEnabledProperty.PropertyName)
                UpdateEnabled();
            else if (e.PropertyName == Frame.HeightProperty.PropertyName)
                UpdateHeight();
            else if (e.PropertyName == Frame.WidthProperty.PropertyName)
                UpdateWidth();
            else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
                UpdateBackground();
            else if (e.PropertyName == View.HorizontalOptionsProperty.PropertyName || e.PropertyName == View.VerticalOptionsProperty.PropertyName)
                UpdateAlignment();
            else if (e.PropertyName == VisualElement.IsTabStopProperty.PropertyName)
                UpdateTabStop();
            else if (e.PropertyName == VisualElement.TabIndexProperty.PropertyName)
                UpdateTabIndex();
        }

        protected virtual void OnGotFocus(object sender, RoutedEventArgs args)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);
        }

        protected virtual void OnLostFocus(object sender, RoutedEventArgs args)
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
        }

        protected virtual void OnRegisterEffect(PlatformEffect effect)
        {
            effect.SetControl(Control);
        }

        protected void SetNativeControl(TNativeElement native)
        {
            Control = native;

            if (AutoTrack && Tracker == null)
                Tracker = new VisualElementTracker<TElement, Control> { Element = Element, Control = Control };

            Element.IsNativeStateConsistent = false;

            Control.AttachedToLogicalTree += Control_AttachedToLogicalTree;
            Control.DetachedFromLogicalTree += Control_DetachedFromLogicalTree;

            Control.LayoutUpdated += (sender, e) =>
            {
                Element.Layout(new Rectangle(0, 0, Control.DesiredSize.Width, Control.DesiredSize.Height));
            };

            Control.GotFocus += OnGotFocus;
            Control.LostFocus += OnLostFocus;

            UpdateBackground();
            UpdateAlignment();
            UpdateWidth();
            UpdateHeight();
        }

        private void Control_AttachedToLogicalTree(object sender, LogicalTreeAttachmentEventArgs e)
        {
            Control.AttachedToLogicalTree -= Control_AttachedToLogicalTree;
            Element.IsNativeStateConsistent = true;
            Appearing();
        }

        private void Control_DetachedFromLogicalTree(object sender, LogicalTreeAttachmentEventArgs e)
        {
            Control.DetachedFromLogicalTree -= Control_DetachedFromLogicalTree;
            Disappearing();
        }

        protected virtual void Appearing()
        {

        }

        protected virtual void Disappearing()
        {

        }

        protected virtual void UpdateBackground()
        {
            //_AControl?.UpdateDependencyColor(AControl.BackgroundProperty, Element.BackgroundColor);
        }

        protected virtual void UpdateHeight()
        {
            if (Control == null || Element == null)
                return;

            Control.Height = Element.Height > 0 ? Element.Height : Double.NaN;
        }

        protected virtual void UpdateWidth()
        {
            if (Control == null || Element == null)
                return;

            Control.Width = Element.Width > 0 ? Element.Width : Double.NaN;
        }

        protected virtual void UpdateNativeWidget()
        {
            UpdateEnabled();
            UpdateTabStop();
            UpdateTabIndex();
        }

        internal virtual void OnModelFocusChangeRequested(object sender, VisualElement.FocusRequestArgs args)
        {
            if (_AControl == null)
                return;

            //if (args.Focus)
            //	args.Result = _AControl.Focus();
            //else
            //{
            //	UnfocusControl(_AControl);
            //	args.Result = true;
            //}
        }

        internal void UnfocusControl(AControl control)
        {
            if (control == null || !control.IsEnabled)
                return;
            control.IsEnabled = false;
            control.IsEnabled = true;
        }

        void HandleTrackerUpdated(object sender, EventArgs e)
        {
            UpdateNativeWidget();
        }

        protected void UpdateTabStop()
        {
            if (_AControl == null)
                return;
            //_AControl.IsTabStop = Element.IsTabStop;

            //// update TabStop of children for complex controls (like as DatePicker, TimePicker, SearchBar and Stepper)
            //var children = ControlExtensions.GetChildren<AControl>(_AControl);
            //foreach (var child in children)
            //	child.IsTabStop = _AControl.IsTabStop;
        }

        protected void UpdateTabIndex()
        {
            //if (_AControl != null)
            //	_AControl.TabIndex = Element.TabIndex;
        }

        protected virtual void UpdateEnabled()
        {
            if (_AControl != null)
                _AControl.IsEnabled = Element.IsEnabled;
        }

        void UpdateAlignment()
        {
            View view = Element as View;
            if (view != null)
            {
                Control.HorizontalAlignment = view.HorizontalOptions.ToNativeHorizontalAlignment();
                Control.VerticalAlignment = view.VerticalOptions.ToNativeVerticalAlignment();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
                return;

            _disposed = true;

            if (Control != null)
            {
                Control.GotFocus -= OnGotFocus;
                Control.LostFocus -= OnLostFocus;
            }

            if (Element != null)
            {
                Element.PropertyChanged -= OnElementPropertyChanged;
                Element.FocusChangeRequested -= OnModelFocusChangeRequested;
            }

            Tracker = null;
        }
    }
}
