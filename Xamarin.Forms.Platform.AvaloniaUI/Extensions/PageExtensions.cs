using System;
using Avalonia;
using Avalonia.Controls;

namespace Xamarin.Forms.Platform.AvaloniaUI.Extensions
{
	public static class PageExtensions
	{
		public static Control ToControl(this Page view)
		{
			if (!Forms.IsInitialized)
				throw new InvalidOperationException("Call Forms.Init() before this");

			if (!(view.RealParent is Application))
			{
				Application app = new DefaultApplication
				{
					MainPage = view
				};

				var formsApplicationPage = new FormsApplicationPage();
				formsApplicationPage.LoadApplication(app);
				var platform = new Platform(formsApplicationPage);
				platform.SetPage(view);
			}

			IVisualElementRenderer renderer = Platform.GetOrCreateRenderer(view);

			if (renderer == null)
			{
				throw new InvalidOperationException($"Could not find or create a renderer for {view}");
			}

			var Control = renderer.GetNativeElement();

			//Control.Loaded += (sender, args) =>
			//{
			//	view.Layout(new Rectangle(0, 0, Control.ActualWidth, Control.ActualHeight));
			//};

			return Control;
		}
	}

	class DefaultApplication : Application
	{
	}
}
