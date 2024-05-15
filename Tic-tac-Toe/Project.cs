using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Common;

namespace Tic_tac_Toe;

public class Project: IProjectMeta
{
    public string Name { get; set; } = "Tic-Tac-Toe Adv";
     public BitmapImage Icon
    {
        get
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/MyResoures/TicTaToe.png");
            return new BitmapImage(uri);
        }
    }

    public void Run()
    {
        MainWindow windows = new MainWindow();
        windows.ShowDialog();
    }


}
