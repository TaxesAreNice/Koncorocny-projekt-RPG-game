using System;
using System.Numerics;
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
using System.Xml.Linq;
using Koncoročný_projekt__RPG_game.UI_Generations;
using static Koncoročný_projekt__RPG_game.Fighting;
using static Koncoročný_projekt__RPG_game.PlayerMovementClass;
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
        private List<List<string>> InventoryChest_Code = [];
        private List<InventoryBlocks_Chest> InventoryChest = [];
        private List<Inventory_Buttons> Inventory_butons = [];
        private List<Fighting_EnemySpawner> current_enemies = [];

        private Image Player_ima = new Image();

        private bool Started = false;
        public bool inventory_on_slot = false;
        public bool inventory_on_slot_q = false;
        public bool inventory_on_slot_chest = false;

        private bool inventory_while_Fighting = false;

        private string CurrentState = "Main"; //Main
        private string CurrentMain = "Map";

        private string itemNAME = "";
        private string enemy_name = "";

        private int YMap = 0;
        private int XMap = 0;

        private int NPC_line_index = 1;

        DispatcherTimer inventory_click_checker = new DispatcherTimer();
        DispatcherTimer inventory_Chest_click_checker = new DispatcherTimer();
        DispatcherTimer inventory_q_click_checker = new DispatcherTimer();

        PlayerMovementClass playerMovement = new PlayerMovementClass();
        InventoryInputs inventoryMovementClass = new InventoryInputs();

        Fighting fighting = new Fighting();

        public enum MapCorner
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }
        MapCorner mapCorner = MapCorner.TopLeft; // Default starting corner
        public enum MapEdge { None, Top, Bottom, Left, Right, TopLeft, TopRight, BottomLeft, BottomRight }

        public MainWindow()
        {
            InitializeComponent();

            inventory_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_click_checker.Tick += Inventory_Click_Checker_Tick;
            inventory_q_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_q_click_checker.Tick += Inventory_Q_Click_Checker_Tick;
            inventory_Chest_click_checker.Interval = TimeSpan.FromMilliseconds(20);
            inventory_Chest_click_checker.Tick += Inventory_Chest_Click_Checker_Tick;

            Studio_Buttons.Add(Slot_1_Studio);
            Studio_Buttons.Add(Slot_2_Studio);
            Studio_Buttons.Add(Slot_3_Studio);
            Studio_Buttons.Add(Slot_4_Studio);

           // GeneretingChestInv(); // don't forget to remove this later
        }

        

        //q_pressed
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

                string Itemdescription = inventoryMovementClass.CheckingForItemDescriptionQ(Inventory_butons[row].Names[inventoryMovementClass.chosed_But_x - minesar]);
                // Inventory_butons[row].slots[inventoryMovementClass.chosed_But_x - minesar]
                Item_Name.Content = Inventory_butons[row].Names[inventoryMovementClass.chosed_But_x - minesar];
                Item_Description.Content = Itemdescription; //32

                if (Itemdescription != "           -")
                {
                    Item_Description.FontSize = 12;
                }
                else
                {
                    Item_Description.FontSize = 30;
                }
            }
            else
            {
                if (inventory_on_slot) { return; }
                Inventory_butons[row].slots[inventoryMovementClass.chosed_But_x - minesar].Background = Brushes.DarkGray; // changes the current position
                inventory_on_slot_q = inventoryMovementClass.q_pressed;

                Item_Name.Content = "           -";
                Item_Description.Content = "           -";
                Item_Description.FontSize = 30;
            }
        }
        private void Inventory_Chest_Click_Checker_Tick(object? sender, EventArgs e)
        {
            if (inventory_on_slot_chest)
            {
                try
                {
                    itemNAME = InventoryChest_Code[chestMovementClass.ChosenY][chestMovementClass.ChosenX];
                }
                catch
                {
                    return;
                }
                if (itemNAME == "") { return; }
                Add_Item_To_Inventory();
                InventoryChest[chestMovementClass.ChosenY].slots[chestMovementClass.ChosenX].image.Source = null;
                inventory_on_slot_chest = false;
                chestMovementClass.isPressed = false;
                InventoryChest_Code[chestMovementClass.ChosenY][chestMovementClass.ChosenX] = "";
            }
            else
            {
                inventory_on_slot_chest = chestMovementClass.isPressed;
               
            }
        }
        private void Inventory_Click_Checker_Tick(object? sender, EventArgs e)
        {



            if (inventory_on_slot)
            {

                Inventory_Code[inventoryMovementClass.backup_chosed_y].slots[inventoryMovementClass.backup_chosed_x].Background = Brushes.DarkGray; // changes the last position
                Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x].Background = Brushes.Gray;
                inventoryMovementClass.PressedTick();
                List<string> Itemdescription = inventoryMovementClass.CheckingForItemDescription();
                //Inventory_Code[inventoryMovementClass.chosed_y].slots[inventoryMovementClass.chosed_x]
                Item_Name.Content = Itemdescription[0];
                Item_Description.Content = Itemdescription[1]; //32

                if (Itemdescription[1] != "           -")
                {
                    Item_Description.FontSize = 12;
                }
                else
                {
                    Item_Description.FontSize = 30;
                }
            }
            else
            {
                if (inventory_on_slot_q) { return; }
                Inventory_Code[inventoryMovementClass.backup_chosed_y].slots[inventoryMovementClass.backup_chosed_x].Background = Brushes.DarkGray;

                inventory_on_slot = inventoryMovementClass.slot_pressed;

                Item_Name.Content = "           -";
                Item_Description.Content = "           -";
                Item_Description.FontSize = 30;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (MapSize.Text.Contains("x") || MapSize.Text.Contains("X"))
            {
                int tester = 0;
                YMap = int.Parse(MapSize.Text.Split('x', 'X')[0]);
                XMap = int.Parse(MapSize.Text.Split('x', 'X')[1]);

                try
                {
                    // tester = 

                    string corner = MapSize.Text.Split('x', 'X')[2];


                    if (MapSize.Text.Contains("TL") || MapSize.Text.Contains("TopLeft") ||
                MapSize.Text.Contains("TR") || MapSize.Text.Contains("TopRight") ||
                MapSize.Text.Contains("BL") || MapSize.Text.Contains("ButtomLeft") ||
                MapSize.Text.Contains("BR") || MapSize.Text.Contains("ButtomRight") ||
                MapSize.Text.Contains("x"))
                    {



                        // TopLeft,
                        //TopRight,
                        //BottomLeft,
                        //BottomRight

                        if (XMap > 14)
                        {
                            XMap = 14;
                        }
                        if (YMap > 6)
                        {
                            YMap = 6;
                        }


                        if (corner == "TL" || corner == "TopLeft")
                        {
                            mapCorner = MapCorner.TopLeft;
                        }
                        else if (corner == "TR" || corner == "TopRight")
                        {
                            mapCorner = MapCorner.TopRight;
                            playerMovement.Player_Pixel_X = (105 * (14 - 1)) + 35;
                            playerMovement.PlayerX = XMap - 1;
                        }
                        else if (corner == "BL" || corner == "ButtomLeft")
                        {
                            mapCorner = MapCorner.BottomLeft;
                            playerMovement.Player_Pixel_Y = (100 * (6 - 1) + 30);
                            playerMovement.PlayerY = YMap - 1;
                        }
                        else if (corner == "BR" || corner == "ButtomRight")
                        {
                            mapCorner = MapCorner.BottomRight;
                            playerMovement.Player_Pixel_X = (105 * (14 - 1)) + 35;
                            playerMovement.Player_Pixel_Y = (100 * (6 - 1)) + 30;
                            playerMovement.PlayerX = XMap - 1;
                            playerMovement.PlayerY = YMap - 1;
                        }

                        playerMovement.MAX_y = YMap - 1;
                        playerMovement.MAX_x = XMap - 1;

                        Started = true;

                        GeneretingMap();
                        GeneretingInventory();
                        GeneretingChestInv();
                    }
                }
                catch
                {
                    MessageBox.Show("If you want to set the corner, write it like this: (for example) 10x10xTopLeft or 10x10xTL. If you don't want to set the corner, just write the size like this: 10x10");
                }


            }
        }

        

        private void GeneretingMap()
        {
            List<Map_Block> row = [];
            int rowY = 0;
            if (mapCorner == MapCorner.TopLeft || mapCorner == MapCorner.TopRight)
            {
                rowY = 0;
            }
            else
            {
                rowY = (6 - YMap) * 105;
            }

            bool fromLeft = false;

            if (mapCorner == MapCorner.TopLeft || mapCorner == MapCorner.BottomLeft)
            {
                fromLeft = true;
            }

            if (YMap < 7) { }

            for (int i = 0; i < YMap; i++)
            {
                Map_Block roww = new Map_Block(XMap, YMap, fromLeft);

                row.Add(roww);

                roww.Margin = new Thickness(0, rowY + 5, 0, 0); // if buttom,  modifier = (-25). if top, modifier = (+5)
                Map_UI.Children.Add(roww);
                Map.Add(row);


                rowY += 100;


                // if top, rowY += 100 + 5; if bottom, rowY -= 100 - 25
                //+100
            }

            // row[0].blocks[0].Background = Brushes.Red;
            // DON'T put "Image" in front of it here, just use the class variable
            Player_ima = new Image()
            {
                Height = 50,
                Width = 50,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(playerMovement.Player_Pixel_X, playerMovement.Player_Pixel_Y, 0, 0)
            };

            SetGameImage(Player_ima, "Characters", "Player", "Player");
            Map_UI.Children.Add(Player_ima);
            // also a thingy here that sets the player's starting position to what ever corner we chose
        }

        private void GeneretingChestInv()
        {
            int rowY = 0;
            int Yrow = 0;
   

            for (int i = 0; i < 3; i++)
            {
                InventoryBlocks_Chest roww = new InventoryBlocks_Chest(i, chestMovementClass);

                roww.Margin = new Thickness(15, rowY + 5 + 0, -200, 0); //817

                ChestInventory_Chest.Children.Add(roww);
                InventoryChest.Add(roww);

                rowY += 80;

            }
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
                inventory_Buttons.VerticalAlignment = VerticalAlignment.Top;


                inventory_Buttons.Margin = new Thickness(EquepsX, 5, 0, 0);

                Inventory_butons.Add(inventory_Buttons);
                Inventory.Children.Add(inventory_Buttons);

                EquepsX += 215;
                Equeps_list_num += 4;
            }

            UpdatePlayerStatsInInventory();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                AdminToggle();
            }
            if (!Started) { return; }

            bool success = false;
            string key = e.Key.ToString();

            if (CurrentState == "Main" && CurrentMain == "Map")
            {
                MapMovement(key, e);
            }
            else if (CurrentState == "Inventory")
            {
                InventoryMovement(success, key, e);
            }
            else if (CurrentState == "Fight")
            {
                FightingMovement(success, key, e);
            }
            else if (CurrentState == "NPC")
            {
                 NPCMovement(success, key, e);
            }
            else if (CurrentState == "Chest")
                {
                    ChestMovement(success, key, e);
            }
        }

        private void ChestMovement(bool success, string key, KeyEventArgs e)
        {
            if (e.Key == Key.E)
            {
              
            }
        }

        private void NPCMovement(bool success, string key, KeyEventArgs e)
        {
            if (e.Key == Key.E)
            {
                MapBlocks_Insides current = Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX];
                if (current.current_NPC_Lines.Count <= NPC_line_index)
                {                     
                    TheInteractions.Content = "";
                    CurrentState = "Main";
                    NPC_line_index = 1;
                    return;
                }
                TheInteractions.Content = current.current_NPC_Lines[NPC_line_index] + "\n(Press Enter to continue),(Press E to end this conversation)";
                NPC_line_index++;
            }
            else if (e.Key == Key.Q)
            {
                TheInteractions.Content = "";
                CurrentState = "Main";
                NPC_line_index = 1;
            }
        }

        private void AdminToggle()
        {
            if (AdmitGrid.Visibility == Visibility.Visible)
            {
                AdmitGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                AdmitGrid.Visibility = Visibility.Visible;
            }
        }
        private void FightingMovement(bool success, string key, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                inventory_while_Fighting = true;
                Fighting_UI.Visibility = Visibility.Hidden;
                Inventory_Open();
                CurrentState = "Inventory";
            }
        }

        private void InventoryMovement(bool success, string key, KeyEventArgs e)
        {
            int x = inventoryMovementClass.chosed_x;
            int y = inventoryMovementClass.chosed_y;
            int XX = inventoryMovementClass.chosed_But_x;
            int row = XX / 4;
            int col = XX % 4;

            switch (e.Key)
            {
                case Key.E:
                    string contentE = "";
                    if (inventory_on_slot_q)
                    {

                        contentE = Inventory_butons[row].Names[col];
                        if (contentE == "") { return; }

                        Player player = fighting.RequestPlayer();

                        Inventory_butons[row].slots[col].Background = Brushes.DarkGray; // changes the last position
                        Inventory_butons[row].slots[col].image.Source = null;
                        Inventory_butons[row].Names[col] = "";
                        itemNAME = contentE;
                        Add_Item_To_Inventory();

                        inventoryMovementClass.SettingWearablesBack(player, contentE);
                        // Also this... Tho it gotta be spesific to each category
                    }
                    else if (inventory_on_slot)
                    {
                        List<string> categories = new List<string>() { "Helmet", "Chestplate", "Leggins", "Boots", "Sword", "Ring", "2nd hand", "Accessory" };
                        contentE = Inventory_Code[y].names[x];

                        Player player = fighting.RequestPlayer();
                        Monster monster = fighting.RequestMonster();

                        string itemType = inventoryMovementClass.E_Pressed(contentE, player, monster, fighting);
                        string JustInCaseWearable = "";



                        if (itemType.Contains("_"))
                        {
                            JustInCaseWearable = itemType.Split('_')[1];
                        }

                        bool nonoFight = false;
                        int fahh = 0;
                        foreach (var category in categories)
                        {
                            if (JustInCaseWearable == category) //here, you gotta somehow figure out how to find the items category
                            {
                                if (Inventory_butons[fahh / 4].Names[fahh % 4] == contentE)
                                {
                                    inventoryMovementClass.SettingWearablesBack(player, contentE);
                                    MessageBox.Show("You can't equip an item that's already equipped! Take it out of the equipment slot first.");
                                    return;
                                }
                                if (inventory_while_Fighting)
                                {
                                    inventoryMovementClass.SettingWearablesBack(player, JustInCaseWearable);
                                    nonoFight = true;
                                    continue;
                                }

                                SetGameImage(Inventory_butons[fahh / 4].slots[fahh % 4].image, "Items", "faf", contentE);
                                Inventory_butons[fahh / 4].Names[fahh % 4] = contentE;

                            }
                            fahh++;
                        }


                        if (!nonoFight)
                        {
                            Inventory_Code[y].slots[x].image.Source = null;
                            Inventory_Code[y].names[x] = "";
                            inventoryMovementClass.ClearSlot(x, y);
                        }


                        if (inventory_while_Fighting && !nonoFight)
                        {

                            Inventory_Open();

                            Fighting_UI.Visibility = Visibility.Visible;

                            inventory_while_Fighting = false;

                            CurrentState = "Fight";

                            EventerChanger($"Player has used {contentE}");

                            EnemiesAttack();

                            UpdatePlayerStats();
                        }
                        else if (inventory_while_Fighting && nonoFight)
                        {
                            nonoFight = false;

                            Inventory_Open();

                            Fighting_UI.Visibility = Visibility.Visible;

                            inventory_while_Fighting = false;

                            CurrentState = "Fight";
                        }
                    }


                    UpdatePlayerStatsInInventory();
                    break;

                case Key.Q:
                    if (inventory_on_slot)
                    {
                        inventoryMovementClass.Q_Pressed();

                        Inventory_Code[y].slots[x].image.Source = null;
                        Inventory_Code[y].names[x] = "";

                        inventoryMovementClass.ClearSlot(x, y);
                    }
                    break;

                case Key.Escape:
                    Inventory_Open();

                    if (inventory_while_Fighting)
                    {
                        inventory_while_Fighting = false;
                        CurrentState = "Fight";
                        Fighting_UI.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void UpdatePlayerStatsInInventory()
        {
            Player player = fighting.RequestPlayer();

            PlayerHP_UI.Content = $"{player.PlayerHP}hp";
            PlayerDefence_UI.Content = $"{player.PlayerDefense}";
            PlayerAttack_UI.Content = $"{player.PlayerAttack}";
        }

        private void MapMovement(string key, KeyEventArgs e)
        {
            int px = playerMovement.PlayerX;
            int py = playerMovement.PlayerY;

            MapBlocks_Insides current = Map[0][py].blocks[px];
            MapBlocks_Insides neighbor = null;


            switch (e.Key)
            {
                case Key.W: if (py > 0) neighbor = Map[0][py - 1].blocks[px]; break;
                case Key.S: if (py < playerMovement.MAX_y) neighbor = Map[0][py + 1].blocks[px]; break;
                case Key.A: if (px > 0) neighbor = Map[0][py].blocks[px - 1]; break;
                case Key.D: if (px < playerMovement.MAX_x) neighbor = Map[0][py].blocks[px + 1]; break;
                case Key.E:
                    MapPresss(current, "E", current.block_type.ToString(), playerMovement.neighborDoorPos.ToString(), CheckingForNeighborsDoors()); //playerMovement.blockType.ToString()
                    return;
                case Key.F:
                    MapPresss(current, "F", playerMovement.doorPos.ToString(), playerMovement.neighborDoorPos.ToString(), CheckingForNeighborsDoors());
                    return;
                case Key.Escape:
                    Inventory_Open();
                    return;
            }

            if (neighbor != null && playerMovement.CheckingForRoomDoors(key, current, neighbor) == "N")
            {
                RoomMoved(current, neighbor, "N");
            }
            else if (neighbor != null && playerMovement.CheckingForRoomDoors(key, current, neighbor) == "C")
            {
                RoomMoved(current, neighbor, "C");
            }
            else if (neighbor != null && playerMovement.CheckingForWalls(key, current, neighbor))
            {
                ChangingPlayerPosition(key);
                ChestGrid.Visibility = Visibility.Hidden;

                int newPx = playerMovement.PlayerX;
                int newPy = playerMovement.PlayerY;
                MapBlocks_Insides freshCurrent = Map[0][newPy].blocks[newPx];
                // -----------------------------------------------

                playerMovement.CheckingForEPrompts(CheckingForEPrompts(), freshCurrent);

                if (currentStudioState == StudioState.Doors)
                {
                    Back_Studio_Click(null, null);
                }

                // Set base interaction content based on your new tile
                if (playerMovement.blockType != PlayerMovementClass.BlockType.Empty_T)
                {
                    // If the space is NOT empty, show the prompt
                    TheInteractions.Content = "There's something you can interact with (E)";
                    playerMovement.blockType = PlayerMovementClass.BlockType.Empty_T; // Reset it so it doesn't keep showing the prompt when you move around on the same tile
                }
                else
                {
                    // If the space IS empty, clear the prompt
                    TheInteractions.Content = "";
                }

                // Inside your MapMovement logic, update the door check to this:
                if (HasNearbyOrNeighborDoor(newPx, newPy, px, py))
                {
                    if (!string.IsNullOrEmpty((string?)TheInteractions.Content)) TheInteractions.Content += "\n";
                    TheInteractions.Content += "There's a door you can interact with (F)";
                }
            }
        }

        private void RoomMoved(MapBlocks_Insides current, MapBlocks_Insides neighbor, string WhichOne)
        {
            if (current == null) return;

            int num = 0;

            if (WhichOne == "C")
            {
                playerMovement.PlayerX = current.NextRoomTeleporter_X;
                playerMovement.PlayerY = current.NextRoomTeleporter_Y;
                num = current.NextRoomTeleporter_Room;
            }
            else if (WhichOne == "N")
            {
                playerMovement.PlayerX = neighbor.NextRoomTeleporter_X;
                playerMovement.PlayerY = neighbor.NextRoomTeleporter_Y;
                num = neighbor.NextRoomTeleporter_Room;
            }
            playerMovement.Player_Pixel_X = (playerMovement.PlayerX * 105) + 30;
            playerMovement.Player_Pixel_Y = (playerMovement.PlayerY * 100) + 30;

            ChangingPlayerPosition("GGs");

            //ClearMap();
            //UploadMap(Num);
        }

        // Upgraded helper method that checks your block AND neighboring walls facing you
        private bool HasNearbyOrNeighborDoor(int x, int y, int oldX, int oldY)
        {
            // 1. Check the block the player is currently standing on
            MapBlocks_Insides current = Map[0][y].blocks[x];
            if (current.left_wall == LeftWallType.DoorClosed || current.left_wall == LeftWallType.DoorOpen ||
                current.right_wall == RightWallType.DoorClosed || current.right_wall == RightWallType.DoorOpen ||
                current.upper_wall == UpperWallType.DoorClosed || current.upper_wall == UpperWallType.DoorOpen ||
                current.downer_wall == DownerWallType.DoorClosed || current.downer_wall == DownerWallType.DoorOpen)
            {
                return true;
            }

            // 2. Check the Neighbor ABOVE (Does its DOWN wall have a door facing you?)
            if (y > 0)
            {
                var above = Map[0][y - 1].blocks[x];
                if (above.downer_wall == DownerWallType.DoorClosed || above.downer_wall == DownerWallType.DoorOpen) return true;
            }

            // 3. Check the Neighbor BELOW (Does its UP wall have a door facing you?)
            if (y < playerMovement.MAX_y)
            {
                var below = Map[0][y + 1].blocks[x];
                if (below.upper_wall == UpperWallType.DoorClosed || below.upper_wall == UpperWallType.DoorOpen) return true;
            }

            // 4. Check the Neighbor to the LEFT (Does its RIGHT wall have a door facing you?)
            if (x > 0)
            {
                var left = Map[0][y].blocks[x - 1];
                if (left.right_wall == RightWallType.DoorClosed || left.right_wall == RightWallType.DoorOpen) return true;
            }

            // 5. Check the Neighbor to the RIGHT (Does its LEFT wall have a door facing you?)
            if (x < playerMovement.MAX_x)
            {
                var right = Map[0][y].blocks[x + 1];
                if (right.left_wall == LeftWallType.DoorClosed || right.left_wall == LeftWallType.DoorOpen) return true;
            }

            return false; // No doors found anywhere around you
        }
        private void MapPresss(MapBlocks_Insides current, string key, string type, string neighbor_type, List<(MapBlocks_Insides, bool OpenedOrClosed, string id)> neighbor)
        {
            string justInCaseEHEMWEARABLES = "";
            if (type.Contains("_"))
            {
                justInCaseEHEMWEARABLES = "_" + type.Split("_")[1];
                type = type.Split("_")[0];
            }

            if (key == "F")
            {
                playerMovement.DoorOpen_Close(type, current, neighbor, neighbor_type);
            }
            else if (key == "E")
            {
                ThingyInteraction(type, current);
            }
        }

        private void ThingyInteraction(string type, MapBlocks_Insides current)
        {
            if (type == "Item")
            {
                itemNAME = current.current_item_Texture;
                Add_Item_To_Inventory();

                current.current_item_Texture = "";
                current.block_type = MapBlocks_Insides.BlockType.Empty;
                current.Item.Source = null;
            }
            else if (type == "NPC")
            {
                if (current.current_NPC_Lines.Count > 0)
                {
                    CurrentState = "NPC";
                    TheInteractions.Content = current.current_NPC_Lines[0] + "\n(Press E to continue or press Q to end this conversation)";
                }
                else
                { 
                    TheInteractions.Content = "This NPC seems to be silent..."; 
                }
            }
            else if (type == "Chest")
            {
                if (ChestGrid.Visibility == Visibility.Visible)
                {
                    CurrentState = "Main";
                    ChestGrid.Visibility = Visibility.Hidden;

                    inventory_Chest_click_checker.Stop();
                    InventoryChest_Code.Clear();
                }
                else
                {
                    CurrentState = "Chest";
                    ChestGrid.Visibility = Visibility.Visible;
                    
                    inventory_Chest_click_checker.Start();
                    //current_Chest_Items
                    int y = 0;
                    int x = 0;
                    for (int i = 0; i < current.current_Chest_Items.Count; i++)
                    {
                        x = i;
                        if (i > 11)
                        {
                            x = i - (11 * y);
                        }
                        if (x == 11) { InventoryChest_Code.Add(new List<string>()); y++; x = 0; }
                        else if (i == 0) { InventoryChest_Code.Add(new List<string>()); }

                        SetGameImage(InventoryChest[y].slots[x].image, "Items", "faf", current.current_Chest_Items[i]);
                        InventoryChest_Code[y].Add(current.current_Chest_Items[i]);

                    }
                   
                }
            }
        }



        private MapBlocks_Insides CheckingForEPrompts()
        {
            int px = playerMovement.PlayerX;
            int py = playerMovement.PlayerY;



            return Map[0][py].blocks[px];
        }
        private List<(MapBlocks_Insides, bool OpenedOrClosed, string id)> CheckingForNeighborsDoors()
        {
            int px = playerMovement.PlayerX;
            int py = playerMovement.PlayerY;

            List<(MapBlocks_Insides Wall, bool OpenedOrClosed, string id)> neighbors = new();


            if (px + 1 < Map[0][py].blocks.Count)
            {
                if (Map[0][py].blocks[px + 1].left_wall == MapBlocks_Insides.LeftWallType.DoorClosed) { neighbors.Add((Map[0][py].blocks[px + 1], false, "left")); }
                else if (Map[0][py].blocks[px + 1].left_wall == LeftWallType.DoorOpen) { neighbors.Add((Map[0][py].blocks[px + 1], true, "left")); }
            }
            if (px - 1 >= 0)
            {
                if (Map[0][py].blocks[px - 1].right_wall == MapBlocks_Insides.RightWallType.DoorClosed) { neighbors.Add((Map[0][py].blocks[px - 1], false, "right")); }
                else if (Map[0][py].blocks[px - 1].right_wall == RightWallType.DoorOpen) { neighbors.Add((Map[0][py].blocks[px - 1], true, "right")); }
            }
            if (py + 1 < Map[0].Count)
            {
                if (Map[0][py + 1].blocks[px].upper_wall == MapBlocks_Insides.UpperWallType.DoorClosed) { neighbors.Add((Map[0][py + 1].blocks[px], false, "up")); }
                else if (Map[0][py + 1].blocks[px].upper_wall == UpperWallType.DoorOpen) { neighbors.Add((Map[0][py + 1].blocks[px], true, "up")); }
            }
            if (py - 1 < Map[0].Count && py > 1) // py - 1...
            {
                if (Map[0][py - 1].blocks[px].downer_wall == MapBlocks_Insides.DownerWallType.DoorClosed) { neighbors.Add((Map[0][py - 1].blocks[px], false, "down")); }
                else if (Map[0][py - 1].blocks[px].downer_wall == DownerWallType.DoorOpen) { neighbors.Add((Map[0][py - 1].blocks[px], true, "down")); }
            }

            return neighbors;
        }

        private void ChangingPlayerPosition(string key)
        {
            Player_ima.Margin = new Thickness(playerMovement.Player_Pixel_X, playerMovement.Player_Pixel_Y, 0, 0);
        }

        private void EventerChanger(string eventText)
        {
            // 1. If we already have 4 or more items, chop off the oldest one at index 0
            while (Eventer.Items.Count >= 5)
            {
                Eventer.Items.RemoveAt(0);
            }

            // 2. Always add the new event text
            Eventer.Items.Add(eventText);
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
            UpdatePlayerStatsInInventory();

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
            SetGameImage(Inventory_Code[ty].slots[tx].image, "Items", "faf", itemNAME);
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
            StartFight();
        }
        private void StartFight()
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
            UpdatePlayerStats();
            Eventer.Items.Clear();
        }

        private void Enemy_Num_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Enemy_Num.Text is int) { return; }

            string backup = enemy_name;
            bool isMonster = false;

            enemy_name = Enemy_Num.Text;

            foreach (var enemy in fighting.enemies)
            {
                if (enemy.name == Enemy_Num.Text)
                {
                    isMonster = true;
                    break;
                }
            }

            if (!isMonster)
            {
                enemy_name = backup;
                return;
            }

            if (fighting.currentEnemies.Count < 4)
            {
                fighting.currentEnemies.Add(new Enemy { EnemyName = enemy_name });
            }
        }

        private void EnemyStatAdder(string name)
        {
            foreach (var enemy in fighting.enemies)
            {
                if (enemy.name == name)
                {
                    fighting.currentEnemies[fighting.currentEnemies.Count - 1].EnemyAttack = enemy.attack;
                    fighting.currentEnemies[fighting.currentEnemies.Count - 1].EnemyDefense = enemy.defense;
                    fighting.currentEnemies[fighting.currentEnemies.Count - 1].EnemyHP = enemy.hp;
                    break;
                }
            }
        }
        private void Spawing_Enemies()
        {
            int enemy_num = fighting.currentEnemies.Count;

            int space_off_x = 0;
            int space_off_y = 0;
            string name = enemy_name;
            // List<string> enemy_names = new List<string>();
            //enemy_names.Add(name);


            switch (enemy_num)
            {
                case 0:
                    MessageBox.Show("You've won this battle");
                    Player plater = fighting.RequestPlayer();

                    plater.PlayerHP += 25;
                    if (plater.PlayerHP >= 100)
                    {
                        plater.PlayerHP = 100;
                    }
                    StartFight();
                    return;
                case 1:
                    name = fighting.currentEnemies[0].EnemyName;
                    EnemyStatAdder(name);
                    Spawing_enemy(name, space_off_x, space_off_y, 0);
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
                        name = fighting.currentEnemies[i].EnemyName;
                        EnemyStatAdder(name);
                        Spawing_enemy(name, space_off_x, space_off_y, i);
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
                        name = fighting.currentEnemies[i].EnemyName;
                        EnemyStatAdder(name);
                        Spawing_enemy(name, space_off_x, space_off_y, i);
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
                        name = fighting.currentEnemies[i].EnemyName;
                        EnemyStatAdder(name);
                        Spawing_enemy(name, space_off_x, space_off_y, i);
                    }
                    break;
            }
        }
        private void Spawing_enemy(string name, int space_off_x, int space_off_y, int enemy_num)
        {
            Fighting_EnemySpawner fighting_EnemySpawner = new Fighting_EnemySpawner(fighting, name, fighting.currentEnemies[enemy_num]);
            SetGameImage(fighting_EnemySpawner.stuff[0].theImage, "Characters", "Enemies", name);
            fighting_EnemySpawner.Margin = new Thickness(space_off_x, space_off_y, 0, 0);
            Enemy_Grid.Children.Add(fighting_EnemySpawner);

            current_enemies.Add(fighting_EnemySpawner);

        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            //current_enemies[0].stuff[0].prog.Value = 100;
            //current_enemies[0].stuff[0].progLab.Content = "100hp";

            //current_enemies[0].stuff[0].atkLabel.Content = "10";
            //current_enemies[0].stuff[0].defLabel.Content = "5";

            string justInCase = "";
            string result = fighting.PlayerAttack();
            if (result.Contains("_"))
            {
                justInCase = result.Split('_')[1];
                result = result.Split('_')[0];
            }
            int i = 0;
            //currentEnemies.RemoveAt(i);
            foreach (var enemy in current_enemies)
            {

                int hpMax = 1;
                foreach (var enemy_case in fighting.enemies)
                {
                    if (i >= fighting.currentEnemies.Count) { break; }
                    if (enemy_case.name == fighting.currentEnemies[i].EnemyName) // error for killin' an enemy
                    {
                        hpMax = enemy_case.hp;
                        break;
                    }
                }

                if (i >= fighting.currentEnemies.Count) { break; }
                enemy.stuff[0].prog.Value = (int)((double)fighting.currentEnemies[i].EnemyHP / hpMax * 100);
                enemy.stuff[0].progLab.Content = $"{fighting.currentEnemies[i].EnemyHP}hp";
                EventerChanger($"The {fighting.currentEnemies[i].EnemyName} has {fighting.currentEnemies[i].EnemyHP}hp left.");
                enemy.Background = Brushes.DarkGray;
                i++;
            }

            if (result == "enemyDead")
            {
                int num = int.Parse(justInCase);

                EventerChanger($"The {fighting.currentEnemies[num].EnemyName} has been defeated.");
                fighting.KillEnemy(num);
                Enemy_Grid.Children.Clear();
                current_enemies.Clear();
                Spawing_Enemies();
            }
            else if (result == "ContinueFight")
            {
                EnemiesAttack();
            }
        }
        private bool chechingIfPhoenixFeatherIsInInventory()
        {
            bool hasPhoenixFeather = false;
            int y = 0;
            foreach (var slot in Inventory_Code)
            {
                int x = 0;
                foreach (var name in slot.names)
                {
                    if (name == "Phoenix Feather")
                    {
                        hasPhoenixFeather = true;
                        Inventory_Code[y].slots[x].image.Source = null;
                        Inventory_Code[y].names[x] = "";
                        inventoryMovementClass.ClearSlot(x, y);
                        break;
                    }
                    x++;
                }
                y++;
            }

            if (hasPhoenixFeather)
            {
                MessageBox.Show("Your Phoenix Feather has saved you from death! You have been revived.");
                //fighting.RevivePlayer();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void EnemiesAttack()
        {
            fighting.EnemyAttacks();
            //fighting.State = TurnState.EnemyTurn;
            bool playerDead = fighting.playerDead();
            if (playerDead && chechingIfPhoenixFeatherIsInInventory())
            {
                fighting.RevivePlayer();
            }
            else if (playerDead)
            {
                EventerChanger("The Player has been defeated.");
                GameOver();
            }

                EventerChanger($"The Player has {fighting.RequestPlayer().PlayerHP}hp left.");
                UpdatePlayerStats();
            
        }
        private void GameOver()
        {
            MessageBox.Show("You died! Game over.");
            Fighting_UI.Visibility = Visibility.Hidden;
            CurrentState = "Main";
            Enemy_Grid.Children.Clear();
            current_enemies.Clear();
        }

        private void UpdatePlayerStats()
        {
            List<int> playerStats = fighting.GetPlayerStats();

            if (playerStats.Count != 3)
            {
                MessageBox.Show("Error: Player stats list does not contain the expected number of elements.");
                return;
            } // Just a safety check

            PlayerHp_Label.Content = $"{playerStats[0]}hp";
            PlayerHp_Bar.Value = (double)playerStats[0] / 100 * 100; // Assuming max HP is 100
          //  Player_Mana.Content = $"{playerStats[3]}";
            Player_Defence.Content = $"{playerStats[2]}";
            Player_Attack.Content = $"{playerStats[1]}";
        }

        private void Enemy_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource != sender) { return; }
            foreach (var enemy in current_enemies)
            {
                enemy.Background = Brushes.DarkGray;
                fighting.selectedEnemy = "";
                fighting.enemySelected = false;
            }
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
                MessageBox.Show($"Failed to load: {fileName}. Error: {ex.Message}");
            }
        }

        private void StudioAct_Click(object sender, RoutedEventArgs e)
        {
            if (!Started) { Studio.Visibility = Visibility.Hidden; return; }
            if (AdmitGrid.Visibility == Visibility.Visible) { AdmitGrid.Visibility = Visibility.Hidden; }
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
            Menu,
            Doors,
            Chests,
            RoomDoors
        }

        private List<List<string>> Studio_Names = new List<List<string>>
        {
            {new List<string> { "wood", "stone", "andezit", "none" }  },
            {new List<string> { "wood", "stone", "andezit", "none" }  },
            {new List<string> { "wood", "stone", "andezit", "none" }  },
            {new List<string> { "wood", "stone", "andezit", "none" }  },
            {new List<string> { "wood", "stone", "andezit", "none" }  },
            {new List<string> { "Krankenwagen", "Bloxy_Cola", "none", "none" }  },
            {new List<string> { "none", "none", @"fa_ulty", "Fafafela" }  },
            {new List<string> { "LeftDoor", "RightDoor", "TopDoor", "ButtomDoor" }  },
            {new List<string> { "Cheski", "Su", "Velmi", "Gejske" }  },
            {new List<string> { "LeftDoor", "RightDoor", "TopDoor", "ButtomDoor" }  }

        };

        private List<Button> Studio_Buttons = new List<Button>();
        private StudioState currentStudioState = StudioState.Menu;
        private ChestMovementClass chestMovementClass = new ChestMovementClass();

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
            string justInCase = "";
            string taxes = Studio_Buttons[i].Content.ToString();
            if (taxes.Contains("_"))
            {
                justInCase = taxes.Split("_")[0];
                taxes = taxes.Split("_")[1];
            }



            if (currentStudioState == StudioState.Left_Walls)
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
            else if (currentStudioState == StudioState.Top_Walls)
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Upper_wall, "Blocks", currentStudioState.ToString(), taxes + "_tops");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].upper_wall = UpperWallType.Wall;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Upper_Wall_Texture = taxes;
            }
            else if (currentStudioState == StudioState.Buttom_Walls)
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Downer_wall, "Blocks", currentStudioState.ToString(), taxes + "_tops");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].downer_wall = DownerWallType.Wall;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Downer_Wall_Texture = taxes;

            }
            else if (currentStudioState == StudioState.Flores)
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Flore, "Blocks", currentStudioState.ToString(), taxes + "_block");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Flore_Texture = taxes;
            }
            else if (currentStudioState == StudioState.Doors && i == 0 && taxes != "")
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Left_wall, "Blocks", "Left_Walls", taxes + "_closed");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Left_Wall_Texture = taxes;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].left_wall = LeftWallType.DoorClosed;


                Back_Studio_Click(null, null);
            }
            else if (currentStudioState == StudioState.Doors && i == 1 && taxes != "")
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Right_wall, "Blocks", "Right_Walls", taxes + "_closed");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Right_Wall_Texture = taxes;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].right_wall = RightWallType.DoorClosed;
                Back_Studio_Click(null, null);
            }
            else if (currentStudioState == StudioState.Doors && i == 2 && taxes != "")
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Upper_wall, "Blocks", "Top_Walls", taxes + "_closed");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Upper_Wall_Texture = taxes;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].upper_wall = UpperWallType.DoorClosed;
                Back_Studio_Click(null, null);
            }
            else if (currentStudioState == StudioState.Doors && i == 3 && taxes != "")
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Downer_wall, "Blocks", "Buttom_Walls", taxes + "_closed");
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Downer_Wall_Texture = taxes;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].downer_wall = DownerWallType.DoorClosed;
                // 

                Back_Studio_Click(null, null);
            }
            else if (currentStudioState == StudioState.RoomDoors && taxes != "")
            {
                if (!Texter_Studio.Text.Contains(",")) { return; }

                if (i == 3)
                    {
                        SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Downer_wall, "Blocks", "Buttom_Walls", taxes + "_room");
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Downer_Wall_Texture = taxes;
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].downer_wall = DownerWallType.RoomDoor;
                    }
                    else if (i == 2)
                    {
                        SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Upper_wall, "Blocks", "Top_Walls", taxes + "_room");
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Upper_Wall_Texture = taxes;
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].upper_wall = UpperWallType.RoomDoor;
                    }
                    else if (i == 1)
                    {
                        SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Right_wall, "Blocks", "Right_Walls", taxes + "_room");
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Right_Wall_Texture = taxes;
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].right_wall = RightWallType.RoomDoor;
                    }
                    else if (i == 0)
                    {
                        SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Left_wall, "Blocks", "Left_Walls", taxes + "_room");
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Left_Wall_Texture = taxes;
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].left_wall = LeftWallType.RoomDoor;

                    }

                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].NextRoomTeleporter_X = int.Parse(Texter_Studio.Text.Split(",")[0]);
                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].NextRoomTeleporter_Y = int.Parse(Texter_Studio.Text.Split(",")[1]);
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].NextRoomTeleporter_Room = int.Parse(Texter_Studio.Text.Split(",")[2]); 
                    Back_Studio_Click(null, null);
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

        private void Doors_Studio_Click(object sender, RoutedEventArgs e)
        {
            currentStudioState = StudioState.Doors;
            faf(7);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string taxes = Texter_Studio.Text;
            if (currentStudioState == StudioState.Items)
            {
                SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Item, "Items", "faf", taxes);
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_item_Texture = taxes;
                Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].block_type = MapBlocks_Insides.BlockType.Item;
            }
            else if (currentStudioState == StudioState.NPCs)
            {
                if (taxes.Contains("/"))
                {
                    string theDialog = taxes.Split("/")[1];

                    for (int i = 0; i < theDialog.Split("|").Length; i++)
                    {
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_NPC_Lines.Add(theDialog.Split("|")[i]);
                    }

                    taxes = taxes.Split("/")[0];
                    SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].NPC, "Characters", "NPC", taxes);
                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_NPC_Texture = taxes;
                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_NPC_Name = taxes;
                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].block_type = MapBlocks_Insides.BlockType.NPC;
                }
            }
            else if (currentStudioState == StudioState.Chests)
            {
                if (taxes.Contains("/"))
                {
                    string theSide = taxes.Split("/")[0];
                    taxes = taxes.Split("/")[1];
                    if (taxes.Contains(","))
                    {
                        for (int i = 0; i < taxes.Split(",").Length; i++)
                        {
                            Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Chest_Items.Add(taxes.Split(",")[i]);
                        }
                    }
                    else
                    {
                        Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Chest_Items.Add(taxes);
                    }

                    SetGameImage(Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].Chest, "Blocks", "Others", "Chest_" + theSide);
                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].block_type = MapBlocks_Insides.BlockType.Chest;
                    Map[0][playerMovement.PlayerY].blocks[playerMovement.PlayerX].current_Chest_Texture = "Chest_" + theSide;
                }
            }
        }

        private void Chest_Studio_Click(object sender, RoutedEventArgs e)
        {
            currentStudioState = StudioState.Chests;
            faf(8);
        }

        private void Exit_Chest_Click(object sender, RoutedEventArgs e)
        {
            CurrentState = "Main";
            ChestGrid.Visibility = Visibility.Hidden;

            inventory_Chest_click_checker.Stop();
            InventoryChest_Code.Clear();
        }

        private void ChestGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void RoomDoor_Studio_Click(object sender, RoutedEventArgs e)
        {
            currentStudioState = StudioState.RoomDoors;
            faf(9);
        }
    }
}