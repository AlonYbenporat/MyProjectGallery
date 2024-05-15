using Common;
using MyProjectGallery.Controls;
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
using MemoryGame;



namespace MyProjectGallery;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IProjectMeta[] projects = new IProjectMeta[]
    {
       new MemoryGame.Project(),
      new PersonManager.Project(),
       new TicTacToe.Project(),
       new Tic_tac_Toe.Project() 
      
       

       
      
    };
    public MainWindow()
    {
        InitializeComponent();
        InitlaizeProjectButtons();
    }

    private void InitlaizeProjectButtons()
    {
        foreach (var project in projects) 
        {
            

            ProjectButton button = new ProjectButton(project)
            {
                Margin = new Thickness(10),
                Width = 100,
                Height = 130
            };
             ProjectPanel.Children.Add(button);
           
        }
    }
}