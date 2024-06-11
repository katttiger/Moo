using Moo.Players;

namespace Moo.Statistic
{
    public class PlayerDAO : IPlayerDAO
    {
        public StreamReader DataReader { get; set; }
        public StreamWriter DataWriter { get; set; }
        public List<IPLayer> PlayerList { get; set; }
        public static void AddDataToScoreboard(string name, int numberOfGuesses)
        {
            StreamWriter output = new StreamWriter("result.txt", append: true);
            output.WriteLine(name + "#&#" + numberOfGuesses);
            output.Close();
        }
        public static List<PlayerData> GetTopList()
        {
            PlayerDAO score = new PlayerDAO();
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();

            string? line = string.Empty;
            while ((line = input.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                PlayerData playerData = new PlayerData(name, guesses);
                int indexOfPlayerData = results.IndexOf(playerData);

                //TODO: FIX: else is never hit. 
                if (indexOfPlayerData < 0)
                {
                    results.Add(playerData);
                }
                else
                {
                    results[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            List<PlayerData> playerList = results.Cast<PlayerData>().ToList();
            return playerList;
        }
    }
}
