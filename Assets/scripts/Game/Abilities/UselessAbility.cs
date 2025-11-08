namespace Game.Abilities
{
    public class UselessAbility : Ability
    {
        public static UselessAbility instance = new();
        public void use(RoomVM room)
        {
            // do nothing
        }
    }
}