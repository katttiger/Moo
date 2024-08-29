namespace Games
{
    public interface IPlayer
    {
        string Name { get; }
        int NumberOfRoundsPlayed { get; }
        int TotalGuesses { get; set; }
        public void UpdatePlayerScore(int guesses);
        public void UpdatePlayerScoreAndRounds(int guesses);
        public double CalculatePlayerAverageScore();

    }
}
