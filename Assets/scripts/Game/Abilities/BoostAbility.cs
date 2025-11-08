using System;

namespace Game.Abilities
{
    public class BoostAbility : Ability
    {
        private int boost;
        private BoostType boostType;

        public BoostAbility(int boost, BoostType boostType)
        {
            this.boost = boost;
            this.boostType = boostType;
        }

        public void use(RoomVM room)
        {
            switch (boostType)
            {
                case BoostType.DAMAGE:
                    room.DamageBoost += boost;
                    break;
                case BoostType.COOLDOWN:
                    room.CooldownBoost += boost;
                    break;
                case BoostType.COST:
                    room.CostBoost += boost;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public enum BoostType
        {
            DAMAGE,
            COOLDOWN,
            COST,
        }
    }
}