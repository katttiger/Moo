using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
using System.Runtime.CompilerServices;

namespace Moo.Context
{
    public class GameController
    {
        public IUI Ui { get; set; }
        public IGame Game { get; set; }
        public PlayerDAO PlayerDAO { get; set; }
        public GameController() { }

        public void Run()
        {
            ChooseGame();
            UI ui = new UI();
            while (Game.IsPlaying)
            {
                ui.Clear();
                Game.Display();
            }
            ui.Exit();
        }

        //TODO Fix a more effective solution to add games to the system
        public void ChooseGame()
        {
            string choseGameMenu = "Menu of games"
                + "\n 1) Moo"
                + "\n 2) MasterMind (number edition)"
                + "\n Press n to exit";
            UI ui = new UI();
            string input = string.Empty;
            ui.WriteOutput(choseGameMenu);
            while (input != null)
            {
                input = ui.HandleInput();
                if (input == "1")
                {
                    Game = new MooGame();
                    break;
                }
                else if (input == "2")
                {
                    Game = new MasterMind();
                    break;
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
