using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class DungeonVM
    {
        Dictionary<RoomVM, Vector3Int> rooms = new();
        private int top;
        private int left;
        private int bottom;
        private int right;

        public DungeonVM(int top, int left, int bottom, int right)
        {
            this.top = top;
            this.left = left;
            this.bottom = bottom;
            this.right = right;
        }
        
        public void AddRoom(RoomVM room, Vector3Int coords)
        {
            rooms[room] = coords;
            printRooms();
        }

        private void printRooms()
        {
            foreach (var room in rooms)
            {
                Debug.Log(room.Key.Room.name + " is at " + room.Value.y + " " + room.Value.x);
            }
        }
    }
}