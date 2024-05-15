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

namespace Rescursive
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(UserInput.Text, out int number)) 
            { 
                int result = Facturial(number);

                Result.Text = $" the valus of facturial of {number} is {result}";


            }else
            {
                Result.Text = "invalif Value";
            }

        }

        private int Facturial(int n)
        {
            if (n <= 1)
            {
                return(n*1);
            }
            return n * Facturial(n-1);
        }
    }
}