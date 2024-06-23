using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Games
{
    public class GameFactory
    {
        List<IGame> Games = new List<IGame>();
        public void ListGames()
        {
            Games = new List<IGame>()
            {
                //{ MooGame Moo },
                //{ MasterMind masterMind },
            };
        }


    }
}
