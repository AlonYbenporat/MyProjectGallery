using System.IO;
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
using static StockView.MainWindow;

namespace LinqDemo;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
   
{ 
    List<Product>rawListOfProducts;
    public MainWindow()
    {
        InitializeComponent();
        LoadProducts();
        AddButtons("Get All", GetAllMethod, GetAllSyntax);
       
    }

    private void GetAllMethod(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Methode Clicked");
    }

    private void GetAllSyntax(object sender, RoutedEventArgs e)
    {
        var result = from product in  rawListOfProducts select product;
    }

    private void AddButtons(string name, RoutedEventHandler ClickMethod, RoutedEventHandler ClickSyntax)
    {
        StackPanel stackPanel = new StackPanel()
        {
            Orientation = Orientation.Horizontal
        };
        ButtonsStack.Children.Add(stackPanel);

        Button btnMethode = new Button
        {
            Margin = new Thickness(5),
            Padding = new Thickness(5),
            FontSize = 15,
            Content =   name + "(M)"

        };
        btnMethode.Click += ClickMethod;

        stackPanel.Children.Add(btnMethode);
        
        Button btnSyntax = new Button
        {
            Margin = new Thickness(5),
            Padding = new Thickness(5),
            FontSize = 15,
            Content = name + "(S)"
        };
        
        btnSyntax.Click += ClickMethod;
        stackPanel.Children.Add(btnSyntax);
    }

    private void LoadProducts()
    {
        string rawJson = File.ReadAllText("Products.json");
        List <Product> products = JsonSerializer.Deserialize<List<Product>>(rawJson);
        ResultDataGrid.ItemsSource = products;
    }
}