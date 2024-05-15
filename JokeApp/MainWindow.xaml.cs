using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JokeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            TB_Joke.Text = "Loading joke...";

            try {
             string joke = await GetJokeFromAPi();
                JokeDTO? jokeObj = JsonSerializer .Deserialize<JokeDTO>(joke);

                if (jokeObj == null)
                {
                    TB_Joke.Text = "No Joke to show;";
                    return;
                }
                if (jokeObj.Type == "single")
                {
                    TB_Joke.Text = jokeObj.Type + "\n-----\n" + jokeObj.Joke;
                } else
                {
                    TB_Joke.Text = jokeObj.Type + "\n----\n" + jokeObj.JokeSetup + "\n----\n" + jokeObj.JokeDelivery;
                }
            }
            catch (Exception ex){
                MessageBox.Show($"Flaied to get Joke: {ex.Message}");
            }
        }

        private  async Task<string> GetJokeFromAPi()
        {
            string respnse = await client.GetStringAsync("https://v2.jokeapi.dev/joke/Any");
            return respnse;
        }
    }
}