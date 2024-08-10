using Moo.Players;

namespace Moo.Statistic
{
    public interface IPlayerDAO
    {
        PlayerData PlayerData { get; set; }
        static List<IPLayer> PlayerList { get; set; }
        void Save(PlayerData playerdata);
        List<PlayerData> GetPlayerDatas(string fileName);
    }
}
