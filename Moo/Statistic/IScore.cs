using Moo.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic
{
    public interface IScore
    {
        //Something that can read a files
        StreamReader FileReader { get; set; }
        List<IPLayer> PlayerList { get; set; }

    }
}
