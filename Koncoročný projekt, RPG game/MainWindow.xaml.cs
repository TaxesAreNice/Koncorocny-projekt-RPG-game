using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Koncoročný_projekt__RPG_game.UI_Generations;

namespace Koncoročný_projekt__RPG_game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<List<Map_Block>> Map = [];
        private List<Inventory_Slots> Inventory_Code = [];
        private List<Inventory_Buttons> Inventory_butons = [];
        private bool Started = false;
        private bool inventory_on_slot = false;
        private bool inventory_on_slot_q = false;

        private string CurrentState = "Main"; //Main
        private string CurrentMain = "Map";

        private string itemNAME = "";

        DispatcherTimer inventory_click_checker = new DispatcherTimer();
        DispatcherTimer inventory_q_click_checker = new DispatcherTimer();

        PlayerMovementClass playerMovement = new PlayerMovementClass();
        InventoryInputs inventoryMovementClass = new InventoryInputs();


        public MainWindow()
        {
            InitializeComponent();

            inventory_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_click_checker.Tick += Inventory_Click_Checker_Tick;
            inventory_q_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_q_click_checker.Tick += Inventory_Q_Click_Checker_Tick;

        }

        private void Inventory_Q_Click_Checker_Tick(object? sender, EventArgs e)
        {
            int rowB = 0;
            int row = 0;
            int minesar = 0;
            int minesarB = 0;

            if (inventoryMovementClass.backup_chosed_But_x >= 4)
            {
                rowB = 1;
                minesarB += 4;
            }
            if (inventoryMovementClass.chosed_But_x >= 4)
            {
                row = 1;
                minesar += 4;
            }

            if (inventory_on_slot_q)
            {
                Inventory_butons[rowB].slots[inventoryMovementClass.backup_chosed_But_x - minesarB].Background = Brushes.DarkGray; // changes the last position
                Inventory_butons[row].slots[inventoryMovementClass.chosed_But_x - minesar].Background = Brushes.Gray; // changes the current position
                inventoryMovementClass.PressedTick_Q();
            }
            else
            {
                Inventory_butons[row].slots[inventoryMovementClass.chosed_But_x - minesar].Background = Brushes.DarkGray; // changes the current position
                inventory_on_slot_q = inventoryMovementClass.q_pressed;
            }
        }

        private void Inventory_Click_Checker_Tick(object? sender, EventArgs e)
        {
           


            if (inventory_on_slot)
            {
                
                Inventory_Code[inventoryMovementClass.backup_chosed_y].slots[inventoryMovementClass.backup_chosed_x].Background = Brushes.DarkGray; // changes the last position
                Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x].Background = Brushes.Gray; // changes the current position
                inventoryMovementClass.PressedTick();

            }
            else
            {
                Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x].Background = Brushes.DarkGray; // changes the current position
                inventory_on_slot = inventoryMovementClass.slot_pressed;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            Started = true;

            GeneretingMap();
            GeneretingInventory();


        }
        private void GeneretingMap()
        {
            List<Map_Block> row = [];
            int rowY = 0;

            for (int i = 0; i < 6; i++)
            {
                Map_Block roww = new Map_Block();

                row.Add(roww);


                roww.Margin = new Thickness(0, rowY + 5, 0, 0);
                Map_UI.Children.Add(roww);
                Map.Add(row);

                rowY += 100;
            }
            //row[0].blocks[0].Background = Brushes.Red;

        }
        private void GeneretingInventory()
        {
            int rowY = 0;
            int Yrow = 0;
            int EquepsX = 5;
            int Equeps_list_num = 0;

            for (int i = 0; i < 7; i++)
            {
                Inventory_Slots roww = new Inventory_Slots(Yrow, inventoryMovementClass);

                roww.Margin = new Thickness(900, rowY + 5 + 0, 0, 0);

                Inventory.Children.Add(roww);
                Inventory_Code.Add(roww);

                rowY += 100;
                Yrow++;
            }

            for (int j = 0; j < 2; j++)
            {
                Inventory_Buttons inventory_Buttons = new Inventory_Buttons(inventoryMovementClass, Equeps_list_num);
                inventory_Buttons.HorizontalAlignment = HorizontalAlignment.Left;
                inventory_Buttons.Margin = new Thickness(EquepsX, 5,5,5);

                Inventory_butons.Add(inventory_Buttons);
                Inventory.Children.Add(inventory_Buttons);

                EquepsX += 215;
                Equeps_list_num += 4;
            }
            

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Started) { return; }

            bool success = false;
            string key = e.Key.ToString();

            if (CurrentState == "Main" && CurrentMain == "Map")
            {
                MapMovement(success, key, e);
            }
            else if (CurrentState == "Inventory")
            {
                InventoryMovement(success, key, e);
            }
            else if (CurrentState == "Fight")
            {
                FightingMovement(success, key, e);
            }

        }

        private void FightingMovement(bool success, string key, KeyEventArgs e)
        {

        }

        private void InventoryMovement(bool success, string key, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Q:
                    MessageBox.Show("test");

                    inventoryMovementClass.Q_Pressed();
                    if (inventory_on_slot)
                    {
                        Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x].Content = ""; // changes the current position's text to nothing
                        Inventory_Code[inventoryMovementClass.chosed_y].names[inventoryMovementClass.chosed_x] = "";
                    }
                    break;
                case Key.E:
                    string content = Inventory_Code[inventoryMovementClass.chosed_y].names[inventoryMovementClass.chosed_x];

                    inventoryMovementClass.E_Pressed(content);
                    if (inventory_on_slot)
                    {
                        Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x].Content = "";
                        Inventory_Code[inventoryMovementClass.chosed_y].names[inventoryMovementClass.chosed_x] = "";
                    }
                    break;
                case Key.Escape:
                    Inventory_Open();
                    break;
            }
        }

        private void MapMovement(bool success, string key, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                    success = playerMovement.CheckingForWalls(key);
                    break;
                case Key.A:
                    success = playerMovement.CheckingForWalls(key);
                    break;
                case Key.S:
                    success = playerMovement.CheckingForWalls(key);
                    break;
                case Key.D:
                    success = playerMovement.CheckingForWalls(key);
                    break;
                case Key.Escape:
                    Inventory_Open();
                    break;
            }

            if (success)
            {
                ChangingPlayerPosition(key);
            }
        }

        private void ChangingPlayerPosition(string key)
        {
            Map[0][playerMovement.LastPlayerY].blocks[playerMovement.LastPlayerX].Background = Brushes.DarkGray; // changes the last position
            Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Background = Brushes.Red; // changes the current position
        }
        private void ChangingInventoryPosition(string key)
        {
            //    Inventory_Code[inventoryMovementClass.LastInventoryY].slots[inventoryMovementClass.LastInventoryX].Background = Brushes.DarkGray; // changes the last position
            //   Inventory_Code[inventoryMovementClass.InventoryY].slots[inventoryMovementClass.InventoryX].Background = Brushes.Gray; // changes the current position
        }
        private void Inventory_Open()
        {
            if (!Started) { return; }
            if (Inventory.Visibility == Visibility.Visible)
            {
                Inventory.Visibility = Visibility.Hidden;
                CurrentState = "Main";
                inventory_click_checker.Stop();
                inventory_q_click_checker.Stop();
                return;
            }

            Inventory.Visibility = Visibility.Visible;
            CurrentState = "Inventory";
            inventory_click_checker.Start();
            inventory_q_click_checker.Start();

        }

        private void Inventory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource != sender) { return; }

            inventory_on_slot = false;
            inventoryMovementClass.slot_pressed = false;
            inventory_on_slot_q  = false;
            inventoryMovementClass.q_pressed = false;

        }

        private void Add_Item_Click(object sender, RoutedEventArgs e)
        {
            Add_Item_To_Inventory();
        }

        private void item_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            itemNAME = item_name.Text;
        }

        private void Add_Item_To_Inventory()
        {
            string sucess = inventoryMovementClass.CheckingForYs(itemNAME);


            if (sucess == "inventory_full")
            {
                MessageBox.Show("Inventory is full!");
                return;
            }


            Inventory_Code[inventoryMovementClass.ender_y].slots[inventoryMovementClass.ender_x].Content = itemNAME; // changes the current position's text to the item
            Inventory_Code[inventoryMovementClass.ender_y].names[inventoryMovementClass.ender_x] = itemNAME;

            if (sucess == "hole")
            {
                inventoryMovementClass.FixingHolesXandYs();
            }
        }

        private void StartFight_but_Click(object sender, RoutedEventArgs e)
        {
            if (!Started) { return; }
            if (Fighting_UI.Visibility == Visibility.Visible)
            {
                Fighting_UI.Visibility = Visibility.Hidden;
                CurrentState = "Main";
                return;
            }

            Fighting_UI.Visibility = Visibility.Visible;
            CurrentState = "Fight";

        }
    }
}