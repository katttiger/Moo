using Games.Ui;

namespace Games
{
    public class PlayerDAO(Player player, string filename) : IPlayerDAO
    {
        private string PathToScore { get; set; } = filename;

        private Player _player = player;
        public Player PlayerData
        {
            get
            {
                return _player;
            }
            set
            {
                if (value == null || value.Name == string.Empty)
                    throw new Exception("Player cannot be empty");
                else
                {
                    _player = value;
                }
            }
        }
        public List<Player> GetPlayerData()
        {
            return DataMethods.GetData(PathToScore);
        }
        public void SavePlayerData()
        {
            DataMethods.AddData(ConvertPlayerDataToString(), PathToScore);
        }
        public string ConvertPlayerDataToString()
        {
            return $"{PlayerData.Name}#&#{PlayerData.TotalGuesses}";
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