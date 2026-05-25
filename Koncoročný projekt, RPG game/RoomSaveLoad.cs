using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Koncoročný_projekt__RPG_game.UI_Generations;
using System.Windows.Controls;

namespace Koncoročný_projekt__RPG_game
{
    public static class RoomSaveLoad
    {
        private const string RoomsFolder = "Rooms";

        private static readonly JsonSerializerOptions JsonOpts = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        // ── SAVE ─────────────────────────────────────────────────────────────
        public static void SaveRoom(List<List<Map_Block>> map, int roomNumber, int xMap, int yMap)
        {
            try
            {
                var roomSave = new RoomSaveData
                {
                    RoomNumber = roomNumber,
                    XMap = xMap,
                    YMap = yMap
                };

                foreach (var mapRow in map[0])
                {
                    var rowData = mapRow.blocks.Select(b => b.Data).ToList();
                    roomSave.Rows.Add(rowData);
                }

                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RoomsFolder);
                Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, $"room_{roomNumber}.json");
                File.WriteAllText(filePath, JsonSerializer.Serialize(roomSave, JsonOpts));

                MessageBox.Show($"Room {roomNumber} saved!\n{filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save failed: {ex.Message}");
            }
        }

        // ── LOAD (returns the RoomSaveData so MainWindow can regenerate the map) ──
        public static RoomSaveData? ReadRoomFile(int roomNumber)
        {
            try
            {
                string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RoomsFolder);
                string filePath = Path.Combine(folder, $"room_{roomNumber}.json");

                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Room {roomNumber} not found!\nLooked in: {filePath}");
                    return null;
                }

                return JsonSerializer.Deserialize<RoomSaveData>(File.ReadAllText(filePath), JsonOpts);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Load failed: {ex.Message}");
                return null;
            }
        }

        // ── APPLY (call after the map has been regenerated at the right size) ──
        public static void ApplyRoomData(
            List<List<Map_Block>> map,
            RoomSaveData roomSave,
            Action<Image, string, string, string> setGameImage)
        {
            var rows = map[0];

            for (int y = 0; y < roomSave.Rows.Count && y < rows.Count; y++)
            {
                var savedRow = roomSave.Rows[y];
                var blockRow = rows[y].blocks;

                for (int x = 0; x < savedRow.Count && x < blockRow.Count; x++)
                {
                    BlockData d = savedRow[x];
                    MapBlocks_Insides b = blockRow[x];

                    b.Data = d;

                    // Floor
                    if (!string.IsNullOrEmpty(d.current_Flore_Texture))
                        setGameImage(b.Flore, "Blocks", "Flores", d.current_Flore_Texture + "_block");

                    // Walls
                    if (!string.IsNullOrEmpty(d.current_Left_Wall_Texture))
                    {
                        string suffix = d.left_wall == MapBlocks_Insides.LeftWallType.DoorClosed ? "_closed"
                                      : d.left_wall == MapBlocks_Insides.LeftWallType.DoorOpen ? "_open"
                                      : d.left_wall == MapBlocks_Insides.LeftWallType.RoomDoor ? "_room"
                                      : "_sides";
                        setGameImage(b.Left_wall, "Blocks", "Left_Walls", d.current_Left_Wall_Texture + suffix);
                    }
                    if (!string.IsNullOrEmpty(d.current_Right_Wall_Texture))
                    {
                        string suffix = d.right_wall == MapBlocks_Insides.RightWallType.DoorClosed ? "_closed"
                                      : d.right_wall == MapBlocks_Insides.RightWallType.DoorOpen ? "_open"
                                      : d.right_wall == MapBlocks_Insides.RightWallType.RoomDoor ? "_room"
                                      : "_sides";
                        setGameImage(b.Right_wall, "Blocks", "Right_Walls", d.current_Right_Wall_Texture + suffix);
                    }
                    if (!string.IsNullOrEmpty(d.current_Upper_Wall_Texture))
                    {
                        string suffix = d.upper_wall == MapBlocks_Insides.UpperWallType.DoorClosed ? "_closed"
                                      : d.upper_wall == MapBlocks_Insides.UpperWallType.DoorOpen ? "_open"
                                      : d.upper_wall == MapBlocks_Insides.UpperWallType.RoomDoor ? "_room"
                                      : "_tops";
                        setGameImage(b.Upper_wall, "Blocks", "Top_Walls", d.current_Upper_Wall_Texture + suffix);
                    }
                    if (!string.IsNullOrEmpty(d.current_Downer_Wall_Texture))
                    {
                        string suffix = d.downer_wall == MapBlocks_Insides.DownerWallType.DoorClosed ? "_closed"
                                      : d.downer_wall == MapBlocks_Insides.DownerWallType.DoorOpen ? "_open"
                                      : d.downer_wall == MapBlocks_Insides.DownerWallType.RoomDoor ? "_room"
                                      : "_tops";
                        setGameImage(b.Downer_wall, "Blocks", "Buttom_Walls", d.current_Downer_Wall_Texture + suffix);
                    }

                    // Item / NPC / Chest
                    if (!string.IsNullOrEmpty(d.current_item_Texture))
                        setGameImage(b.Item, "Items", "faf", d.current_item_Texture);
                    if (!string.IsNullOrEmpty(d.current_NPC_Texture))
                        setGameImage(b.NPC, "Characters", "NPC", d.current_NPC_Texture);
                    if (!string.IsNullOrEmpty(d.current_Chest_Texture))
                        setGameImage(b.Chest, "Blocks", "Others", d.current_Chest_Texture);
                }
            }
        }
    }
}