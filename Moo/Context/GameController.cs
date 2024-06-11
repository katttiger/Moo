using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;

namespace Moo.Context
{
    public class GameController
    {
        public IUI Ui { get; set; }
        public IGame Game;
        public GameController() { }
        public GameController(IUI ui, IGame game)
        {
            Ui = ui;
            this.Game = game;
        }

        public void Run()
        {
            while (Game.IsPlaying)
            {
                Ui.Clear();
                Game.DisplayMooGame();
                ShowTopList(this.Ui);
            }
            Ui.Exit();
        }

        public void ShowTopList(IUI Ui)
        {
            //Fetches input
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = Score.GetTopList();
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            //Error: Object reference not set to an instance of an object

            Ui.WriteOutput("Player   games average");
            foreach (PlayerData p in results)
            {
                Ui.WriteOutput(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGamesPlayed, p.CalculatePlayerAverageScore()));
            }
            input.Close();
        }
    }
}
