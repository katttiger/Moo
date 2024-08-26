using Games.Ui;

namespace Games
{
    class Program
    {
        public static void Main()
        {
            UserInterface Ui = new();
            GameContext controller = new(Ui);

            controller.PrintMenuOfGames();
            controller.ChooseGame();

            controller.Run();
        }
    }
}