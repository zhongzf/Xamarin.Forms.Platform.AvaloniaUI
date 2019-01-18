using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public static class AvaloniaCefGlueApplicationExtensions
    {
        public static T ConfigureAvaloniaCefGlue<T>(this T builder, string[] args, IServiceProvider serviceProvider) where T : AppBuilderBase<T>, new()
        {
            return builder.AfterSetup((b) =>
            {
                try
                {
                    CefRuntime.Load();
                }
                catch (DllNotFoundException ex)
                {

                }
                catch (CefRuntimeException ex)
                {

                }
                catch (Exception ex)
                {

                }

                var mainArgs = new CefMainArgs(args);
                var cefApp = new AvaloniaCefApp(serviceProvider);

                var exitCode = CefRuntime.ExecuteProcess(mainArgs, cefApp, IntPtr.Zero);
                if (exitCode != -1) { return; }

                var location = System.Reflection.Assembly.GetEntryAssembly().Location;
                var directory = System.IO.Path.GetDirectoryName(location);

                var cefSettings = new CefSettings
                {

                    //SingleProcess = true,
                    WindowlessRenderingEnabled = true,
                    MultiThreadedMessageLoop = false,
                    LogSeverity = CefLogSeverity.Disable,
                    LogFile = "cef.log",
                    //ExternalMessagePump = true
                };
                cefSettings.NoSandbox = true;

                try
                {
                    CefRuntime.Initialize(mainArgs, cefSettings, cefApp, IntPtr.Zero);
                }
                catch (CefRuntimeException ex)
                {

                }
            });
        }

    }
}
