using Games.Games;
using Games.UI;

namespace Games.Context
{
    public class GameContext(IUI ui)
    {
        private IGame Game;
        private readonly IUI Ui = ui;
        private readonly List<IGame> Games = [];
        bool gameHasBeenSet = false;

        public void AddGameToList()
        {
            Games.AddRange(
            [
                new MooGame(),
                new MasterMind()
            ]);
        }

        public void SetGame(IGame game)
        {
            Game = game;
            gameHasBeenSet = true;
        }

        public void Run()
        {
            while (Game.IsPlaying)
            {
                Ui.Clear();
                Game.Display();
            }
            Ui.Exit();
        }

        public void PrintMenuOfGames()
        {
            AddGameToList();
            Ui.WriteOutput("Menu of games:");

            foreach (var game in Games)
            {
                Ui.WriteOutput($"{Games.IndexOf(game) + 1}) {game.ToString().AsSpan(12)}");
            }
        }

        public void ChooseGame()
        {
            while (!gameHasBeenSet)
            {
                int input = Ui.ParseStringToInt(Ui.HandleInput());
                foreach (var game in Games)
                {
                    if (Games.IndexOf(game) + 1 == input)
                    {
                        SetGame(game);
                    }
                }
                Ui.WriteOutput("Please enter a valid number.");
            }
        }
    }
}
