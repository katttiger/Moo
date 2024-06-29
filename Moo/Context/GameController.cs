using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
using System.Runtime.CompilerServices;

namespace Moo.Context
{
    public class GameController
    {
        public List<IGame> Games = new List<IGame>();
        public IUI Ui { get; set; }
        public IGame Game { get; set; }
        public PlayerDAO PlayerDAO { get; set; }
        public GameController() { }

        public Creator creator;

        public void Run()
        {
            Creator gameId = ChooseGame();
            Game = gameId.ActivateGameFactory();
            UI ui = new UI();
            while (Game.IsPlaying)
            {
                ui.Clear();
                Game.Display();
            }
            ui.Exit();
        }

        //TODO Fix a more effective solution to add games to the system
        public Creator ChooseGame()
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
                ui.WriteOutput($"{Games.IndexOf(game) + 1} {game.ToString().Substring(10)}");
            }
            ui.WriteOutput("Press n to exit");
            string input = string.Empty;

            //TODO: Improve to facilitate addition of games.
            while (input != null)
            {
                input = ui.HandleInput();
                if (input == "1")
                {
                    return new MooGameCreator();
                }
                else if (input == "2")
                {
                    return new MasterMindCreator();
                }
                else if (input == "n")
                {
                    ui.Exit();
                }
                else
                {
                    ui.WriteOutput("Please enter a given digit.");
                }
            }
            return new MooGameCreator();
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
