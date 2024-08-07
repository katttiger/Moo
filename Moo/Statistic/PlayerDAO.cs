using Moo.Players;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Moo.Statistic
{
    //Responsible for saving and retrieving player data.
    //CRUD
    public struct PlayerDAO : IPlayerDAO
    {
        //Skicka in filePath till ctor för PlayerDAO.
        //Gör att du kan köra tester mot en annan
        //textfil som du behöver skapa upp i varje test

        private readonly string _fileName = string.Empty;
        private const string Seperator = "#&#";
        public PlayerDAO(string filename)
        {
            _fileName = filename;
        }
        public static void AddPlayerdataToScoreboard(string result, string path)
        {
            DataMethods.Create(result, path);
        }
        public static List<PlayerData> GetTopList(string fileName)
        {
            StreamReader reader = new(fileName);
            List<PlayerData> playerList = [];

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { Seperator }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                PlayerData playerData = new(name, guesses);
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

        #region
        //Return list of player data from textfile
        public List<PlayerData> GetPlayerDatas()
        {
            return GetTopList(_fileName);
        }

        //Should save player data to textfile
        public void Save(string name, int totalGuesses)
        {
            AddPlayerdataToScoreboard(totalGuesses.ToString(), _fileName);
        }

        #endregion



    }
}
