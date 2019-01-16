using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Xamarin.Forms.Platform.AvaloniaUI.Interfaces;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class SelectionChangedEventArgs : EventArgs
	{
		public SelectionChangedEventArgs(object oldElement, object newElement)
		{
			OldElement = oldElement;
			NewElement = newElement;
		}

		public object NewElement { get; private set; }

		public object OldElement { get; private set; }
	}

	//[Avalonia.Markup.ContentProperty("ItemsSource")]
	public class FormsMultiPage : FormsPage
	{
		public FormsTransitioningContentControl FormsContentControl { get; private set; }

		public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        public static readonly DirectProperty<FormsMultiPage, IContentLoader> ContentLoaderProperty = AvaloniaProperty.RegisterDirect<FormsMultiPage, IContentLoader>(nameof(ContentLoader), o => o.ContentLoader, (o, v) => o.ContentLoader = v);
        public static readonly AvaloniaProperty ItemsSourceProperty;// = AvaloniaProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(FormsMultiPage));
        public static readonly AvaloniaProperty SelectedItemProperty;// = AvaloniaProperty.Register("SelectedItem", typeof(object), typeof(FormsMultiPage), new PropertyMetadata(OnSelectedItemChanged));
        public static readonly AvaloniaProperty SelectedIndexProperty;// = AvaloniaProperty.Register("SelectedIndex", typeof(int), typeof(FormsMultiPage), new PropertyMetadata(0));

        private IContentLoader _contentLoader;
        public IContentLoader ContentLoader
        {
            get { return _contentLoader; }
            set { SetAndRaise(ContentLoaderProperty, ref _contentLoader, value); }
        }

        public ObservableCollection<object> ItemsSource
		{
			get { return (ObservableCollection<object>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}

		public object SelectedItem
		{
			get { return (object)GetValue(SelectedItemProperty); }
			set { SetValue(SelectedItemProperty, value); }
		}

		public int SelectedIndex
		{
			get { return (int)GetValue(SelectedIndexProperty); }
			set { SetValue(SelectedIndexProperty, value); }
		}

		private static void OnSelectedItemChanged(AvaloniaObject o, AvaloniaPropertyChangedEventArgs e)
		{
			if (e.OldValue == e.NewValue) return;
			((FormsMultiPage)o).OnSelectedItemChanged(e.OldValue, e.NewValue);
		}

		private void OnSelectedItemChanged(object oldValue, object newValue)
		{
			if (ItemsSource == null) return;
			SelectedIndex = ItemsSource.Cast<object>().ToList().IndexOf(newValue);
			SelectionChanged?.Invoke(this, new SelectionChangedEventArgs(oldValue, newValue));
		}

		public FormsMultiPage()
		{
            // TODO:
			//SetValue(FormsMultiPage.ItemsSourceProperty, new ObservableCollection<object>());
		}

		//public override void OnApplyTemplate()
		//{
		//	base.OnApplyTemplate();
		//	FormsContentControl = Template.FindName("PART_Multi_Content", this) as FormsTransitioningContentControl;
		//}

		//public override bool GetHasNavigationBar()
		//{
		//	if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
		//	{
		//		return page.GetHasNavigationBar();
		//	}
		//	return false;
		//}

		//public override IEnumerable<Control> GetPrimaryTopBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.PrimaryTopBarCommands);

		//	if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetPrimaryTopBarCommands());
		//	}

		//	return Controls;
		//}

		//public override IEnumerable<Control> GetSecondaryTopBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.SecondaryTopBarCommands);

		//	if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetSecondaryTopBarCommands());
		//	}

		//	return Controls;
		//}

		//public override IEnumerable<Control> GetPrimaryBottomBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.PrimaryBottomBarCommands);

		//	if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetPrimaryBottomBarCommands());
		//	}

		//	return Controls;
		//}

		//public override IEnumerable<Control> GetSecondaryBottomBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.SecondaryBottomBarCommands);

		//	if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetSecondaryBottomBarCommands());
		//	}

		//	return Controls;
		//}
	}
}
