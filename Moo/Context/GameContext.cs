using Moo.Games;
using Moo.Interfaces;
using Moo.Statistic;

namespace Moo.Context
{
    public class GameContext
    {
        private IGame Game;
        private readonly IUI Ui;
        private readonly List<IGame> Games = [];
        bool gameHasBeenSet = false;
        public GameContext(IUI ui)
        {
            this.Ui = ui;
        }

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
            this.Game = game;
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
                Ui.WriteOutput($"{Games.IndexOf(game) + 1}) {game.ToString().AsSpan(10)}");
            }
        }

        public void ChooseGame()
        {
            while (!gameHasBeenSet)
            {
                string input = Ui.HandleInput();
                if (!input.Any(char.IsLetter))
                {
                    foreach (var game in Games)
                    {
                        if ((Games.IndexOf(game) + 1).ToString() == input)
                        {
                            SetGame(game);
                        }
                    }
                    Ui.WriteOutput("Please enter a valid number.");
                }
            }
        }
    }
}
