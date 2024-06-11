using Moo.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic
{
    public class Score : IScore
    {

        //Kopplad mot ...
        //StreamReader
        public static List<PlayerData> GetTopList()
        {
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();
            string? line = string.Empty;
            //Why is it important to declare line all the time?
            while ((line = input.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                PlayerData playerData = new PlayerData(name, 1);
                int indexOfPlayerData = results.IndexOf(playerData);

                //TODO: else is never hit.
                if (indexOfPlayerData < 0)
                {
                    results.Add(playerData);
                }
                else
                {
                    results[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            return results;
        }
    }
}
