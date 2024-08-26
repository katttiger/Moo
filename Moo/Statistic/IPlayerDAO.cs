namespace Games
{
    public interface IPlayerDAO
    {
        Player PlayerData { get; set; }
        void SavePlayerdataToGameScoreTable();
    }
}
