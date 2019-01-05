using System.ComponentModel;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using static System.String;
using AControl = Avalonia.Controls.Control;
using ATextBox = Avalonia.Controls.TextBox;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class EntryRenderer : ViewRenderer<Entry, ATextBox>
    {
        bool _fontApplied;
        bool _ignoreTextChange;
        Brush _placeholderDefaultBrush;

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            if (e.NewElement != null)
            {
                if (Control == null) // Construct and SetNativeControl and suscribe control event
                {
                    SetNativeControl(new ATextBox());
                    Control.LostFocus += OnTextBoxUnfocused;
                    //Control.TextChanged += TextBoxOnTextChanged;
                    Control.KeyUp += TextBoxOnKeyUp;
                }

                // Update Control properties
                UpdateInputScope();
                UpdateIsPassword();
                UpdateText();
                UpdatePlaceholder();
                UpdateColor();
                UpdateFont();
                UpdateAlignment();
                UpdatePlaceholderColor();
                UpdateMaxLength();
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Entry.TextProperty.PropertyName)
                UpdateText();
            else if (e.PropertyName == Entry.PlaceholderProperty.PropertyName)
                UpdatePlaceholder();
            else if (e.PropertyName == Entry.IsPasswordProperty.PropertyName)
                UpdateIsPassword();
            else if (e.PropertyName == Entry.TextColorProperty.PropertyName)
                UpdateColor();
            else if (e.PropertyName == InputView.KeyboardProperty.PropertyName)
                UpdateInputScope();
            else if (e.PropertyName == Entry.FontAttributesProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Entry.FontFamilyProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Entry.FontSizeProperty.PropertyName)
                UpdateFont();
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
                UpdateAlignment();
            else if (e.PropertyName == Entry.PlaceholderColorProperty.PropertyName)
                UpdatePlaceholderColor();
            else if (e.PropertyName == InputView.MaxLengthProperty.PropertyName)
                UpdateMaxLength();
        }

        internal override void OnModelFocusChangeRequested(object sender, VisualElement.FocusRequestArgs args)
        {
            //if (args.Focus)
            //	args.Result = Control.Focus();
            //else
            //{
            //	UnfocusControl(Control);
            //	args.Result = true;
            //}
        }

        void OnTextBoxUnfocused(object sender, RoutedEventArgs e)
        {
            if (Element.TextColor.IsDefault)
                return;

            if (!IsNullOrEmpty(Element.Text))
                Control.Foreground = Element.TextColor.ToBrush();
        }

        void TextBoxOnKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Enter)
                ((IEntryController)Element).SendCompleted();
        }

        //void TextBoxOnTextChanged(object sender, Avalonia.Controls.TextChangedEventArgs textChangedEventArgs)
        //{
        //	// Signal to the UpdateText method that the change to TextProperty doesn't need to update the control
        //	// This prevents the cursor position from getting lost
        //	_ignoreTextChange = true;
        //	((IElementController)Element).SetValueFromRenderer(Entry.TextProperty, Control.Text);

        //	// If an Entry.TextChanged handler modified the value of the Entry's text, the values could now be 
        //	// out-of-sync; re-sync them and force the TextBox cursor to the end of the text
        //	string entryText = Element.Text;
        //	if (Control.Text != entryText)
        //	{
        //		Control.Text = entryText;
        //		if (Control.Text != null)
        //			Control.SelectionStart = Control.Text.Length;
        //	}

        //	_ignoreTextChange = false;
        //}

        void UpdateAlignment()
        {
            if (Control == null)
                return;

            Control.TextAlignment = Element.HorizontalTextAlignment.ToNativeTextAlignment();
        }

        void UpdateColor()
        {
            if (Control == null)
                return;

            Entry entry = Element;
            if (entry != null)
            {
                if (!IsNullOrEmpty(entry.Text))
                {
                    //if (!entry.TextColor.IsDefault)
                    //	Control.Foreground = entry.TextColor.ToBrush();
                    //else
                    //	Control.Foreground = (Brush)AControl.ForegroundProperty.GetMetadata(typeof(FormsTextBox)).DefaultValue;

                    // Force the PhoneTextBox control to do some internal bookkeeping
                    // so the colors change immediately and remain changed when the control gets focus
                    //Control.OnApplyTemplate();
                }
            }
            //else
            //	Control.Foreground = (Brush)AControl.ForegroundProperty.GetMetadata(typeof(FormsTextBox)).DefaultValue;
        }

        void UpdateFont()
        {
            if (Control == null)
                return;

            Entry entry = Element;

            if (entry == null)
                return;

            bool entryIsDefault = entry.FontFamily == null && entry.FontSize == Device.GetNamedSize(NamedSize.Default, typeof(Entry), true) && entry.FontAttributes == FontAttributes.None;

            if (entryIsDefault && !_fontApplied)
                return;

            if (entryIsDefault)
            {
                //Control.ClearValue(AControl.FontStyleProperty);
                //Control.ClearValue(AControl.FontSizeProperty);
                //Control.ClearValue(AControl.FontFamilyProperty);
                //Control.ClearValue(AControl.FontWeightProperty);
                //Control.ClearValue(AControl.FontStretchProperty);
            }
            else
                Control.ApplyFont(entry);

            _fontApplied = true;
        }

        void UpdateInputScope()
        {
            //Control.InputScope = Element.Keyboard.ToInputScope();
        }

        void UpdateIsPassword()
        {
            //Control.IsPassword = Element.IsPassword;
        }

        void UpdatePlaceholder()
        {
            //Control.PlaceholderText = Element.Placeholder ?? string.Empty;
        }

        void UpdatePlaceholderColor()
        {
            Color placeholderColor = Element.PlaceholderColor;

            //if (placeholderColor.IsDefault)
            //{
            //	if (_placeholderDefaultBrush == null)
            //	{
            //		_placeholderDefaultBrush = (Brush)AControl.ForegroundProperty.GetMetadata(typeof(FormsTextBox)).DefaultValue; 
            //	}

            //	// Use the cached default brush
            //	Control.PlaceholderForegroundBrush = _placeholderDefaultBrush;
            //	return;
            //}

            //if (_placeholderDefaultBrush == null)
            //{
            //	// Cache the default brush in case we need to set the color back to default
            //	_placeholderDefaultBrush = Control.PlaceholderForegroundBrush;
            //}

            //Control.PlaceholderForegroundBrush = placeholderColor.ToBrush();
        }

        void UpdateText()
        {
            // If the text property has changed because TextBoxOnTextChanged called SetValueFromRenderer,
            // we don't want to re-update the text and reset the cursor position
            if (_ignoreTextChange)
                return;

            if (Control.Text == Element.Text)
                return;

            Control.Text = Element.Text ?? "";
            //Control.Select(Control.Text == null ? 0 : Control.Text.Length, 0);
        }

        void UpdateMaxLength()
        {
            //Control.MaxLength = Element.MaxLength;

            var currentControlText = Control.Text;
            if (currentControlText != null)
            {
                if (currentControlText.Length > Element.MaxLength)
                {
                    Control.Text = currentControlText.Substring(0, Element.MaxLength);
                }
            }
        }

        bool _isDisposed;

        protected override void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (Control != null)
                {
                    Control.LostFocus -= OnTextBoxUnfocused;
                    //Control.TextChanged -= TextBoxOnTextChanged;
                    Control.KeyUp -= TextBoxOnKeyUp;
                }
            }

            _isDisposed = true;
            base.Dispose(disposing);
        }
    }
}
