using Moo.Players;

namespace Moo.Statistic
{
    public interface IPlayerDAO
    {
        //Something that can read playerdata
        StreamWriter DataWriter { get; set; }
        StreamReader DataReader { get; set; }
        List<IPLayer> PlayerList { get; set; }
    }
}
