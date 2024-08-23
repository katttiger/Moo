using Games.Ui;

namespace Games
{
    class Program
    {
        public static void Main()
        {
            UserInterface Ui = new();
            GameContext controller = new(Ui);

            //Uncomment to display menu and allow choice of games
            controller.PrintMenuOfGames();
            controller.ChooseGame();
            controller.RunGame();
        }
    }
}