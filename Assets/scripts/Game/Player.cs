namespace Game
{
    public class Player
    {
        private int budget;
        public int Budget
        {
            get
            {
                return budget;
            }
            set
            {
                budget = value;
                currentBudget = budget;
            }
        }

        public int currentBudget
        {
            get;
            set;
        }
        private int lives = 3;
        private int currentLives = 3;
        
        public void spend(int amount)
        {
            currentBudget -= amount;
        }

        public bool hasBudgetFor(int amount)
        {
            return currentBudget >= amount;
        }
        
        
    }
}