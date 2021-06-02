using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test_UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(IdNews), "selected")]
    public partial class PageNews : ContentPage
    {
        public string IdNews
        {
            set
            {
                LoadNews(value);
            }
        }
        public PageNews()
        {
            InitializeComponent();
        }
        void LoadNews(string id)
        {
            try
            {
                News news = MainPage.listNews.FirstOrDefault(a => a.Id == int.Parse(id));
                BindingContext = news;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load news.");
            }
        }
    }

    
}