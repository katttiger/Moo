using Games.Ui;

namespace Games
{
    public class PlayerDAO(Player player, string filename, string game) : IPlayerDAO
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

        private string Game = game;

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
        public void SavePlayerdataToGameScoreTable()
        {
            DataMethods.AddData(ConvertPlayerDataToString(), PathToSavedData);
        }
        public string ConvertPlayerDataToString()
        {
            return $"{PlayerData.Name}#&#{PlayerData.CalculatePlayerAverageScore()}";
        }
    }
}