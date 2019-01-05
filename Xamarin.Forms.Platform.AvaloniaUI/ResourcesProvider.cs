using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    internal class ResourcesProvider : ISystemResourcesProvider
    {
        ResourceDictionary _dictionary;

        public IResourceDictionary GetSystemResources()
        {
            _dictionary = new ResourceDictionary();

            UpdateStyles();

            return _dictionary;
        }

        Style GetStyle(Avalonia.Styling.Style style, Avalonia.Controls.TextBlock hackbox)
        {
            //hackbox.Style = style;

            var result = new Style(typeof(Label));
            result.Setters.Add(new Setter { Property = Label.FontFamilyProperty, Value = hackbox.FontFamily });
            result.Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = hackbox.FontSize });

            return result;
        }

        void UpdateStyles()
        {
            //var textBlock = new Avalonia.Controls.TextBlock();
            //_dictionary[Device.Styles.TitleStyleKey] = GetStyle((Avalonia.Styling.Style)Avalonia.Application.Current.Resources["HeaderTextBlockStyle"], textBlock);
            //_dictionary[Device.Styles.SubtitleStyleKey] = GetStyle((Avalonia.Styling.Style)Avalonia.Application.Current.Resources["SubheaderTextBlockStyle"], textBlock);
            //_dictionary[Device.Styles.BodyStyleKey] = GetStyle((Avalonia.Styling.Style)Avalonia.Application.Current.Resources["BodyTextBlockStyle"], textBlock);
            //_dictionary[Device.Styles.CaptionStyleKey] = GetStyle((Avalonia.Styling.Style)Avalonia.Application.Current.Resources["CaptionTextBlockStyle"], textBlock);
            //_dictionary[Device.Styles.ListItemTextStyleKey] = GetStyle((Avalonia.Styling.Style)Avalonia.Application.Current.Resources["BaseTextBlockStyle"], textBlock);
            //_dictionary[Device.Styles.ListItemDetailTextStyleKey] = GetStyle((Avalonia.Styling.Style)Avalonia.Application.Current.Resources["BodyTextBlockStyle"], textBlock);
        }
    }
}
