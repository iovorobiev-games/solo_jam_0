using Game.Abilities;

namespace Game.data
{
    public class Room
    {
        public string name
        {
            get;
            private set;
        }

        public string spritePath
        {
            get;
            private set;
        }

        public string dweller
        {
            get;
            private set;
        }

        public int might
        {
            get;
            private set;
        }

        public int cooldown
        {
            get;
            private set;       
        }

        public int price
        {
            get;
            private set;       
        }

        public Skill Skill
        {
            get;
            private set;
        }

        public Room(string name, string spritePath, string dweller, int might, int cooldown, int price, Skill skill)
        {
            this.name = name;
            this.spritePath = spritePath;
            this.dweller = dweller;
            this.might = might;
            this.cooldown = cooldown;
            this.price = price;
            Skill = skill;       
        }
    }
}