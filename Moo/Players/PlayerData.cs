using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Players
{
    public class PlayerData : IPLayer
    {
        public string Name { get; private set; }
        public int NumberOfGamesPlayed { get; private set; }
        int totalGuesses { get; set; }
        public bool IsActive { get; private set; }

        public PlayerData(string name, int gamesPlayed)
        {
            Name = name;
            NumberOfGamesPlayed = gamesPlayed;
        }

        public PlayerData(string name, int gamesPlayed, int guesses, bool isActive = true)
        {
            Name = name;
            NumberOfGamesPlayed = gamesPlayed;
            totalGuesses = guesses;
            IsActive = isActive;
        }

        public void UpdatePlayerScore(int guesses)
        {
            totalGuesses += guesses;
            //Number of rounds?
            NumberOfGamesPlayed++;
        }
        public double CalculatePlayerAverageScore()
        {
            return (double)totalGuesses / NumberOfGamesPlayed;
        }
    }
}
