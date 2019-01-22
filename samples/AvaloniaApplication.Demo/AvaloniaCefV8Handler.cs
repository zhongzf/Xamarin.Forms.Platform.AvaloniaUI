using System;
using System.Collections.Generic;
using System.Text;
using Xilium.CefGlue;

namespace AvaloniaApplication.Demo
{
    public class AvaloniaCefV8Handler : CefV8Handler
    {
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            returnValue = CefV8Value.CreateString("test result from C#");
            exception = null;
            var arg0 = arguments[0].GetStringValue();
            var arg1 = arguments[1].GetIntValue();
            
            // TODO:
            return true;
        }
    }
}
