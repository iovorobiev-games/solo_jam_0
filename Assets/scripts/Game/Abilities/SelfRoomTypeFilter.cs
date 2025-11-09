using UnityEngine;

namespace Game.Abilities
{
    public class SelfRoomTypeFilter : Filter
    {
        private readonly RoomTypeFilter type;

        public SelfRoomTypeFilter(RoomTypeFilter type)
        {
            this.type = type;
        }
        public bool filter(RoomVM room, Vector2Int relativePos)
        {
            if (relativePos != Vector2Int.zero || !room.IsPlaced)
            {
                return false;
            }

            var roomPos = room.dungeon.GetRoomPosition(room);
            var leftRoom = room.dungeon.GetRoom(roomPos + Vector3Int.left);
            var upRoom = room.dungeon.GetRoom(roomPos + Vector3Int.up);
            var downRoom = room.dungeon.GetRoom(roomPos + Vector3Int.down);
            var rightRoom = room.dungeon.GetRoom(roomPos + Vector3Int.right);
            return type.filter(leftRoom, relativePos) || type.filter(upRoom, relativePos) || type.filter(downRoom, relativePos) || type.filter(rightRoom, relativePos);
        }
    }
}