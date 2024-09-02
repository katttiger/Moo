namespace Games.Statistic.PlayerDAO
{
    public class PlayerDAO : IPlayerDAO
    {
        private string _pathToSavedData;
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

        private IPlayer _player;
        public IPlayer PlayerData
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
        public PlayerDAO(IPlayer player, string filename)
        {
            _player = player;
            _pathToSavedData = filename;
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