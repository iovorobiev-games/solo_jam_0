using System.Linq;
using Game.data;

namespace Game
{
    public class RandomRoomGenerator
    {
        public static Room generate()
        {
            var allRooms = RoomDB.rooms.Values.ToList();
            return allRooms[UnityEngine.Random.Range(0, allRooms.Count)];
        }
    }
}