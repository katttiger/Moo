using Moo.Players;

namespace Moo.Statistic
{
    public class PlayerDAO : IPlayerDAO
    {
        public static void AddDataToScoreboard(string result, string path)
        {
            IPlayerDAO.PlayerDataWriter = new(path, append: true);
            IPlayerDAO.PlayerDataWriter.WriteLine(result);
            IPlayerDAO.PlayerDataWriter.Close();
        }

        public static List<PlayerData> GetTopList()
        {
            IPlayerDAO.PlayerDataReader = new("result.txt");
            IPlayerDAO.PlayerList = new List<IPLayer>();

            string? line;
            while ((line = IPlayerDAO.PlayerDataReader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                //TODO: if the player is actively gaming, increase number of games played by 1.
                PlayerData playerData = new PlayerData(name, guesses);
                int indexOfPlayerData = IPlayerDAO.PlayerList.IndexOf(playerData);

                //FIX: else is never hit.
                if (indexOfPlayerData < 0)
                {
                    IPlayerDAO.PlayerList.Add(playerData);
                }
                else
                {
                    IPlayerDAO.PlayerList[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            List<PlayerData> playerList = IPlayerDAO.PlayerList.Cast<PlayerData>().ToList();
            return playerList;
        }
    }
}
