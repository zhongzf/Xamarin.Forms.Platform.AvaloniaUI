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
    }
}
