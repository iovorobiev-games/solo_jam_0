using Game.data;
using UnityEngine;

namespace Game.Abilities
{
    public class RoomTypeFilter : Filter
    {
        private string roomName;

        public RoomTypeFilter(string roomName)
        {
            this.roomName = roomName;
        }
        
        public bool filter(RoomVM room, Vector2Int relativePos)
        {
            return roomName == room.Room.name;
        }
    }
}