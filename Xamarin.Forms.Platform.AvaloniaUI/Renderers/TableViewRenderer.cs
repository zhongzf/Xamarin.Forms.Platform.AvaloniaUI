//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Avalonia;
//using Avalonia.Controls;
//using Avalonia.Data;
//using Avalonia.Input;
//using WList = Avalonia.Controls.ListView;

//namespace Xamarin.Forms.Platform.AvaloniaUI
//{
//	public class TableViewDataTemplateSelector : Avalonia.Controls.DataTemplateSelector
//	{
//		public override Avalonia.DataTemplate SelectTemplate(object item, AvaloniaObject container)
//		{
//			if(item is Cell)
//				return Avalonia.Application.Current.MainWindow.FindResource("CellTemplate") as Avalonia.DataTemplate;
//			else
//				return Avalonia.Application.Current.MainWindow.FindResource("TableSectionHeader") as Avalonia.DataTemplate;
//		}
//	}
	
//	public class TableViewRenderer : ViewRenderer<TableView, WList>
//	{
//		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
//		{
//			SizeRequest result = base.GetDesiredSize(widthConstraint, heightConstraint);
//			result.Minimum = new Size(40, 40);
//			return result;
//		}


//		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TableView> e)
//		{
//			if (e.OldElement != null)
//			{
//				Element.ModelChanged -= OnModelChanged;
//			}

//			if (e.NewElement != null)
//			{
//				if (Control == null) // construct and SetNativeControl and suscribe control event
//				{
//					var listView = new WList
//					{
//						ItemTemplateSelector = new TableViewDataTemplateSelector(),
//						Style = (Avalonia.Styling.Style)Avalonia.Application.Current.Resources["TableViewTemplate"],
//					};
					
//					SetNativeControl(listView);
//					Control.SelectionChanged += Control_SelectionChanged;
//				}

//				// Update control property 
//				Control.ItemsSource = GetTableViewRow();

//				// Element event
//				Element.ModelChanged += OnModelChanged;
//			}

//			base.OnElementChanged(e);
//		}

	
//		private void Control_SelectionChanged(object sender, SelectionChangedEventArgs e)
//		{
//			foreach (object item in e.AddedItems)
//			{
//				Cell cell = item as Cell;
//				if (cell != null)
//				{
//					if (cell.IsEnabled)
//						Element.Model.RowSelected(cell);
//					break;
//				}
//			}

//			Control.SelectedItem = null;
//		}

//		void OnModelChanged(object sender, EventArgs eventArgs)
//		{
//			Control.ItemsSource = GetTableViewRow();
//		}

//		public IList<object> GetTableViewRow()
//		{
//			List<object> result = new List<object>();
			
//			foreach (var item in Element.Root)
//			{
//				if (!string.IsNullOrWhiteSpace(item.Title))
//					result.Add(item);
				
//				result.AddRange(item);
//			}
//			return result;
//		}

//		bool _isDisposed;

//		protected override void Dispose(bool disposing)
//		{
//			if (_isDisposed)
//				return;

//			if (disposing)
//			{
//				if (Control != null)
//				{
//					Control.SelectionChanged -= Control_SelectionChanged;
//				}

//				if(Element != null)
//				{
//					Element.ModelChanged -= OnModelChanged;
//				}
//			}

//			_isDisposed = true;
//			base.Dispose(disposing);
//		}
//	}
//}
