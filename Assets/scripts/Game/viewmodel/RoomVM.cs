using System;
using Cysharp.Threading.Tasks;
using DefaultNamespace;
using Game.Abilities;
using Game.data;
using UnityEngine;

namespace Game
{
    public class RoomVM
    {
        public Room Room  { get; }

        public int currentCooldown
        {
            get;
            set;
        }

        public bool IsPlaced
        {
            get;
            set;
        }

        public int DamageBoost
        {
            get;
            set;
        } 
        
        public int CooldownBoost
        {
            get;
            set;
        }

        public int CostBoost
        {
            get;
            set;
        }

        public DungeonVM dungeon
        {
            get;
            private set;
        }
        private int skillUses;
        private readonly RoomSelectionView selectionView;

        public RoomVM(Room room)
        {
            Room = room;
            dungeon = DI.sceneScope.getInstance<DungeonVM>();
            selectionView = DI.sceneScope.getInstance<RoomSelectionView>();
        }

        public async UniTask SetActive(bool active)
        {
            currentCooldown = active ? 0 : getCooldown();
        }

        public void tick()
        {
            if (currentCooldown > 0)
            {
                currentCooldown--;
            }
        }
        
        public bool IsActive()
        {
            return currentCooldown == 0;
        }

        public int getCooldown()
        {
            return Math.Max(Room.cooldown - CooldownBoost, 0);
        }
        
        public int getCost()
        {
            return Math.Max(Room.price - CostBoost, 0);
        }

        public int getDamage()
        {
            return Room.might + DamageBoost;
        }

        public void resetBoosts()
        {
            DamageBoost = 0;
            CooldownBoost = 0;
        }

        public void triggerSkillOn(RoomVM room, Vector2Int coords)
        {
            if (Room.Skill.filter(room, coords))
            {
                Room.Skill.ability.use(room);
            }
        }
        
        public void triggerSkill()
        {
            if (!IsActive() || (Room.Skill.MaxUses > 0 && skillUses >= Room.Skill.MaxUses))
            {
                return;
            }
            Debug.Log("Trigger Skill " + Room.name);
            var impactedRooms = dungeon.GetRoomsFromRoomSkillFilter(this);
            Debug.Log("Impacted rooms count: " + impactedRooms.Count);
            foreach (var impacted in impactedRooms)
            {
                Debug.Log("Triggering for impacted " + impacted.Room.name);
                Room.Skill.ability.use(impacted);
            }

            foreach (var roomVm in selectionView.selectableRoomsArray)
            {
                if (!Room.Skill.filter(roomVm, Vector2Int.zero))
                {
                    continue;
                }
                Room.Skill.ability.use(roomVm);
            }
            if (Room.Skill.MaxUses > 0)
            {
                skillUses++;
            }
        }
    }
}