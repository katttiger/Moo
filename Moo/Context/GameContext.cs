using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Moo.Context
{
    public class GameContext
    {
        private IGame Game;
        private readonly IUI Ui;
        private readonly List<IGame> Games = [];

        public PlayerDAO PlayerDAO { get; set; }

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

        public void ChooseGame()
        {
            AddGameToList();
            Ui.WriteOutput("Menu of games");
            foreach (var game in Games)
            {
                Ui.WriteOutput($"{Games.IndexOf(game) + 1}) {game.ToString().AsSpan(10)}");
            }
            string input = string.Empty;

            while (!input.Any(char.IsDigit) || Game == null)
            {
                input = Ui.HandleInput();
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
