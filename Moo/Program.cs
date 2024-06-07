using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Moo;


namespace MooGame
{
    class Program
    {
        public static void Main(string[] args)
        {
            UI ui=new UI();
            Game game=new Game();
            GameController controller = new GameController(ui, game);
            controller.Run();
        }
    }
}