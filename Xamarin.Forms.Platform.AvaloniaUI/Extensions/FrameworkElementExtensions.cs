using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
	public static class ControlExtensions
	{
		public static object UpdateDependencyColor(this AvaloniaObject depo, AvaloniaProperty dp, Color newColor)
		{
            // TODO: 
            //if (!newColor.IsDefault)
            //	depo.SetValue(dp, newColor.ToBrush());
            //else
            //	depo.ClearValue(dp);

            //return depo.GetValue(dp);
            // TODO:
            return null;
		}

		//internal static IEnumerable<T> GetChildren<T>(this AvaloniaObject parent) where T : AvaloniaObject
		//{
		//	//int myChildrenCount = VisualTreeHelper.GetChildrenCount(parent);
		//	//for (int i = 0; i < myChildrenCount; i++)
		//	//{
		//	//	var child = VisualTreeHelper.GetChild(parent, i);
		//	//	if (child is T)
		//	//		yield return child as T;
		//	//	else
		//	//	{
		//	//		foreach (var subChild in child.GetChildren<T>())
		//	//			yield return subChild;
		//	//	}
		//	//}
		//}
	}
}
