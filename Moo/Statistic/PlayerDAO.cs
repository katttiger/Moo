using Moo.Players;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Moo.Statistic
{
    //Responsible for saving and retrieving player data.
    public class PlayerDAO : IPlayerDAO
    {
        //Skicka in filePath till ctor för PlayerDAO.
        //Gör att du kan köra tester mot en annan
        //textfil som du behöver skapa upp i varje test
        private string _fileName = string.Empty;
        private const string Seperator = "#&#";
        public PlayerDAO(string filename)
        {
            _fileName = filename;
        }
        public static List<PlayerData> GetTopList(string fileName)
        {
            StreamReader reader = new(fileName);
            List<PlayerData> playerList = new List<PlayerData>();

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                PlayerData playerData = new PlayerData(name, guesses);
                int indexOfPlayerData = playerList.IndexOf(playerData);

                //FIX: else is never hit.
                if (indexOfPlayerData < 0)
                {
                    playerList.Add(playerData);
                }
                else
                {
                    playerList[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            reader.Close();
            return playerList;
        }

        public static void AddPlayerdataToScoreboard(string result, string path)
        {
            StreamWriter writer = new StreamWriter(path, append: true);
            writer.Write(result + writer.NewLine);
            writer.Close();
        }

        #region
        //Return list of player data from textfile
        public List<PlayerData> GetPlayerDatas()
        {
            throw new NotImplementedException();
        }

        //Should save player data to textfile
        public void Save(string name, int totalGuesses)
        {
            throw new NotImplementedException();
        }

        #endregion

        //used for testing
        public class FakePlayerDAO : IPlayerDAO
        {
            string _txtFileContent = string.Empty;
            public FakePlayerDAO(string initialFileContent)
            {
                _txtFileContent = initialFileContent;
            }
            //Return player data
            public List<PlayerData> GetPlayerDatas()
            {
                List<PlayerData> results = new List<PlayerData>();
                string line;
                string[] lines = _txtFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    Debug.WriteLine(lines[i]);
                    line = lines[i];
                    string[] nameAndScore = line.Split(new string[] { Seperator }, StringSplitOptions.None);
                    string name = nameAndScore[0];
                    int guesses = Convert.ToInt32(nameAndScore[1]);
                    PlayerData pd = new PlayerData(name, guesses);
                    int pos = results.IndexOf(pd);
                    if (pos < 0)
                    {
                        results.Add(pd);
                    }
                    else
                    {
                        results[pos].UpdatePlayerScore(guesses);
                    }
                }
                results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
                return results;
            }
            //Save to memory
            public void Save(string name, int totalGuesses)
            {
                _txtFileContent += name + Seperator + totalGuesses + Environment.NewLine;
                Debug.WriteLine("Saved");
            }
        }
    }

}
