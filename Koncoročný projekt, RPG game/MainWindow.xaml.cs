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
using static Koncoročný_projekt__RPG_game.UI_Generations.MapBlocks_Insides;

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
        private List<Fighting_EnemySpawner> current_enemies = [];

        private bool Started = false;
        private bool inventory_on_slot = false;
        private bool inventory_on_slot_q = false;

        private string CurrentState = "Main"; //Main
        private string CurrentMain = "Map";

        private string itemNAME = "";
        private string enemy_name = "";

        DispatcherTimer inventory_click_checker = new DispatcherTimer();
        DispatcherTimer inventory_q_click_checker = new DispatcherTimer();

        PlayerMovementClass playerMovement = new PlayerMovementClass();
        InventoryInputs inventoryMovementClass = new InventoryInputs();
        Fighting fighting = new Fighting();
        private enum MapEdge
        {
            None,
            Top,
            Bottom,
            Left,
            Right
        }

        public MainWindow()
        {
            InitializeComponent();

            inventory_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_click_checker.Tick += Inventory_Click_Checker_Tick;
            inventory_q_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_q_click_checker.Tick += Inventory_Q_Click_Checker_Tick;

            Studio_Buttons.Add(Slot_1_Studio);
            Studio_Buttons.Add(Slot_2_Studio);
            Studio_Buttons.Add(Slot_3_Studio);
            Studio_Buttons.Add(Slot_4_Studio);
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
                Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x].Background = Brushes.Gray;
                inventoryMovementClass.PressedTick();

            }
            else
            {
                Inventory_Code[inventoryMovementClass.backup_chosed_y].slots[inventoryMovementClass.backup_chosed_x].Background = Brushes.DarkGray;

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
                inventory_Buttons.Margin = new Thickness(EquepsX, 5, 5, 5);

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
            // We only care about E and Q if a slot is actually selected
            int x = inventoryMovementClass.chosed_x;
            int y = inventoryMovementClass.chosed_y;

            switch (e.Key)
            {
                case Key.E: // Uses items
                    if (inventory_on_slot)
                    {
                        string contentE = Inventory_Code[y].names[x];

                        // 1. Logic: Check if it's a potion/weapon etc.
                        inventoryMovementClass.E_Pressed(contentE);

                        // 2. Visual: Clear the image
                        Inventory_Code[y].slots[x].image.Source = null;
                        Inventory_Code[y].names[x] = "";

                        // 3. Logic: Mark as a "Hole" so it can be filled later
                        inventoryMovementClass.ClearSlot(x, y);
                    }
                    break;

                case Key.Q: // Removes items
                    if (inventory_on_slot)
                    {
                        // 1. Logic: Register that Q was pressed
                        inventoryMovementClass.Q_Pressed();

                        // 2. Visual: Clear the image
                        Inventory_Code[y].slots[x].image.Source = null;
                        Inventory_Code[y].names[x] = "";

                        // 3. Logic: Mark as a "Hole"
                        inventoryMovementClass.ClearSlot(x, y);
                    }
                    break;

                case Key.Escape:
                    Inventory_Open();
                    break;
            }
        }

        private bool GetCurrentEdge(int x, int y)
        {
            // Check X (Width is 12, so max index is 11)
            if (x <= 0) return true;
            if (x >= playerMovement.MAX_x) return true;

            // Check Y (Height is 6, so max index is 5)
            if (y <= 0) return true;
            if (y >= playerMovement.MAX_y) return true;
            return false;
        }
        private void MapMovement(bool success, string key, KeyEventArgs e)
        {
            bool mappEddgee = GetCurrentEdge(playerMovement.PlayerX, playerMovement.PlayerY);

            if (mappEddgee)
            {
                switch (e.Key)
                {
                    case Key.W:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], mappEddgee);
                        break;
                    case Key.A:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], mappEddgee);
                        break;
                    case Key.S:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], mappEddgee);
                        break;
                    case Key.D:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], mappEddgee);
                        break;
                    case Key.Escape:
                        Inventory_Open();
                        break;
                }
            }
            else
            {
                switch (e.Key)
                {
                    case Key.W:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY - 1].blocks[playerMovement.PlayerX], mappEddgee);
                        break;
                    case Key.A:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX - 1], mappEddgee);
                        break;
                    case Key.S:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY + 1].blocks[playerMovement.PlayerX], mappEddgee);
                        break;
                    case Key.D:
                        success = playerMovement.CheckingForWalls(key, Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX], Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX + 1], mappEddgee);
                        break;
                    case Key.Escape:
                        Inventory_Open();
                        break;
                }
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
           // SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Middle, "Characters", "Player", "AGuy");


            // This changes the actual map data


            // Then update your UI label
            CurrentBlockType.Content = $"Current block: {Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].block_type}";
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
            inventory_on_slot_q = false;
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
            // 1. Logic sets ender_x/y to the correct spot (either a hole or the next empty slot)
            string success = inventoryMovementClass.CheckingForYs(itemNAME);

            if (success == "inventory_full")
            {
                MessageBox.Show("Your inventory is full! You can't add more items.");
                return;
            }


            // 2. Get the current target coordinates
            int tx = inventoryMovementClass.ender_x;
            int ty = inventoryMovementClass.ender_y;

            // 3. Draw the item
            SetGameImage(Inventory_Code[ty].slots[tx].image, "Items", "faf", "AGuy");
            Inventory_Code[ty].names[tx] = itemNAME;

            // 4. Handle the pointers
            if (success == "hole")
            {
                // Put the pointer back to the "real" end of the inventory
                inventoryMovementClass.FixingHolesXandYs();
            }
            else
            {
                // ONLY move the pointer forward if we didn't just fill a hole
                inventoryMovementClass.MovePointerForward();
            }
        }

        private void StartFight_but_Click(object sender, RoutedEventArgs e)
        {
            if (!Started) { return; }
            if (Fighting_UI.Visibility == Visibility.Visible)
            {
                Fighting_UI.Visibility = Visibility.Hidden;
                CurrentState = "Main";
                Enemy_Grid.Children.Clear();
                current_enemies.Clear();
                return;
            }

            Fighting_UI.Visibility = Visibility.Visible;
            CurrentState = "Fight";
            Spawing_Enemies();

        }

        private void Enemy_Num_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Enemy_Num.Text is int) { return; }

            enemy_name = Enemy_Num.Text;

            if (fighting.currentEnemies.Count < 4)
            {
                fighting.currentEnemies.Add(enemy_name);
            }
        }

        private void Spawing_Enemies()
        {
            int enemy_num = fighting.currentEnemies.Count;
            int space_off_x = 0;
            int space_off_y = 0;
            string name = "";

            switch (enemy_num)
            {
                case 0:
                    MessageBox.Show("You need to add at least 1 enemy!");
                    return;
                case 1:
                    Spawing_enemy(name, space_off_x, space_off_y);
                    break;
                case 2:
                    for (int i = 0; i < enemy_num; i++)
                    {
                        if (i == 1)
                        {
                            space_off_x += 450;
                        }
                        else
                        {
                            space_off_x = -200;
                        }
                        Spawing_enemy(name, space_off_x, space_off_y);
                    }
                    break;
                case 3:
                    for (int i = 0; i < enemy_num; i++)
                    {
                        if (i == 2)
                        {
                            space_off_x = 100;
                            space_off_y = 320;
                        }
                        else if (i == 1)
                        {
                            space_off_x = -150;
                        }
                        else
                        {
                            space_off_x += 300;
                            space_off_y = -100;
                        }
                        Spawing_enemy(name, space_off_x, space_off_y);
                    }
                    break;
                case 4:
                    for (int i = 0; i < enemy_num; i++)
                    {
                        if (i == 0) // top-left
                        {
                            space_off_x = -220;
                            space_off_y = -220;
                        }
                        else if (i == 1) // top-right
                        {
                            space_off_x = 220;
                            space_off_y = -220;
                        }
                        else if (i == 2) // bottom-left
                        {
                            space_off_x = -220;
                            space_off_y = 220;
                        }
                        else if (i == 3) // bottom-right
                        {
                            space_off_x = 220;
                            space_off_y = 220;
                        }

                        Spawing_enemy(name, space_off_x, space_off_y);
                    }
                    break;
            }
        }
        private void Spawing_enemy(string name, int space_off_x, int space_off_y)
        {
            Fighting_EnemySpawner fighting_EnemySpawner = new Fighting_EnemySpawner();
            fighting_EnemySpawner.Margin = new Thickness(space_off_x, space_off_y, 0, 0);
            Enemy_Grid.Children.Add(fighting_EnemySpawner);

            current_enemies.Add(fighting_EnemySpawner);
        }

        private void NextTurn_Click(object sender, RoutedEventArgs e)
        {
            current_enemies[0].stuff[0].prog.Value = 100;
            current_enemies[0].stuff[0].progLab.Content = "100hp";

            current_enemies[0].stuff[0].atkLabel.Content = "10";
            current_enemies[0].stuff[0].defLabel.Content = "5";
        }


        public void SetGameImage(Image targetControl, string folder, string insiderFolder, string fileName)
        {
            try
            {
                string path = $"pack://application:,,,/Images/{folder}/{insiderFolder}/{fileName}.png";
                targetControl.Source = new BitmapImage(new Uri(path));
            }
            catch (Exception ex)
            {
                // Helpful if you forget to set an image to "Resource"
                System.Diagnostics.Debug.WriteLine($"Failed to load: {fileName}. Error: {ex.Message}");
            }
        }

        private void StudioAct_Click(object sender, RoutedEventArgs e)
        {
            if (!Started) { Studio.Visibility = Visibility.Hidden; return; }
            if (Studio.Visibility == Visibility.Visible)
            {
                Studio.Visibility = Visibility.Hidden;
                return;
            }
            else
            {
                Studio.Visibility = Visibility.Visible;

            }
        }

        private enum StudioState
        {
            Left_Walls,
            Right_Walls,
            Top_Walls,
            Buttom_Walls,
            Flores,
            Items,
            NPCs,
            Menu

        }

        private List<List<string>> Studio_Names = new List<List<string>>
        {
            {new List<string> { "gray", "red", "none", "none" }  },
            {new List<string> { "gray", "red", "none", "none" }  },
            {new List<string> { "gray", "red", "none", "none" }  },
            {new List<string> { "gray", "red", "none", "none" }  },
            {new List<string> { "gray", "red", "none", "none" }  },
            {new List<string> { "Krankenwagen", "bloxy cola", "none", "none" }  },
            {new List<string> { "Gerry", "Fafafela", "fa_ulty", "none" }  },
        };

        private List<Button> Studio_Buttons = new List<Button>();
        private StudioState currentStudioState = StudioState.Menu;
        private void Left_Walls_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 0;
            currentStudioState = StudioState.Left_Walls; faf(i);
        }

        private void Left_Walls_Studio_Click_1(object sender, RoutedEventArgs e)
        {
            int i = 0;
            currentStudioState = StudioState.Left_Walls; faf(i);
        }

        private void Right_Walls_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 1;
            currentStudioState = StudioState.Right_Walls; faf(i);
        }

        private void Top_Walls_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 2;
            currentStudioState = StudioState.Top_Walls; faf(i);
        }

        private void Buttom_Walls_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 3;
            currentStudioState = StudioState.Buttom_Walls; faf(i);
        }

        private void Flores_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 4;
            currentStudioState = StudioState.Flores; faf(i);
        }

        private void Items_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 5;
            currentStudioState = StudioState.Items; faf(i);
        }

        private void NPCs_Studio_Click(object sender, RoutedEventArgs e)
        {
            int i = 6;
            currentStudioState = StudioState.NPCs;
            faf(i);
        }
        private void faf(int i)
        {
            Back_Studio.Visibility = Visibility.Visible;
            Buttons_Grid_Studio.Visibility = Visibility.Hidden;
            Current_State_Studio.Content = $"Current State: {currentStudioState.ToString()}";

            foreach (Button button in Studio_Buttons)
            {
                button.Content = Studio_Names[i][Studio_Buttons.IndexOf(button)];
            }


        }

        private void Back_Studio_Click(object sender, RoutedEventArgs e)
        {
            currentStudioState = StudioState.Menu;
            Back_Studio.Visibility = Visibility.Hidden;
            Buttons_Grid_Studio.Visibility = Visibility.Visible;
            Current_State_Studio.Content = $"Current State: {currentStudioState.ToString()}";
            foreach (Button button in Studio_Buttons)
            {
                button.Content = "";
            }
        }

        void faff(int i)
        {
            string taxes = Studio_Buttons[i].Content.ToString();

            

            if (currentStudioState == StudioState.Left_Walls )
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Left_wall, "Blocks", currentStudioState.ToString(), taxes + "_sides");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].left_wall = LeftWallType.Wall;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Left_Wall_Texture = taxes;
            }
            else if (currentStudioState == StudioState.Right_Walls)
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Right_wall, "Blocks", currentStudioState.ToString(), taxes + "_sides");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].right_wall = RightWallType.Wall;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Right_Wall_Texture = taxes;
            }
            else if (currentStudioState == StudioState.Top_Walls )
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Upper_wall, "Blocks", currentStudioState.ToString(), taxes + "_tops");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].upper_wall = UpperWallType.Wall;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Upper_Wall_Texture = taxes;
            }
            else if (currentStudioState == StudioState.Buttom_Walls )
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Downer_wall, "Blocks", currentStudioState.ToString(), taxes + "_tops");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].downer_wall = DownerWallType.Wall;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Downer_Wall_Texture = taxes;
                
            }
            else if (currentStudioState == StudioState.Flores )
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Flore, "Blocks", currentStudioState.ToString(), taxes + "_block");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Flore_Texture = taxes;
            }


        }
        private void Slot_1_Studio_Click(object sender, RoutedEventArgs e)
        {
            faff(0);
        }

        private void Slot_2_Studio_Click(object sender, RoutedEventArgs e)
        {
            faff(1);
        }

        private void Slot_3_Studio_Click(object sender, RoutedEventArgs e)
        {
                        faff(2);    
        }

        private void Slot_4_Studio_Click(object sender, RoutedEventArgs e)
        {
                        faff(3);    
        }



        
    }
}

