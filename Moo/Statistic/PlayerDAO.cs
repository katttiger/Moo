using Games.Statistic.APIMethods;
using Moo.Interfaces;
using Moo.Players;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Moo.Statistic
{
    //Responsible for saving and retrieving player data.
    //CRUD
    public struct PlayerDAO : IPlayerDAO
    {
        //PlayerData PlayerData;

        string PathToScore;
        private const string Seperator = "#&#";
        public PlayerData PlayerData { get; set; }

        public PlayerDAO(PlayerData player, string filename)
        {
            this.PlayerData = player;
            this.PathToScore = filename;
        }

        public List<PlayerData> GetPlayerDatas(string fileName)
        {
            return DataMethods.Get(fileName);
        }

        public void Save(PlayerData playerdata)
        {
            DataMethods.Add(PlayerData, PathToScore);
        }
    }
}
/*///Skicka in filePath till ctor för PlayerDAO.

////Gör att du kan köra tester mot en annan
////textfil som du behöver skapa upp i varje test

//private readonly string _fileName = string.Empty;
//private const string Seperator = "#&#";
//public PlayerDAO(string filename)
//{
//    _fileName = filename;
//}

//public static void AddPlayerdataToScoreboard(string result, string path)
//{
//    DataMethods.Create(result, path);
//}

////Return list of player data from textfile
//public readonly List<PlayerData> GetPlayerDatas()
//{
//    return GetTopList(_fileName);
//}

////Should save player data to textfile
//public void Save(string name, int totalGuesses)
//{
//    AddPlayerdataToScoreboard(totalGuesses.ToString(), _fileName);
//}*/