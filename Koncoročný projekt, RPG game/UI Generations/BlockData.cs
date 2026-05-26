using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Koncoročný_projekt__RPG_game.UI_Generations.MapBlocks_Insides;

namespace Koncoročný_projekt__RPG_game.UI_Generations
{

    public class BlockData
    {
        // --- Textures ---
        public string current_Left_Wall_Texture { get; set; } = "";
        public string current_Right_Wall_Texture { get; set; } = "";
        public string current_Upper_Wall_Texture { get; set; } = "";
        public string current_Downer_Wall_Texture { get; set; } = "";
        public string current_Flore_Texture { get; set; } = "";
        public string current_item_Texture { get; set; } = "";
        public string current_Enemy_Texture { get; set; } = "";
        public string current_NPC_Texture { get; set; } = "";
        public string current_NPC_Name { get; set; } = "Grrr";
        public string current_Chest_Texture { get; set; } = "";
        public List<string> NPC_Enemies { get; set; } = [];

        // --- Lists ---
        public List<string> current_NPC_Lines { get; set; } = [];
        public List<string> current_Chest_Items { get; set; } = [];

        // --- Teleporter ---
        public int NextRoomTeleporter_X { get; set; } = 0;
        public int NextRoomTeleporter_Y { get; set; } = 0;
        public int NextRoomTeleporter_Room { get; set; } = 0;
        public int NPC_Aura { get; set; } = 0;

        // --- Block / Wall types ---
        public BlockType block_type { get; set; } = BlockType.Empty;
        public LeftWallType left_wall { get; set; } = LeftWallType.None;
        public RightWallType right_wall { get; set; } = RightWallType.None;
        public UpperWallType upper_wall { get; set; } = UpperWallType.None;
        public DownerWallType downer_wall { get; set; } = DownerWallType.None;
    }

    /// <summary>
    /// One saved room — stores size so we can regenerate the map on load.
    /// </summary>
    public class RoomSaveData
    {
        public int RoomNumber { get; set; }
        public int XMap { get; set; }   // columns
        public int YMap { get; set; }   // rows
        public List<List<BlockData>> Rows { get; set; } = [];
    }
}

