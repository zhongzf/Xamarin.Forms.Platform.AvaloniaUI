using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Xamarin.Forms.Platform.AvaloniaUI.Extensions;
using Xamarin.Forms.Platform.AvaloniaUI.Interfaces;

namespace Xamarin.Forms.Platform.AvaloniaUI.Controls
{
	//[TemplatePart(Name = "PART_Navigation_Content", Type = typeof(FormsTransitioningContentControl))]
	public class FormsNavigationPage : FormsPage, IFormsNavigation
	{
		public FormsTransitioningContentControl FormsContentControl { get; private set; }

		public ObservableCollection<object> InternalChildren { get; } = new ObservableCollection<object>();

        public static readonly AvaloniaProperty ContentLoaderProperty;// = AvaloniaProperty.Register("ContentLoader", typeof(IContentLoader), typeof(FormsNavigationPage), new PropertyMetadata(new DefaultContentLoader()));
        public static readonly AvaloniaProperty CurrentPageProperty;// = AvaloniaProperty.Register("CurrentPage", typeof(object), typeof(FormsNavigationPage));

		public IContentLoader ContentLoader
		{
			get { return (IContentLoader)GetValue(ContentLoaderProperty); }
			set { SetValue(ContentLoaderProperty, value); }
		}

		public object CurrentPage
		{
			get { return (object)GetValue(CurrentPageProperty); }
			set { SetValue(CurrentPageProperty, value); }
		}

		public int StackDepth
		{
			get { return InternalChildren.Count; }
		}

		public FormsNavigationPage()
		{
			//this.DefaultStyleKey = typeof(FormsNavigationPage);
		}

		public FormsNavigationPage(object root)
			: this()
		{
			this.Push(root);
		}

		//public override void OnApplyTemplate()
		//{
		//	base.OnApplyTemplate();
		//	FormsContentControl = Template.FindName("PART_Navigation_Content", this) as FormsTransitioningContentControl;
		//}

		public void InsertPageBefore(object page, object before)
		{
			int index = InternalChildren.IndexOf(before);
			InternalChildren.Insert(index, page);
			//ParentWindow?.SynchronizeAppBar();
		}

		public void RemovePage(object page)
		{
			if (InternalChildren.Remove(page))
			{
				if (FormsContentControl != null)
				{
					FormsContentControl.Transition = TransitionType.Normal;
				}
				CurrentPage = InternalChildren.Last();
			}

			//ParentWindow?.SynchronizeAppBar();
		}

		public void Pop()
		{
			Pop(true);
		}

		public void Pop(bool animated)
		{
			if (StackDepth <= 1)
				return;

			if (InternalChildren.Remove(InternalChildren.Last()))
			{
				if (FormsContentControl != null)
				{
					FormsContentControl.Transition = animated ? TransitionType.Right : TransitionType.Normal;
				}
				CurrentPage = InternalChildren.Last();
			}
		}

		public void PopToRoot()
		{
			PopToRoot(true);
		}

		public void PopToRoot(bool animated)
		{
			if (StackDepth <= 1)
				return;

			object[] childrenToRemove = InternalChildren.Skip(1).ToArray();
			foreach (object child in childrenToRemove)
				InternalChildren.Remove(child);

			if (FormsContentControl != null)
			{
				FormsContentControl.Transition = animated ? TransitionType.Right : TransitionType.Normal;
			}
			CurrentPage = InternalChildren.Last();
		}

		public void Push(object page)
		{
			Push(page, true);
		}

		public void Push(object page, bool animated)
		{
			InternalChildren.Add(page);
			if (FormsContentControl != null)
			{
				FormsContentControl.Transition = animated ? TransitionType.Left : TransitionType.Normal;
			}
			CurrentPage = page;
		}

		//public override string GetTitle()
		//{
		//	if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
		//	{
		//		return page.GetTitle();
		//	}
		//	return "";
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

		public bool GetHasBackButton()
		{
			if (FormsContentControl != null && FormsContentControl.Content is FormsPage page)
			{
				//return page.HasBackButton && StackDepth > 1;
			}
			return false;
		}

		public string GetBackButtonTitle()
		{
			if (StackDepth > 1)
			{
				return this.InternalChildren[StackDepth - 2].GetPropValue<string>("Title") ?? "Back";
			}
			return "";
		}

		public void PopModal()
		{
			PopModal(true);
		}

		public void PopModal(bool animated)
		{
			//ParentWindow?.PopModal(animated);
		}

		public void PushModal(object page)
		{
			PushModal(page, true);
		}

		public void PushModal(object page, bool animated)
		{
			//ParentWindow?.PushModal(page, animated);
		}


		public virtual void OnBackButtonPressed()
		{
			Pop();
		}
	}
}
