using Games.Ui;
using System.IO.Enumeration;

namespace Games
{
    public class PlayerDAO(Player player, string filename) : IPlayerDAO
    {
        private string _pathToSavedData = filename;
        private string PathToSavedData
        {
            get
            {
                return _pathToSavedData;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Data must have a source path.");
                else
                    _pathToSavedData = value;
            }
        }

        private Player _player = player;
        public Player PlayerData
        {
            get
            {
                return _player;
            }
            set
            {
                if (value.TotalGuesses == 0 || string.IsNullOrEmpty(value.Name))
                    throw new Exception("Player cannot be empty");
                else
                {
                    _player = value;
                }
            }
        }
        public void SavePlayerData()
        {
            DataMethods.AddData(ConvertPlayerDataToString(), PathToSavedData);
        }
        public string ConvertPlayerDataToString()
        {
            return $"{PlayerData.Name}#&#{PlayerData.CalculatePlayerAverageScore()}";
        }

        public List<Player> GetPlayerData()
        {
            return DataMethods.GetPlayerData(PathToSavedData);
        }
        public void ShowTopList()
        {
            UserInterface userInterface = new();
            List<Player> results = GetPlayerData();
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            userInterface.WriteOutput("Player       games average");
            foreach (Player player in results)
            {
                userInterface.WriteOutput(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfRoundsPlayed, player.CalculatePlayerAverageScore()));
            }
        }
    }
}