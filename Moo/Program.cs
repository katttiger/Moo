using Moo.Context;
using Moo.Interfaces;

namespace Program
{
    class Program
    {
        public static void Main()
        {
            UI Ui = new();
            GameContext controller = new(Ui);
            controller.PrintMenuOfGames();
            controller.ChooseGame();
            controller.Run();
        }
    }
}