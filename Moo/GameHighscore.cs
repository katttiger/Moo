using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moo;

namespace Moo
{
    public static class GameHighscore
    {
        public static void ShowTopList()
        {
            //Fetches input
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();

            //what is "line" used for?
            string line;
            while ((line = input.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                PlayerData playerData = new PlayerData(name, 1, guesses);
                int indexOfPlayerData = results.IndexOf(playerData);

                //TODO: Else is never hit.
                if (indexOfPlayerData < 0)
                {
                    results.Add(playerData);
                }
                //pos === -1 => does not hit else. It does not change when you play more rounds.
                else
                {
                    results[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }

            //Prints out the score
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            Console.WriteLine("Player   games average");
            foreach (PlayerData p in results)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGamesPlayed, p.CalculatePlayerAverageScore()));
            }
            input.Close();
        }
    }
}
