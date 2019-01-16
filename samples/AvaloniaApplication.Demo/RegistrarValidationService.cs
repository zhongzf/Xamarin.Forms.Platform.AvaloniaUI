using Xamarin.Forms;
using Xamarin.Forms.ControlGallery.WPF;
using Xamarin.Forms.Controls;

[assembly: Dependency(typeof(RegistrarValidationService))]
namespace Xamarin.Forms.ControlGallery.WPF
{
	public class RegistrarValidationService : IRegistrarValidationService
	{
		public bool Validate(VisualElement element, out string message)
		{
			message = "Success";

			if (element == null || element is OpenGLView)
				return true;

			var renderer = Platform.AvaloniaUI.Platform.GetOrCreateRenderer(element);

			if (renderer == null 
				|| renderer.GetType().Name == "DefaultRenderer"
				)
			{
				message = $"Failed to load proper WPF renderer for {element.GetType().Name}";
				return false;
			}

			return true;
		}
	}
}