using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo
{
  class PlayerData
    {
        public string Name { get; private set; }
        public int NumberOfGamesPlayed { get; private set; }
        public int totalGuesses;

        public PlayerData(string name, int gamesPlayed, int guesses)
        {
            this.Name = name;
            NumberOfGamesPlayed = gamesPlayed;
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
