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
        private static readonly JsonSerializerOptions JsonOpts = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };
        public static void SaveRoomToFolder(
            List<List<Map_Block>> map, int roomNumber, int xMap, int yMap, string folder)
        {
            try
            {
                var roomSave = new RoomSaveData { RoomNumber = roomNumber, XMap = xMap, YMap = yMap };
                foreach (var mapRow in map[0])
                    roomSave.Rows.Add(mapRow.blocks.Select(b => b.Data).ToList());

                Directory.CreateDirectory(folder);
                File.WriteAllText(
                    Path.Combine(folder, $"room_{roomNumber}.json"),
                    JsonSerializer.Serialize(roomSave, JsonOpts));
            }
            catch (Exception ex) { MessageBox.Show($"Save failed: {ex.Message}"); }
        }

        public static RoomSaveData? ReadRoomFromFolder(int roomNumber, string folder)
        {
            try
            {
                string filePath = Path.Combine(folder, $"room_{roomNumber}.json");
                if (!File.Exists(filePath))
                {
                    MessageBox.Show($"Room {roomNumber} not found in {folder}");
                    return null;
                }
                return JsonSerializer.Deserialize<RoomSaveData>(File.ReadAllText(filePath), JsonOpts);
            }
            catch (Exception ex) { MessageBox.Show($"Load failed: {ex.Message}"); return null; }
        }
        private const string RoomsFolder = "Rooms";
        private static string LegacyFolder =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RoomsFolder);

        public static void SaveRoom(List<List<Map_Block>> map, int roomNumber, int xMap, int yMap)
            => SaveRoomToFolder(map, roomNumber, xMap, yMap, LegacyFolder);

        public static RoomSaveData? ReadRoomFile(int roomNumber)
            => ReadRoomFromFolder(roomNumber, LegacyFolder);

        public static void ApplyRoomData(
            List<List<Map_Block>> map,
            RoomSaveData roomSave,
            Action<System.Windows.Controls.Image, string, string, string> setGameImage)
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

                    if (!string.IsNullOrEmpty(d.current_Flore_Texture))
                        setGameImage(b.Flore, "Blocks", "Flores", d.current_Flore_Texture + "_block");

                    if (!string.IsNullOrEmpty(d.current_Left_Wall_Texture))
                        setGameImage(b.Left_wall, "Blocks", "Left_Walls", d.current_Left_Wall_Texture);
                    if (!string.IsNullOrEmpty(d.current_Right_Wall_Texture))
                        setGameImage(b.Right_wall, "Blocks", "Right_Walls", d.current_Right_Wall_Texture);
                    if (!string.IsNullOrEmpty(d.current_Upper_Wall_Texture))
                        setGameImage(b.Upper_wall, "Blocks", "Top_Walls", d.current_Upper_Wall_Texture);
                    if (!string.IsNullOrEmpty(d.current_Downer_Wall_Texture))
                        setGameImage(b.Downer_wall, "Blocks", "Buttom_Walls", d.current_Downer_Wall_Texture);

                    if (!string.IsNullOrEmpty(d.current_item_Texture))
                        setGameImage(b.Item, "Items", "faf", d.current_item_Texture);
                    if (!string.IsNullOrEmpty(d.current_NPC_Texture))
                    {
                        string npcFolder = d.NPC_Aura > 0 ? "Enemies" : "NPC";
                        setGameImage(b.NPC, "Characters", npcFolder, d.current_NPC_Texture);
                    }
                    if (!string.IsNullOrEmpty(d.current_Chest_Texture))
                        setGameImage(b.Chest, "Blocks", "Others", d.current_Chest_Texture);
                }
            }
        }
    }
}
