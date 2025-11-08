using Game.data;
using UnityEngine;

namespace Game.Abilities
{
    public class NewCardsFilter: Filter
    {
        public static NewCardsFilter instance = new();
        public bool filter(RoomVM room, Vector2Int relativePos)
        {
            return !room.IsPlaced;
        }
    }
}