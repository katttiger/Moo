using Games.Player;
using Games.Player.APIMethods;
using Games.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Games.Player
{
    public class PlayerDataContext
    {
        public PlayerDataContext(string pathToData = "")
        {
            PathToData = pathToData;
        }
        string PathToData { get; set; } = "";
        public static void SavePlayerData(PlayerDAO playerDAO)
        {
            DataMethods.Add(playerDAO, "");
        }
        public static void ShowTopList(IUI ui)
        {
            PlayerDAO playerDAO = new();
            List<PlayerData> results = playerDAO.GetPlayerDatas("");
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            ui.WriteOutput("Player       games average");
            foreach (PlayerData player in results)
            {
                ui.WriteOutput(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGamesPlayed, player.CalculatePlayerAverageScore()));
            }
        }
    }
}
