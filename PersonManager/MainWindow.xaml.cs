using PersonManager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace PersonManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const string filePath = "Pepole.json";
    private readonly ICollectionView pepoleView;
    
    public MainWindow()
    {
        InitializeComponent();
        pepoleView = CollectionViewSource.GetDefaultView(People);
        PepolesDataGrid.ItemsSource = pepoleView;
        LoadedData();
    }

   private ObservableCollection<Person> People
    {
        get; 
        set;
    }= new ObservableCollection<Person>();
    //private List<Person> People
    //{
    //    get; 
    //    set;
    //} =new List<Person>();

    public void HandleSelectionChnged(object sender, SelectionChangedEventArgs e)
    {
        if (PepolesDataGrid.SelectedItem is Person selctedPerson)
        {
            TB_ID.Text = selctedPerson.ID.ToString();
            TB_Name.Text = selctedPerson.Name.ToString();
            TB_Age.Text = selctedPerson.Age.ToString();
        }
    }
    private void HandleAddClick(object sender, RoutedEventArgs e)
    {
        if (
            int.TryParse(TB_ID.Text, out int id) &&
            int.TryParse(TB_Age.Text, out int age) &&
            TB_Name.Text.Length > 0)
        {
            Person newPerson = new Person()
            {
                ID = GenerateID(),
                Name = TB_Name.Text,
                Age = age
            };
            People.Add(newPerson);
            SaveData();
            ClearForm();
        }
        




    }

    private int GenerateID()
    {
        return People.Count == 0 ? 1 :People.Max(p => p.ID) +1 ;
    }

    public void HandleUpdateClick(object sender, RoutedEventArgs e)
    {
        if (PepolesDataGrid.SelectedItems is Person selectedPeron && 
            int.TryParse(TB_ID.Text, out int id)&&
            int.TryParse(TB_Age.Text, out int age)&&
            TB_Name.Text.Length  > 0) {

     
        
       
            selectedPeron.ID = id;
            selectedPeron.Name = TB_Name.Text;
            selectedPeron.Age = age;

            PepolesDataGrid.Items.Refresh();
            SaveData();
            ClearForm();
        }
            

    }
    private void HandleDelteClick(object sender, RoutedEventArgs e)
    {
        MessageBoxResult result = MessageBox.Show("Are you syre?", "Delte", MessageBoxButton.OKCancel);
        if (result == MessageBoxResult.Cancel) 
        { 
            return; 
        }
        Button btn = sender as Button;
        if(btn == null)
        {
            return ;
        }
        if(btn.DataContext is Person personToDelete)
        {
            People.Remove(personToDelete);
            SaveData();
        }
    }
        private void HandleFilterKeyUp(object sender, KeyEventArgs e)
            {
                string filterString = TB_Filter.Text.ToLower();
                pepoleView.Filter = o =>
        {
                if(o is Person personToFilter)
            {
                return
                personToFilter.Name.ToLower().Contains(filterString);
            }
                return false;
        };

            }

    private void HandleFilterFocus(object sender, RoutedEventArgs e)
    {
        if (TB_Filter.Text != "Filter...")
        {
            return;
        }
        TB_Filter.Text = "";
    }

    private void HandleFilterLostFocus(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(TB_Filter.Text))
        {
            return;
        }
        TB_Filter.Text = "Filter...";
    }

    private void ClearForm()
    {
        TB_ID.Clear();
        TB_Name.Clear();
        TB_Age.Clear();
        PepolesDataGrid.SelectedItem = null;
    }

    private void SaveData()
    {
        try { 
        string rawata = JsonSerializer.Serialize(People);
        File.WriteAllText(filePath, rawata);
    }catch (Exception ex)
        {
            MessageBox.Show($"Faild to Save data: {ex.Message}");
        }
        }

    private void LoadedData()
    {
        if (!File.Exists(filePath))
        {
            return;
        }
        //People = new List<Person>();
        try { 
       string rawData = File.ReadAllText(filePath);
        List<Person>? result = JsonSerializer.Deserialize<List<Person>>(rawData); 
        if(result == null)
        {
            return;
        }
        foreach( Person person in result )
        {
            People.Add(person);
        }
        } catch( Exception ex) {
            MessageBox.Show($"Faild to load data: {ex.Message}");
            
        
        }
    }

}