﻿using Games.UI;

namespace Games
{
    class Program
    {
        public static void Main()
        {
            UserInterface Ui = new();
            GameContext controller = new(Ui);
            controller.Run();
        }
    }
}