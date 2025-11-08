using System;
using System.Collections.Generic;
using System.Linq;
using Game.Abilities;
using Game.data;
using UnityEngine;

namespace Game
{
    public class DungeonVM
    {
        Dictionary<Vector3Int,RoomVM> rooms = new();
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
            rooms[coords] = room;
            room.triggerSkill();
            foreach (var roomVm in rooms)
            {
                var relativeCoords = roomVm.Key - coords;
                roomVm.Value.triggerSkillOn(room, new Vector2Int(relativeCoords.x, relativeCoords.y));
            }
        }

        public Vector3Int[] getPathThroughDungeon()
        {
            var values = rooms
                .Where(roomVm => roomVm.Value.IsActive())
                .ToDictionary(roomVm => roomVm.Key, roomVm => roomVm.Value)
                .Keys.ToArray();
            Array.Sort(values, (a, b) => a.y.CompareTo(b.y) == 0 ? a.x.CompareTo(b.x) : b.y.CompareTo(a.y));
            return values;
        }

        public void tick()
        {
            foreach (var (_, room) in rooms)
            {
                room.tick();
            }
        }

        public void retriggerSkills()
        {
            foreach (var entry in rooms)
            {
                entry.Value.resetBoosts();
            }
            foreach (var entry in rooms)
            {
                entry.Value.triggerSkill();
            }
        }
        
        public RoomVM GetRoom(Vector3Int coords)
        {
            return rooms[coords];
        }

        public List<RoomVM> GetRoomsFromRoomSkillFilter(RoomVM filter)
        {
            var coords = rooms.First((entry) => filter == entry.Value).Key;
            return GetRoomsFromRoomSkillFilter(filter, coords);
        }
        
        public List<RoomVM> GetRoomsFromRoomSkillFilter(RoomVM filter, Vector3Int coords)
        {
            return rooms.Where((entry) =>
            {
                var coordsDifference = entry.Key - coords;
                return filter.Room.Skill.filter(entry.Value, new Vector2Int(coordsDifference.x, coordsDifference.y));
            }).Select((pair) => pair.Value).ToList();
        }
        
        private void printRooms()
        {
            foreach (var room in rooms)
            {
                Debug.Log(room.Value.Room.name + " is at " + room.Key.y + " " + room.Key.x);
            }
        }
    }
}