namespace Game
{
    public class EnemyVM
    {
        public int HitPoints
        {
            get;
            set;
        }

        public bool takeDamage(int damage)
        {
            HitPoints -= damage;
            return HitPoints > 0;
        }
    }
}
