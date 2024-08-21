namespace Games
{
    public class DataMethods : IDataMethods
    {
        public static void AddData(string data, string pathToData)
        {
            StreamWriter writer = new(pathToData, append: true);
            writer.WriteLine(data);
            writer.Close();
        }
        public static List<Player> GetPlayerData(string pathToData)
        {
            StreamReader reader = new(pathToData);
            const string Seperator = "#&#";

            List<Player> playerList = [];

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { Seperator }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);
                Player playerData = new(name, guesses);

                //FIX: else is never hit.
                if (playerList.IndexOf(playerData) < 0)
                {
                    playerList.Add(playerData);
                }
                else
                {
                    playerList[playerList.IndexOf(playerData)].CalculatePlayerAverageScore();
                }
            }
            reader.Close();
            return playerList;
        }
    }
}
