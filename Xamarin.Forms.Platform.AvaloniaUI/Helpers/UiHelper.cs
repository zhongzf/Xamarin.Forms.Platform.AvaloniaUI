using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.AvaloniaUI.Helpers
{
    static class UiHelper
    {
        public static void ExecuteInUiThread(Action action)
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                action?.Invoke();
            }
            else
            {
                Dispatcher.UIThread.InvokeAsync(action);
            }
        }
    }
}
