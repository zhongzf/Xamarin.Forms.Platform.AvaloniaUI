using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms.Platform.AvaloniaUI.Helpers;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class StackLayoutRenderer : ViewRenderer<StackLayout, StackPanel>
    {
        IElementController ElementController => Element as IElementController;
        bool _isZChanged;

        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null) // construct and SetNativeControl and suscribe control event
                {
                    SetNativeControl(new StackPanel());
                }

                // Update control property 
                UpdateClipToBounds();
                foreach (Element child in ElementController.LogicalChildren)
                {
                    HandleChildAdded(Element, new ElementEventArgs(child));
                }

                // Suscribe element event
                Element.ChildAdded += HandleChildAdded;
                Element.ChildRemoved += HandleChildRemoved;
                Element.ChildrenReordered += HandleChildrenReordered;
            }

            base.OnElementChanged(e);
        }

        protected override void Appearing()
        {
            base.Appearing();
            Element.Layout(new Rectangle(0, 0, Control.Bounds.Width, Control.Bounds.Height));
        }

        void HandleChildAdded(object sender, ElementEventArgs e)
        {
            UiHelper.ExecuteInUiThread(() =>
            {
                var view = e.Element as VisualElement;

                if (view == null)
                    return;

                IVisualElementRenderer renderer = Platform.GetOrCreateRenderer(view);
                Control native = renderer.GetNativeElement();
                Control.Children.Add(native);
                if (_isZChanged)
                {
                    EnsureZIndex();
                }
            });
        }

        void HandleChildRemoved(object sender, ElementEventArgs e)
        {
            UiHelper.ExecuteInUiThread(() =>
            {
                var view = e.Element as VisualElement;

                if (view == null)
                    return;

                Control native = Platform.GetRenderer(view)?.GetNativeElement() as Control;
                if (native != null)
                {
                    Control.Children.Remove(native);
                    view.Cleanup();
                    if (_isZChanged)
                    {
                        EnsureZIndex();
                    }
                }
            });
        }

        void HandleChildrenReordered(object sender, EventArgs e)
        {
            EnsureZIndex();
        }

        void EnsureZIndex()
        {
            if (ElementController.LogicalChildren.Count == 0)
                return;

            for (var z = 0; z < ElementController.LogicalChildren.Count; z++)
            {
                var child = ElementController.LogicalChildren[z] as VisualElement;
                if (child == null)
                    continue;

                IVisualElementRenderer childRenderer = Platform.GetRenderer(child);

                if (childRenderer == null)
                    continue;

                //if (Canvas.GetZIndex(childRenderer.GetNativeElement()) != (z + 1))
                //{
                //	if (!_isZChanged)
                //		_isZChanged = true;

                //	Canvas.SetZIndex(childRenderer.GetNativeElement(), z + 1);
                //}
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Layout.IsClippedToBoundsProperty.PropertyName)
            {
                UpdateClipToBounds();
            }
        }

        protected override void UpdateBackground()
        {
            Control.UpdateDependencyColor(FormsPanel.BackgroundProperty, Element.BackgroundColor);
        }

        protected override void UpdateNativeWidget()
        {
            base.UpdateNativeWidget();
            UpdateClipToBounds();
        }

        void UpdateClipToBounds()
        {
            Control.Clip = null;
            if (Element.IsClippedToBounds)
            {
                Control.Clip = new RectangleGeometry { Rect = new Rect(0, 0, Control.Bounds.Width, Control.Bounds.Height) };
            }
        }

        bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (Element != null)
                {
                    Element.ChildAdded -= HandleChildAdded;
                    Element.ChildRemoved -= HandleChildRemoved;
                    Element.ChildrenReordered -= HandleChildrenReordered;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
