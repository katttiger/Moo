namespace Games.Player
{
    public class PlayerData(string name, int guesses) : IPLayer
    {
        public string Name { get; private set; } = name;
        public int NumberOfGamesPlayed { get; private set; } = 1;
        public int TotalGuesses { get; set; } = guesses;

        public void UpdatePlayerStatus(int guesses)
        {
            TotalGuesses += guesses;
            NumberOfGamesPlayed++;
        }
        public double CalculatePlayerAverageScore()
        {
            return TotalGuesses / NumberOfGamesPlayed;
        }
    }
}
