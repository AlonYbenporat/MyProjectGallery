using System.IO;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tradecalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadJsonData();
            //client.BaseAddress = new Uri("https://663f5038e3a7c3218a4cc2b8.mockapi.io/");
        }

        private void LoadJsonData()
        {
            try { 
            string jsonFilePath = File.ReadAllText("trade.json");
            List<CurrencyInfo> currencies = JsonSerializer.Deserialize<List<CurrencyInfo>>(jsonFilePath);
            currencyDataGrid.ItemsSource = currencies;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("JSON file not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error deserializing JSON: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
            

    }
}