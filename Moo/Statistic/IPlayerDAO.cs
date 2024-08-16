using Games.Player;

namespace Games.Player
{
    public interface IPlayerDAO
    {
        PlayerData PlayerNameAndScore { get; set; }
        static List<IPLayer> PlayerList { get; set; }
        void Save(PlayerData playerdata);
        List<PlayerData> GetPlayerDatas(string fileName);
    }
}
