using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class TilePlacement : MonoBehaviour
    {
        public Tilemap tilemap;
        public int left;
        public int top;
        public int right;
        public int bottom;

        private bool highlight = false;
        private Vector3Int lastKnownPosition = Vector3Int.forward;
        
        private void Awake()
        {
            DI.sceneScope.register(this);
        }

        private void Update()
        {
            if (!highlight)
            {
                return;
            }
            
            Vector3 closestTilePosition = GetClosestTilePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            var position = tilemap.WorldToCell(closestTilePosition);
            if (position == lastKnownPosition)
            {
                return;
            }
            if (lastKnownPosition != Vector3Int.forward)
            {
                tilemap.SetColor(position, Color.white);
            }
            tilemap.SetTileFlags(position, TileFlags.None);
            tilemap.SetColor(position, Color.green);
            lastKnownPosition = position;
        }

        public void highlightClosestPosition(bool highlight)
        {
            this.highlight = highlight;
        }
        
        public Vector3 GetClosestTilePosition(Vector3 worldPosition)
        {
            Vector3Int centerCell = tilemap.WorldToCell(worldPosition);
            Vector3Int closestTilePosition = Vector3Int.zero;
            float closestDistance = float.MaxValue;

            // Only search within the bounded rectangle
            for (int x = left; x <= right; x++)
            {
                for (int y = bottom; y <= top; y++)
                {
                    Vector3Int cellPosition = new Vector3Int(x, y, centerCell.z);

                    // Check if tile exists (not empty)
                    if (tilemap.HasTile(cellPosition))
                    {
                        Vector3 tileWorldPosition = tilemap.CellToWorld(cellPosition);
                        float distance = Vector3.Distance(worldPosition, tileWorldPosition);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            closestTilePosition = new Vector3Int(cellPosition.x, cellPosition.y, cellPosition.y);
                        }
                    }
                }
            }
            Debug.Log("Closest tile position is: " + closestTilePosition + "");
            Debug.Log("Closest tile position world is: " +  tilemap.CellToWorld(closestTilePosition) + "");
            return tilemap.CellToWorld(closestTilePosition) + Vector3.one * 0.5f;
        }
    }
}