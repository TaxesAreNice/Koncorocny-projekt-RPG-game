using System;
using System.IO;
using System.Runtime;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Tester
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

        private TheStuff _stuff = new TheStuff();
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "config.json");


        private void LoadData(TheStuff stuff)
        {
            if (File.Exists(_filePath))
            {
                // Read text from file and convert back into the class
                string json = File.ReadAllText(_filePath);
                _stuff = JsonSerializer.Deserialize<TheStuff>(json);
            }        }

        // --- THE SAVE FUNCTION ---
        private void SaveData(TheStuff _stuff)
        {
            // Ensure the "Data" directory actually exists before saving
            string directory = System.IO.Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            // Convert class to text and write to file
            string json = JsonSerializer.Serialize(_stuff);
            File.WriteAllText(_filePath, json);
        }

        private void Grr_TextChanged(object sender, TextChangedEventArgs e)
        {
            _stuff.Name = Grr.Text;
            SaveData();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}