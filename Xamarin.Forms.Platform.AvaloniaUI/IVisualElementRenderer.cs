using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
	public interface IVisualElementRenderer : IRegisterable, IDisposable
	{
		Control GetNativeElement();

		VisualElement Element { get; }
		
		event EventHandler<VisualElementChangedEventArgs> ElementChanged;

		SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);

		void SetElement(VisualElement element);
		
	}
}
