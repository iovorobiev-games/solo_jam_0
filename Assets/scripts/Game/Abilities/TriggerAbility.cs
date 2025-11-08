namespace Game.Abilities
{
    public class TriggerAbility : Ability
    {
        public void use(RoomVM room)
        {
            room.triggerSkill();
        }
    }
}