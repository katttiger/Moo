namespace Games
{
    public interface IPlayerDAO
    {
        Player PlayerData { get; set; }
        void SavePlayerData();
        List<Player> GetPlayerData();
    }
}
