using Games.Player;

namespace Games.Statistic
{
    public interface IPlayerDAO
    {
        PlayerData PlayerData { get; set; }
        void SavePlayerData();
        List<PlayerData> GetPlayerData();
    }
}
