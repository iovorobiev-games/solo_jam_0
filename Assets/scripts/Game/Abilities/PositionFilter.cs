using System;
using System.Collections.Generic;
using Game.data;
using UnityEngine;

namespace Game.Abilities
{
    public class PositionFilter : Filter
    {
        private Vector2Int[] positions;

        public PositionFilter(Vector2Int[] positions)
        {
            this.positions = positions;
        }

        public bool filter(RoomVM room, Vector2Int relativePos)
        {
            return Array.Exists(positions, pos => pos == relativePos);
        }
    }
}