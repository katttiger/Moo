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
        public static void Main()
        {
            GameContext controller = new();
            controller.Run();
        }
    }
}