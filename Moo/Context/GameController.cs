using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moo.Games;
using Moo.Interfaces;


namespace Moo.Context
{
    public class GameController
    {
        public UI Ui;
        public IGame Game;

        public GameController(UI ui, IGame game)
        {
            Ui = ui;
            this.Game = game;
        }

        public void Run()
        {
            while (Game.IsPlaying)
            {
                Clear();
                Game.Display();
            }
            Exit();
        }

        void Exit()
        {
            Environment.Exit(0);
        }
        void Clear()
        {
            Console.Clear();
        }
    }

}
