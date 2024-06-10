using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Games
{
    public interface IGame
    {
        public bool IsPlaying { get; }
        public void Display();
    }
}
