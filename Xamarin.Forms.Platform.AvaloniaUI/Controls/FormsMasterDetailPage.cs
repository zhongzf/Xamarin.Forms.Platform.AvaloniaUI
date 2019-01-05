using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using Xamarin.Forms.Platform.AvaloniaUI.Interfaces;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	public class FormsMasterDetailPage : FormsPage
	{
		FormsContentControl lightMasterContentControl;
		FormsContentControl lightDetailContentControl;

        public static readonly AvaloniaProperty MasterPageProperty;// = AvaloniaProperty.Register("MasterPage", typeof(object), typeof(FormsMasterDetailPage));
        public static readonly AvaloniaProperty DetailPageProperty;// = AvaloniaProperty.Register("DetailPage", typeof(object), typeof(FormsMasterDetailPage));
        public static readonly AvaloniaProperty ContentLoaderProperty;// = AvaloniaProperty.Register("ContentLoader", typeof(IContentLoader), typeof(FormsMasterDetailPage), new PropertyMetadata(new DefaultContentLoader()));
        public static readonly AvaloniaProperty IsPresentedProperty;// = AvaloniaProperty.Register("IsPresented", typeof(bool), typeof(FormsMasterDetailPage));

		public object MasterPage
		{
			get { return (object)GetValue(MasterPageProperty); }
			set { SetValue(MasterPageProperty, value); }
		}

		public object DetailPage
		{
			get { return (object)GetValue(DetailPageProperty); }
			set { SetValue(DetailPageProperty, value); }
		}

		public bool IsPresented
		{
			get { return (bool)GetValue(IsPresentedProperty); }
			set { SetValue(IsPresentedProperty, value); }
		}

		public IContentLoader ContentLoader
		{
			get { return (IContentLoader)GetValue(ContentLoaderProperty); }
			set { SetValue(ContentLoaderProperty, value); }
		}

		public FormsMasterDetailPage()
		{
			//this.DefaultStyleKey = typeof(FormsMasterDetailPage);
		}

		//public override void OnApplyTemplate()
		//{
		//	base.OnApplyTemplate();
		//	lightMasterContentControl = Template.FindName("PART_Master", this) as FormsContentControl;
		//	lightDetailContentControl = Template.FindName("PART_Detail_Content", this) as FormsContentControl;
		//}

		//public override string GetTitle()
		//{
		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		return page.GetTitle();
		//	}
		//	return this.Title;
		//}

		//public override Brush GetTitleBarBackgroundColor()
		//{
		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		return page.GetTitleBarBackgroundColor();
		//	}
		//	return this.TitleBarBackgroundColor;
		//}

		//public override Brush GetTitleBarTextColor()
		//{
		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		return page.GetTitleBarTextColor();
		//	}
		//	return this.TitleBarTextColor;
		//}

		//public override IEnumerable<Control> GetPrimaryTopBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.PrimaryTopBarCommands);

		//	if (lightMasterContentControl != null && lightMasterContentControl.Content is FormsPage masterPage)
		//	{
		//		Controls.AddRange(masterPage.GetPrimaryTopBarCommands());
		//	}

		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetPrimaryTopBarCommands());
		//	}

		//	return Controls;
		//}

		//public override IEnumerable<Control> GetSecondaryTopBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.SecondaryTopBarCommands);

		//	if (lightMasterContentControl != null && lightMasterContentControl.Content is FormsPage masterPage)
		//	{
		//		Controls.AddRange(masterPage.GetSecondaryTopBarCommands());
		//	}

		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetSecondaryTopBarCommands());
		//	}

		//	return Controls;
		//}

		//public override IEnumerable<Control> GetPrimaryBottomBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.PrimaryBottomBarCommands);

		//	if (lightMasterContentControl != null && lightMasterContentControl.Content is FormsPage masterPage)
		//	{
		//		Controls.AddRange(masterPage.GetPrimaryBottomBarCommands());
		//	}

		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetPrimaryBottomBarCommands());
		//	}

		//	return Controls;
		//}

		//public override IEnumerable<Control> GetSecondaryBottomBarCommands()
		//{
		//	List<Control> Controls = new List<Control>();
		//	Controls.AddRange(this.SecondaryBottomBarCommands);

		//	if (lightMasterContentControl != null && lightMasterContentControl.Content is FormsPage masterPage)
		//	{
		//		Controls.AddRange(masterPage.GetSecondaryBottomBarCommands());
		//	}

		//	if (lightDetailContentControl != null && lightDetailContentControl.Content is FormsPage page)
		//	{
		//		Controls.AddRange(page.GetSecondaryBottomBarCommands());
		//	}

		//	return Controls;
		//}
	}
}
