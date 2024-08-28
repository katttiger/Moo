namespace Games
{
    public interface IPlayerDAO
    {
        IPlayer PlayerData { get; set; }
        void SavePlayerdataToGameScoreTable();
    }
}
