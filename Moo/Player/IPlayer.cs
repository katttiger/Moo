namespace Games
{
    public interface IPLayer
    {
        string Name { get; }
        int NumberOfRoundsPlayed { get; }
        int TotalGuesses { get; set; }
        //public void UpdatePlayerScore(int guesses);
        //public double CalculatePlayerAverageScore();
    }
}
