using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class FormsPage : UserControl
	{
		public FormsWindow ParentWindow
		{
			get
			{
				if (Avalonia.Application.Current.MainWindow is FormsWindow parentWindow)
					return parentWindow;
				return null;
			}
		}

		public FormsPage()
		{
		}

		private void OnPropertyChanged(object sender, EventArgs arg)
		{
		}

		private void Commands_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
		{
		}

		protected virtual void Appearing()
		{
		}

		protected virtual void Disappearing()
		{
		}
	}
}
