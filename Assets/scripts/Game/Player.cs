namespace Game
{
    public class Player
    {
        private int budget;
        private int currentBudget;
        private int lives = 3;
        private int currentLives = 3;
        
        public Player()
        {
        }

        public void setBudget(int budget)
        {
            this.budget = budget;
            currentBudget = budget;
        }

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