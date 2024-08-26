using Games.Ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Statistic
{
    public class PlayerscorePresenter
    {
        public List<Player> GetPlayerData(string pathToData)
        {
            return DataMethods.GetPlayerdataFromFile(pathToData);
        }
        public void ShowTopListForGame(string pathToData)
        {
            UserInterface userInterface = new();
            List<Player> results = GetPlayerData(pathToData);
            results.Sort((p1, p2) => p1.TotalGuesses.CompareTo(p2.TotalGuesses));

            //DO NOT REMOVE: Usable when saving data to a database (has not been implemented).
            //DO NOT REMOVE: results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));

            userInterface.WriteOutput("Player      games average");
            foreach (Player player in results)
            {
                userInterface.WriteOutput(
                    string.Format("{0,-9}{1,5:D}{2,9:F2}",
                    player.Name, player.NumberOfRoundsPlayed,
                    player.TotalGuesses));
            }
        }
    }
}
