using Moo.Players;

namespace Moo.Statistic
{
    public interface IPlayerDAO
    {
        //Something that can read playerdata
        static StreamWriter PlayerDataWriter { get; set; }
        static StreamReader PlayerDataReader { get; set; }
        static List<IPLayer> PlayerList { get; set; }
    }
}
