namespace Games
{
    public interface IPlayer
    {
        string Name { get; }
        int NumberOfRoundsPlayed { get; }
        int TotalGuesses { get; set; }
        public void UpdatePlayerScoreAndRounds(int guesses);
        public double CalculatePlayerAverageScore();
        public void UpdatePlayerScore(int guesses);

    }
}
