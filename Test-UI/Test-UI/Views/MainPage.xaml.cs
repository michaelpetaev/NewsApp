using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace Test_UI.Views
{
    public partial class MainPage : ContentPage
    {
        public static List<News> listNews = new List<News>();
        public MainPage()
        {
            InitializeComponent();
            updateNews();
        }

        async public void updateNews()
        {
            string response = await HttpRequest("https://education-erp.com/api/ClientApplication/News?schoolType=ballet&cityId=1&count=10");
            listNews = JsonConvert.DeserializeObject<List<News>>(response);
            collectionView.ItemsSource = listNews;
        }

        async public Task<string> HttpRequest(string url)
        {
            string json = "";
            try
            {
                HttpClient client = new HttpClient();
                json = await client.GetStringAsync(url);
                return json;
            }
            catch
            {
                return json;
            }
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null && e.CurrentSelection.Count!=0)
            {
                string id = ((News)e.CurrentSelection.FirstOrDefault()).Id.ToString();
                await Shell.Current.GoToAsync($"{nameof(Views.PageNews)}?selected={id}");
            }
            ((CollectionView)sender).SelectedItem = null;
        }
    }

    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string PreviewPath { get; set; }
        public string MainImagePath { get; set; }
        public List<string> AdditionalImagesPaths { get; set; }
        public object Language { get; set; }
    }

    public class UrlConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString();
            }
            else
            {
                return "https://junior-projects.com/images/logo.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }

    public class HtmlConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string without = HttpUtility.HtmlDecode(value.ToString());
            without = Regex.Replace(without, @"\<.*?\>", "");
            without = without.TrimStart();
            return without;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }

}
