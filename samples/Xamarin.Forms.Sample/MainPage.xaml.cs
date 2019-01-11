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
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Debug.WriteLine("test");
            Application.Current.Quit();
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine(e.NewTextValue);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //webView.Source = "http://www.bing.com";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string startUrl = $"file:///{baseDirectory}app/index.html";
            webView.Source = startUrl;
            webView.Reload();
        }
    }
}
