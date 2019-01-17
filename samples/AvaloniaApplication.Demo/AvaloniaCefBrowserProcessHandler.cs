using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;
using Avalonia.Threading;
using System.Reactive.Linq;

namespace AvaloniaApplication.Demo
{
    public sealed class AvaloniaCefBrowserProcessHandler : CefBrowserProcessHandler
    {
        IDisposable _current;
        private object schedule = new object();

        protected override void OnScheduleMessagePumpWork(long delayMs)
        {
            lock (schedule)
            {
                if (_current != null)
                {
                    _current.Dispose();
                }

                if (delayMs <= 0)
                {
                    delayMs = 1;
                }

                _current = Observable.Interval(TimeSpan.FromMilliseconds(delayMs)).ObserveOn(AvaloniaScheduler.Instance).Subscribe((i) =>
                {
                    CefRuntime.DoMessageLoopWork();
                });
            }
        }
    }
}
