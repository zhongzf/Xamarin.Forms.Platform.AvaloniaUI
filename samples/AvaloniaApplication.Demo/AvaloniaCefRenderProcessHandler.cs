using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public class AvaloniaCefRenderProcessHandler : CefGlue.Avalonia.AvaloniaCefRenderProcessHandler
    {
        protected override void OnWebKitInitialized()
        {
            var cefV8Handler = new AvaloniaCefV8Handler();

            const string javascriptCode = @"function avalonia() { }

            if (!avalonia) avalonia = { };

            (function() {

                avalonia.__defineGetter__('myParam',

                function() {

                    native function GetMyParam();

                    return GetMyParam();

                });

                avalonia.__defineSetter__('myParam',

                function(arg0) {

                    native function SetMyParam(arg0);

                    SetMyParam(arg0);

                });

                avalonia.myFunction = function(arg0, arg1, arg2) {

                    native function MyFunction(arg0, arg1, arg2);

                    return MyFunction(arg0, arg1, arg2);

                };

                avalonia.getMyParam = function() {

                    native function GetMyParam();

                    return GetMyParam();

                };

                avalonia.setMyParam = function(arg0) {

                    native function SetMyParam(arg0);

                    SetMyParam(arg0);

                };

            })();";

            CefRuntime.RegisterExtension("avalonia", javascriptCode, cefV8Handler);

            base.OnWebKitInitialized();
        }
    }
}
