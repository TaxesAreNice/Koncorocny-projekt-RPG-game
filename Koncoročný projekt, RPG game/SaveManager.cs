using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Koncoročný_projekt__RPG_game.UI_Generations;

namespace Koncoročný_projekt__RPG_game
{
    public static class SaveManager
    {
        public static string CurrentSaveName { get; private set; } = "";

        private const string SavesRoot = "Saves";
        private const string DefaultName = "DEFAULT";
        private const string PlayerFile = "player.json";

        private static readonly JsonSerializerOptions JsonOpts = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        };

        private static string SavesRootPath =>
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SavesRoot);

        private static string DefaultPath =>
            Path.Combine(SavesRootPath, DefaultName);

        private static string CurrentSavePath =>
            Path.Combine(SavesRootPath, CurrentSaveName);

        private static string RoomPath(string saveFolder, int roomNumber) =>
            Path.Combine(saveFolder, $"room_{roomNumber}.json");

        private static string PlayerPath(string saveFolder) =>
            Path.Combine(saveFolder, PlayerFile);

        public static bool NewGame(string saveName, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(saveName))
            {
                error = "Save name cannot be empty!";
                return false;
            }

            if (!Directory.Exists(DefaultPath))
            {
                error = $"DEFAULT folder not found!\nExpected: {DefaultPath}\nBuild your world first using the Studio and save each room into Saves/DEFAULT/.";
                return false;
            }

            string dest = Path.Combine(SavesRootPath, saveName);

            if (Directory.Exists(dest))
            {
                error = $"A save named '{saveName}' already exists!";
                return false;
            }

            try
            {
                CopyDirectory(DefaultPath, dest);

                var freshPlayer = new PlayerSaveData();
                File.WriteAllText(PlayerPath(dest), JsonSerializer.Serialize(freshPlayer, JsonOpts));

                CurrentSaveName = saveName;
                return true;
            }
            catch (Exception ex)
            {
                error = $"Failed to create save: {ex.Message}";
                return false;
            }
        }

        public static PlayerSaveData? LoadGame(string saveName, out string error)
        {
            error = "";
            if (string.IsNullOrWhiteSpace(saveName))
            {
                error = "Save name cannot be empty!";
                return null;
            }

            string folder = Path.Combine(SavesRootPath, saveName);
            if (!Directory.Exists(folder))
            {
                error = $"No save named '{saveName}' was found!";
                return null;
            }

            string playerFile = PlayerPath(folder);
            if (!File.Exists(playerFile))
            {
                error = $"Save '{saveName}' is missing player.json!";
                return null;
            }

            try
            {
                var data = JsonSerializer.Deserialize<PlayerSaveData>(
                    File.ReadAllText(playerFile), JsonOpts);

                if (data == null) { error = "Failed to read player data."; return null; }

                CurrentSaveName = saveName;
                return data;
            }
            catch (Exception ex)
            {
                error = $"Failed to load save: {ex.Message}";
                return null;
            }
        }

        public static void SavePlayer(PlayerSaveData data)
        {
            if (string.IsNullOrEmpty(CurrentSaveName)) return;
            try
            {
                File.WriteAllText(PlayerPath(CurrentSavePath),
                    JsonSerializer.Serialize(data, JsonOpts));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save player: {ex.Message}");
            }
        }

        public static void SaveRoom(List<List<UI_Generations.Map_Block>> map, int roomNumber, int xMap, int yMap)
        {
            string folder = string.IsNullOrEmpty(CurrentSaveName)
                  ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rooms")
                  : CurrentSavePath;
            RoomSaveLoad.SaveRoomToFolder(map, roomNumber, xMap, yMap, folder);
        }

        public static RoomSaveData? ReadRoom(int roomNumber)
        {
            if (string.IsNullOrEmpty(CurrentSaveName))
                return RoomSaveLoad.ReadRoomFromFolder(roomNumber,
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rooms"));
            return RoomSaveLoad.ReadRoomFromFolder(roomNumber, CurrentSavePath);
        }

        private static void CopyDirectory(string source, string dest)
        {
            Directory.CreateDirectory(dest);
            foreach (var file in Directory.GetFiles(source))
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)));
            foreach (var dir in Directory.GetDirectories(source))
                CopyDirectory(dir, Path.Combine(dest, Path.GetFileName(dir)));
        }
    }
}
