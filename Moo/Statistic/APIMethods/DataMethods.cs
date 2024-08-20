namespace Games
{
    public class DataMethods : IDataMethods
    {
        public static void AddData(string data, string pathToData)
        {
            StreamWriter writer = new(pathToData, append: true);
            writer.Write(data);
            writer.Close();
        }
        public static List<Player> GetData(string pathToData)
        {
            StreamReader reader = new(pathToData);
            string Seperator = "#&#";

            List<Player> playerList = [];

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { Seperator }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                Player playerData = new(name, guesses);
                int indexOfPlayerData = playerList.IndexOf(playerData);

                //FIX: else is never hit.
                if (indexOfPlayerData < 0)
                {
                    playerList.Add(playerData);
                }
                else
                {
                    playerList[indexOfPlayerData].UpdatePlayerStatus(guesses);
                }
            }
            reader.Close();
            return playerList;
        }
    }
}
