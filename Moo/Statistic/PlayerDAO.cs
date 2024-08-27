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
        public string ConvertPlayerDataToString()
        {
            return $"{PlayerData.Name}#&#{PlayerData.CalculatePlayerAverageScore()}";
        }
        public void SavePlayerdataToGameScoreTable()
        {
            DataMethods.AddData(ConvertPlayerDataToString(), PathToSavedData);
        }
    }
}