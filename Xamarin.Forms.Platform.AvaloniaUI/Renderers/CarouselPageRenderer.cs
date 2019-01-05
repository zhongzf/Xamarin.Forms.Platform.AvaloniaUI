using System.ComponentModel;
using Xamarin.Forms.Platform.AvaloniaUI.Controls;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
	public class CarouselPageRenderer : VisualMultiPageRenderer<CarouselPage, ContentPage, FormsCarouselPage>
	{
		protected override void OnElementChanged(ElementChangedEventArgs<CarouselPage> e)
		{
			if (e.NewElement != null)
			{
				if (Control == null) // construct and SetNativeControl and suscribe control event
				{
					SetNativeControl(new FormsCarouselPage() { ContentLoader = new FormsContentLoader() });
				}
			}
			
			base.OnElementChanged(e);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
		}
	}
}
