namespace Game.data
{
    public class Room
    {
        public string name;
        public string spritePath;
        public string description;
        public int might;
        public int cooldown;
        public int price;

        public Room(string name, string spritePath, string description, int might, int cooldown, int price)
        {
            this.name = name;
            this.spritePath = spritePath;
            this.description = description;
            this.might = might;
            this.cooldown = cooldown;
            this.price = price;
        }
    }
}