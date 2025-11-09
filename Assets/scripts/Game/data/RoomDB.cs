using System.Collections.Generic;

namespace Game.data
{
    public class RoomDB
    {
        public static string spritesheetpath = "dm";
        public static Dictionary<string, Room> rooms = new()
        {
            {"cellar", new Room("cellar", "1", "Rats", 1, 2, 1, SkillDB.skills["rats"])},
            {"lab_left_right", new Room("Alchemist Lab", "4", "Alchemist", 1, 4, 2, SkillDB.skills["damage_boost_left_right_1"])},
            {"lab_top_down", new Room("Alchemist Lab", "4", "Alchemist", 1, 4, 2, SkillDB.skills["damage_boost_top_down_1"])},
            {"fungi_grove", new Room(
                "Fungi grove", 
                "6", 
                "Fungi", 
                1, 
                2, 
                2, 
                SkillDB.skills["fungi_bonus"]
                )
            },
            {"tomb", new Room("Tomb", "5", "Skeletons", 3, 4, 2, SkillDB.skills["self_cd_reduce"])},
            {"chest", new Room("Treasury", "7", "Treasures!", 1, 2, 1, SkillDB.skills["cost_reduce_one_shot"])}
        };
    }
}