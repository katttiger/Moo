using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Players
{
    public interface IPLayer
    {
        string Name { get; }
        int NumberOfGamesPlayed { get; }
        int totalGuesses { get; set; }

        public void UpdatePlayerScore(int guesses);

        public double CalculatePlayerAverageScore();
    }
}
