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
        public int TotalGuesses { get; set; }
        public PlayerData(string name, int guesses)
        {
            Name = name;
            NumberOfGamesPlayed = 0;
            this.TotalGuesses = guesses;
        }
        public PlayerData() { }

        public void UpdatePlayerScore(int guesses)
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
