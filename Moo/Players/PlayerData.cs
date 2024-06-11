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
        public int totalGuesses { get; set; }
        public PlayerData(string name, int guesses)
        {
            Name = name;
            NumberOfGamesPlayed = 1;
            totalGuesses = guesses;
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
