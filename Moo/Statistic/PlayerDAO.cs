using Games.Player;
using Games.Statistic.APIMethods;
using Games.Ui;

namespace Games.Statistic
{
    public class PlayerDAO(PlayerData player, string filename) : IPlayerDAO
    {
        private string PathToScore { get; set; } = filename;
        public PlayerData PlayerData { get; set; } = player;

        public List<PlayerData> GetPlayerData()
        {
            return DataMethods.Get(PathToScore);
        }
        public void SavePlayerData()
        {
            DataMethods.Add(ConvertPlayerDataToString(), PathToScore);
        }
        public string ConvertPlayerDataToString()
        {
            return $"{PlayerData.Name}#&#{PlayerData.TotalGuesses}";
        }
        public void ShowTopList()
        {
            UserInterface userInterface = new();
            List<PlayerData> results = GetPlayerData();
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            userInterface.WriteOutput("Player       games average");
            foreach (PlayerData player in results)
            {
                userInterface.WriteOutput(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGamesPlayed, player.CalculatePlayerAverageScore()));
            }
        }
    }
}