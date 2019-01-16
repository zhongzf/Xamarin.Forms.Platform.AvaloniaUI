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
    }
}
