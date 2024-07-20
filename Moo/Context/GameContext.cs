using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
using System.Runtime.CompilerServices;

namespace Moo.Context
{
    public class GameContext
    {
        public IGame Game;
        private readonly UI UI = new();
        private readonly List<IGame> Games = [];

        public PlayerDAO PlayerDAO { get; set; }
        public GameContext() { }
        public void SetGame(IGame game)
        {
            this.Game = game;
        }

        public void Run()
        {
            ChooseGame();
            while (Game.IsPlaying)
            {
                UI.Clear();
                Game.Display();
            }
            UI.Exit();
        }

        //TODO: Fix a more effectiva and durable solution to add games.
        public void ChooseGame()
        {
            Games.AddRange(
            [
                new MooGame(),
                new MasterMind()
            ]);

            UI.WriteOutput("Menu of games");
            foreach (var game in Games)
            {
                UI.WriteOutput($"{Games.IndexOf(game) + 1}) {game.ToString().Substring(10)}");
            }
            string input = string.Empty;

            while (!input.Any(char.IsDigit) || Game == null)
            {
                input = UI.HandleInput();
                foreach (var game in Games)
                {
                    if ((Games.IndexOf(game) + 1).ToString() == input)
                    {
                        SetGame(game);
                    }
                }
                UI.WriteOutput("Please enter a valid number.");
            }
        }
        public void ShowTopList(IUI ui)
        {
            StreamReader input = new StreamReader("result.txt");
            input.Close();

            List<PlayerData> results = PlayerDAO.GetTopList("result.txt");
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            ui.WriteOutput("Player       games average");

            foreach (PlayerData player in results)
            {
                ui.WriteOutput(string.Format("{0,-9}{1,5:D}{2,9:F2}", player.Name, player.NumberOfGamesPlayed, player.CalculatePlayerAverageScore()));
            }
            input.Close();
        }
    }
}
