using Games.Ui;
using System.Runtime.CompilerServices;

namespace Games
{
    public class DataMethods : IDataMethods
    {
        public static void AddData(string data, string pathToData)
        {
            if (!string.IsNullOrEmpty(pathToData) && data.Contains("#&#"))
            {
                StreamWriter writer = new(pathToData, append: true);
                writer.WriteLine(data);
                writer.Close();
            }
        }
        public static List<Player> GetPlayerdataFromFile(string pathToData)
        {
            StreamReader reader = new(pathToData);
            const string Seperator = "#&#";
            UserInterface ui = new();
            List<Player> playerList = [];

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    string[] playerNameAndScoreArray = line.Split(
                        new string[] { Seperator }, StringSplitOptions.None);

                    Player playerData = new(playerNameAndScoreArray[0],
                        ui.ParseStringToInt(playerNameAndScoreArray[1]));
                    playerList.Add(playerData);
                }
            }
            reader.Close();
            return playerList;
        }
    }
}
