using Cysharp.Threading.Tasks;
using Game.data;

namespace Game
{
    public class RoomVM
    {
        public Room Room  { get; }
        private int currentCooldown = 0;

        public RoomVM(Room room)
        {
            Room = room;
        }

        public async UniTask SetActive(bool active)
        {
            currentCooldown = active ? 0 : Room.cooldown;
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
        
        public int getCost()
        {
            return Room.price;
        }

        public int getDamage()
        {
            return Room.might;
        }
    }
}