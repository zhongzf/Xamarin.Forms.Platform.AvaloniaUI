//using System;
//using Avalonia.Input;
//using Avalonia.Controls;
//using Xamarin.Forms.Internals;

//namespace Xamarin.Forms.Platform.AvaloniaUI
//{
//	public class TextCellRenderer : ICellRenderer
//	{
//		public virtual Avalonia.DataTemplate GetTemplate(Cell cell)
//		{
//			/*if (cell.RealParent is ListView)
//			{
//				if (cell.GetIsGroupHeader<ItemsView<Cell>, Cell>())
//					return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["ListViewHeaderTextCell"];

//				return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["ListViewTextCell"];
//			}*/

//			return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["TextCell"];
//		}
//	}

//	public class EntryCellRendererCompleted : ICommand
//	{
//		public bool CanExecute(object parameter)
//		{
//			return true;
//		}

//		public event EventHandler CanExecuteChanged;

//		public void Execute(object parameter)
//		{
//			var entryCell = (IEntryCellController)parameter;
//			entryCell.SendCompleted();
//		}

//		protected virtual void OnCanExecuteChanged()
//		{
//			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//		}
//	}

//	public class EntryCellPhoneTextBox : TextBox
//	{
//		public event EventHandler KeyboardReturnPressed;

//		protected override void OnKeyUp(KeyEventArgs e)
//		{
//			if (e.Key == Key.Enter)
//			{
//				EventHandler handler = KeyboardReturnPressed;
//				if (handler != null)
//					handler(this, EventArgs.Empty);
//			}
//			base.OnKeyUp(e);
//		}
//	}

//	public class EntryCellRenderer : ICellRenderer
//	{
//		public virtual Avalonia.DataTemplate GetTemplate(Cell cell)
//		{
//			return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["EntryCell"];
//		}
//	}

//	public class ViewCellRenderer : ICellRenderer
//	{
//		public virtual Avalonia.DataTemplate GetTemplate(Cell cell)
//		{
//			return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["ViewCell"];
//		}
//	}

//	public class SwitchCellRenderer : ICellRenderer
//	{
//		public virtual Avalonia.DataTemplate GetTemplate(Cell cell)
//		{
//			return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["SwitchCell"];
//		}
//	}

//	public class ImageCellRenderer : ICellRenderer
//	{
//		public virtual Avalonia.DataTemplate GetTemplate(Cell cell)
//		{
//			return (Avalonia.DataTemplate)Avalonia.Application.Current.Resources["ImageCell"];
//		}
//	}
//}