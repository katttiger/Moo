using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Games
{
    public abstract class Creator
    {
        public abstract IGame FactoryMethod();
        public IGame ActivateGameFactory()
        {
            return FactoryMethod();
        }
    }
    class MooGameCreator : Creator
    {
        public override IGame FactoryMethod()
        {
            return new MooGame();
        }
    }
    class MasterMindCreator : Creator
    {
        public override IGame FactoryMethod()
        {
            return new MasterMind();
        }
    }
}
