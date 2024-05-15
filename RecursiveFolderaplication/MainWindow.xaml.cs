using Microsoft.Win32;
using System.IO;
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

namespace RecursiveFolderaplication
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

        private void SelecetFolderClick(object sender, RoutedEventArgs e)
        {
            OpenFolderDialog dlg = new OpenFolderDialog();
            if (dlg.ShowDialog() == false)
            {
                return;
            }
            SelcetedRootTextBlock.Text = dlg.FolderName;

            ResultsTreeeView.Items.Clear();
            TreeViewItem root = BuildDirectory(dlg.FolderName);
            ResultsTreeeView.Items.Add(root);



        }

        private TreeViewItem BuildDirectory(string folderName)
        {
            DirectoryInfo info = new DirectoryInfo(folderName);
            TreeViewItem directroyItem = new TreeViewItem()
            {
                Header = info.Name + " " + info.Extension + " " + info.CreationTime
            };

            
            foreach (DirectoryInfo dirinfo in info.GetDirectories())
            {
                TreeViewItem ChildBranch = BuildDirectory(dirinfo.FullName);
                directroyItem.Items.Add(ChildBranch);
            }
            foreach (FileInfo fileInfo in info.GetFiles())
            {
                TreeViewItem fileNode = new TreeViewItem()
                {
                    Header = $"{fileInfo.Name} {fileInfo.Extension} {fileInfo.CreationTime} {GetSizeString(fileInfo.Length)}"
                };
                directroyItem.Items.Add(fileNode);
            }

            return directroyItem;

        }
        private string GetSizeString(long sizeInBytes)
        {
            const long bytesInKB = 1024;
            const long bytesInMB = bytesInKB * 1024;
            const long bytesInGB = bytesInMB * 1024;

            if (sizeInBytes >= bytesInGB)
            {
                double sizeInGB = (double)sizeInBytes / bytesInGB;
                return $"{sizeInGB:F2} GB";
            }
            else if (sizeInBytes >= bytesInMB)
            {
                double sizeInMB = (double)sizeInBytes / bytesInMB;
                return $"{sizeInMB:F2} MB";
            }
            else if (sizeInBytes >= bytesInKB)
            {
                double sizeInKB = (double)sizeInBytes / bytesInKB;
                return $"{sizeInKB:F2} KB";
            }
            else
            {
                return $"{sizeInBytes} bytes";
            }
        }
    }
}