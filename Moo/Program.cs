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
            /*
              // The client code picks a concrete strategy and passes it to the
    //        // context. The client should be aware of the differences between
    //        // strategies in order to make the right choice.
    //        var context = new Context();

    //        Console.WriteLine("Client: Strategy is set to normal sorting.");
    //        context.SetStrategy(new ConcreteStrategyA());
    //        context.DoSomeBusinessLogic();

    //        Console.WriteLine();

    //        Console.WriteLine("Client: Strategy is set to reverse sorting.");
    //        context.SetStrategy(new ConcreteStrategyB());
    //        context.DoSomeBusinessLogic();*/

            //var context = new Context();
            //context.SetStrategy(new ConcreteStrategyB());
            //context.DisplayGame();

            //context.SetStrategy(new ConcreteStrategyA());
            GameContext controller = new GameContext();
            controller.Run();
        }
    }
}