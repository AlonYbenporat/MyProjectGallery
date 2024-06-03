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
        AddButtons("Get All Names", GetAllNamesMethod, GetAllNamesSyntax);
        AddButtons("Get All New Obj", GetAllObjMethod, GetAllObjSyntax);
        AddButtons("Get All New AnonObj", GetAllAnonMethod, GetAllAnonSyntax);

        AddButtons("Order By", OrderByMethod, OrderBySyntax);
        AddButtons("Use String Ext", UseStringExtension, UseStringExtension);

    }

    private void UseStringExtension(object sender, RoutedEventArgs e)
    {
        string result = StringExtensions.FirstLetterToupper("simon");
        string result2 = "bob".FirstLetterToupper();
        MessageBox.Show(result);
        MessageBox.Show(result2);

        if (2.IsEven())
        {
            MessageBox.Show("i am Even");
        }
        else
        {
            MessageBox.Show("I Am Odd");
        }
    }

    private void OrderByMethod(object obj, RoutedEventArgs e)
    {
        var result =
            rawListOfProducts
            .OrderBy(product => product.CategoryId)
            .ThenByDescending(product => product.Name)

            .Select(product => product);
            result.ReturnAWord();
        
    }
    private void  OrderBySyntax(object sender,RoutedEventArgs e)
    {
        var result = 
            from product in rawListOfProducts
            orderby product.CategoryId, product.Name ascending
            select product;
        ResultDataGrid.ItemsSource = result;
    }
    private void GetAllAnonMethod(object sender, RoutedEventArgs e)
    {
        var result =
               rawListOfProducts.Select(product => new 
               {
                   SomeName = product.Name

               });
        ResultDataGrid.ItemsSource = result;
    }

    private void GetAllAnonSyntax(object sender, RoutedEventArgs e)
    {
        var result =

             from product in rawListOfProducts
             select new 
             {
                 MyName = product.Name,


             };
        
        ResultDataGrid.ItemsSource = result;


    }
    private void GetAllObjMethod(object sender, RoutedEventArgs e)
    {
       var result =
              rawListOfProducts.Select(product => new ShortProduct
              {
                  MyName = product.Name

              });
              ResultDataGrid.ItemsSource = result;
    }

    private void GetAllObjSyntax(object sender, RoutedEventArgs e)
    {
        IEnumerable<ShortProduct> result =
            
             from product in rawListOfProducts
             select new ShortProduct
             {
                MyName = product.Name,


             };
        //List<Product> listResult = result.ToList();
        ResultDataGrid.ItemsSource = result;


    }
    public class ShortProduct
    {
        public string MyName { get; set; }
    }
    private void GetAllNamesMethod(object sender, RoutedEventArgs e)
    {
        IEnumerable<string> result =
              rawListOfProducts.Select(product => product.Name);
        List<string> listOfNames =result.ToList();
        ResultDataGrid.ItemsSource = listOfNames;
    }

    private void GetAllNamesSyntax(object sender, RoutedEventArgs e)
    {
        IEnumerable<string> result =
            from product in rawListOfProducts
            select product.Name;
        List<string>listOfNames = result.ToList();

        ResultDataGrid.ItemsSource = result;
    }

        private void GetAllMethod(object sender, RoutedEventArgs e)
    {
        IEnumerable<Product> result =
              rawListOfProducts.Select(product => product);
              ResultDataGrid.ItemsSource = result;
    }

    private void GetAllSyntax(object sender, RoutedEventArgs e)
    {
       IEnumerable<Product> result = 
            from product in  rawListOfProducts 
            select product;
        //List<Product> listResult = result.ToList();
        ResultDataGrid.ItemsSource = result;


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
        
        btnSyntax.Click += ClickSyntax;
        stackPanel.Children.Add(btnSyntax);
    }

    private void LoadProducts()
    {
        string rawJson = File.ReadAllText("Products.json");
        rawListOfProducts = JsonSerializer.Deserialize<List<Product>>(rawJson);
        //ResultDataGrid.ItemsSource = products;
    }
}