using Moo.Context;
using Moo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Games
{
    public class MasterMind : IGame
    {
        public MasterMind()
        {
            
        }
        public bool IsPlaying { get; set; } = true;

        public string PathToScore { get; set; }

        GameController gameController = new GameController();
        UI Ui = new UI();
        public void Display()
        {
            Ui.WriteOutput("You have choosen mastermind.");
            Ui.Exit();
        }
    }
}
