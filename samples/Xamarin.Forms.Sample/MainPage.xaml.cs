using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xamarin.Forms.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //webView.Source = "http://www.bing.com";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string startUrl = $"file:///{baseDirectory}app/index.html";
            webView.Source = startUrl;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //webView.Eval("alert('test');");
            webView.Source = "http://www.bing.com";
        }

        private void Button_Back_Clicked(object sender, EventArgs e)
        {
            webView.GoBack();
        }

        private void Button_Forward_Clicked(object sender, EventArgs e)
        {
            webView.GoForward();
        }

        private void Button_Test_Clicked(object sender, EventArgs e)
        {
            //webView.Eval("eval('alert(1);')");
            //webView.Eval("console.log(1);");
            webView.Eval("$('.testdiv').html('test content - from c#');");
        }
    }
}
