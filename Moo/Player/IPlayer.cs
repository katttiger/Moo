namespace Games
{
    public interface IPlayer
    {
        string Name { get; }
        int NumberOfRoundsPlayed { get; }
        int TotalGuesses { get; set; }
        public void UpdatePlayerStatus(int guesses);
        public double CalculatePlayerAverageScore();

    }
}
