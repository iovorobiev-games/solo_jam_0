using System.Collections.Generic;

namespace Game.data
{
    public class RoomDB
    {
        public static string spritesheetpath = "dm";
        public static Dictionary<string, Room> rooms = new()
        {
            {"cellar", new Room("cellar", "1", "Cellar", 1, 1, 1)},
        };
    }
}