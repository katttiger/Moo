using Moo.Interfaces;
using Moo.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moo.Statistic
{
    public class PlayerDataContext
    {
        public static void SavePlayerData()
        {

        }
        public static void FetchPlayerData()
        {

        }
        public static void LoadPlayerData() { }

        public static void ShowTopList(IUI ui)
        {
            StreamReader input = new("result.txt");
            input.Close();

            List<PlayerData> results = PlayerDAO.GetTopList("result.txt");
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            ui.WriteOutput("Player       games average");

            foreach (PlayerData player in results)
            {
                ui.WriteOutput(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGamesPlayed, player.CalculatePlayerAverageScore()));
            }
            input.Close();
        }
    }
}
