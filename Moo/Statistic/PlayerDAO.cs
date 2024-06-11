using Moo.Players;

namespace Moo.Statistic
{
    public class PlayerDAO : IPlayerDAO
    {
        public static StreamReader PlayerDataReader { get; set; }
        public static StreamWriter PlayerDataWriter { get; set; }
        public static List<IPLayer> PlayerList { get; set; }
        public static void AddDataToScoreboard(string result, string path)
        {
            //output.WriteLine(name + "#&#" + nGuess)
            PlayerDataWriter = new(path, append: true);
            PlayerDataWriter.WriteLine(result);
            PlayerDataWriter.Close();
        }
        public static List<PlayerData> GetTopList()
        {
            PlayerDataReader = new("result.txt");
            PlayerList = new List<IPLayer>();
            string? line = string.Empty;
            while ((line = PlayerDataReader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                PlayerData playerData = new PlayerData(name, guesses);
                int indexOfPlayerData = PlayerList.IndexOf(playerData);

                //TODO: FIX: else is never hit. 
                if (indexOfPlayerData < 0)
                {
                    PlayerList.Add(playerData);
                }
                else
                {
                    PlayerList[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            List<PlayerData> playerList = PlayerList.Cast<PlayerData>().ToList();
            return playerList;
        }
    }
}
