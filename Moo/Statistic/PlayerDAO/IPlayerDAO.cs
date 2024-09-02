namespace Games.Statistic.PlayerDAO
{
    public interface IPlayerDAO
    {
        IPlayer PlayerData { get; set; }
        void SavePlayerdataToGameScoreTable();
    }
}
