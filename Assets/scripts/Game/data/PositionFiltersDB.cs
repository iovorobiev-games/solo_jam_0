using System.Collections.Generic;
using Game.Abilities;
using UnityEngine;

namespace Game.data
{
    public class PositionFiltersDB
    {
        public static Dictionary<string, PositionFilter> filters = new()
        {
            { "all", new PositionFilter(new[] { Vector2Int.down, Vector2Int.left, Vector2Int.right, Vector2Int.up }) },
            { "top_down", new PositionFilter(new[] { Vector2Int.down, Vector2Int.up }) },
            { "left_right", new PositionFilter(new[] { Vector2Int.left, Vector2Int.right }) },
            { "left_down", new PositionFilter(new[] { Vector2Int.left, Vector2Int.down }) },
            { "left_up", new PositionFilter(new[] { Vector2Int.left, Vector2Int.up,  }) },
            { "right_down", new PositionFilter(new[] { Vector2Int.right, Vector2Int.down }) },
            { "right_up", new PositionFilter(new[] { Vector2Int.right, Vector2Int.up }) },
            { "left", new PositionFilter(new[] { Vector2Int.left}) },
            { "down", new PositionFilter(new[] { Vector2Int.down }) },
            { "up", new PositionFilter(new[] { Vector2Int.up }) },
            { "right", new PositionFilter(new[] { Vector2Int.right }) },
        };
    }
}