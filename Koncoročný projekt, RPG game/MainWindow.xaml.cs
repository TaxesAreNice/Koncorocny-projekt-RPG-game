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
using Koncoročný_projekt__RPG_game.UI_Generations;

namespace Koncoročný_projekt__RPG_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<List<Map_Block>> Map = [];
      
        public MainWindow()
        {
            InitializeComponent();


          

        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            List<Map_Block> row = [];

            for (int i = 0; i < 1; i++)
            {
                Map_Block block = new Map_Block();

                row.Add(block);
                
                
                
                Map_UI.Children.Add(block);
            }
        }
    }
}