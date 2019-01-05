using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class FormsPanel : Panel
    {
        IElementController ElementController => Element as IElementController;

        public Layout Element { get; private set; }

        public FormsPanel(Layout element)
        {
            Element = element;
        }

        //protected override Avalonia.Size ArrangeOverride(Avalonia.Size finalSize)
        //{
        //    if (Element == null)
        //        return finalSize;

        //    Element.IsInNativeLayout = true;

        //    for (var i = 0; i < ElementController.LogicalChildren.Count; i++)
        //    {
        //        var child = ElementController.LogicalChildren[i] as VisualElement;
        //        if (child == null)
        //            continue;

        //        IVisualElementRenderer renderer = Platform.GetRenderer(child);
        //        if (renderer == null)
        //            continue;
        //        Rectangle bounds = child.Bounds;

        //        var nativeElement = renderer.GetNativeElement();
        //        var width = Math.Max(Math.Max(0, bounds.Width), DesiredSize.Width);
        //        var height = Math.Max(Math.Max(0, bounds.Height), DesiredSize.Height);
        //        nativeElement.Arrange(new Rect(bounds.X, bounds.Y, width, height));
        //    }

        //    Element.IsInNativeLayout = false;

        //    return finalSize;
        //}

        //protected override Avalonia.Size MeasureOverride(Avalonia.Size availableSize)
        //{
        //    if (Element == null || availableSize.Width * availableSize.Height == 0)
        //        return new Avalonia.Size(0, 0);

        //    Element.IsInNativeLayout = true;

        //    for (var i = 0; i < ElementController.LogicalChildren.Count; i++)
        //    {
        //        var child = ElementController.LogicalChildren[i] as VisualElement;
        //        if (child == null)
        //            continue;
        //        IVisualElementRenderer renderer = Platform.GetOrCreateRenderer(child);
        //        if (renderer == null)
        //            continue;

        //        Control control = renderer.GetNativeElement();

        //        if (control.Width != child.Width || control.Height != child.Height)
        //        {
        //            double width = child.Width <= -1 ? Width : child.Width;
        //            double height = child.Height <= -1 ? Height : child.Height;
        //            width = Math.Max(double.IsNaN(width) ? 0.0 : width, DesiredSize.Width);
        //            height = Math.Max(double.IsNaN(height) ? 0.0 : height, DesiredSize.Height);
        //            control.Measure(new Avalonia.Size(width, height));
        //        }
        //    }

        //    Avalonia.Size result;
        //    if (double.IsInfinity(availableSize.Width) || double.IsPositiveInfinity(availableSize.Height))
        //    {
        //        Size request = Element.Measure(availableSize.Width, availableSize.Height, MeasureFlags.IncludeMargins).Request;
        //        result = new Avalonia.Size(request.Width, request.Height);
        //    }
        //    else
        //    {
        //        result = availableSize;
        //    }
        //    Element.IsInNativeLayout = false;

        //    if (Double.IsPositiveInfinity(result.Height))
        //        result.WithHeight(0.0);
        //    if (Double.IsPositiveInfinity(result.Width))
        //        result.WithWidth(0.0);

        //    return result;
        //}
    }
}
