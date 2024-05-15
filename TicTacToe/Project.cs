using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TicTacToe; 

public class Project : IProjectMeta
{
public string Name { get; set; } = "Tic-Tac-Toe";
    public BitmapImage Icon {
        get
        {
          string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/TicTaToe.png");
            return new BitmapImage(uri);
        } 
    }

    //public BitmapImage Icon => new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}/Resources/TicTaToe.png", UriKind.Absolute));
    public void Run()
    {
        MainWindow windows = new MainWindow();
        windows.ShowDialog();
    }


}
