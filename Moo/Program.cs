using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Moo.Context;
using Moo.Games;
using Moo.Interfaces;


namespace Program
{
    class Program
    {
        public static void Main(string[] args)
        {
            UI ui = new UI();
            //TODO: add a menu to chose game
            IGame game = new MooGame();
            GameController controller = new GameController(ui, game);
            controller.Run();
        }
    }
}