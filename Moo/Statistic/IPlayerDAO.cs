using Moo.Players;

namespace Moo.Statistic
{
    public interface IPlayerDAO
    {
        //Something that can read playerdata
        static List<IPLayer> PlayerList { get; set; }
        void Save(string name, int totalGuesses);
        List<PlayerData> GetPlayerDatas();

    }
}
