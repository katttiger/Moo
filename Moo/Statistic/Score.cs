using Moo.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic
{
    public class Score : IScore
    {
        public StreamReader FileReader { get; set; }

        public List<IPLayer> PlayerList { get; set; }

        public Score(StreamReader streamReader, List<IPLayer> list)
        {
            FileReader = streamReader;
            PlayerList = list;
        }

        //Kopplad mot ...

        //StreamReader
        public static List<PlayerData> GetTopList()
        {
            Score score = new(new StreamReader("result.txt"), new List<IPLayer>());
            string? line = string.Empty;
            //Why is it important to declare line all the time?
            while ((line = score.FileReader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                PlayerData playerData = new PlayerData(name, guesses);
                int indexOfPlayerData = score.PlayerList.IndexOf(playerData);

                //TODO: else is never hit.
                if (indexOfPlayerData < 0)
                {
                    score.PlayerList.Add(playerData);
                }
                else
                {
                    score.PlayerList[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            List<PlayerData> playerList = score.PlayerList.Cast<PlayerData>().ToList();
            return playerList;
        }
    }
}
