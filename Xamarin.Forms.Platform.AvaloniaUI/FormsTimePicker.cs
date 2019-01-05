using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;

namespace Xamarin.Forms.Platform.AvaloniaUI
{
    public class FormsTimePicker : TextBox
    {
        #region Properties
        public static readonly DirectProperty<FormsTimePicker, TimeSpan?> TimeProperty = AvaloniaProperty.RegisterDirect<FormsTimePicker, TimeSpan?>(nameof(Time), o => o.Time, (o, v) => o.Time = v);
        private TimeSpan? _time;
        public TimeSpan? Time
        {
            get { return _time; }
            set { SetAndRaise(TimeProperty, ref _time, value); }
        }

        public static readonly DirectProperty<FormsTimePicker, string> TimeFormatProperty = AvaloniaProperty.RegisterDirect<FormsTimePicker, string>(nameof(TimeFormat), o => o.TimeFormat, (o, v) => o.TimeFormat = v);
        private string _timeFormat;
        public String TimeFormat
        {
            get { return _timeFormat; }
            set { SetAndRaise(TimeFormatProperty, ref _timeFormat, value); }
        }
        #endregion

        #region Events
        public delegate void TimeChangedEventHandler(object sender, TimeChangedEventArgs e);
        public event TimeChangedEventHandler TimeChanged;
        #endregion

        public FormsTimePicker()
        {

        }

        //public override void OnApplyTemplate()
        //{
        //	base.OnApplyTemplate();
        //	SetText();
        //}

        private void SetText()
        {
            if (Time == null)
                Text = null;
            else
            {
                var dateTime = new DateTime(Time.Value.Ticks);

                String text = dateTime.ToString(String.IsNullOrWhiteSpace(TimeFormat) ? @"hh\:mm" : TimeFormat.ToLower());
                if (text.CompareTo(Text) != 0)
                    Text = text;
            }
        }

        private void SetTime()
        {
            DateTime dateTime = DateTime.MinValue;
            String timeFormat = String.IsNullOrWhiteSpace(TimeFormat) ? @"hh\:mm" : TimeFormat.ToLower();

            if (DateTime.TryParseExact(Text, timeFormat, null, System.Globalization.DateTimeStyles.None, out dateTime))
            {
                if ((Time == null) || (Time != null && Time.Value.CompareTo(dateTime.TimeOfDay) != 0))
                {
                    if (dateTime.TimeOfDay < TimeSpan.FromHours(24) && dateTime.TimeOfDay > TimeSpan.Zero)
                        Time = dateTime.TimeOfDay;
                    else
                        SetText();
                }
            }
            else
                SetText();
        }

        #region Overrides
        //protected override void OnLostFocus(RoutedEventArgs e)
        //{
        //	SetTime();
        //	base.OnLostFocus(e);
        //}

        //protected override void OnGotFocus(RoutedEventArgs e)
        //{
        //	base.OnGotFocus(e);
        //}
        #endregion

        #region Property Changes
        private static void OnTimePropertyChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            FormsTimePicker element = d as FormsTimePicker;
            if (element == null)
                return;

            element.OnTimeChanged(e.OldValue as TimeSpan?, e.NewValue as TimeSpan?);
        }

        private void OnTimeChanged(TimeSpan? oldValue, TimeSpan? newValue)
        {
            SetText();

            if (TimeChanged != null)
                TimeChanged(this, new TimeChangedEventArgs(oldValue, newValue));
        }

        private static void OnTimeFormatPropertyChanged(AvaloniaObject d, AvaloniaPropertyChangedEventArgs e)
        {
            FormsTimePicker element = d as FormsTimePicker;
            if (element == null)
                return;

            element.OnTimeFormatChanged();
        }

        private void OnTimeFormatChanged()
        {
            SetText();
        }
        #endregion
    }

    public class TimeChangedEventArgs : EventArgs
    {
        private TimeSpan? _oldTime;
        private TimeSpan? _newTime;

        public TimeSpan? OldTime
        {
            get { return _oldTime; }
        }

        public TimeSpan? NewTime
        {
            get { return _newTime; }
        }

        public TimeChangedEventArgs(TimeSpan? oldTime, TimeSpan? newTime)
        {
            _oldTime = oldTime;
            _newTime = newTime;
        }
    }
}
