using Games.Games;
using Games.Ui;
using Games.UI;
using System.Diagnostics;

namespace Games.Context
{
    public class GameContext(IUI userInterface)
    {
        private IGame? Game;
        private readonly IUI userInterface = userInterface;
        private readonly List<IGame> GamesList = [];
        private bool gameHasBeenSet = false;
        public void AddGameToList()
        {
            GamesList.AddRange(
            [
                new MooGame(),
                new MasterMind()
            ]);
        }
        public void Run()
        {
            Game = new MooGame();
            while (Game.IsPlaying)
            {
                userInterface.Clear();
                Game.Display();
            }
            userInterface.Exit();
        }
        public void PrintMenuOfGames()
        {
            AddGameToList();
            userInterface.WriteOutput("Menu of games:");

            foreach (var game in GamesList)
            {
                userInterface.WriteOutput($"{GamesList.IndexOf(game) + 1}) {game.ToString().AsSpan(12)}");
            }
        }
        public void ChooseGame()
        {
            while (!gameHasBeenSet)
            {
                int input = userInterface.ParseStringToInt(userInterface.HandleInput());
                foreach (var game in GamesList)
                {
                    if (GamesList.IndexOf(game) + 1 == input)
                    {
                        SetGame(game);
                    }
                }
                userInterface.WriteOutput("Please enter a valid number.");
            }
        }
        public void SetGame(IGame game)
        {
            Game = game;
            gameHasBeenSet = true;
        }

    }
}
