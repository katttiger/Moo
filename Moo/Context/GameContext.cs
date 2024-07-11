using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
using System.Runtime.CompilerServices;

namespace Moo.Context
{
    public class GameContext
    {
        public List<IGame> Games = new List<IGame>();
        public IUI Ui;

        public IGame Game;
        public PlayerDAO PlayerDAO { get; set; }
        public GameContext() { }
        public void SetGame(IGame game)
        {
            this.Game = game;
        }

        public void Run()
        {
            UI ui = new UI();
            ChooseGame();
            while (Game.IsPlaying)
            {
                ui.Clear();
                Game.Display();
            }
            ui.Exit();
        }

        //TODO: Fix a more effectiva and durable solution to add games.
        public void ChooseGame()
        {
            Games.AddRange(new List<IGame>
            {
                new MooGame(),
                new MasterMind()
            }
            );

            UI ui = new UI();
            ui.WriteOutput("Menu of games");
            foreach (var game in Games)
            {
                ui.WriteOutput($"{Games.IndexOf(game) + 1}) {game.ToString().Substring(10)}");
            }
            string input = string.Empty;

            //TODO: Improve conditional to facilitate addition of games.
            while (!input.Any(char.IsDigit))
            {
                input = ui.HandleInput();
                if (input == "1")
                {
                    SetGame(new MooGame());
                }
                else if (input == "2")
                {
                    SetGame(new MasterMind());
                }
                else
                {
                    ui.WriteOutput("Please enter a given digit.");
                }
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
