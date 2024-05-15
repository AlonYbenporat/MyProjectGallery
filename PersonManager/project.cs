using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PersonManager
{
    public class Project : IProjectMeta
    {
        public string Name { get; set; } = "Person Manager";
        public BitmapImage Icon
        {
            get
            {
                string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                Uri uri = new Uri($"pack://application:,,,/{assemblyName};component/Resources/PersonManager2.png");
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
}

