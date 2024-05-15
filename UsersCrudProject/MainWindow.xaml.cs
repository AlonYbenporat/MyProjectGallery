using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UsersCrudProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        private List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent(); 
            client.BaseAddress = new Uri("https://663f5038e3a7c3218a4cc2b8.mockapi.io/");
        }

        private async void Button_ClickLoad(object sender, RoutedEventArgs e)
        {
            users = await client.GetFromJsonAsync<List<User>>("users");
            UsersListBox.ItemsSource = users;

        }

        private async void Button_ClickAdd(object sender, RoutedEventArgs e)
        {
            User userToADD = new User {
                Name = TBUserName.Text,
                Email = TBUserEmail.Text,
            };
            await client.PostAsJsonAsync("users", userToADD);
            Button_ClickLoad(sender, e);

        }

        private async void Button_ClickUpdate(object sender, RoutedEventArgs e)
        {
            if(UsersListBox.SelectedItem is User userToUpdate)
            { 
                userToUpdate.Name = TBUserName.Text;
                userToUpdate.Email = TBUserEmail.Text;
                await client.PutAsJsonAsync("users/"+ userToUpdate.ID, userToUpdate);
                Button_ClickLoad(sender, e);
            
            }
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedItem is User userToUpdate)
            {
                if (MessageBox.Show("Are you Sure you wan to edlete", "delete", MessageBoxButton.YesNo)==MessageBoxResult.No)
                {
                    return;
                }
                client.DeleteAsync("users/" + userToUpdate.ID);
                Button_ClickLoad(sender,e);
            }
        }
    }
}