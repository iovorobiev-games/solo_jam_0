using Game.data;
using UnityEngine;

namespace Game.Abilities
{
    public interface Filter
    {
        bool filter(RoomVM room, Vector2Int relativePos);
    }
}