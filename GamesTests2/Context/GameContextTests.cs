using Games;
using Games.Statistic;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class GameContextTests
    {
        [TestMethod()]
        public void RunGameTest()
        {
            Assert.IsFalse(MockGameContextUI.ExitTest());
        }

        [TestMethod()]
        public void PresentScoreTest()
        {
            MooGame game = new(new UserInterface());
            Assert.IsFalse(string.IsNullOrEmpty(game.PathToScore));
        }
    }

    class MockGameContext
    {
        private IGame Game;
        private IUserInterface UserInterface { get; set; }
        private readonly GameLobby gamelobby = new(new UserInterface());
        public MockGameContext(IUserInterface userinterface)
        {
            this.UserInterface = userinterface;
        }
        public void Run()
        {
            gamelobby.PrintMenuOfGames();
            Game = gamelobby.ChooseGame();

            while (Game.isPlaying)
            {
                UserInterface.Clear();
                Game.Display();
            }

            if (string.IsNullOrEmpty(Game.PathToScore))
            {
                UserInterface.WriteOutput("List of scores cannot be found.");
            }
            else
            {
                PlayerscorePresenter.ShowTopListForGame(Game.PathToScore);
            }
            UserInterface.Exit();
        }

    }

    class MockGameLobby2
    {
        public List<IGame> GamesList;
        readonly IUserInterface userInterface;

        public MockGameLobby2(IUserInterface ui)
        {
            GamesList =
            [
                    new MooGame(ui),
                    new MastermindGame(ui),
            ];

            userInterface = ui;
        }

        public void PrintMenuOfGames()
        {
            if (GamesList.Count > 0)
            {
                userInterface.WriteOutput("Menu of games:");
                foreach (IGame game in GamesList)
                {
                    userInterface.WriteOutput($"{GamesList.IndexOf(game) + 1})" +
                        $" {game.ToString()[6..^4]}");
                }
            }
            else
            {
                userInterface.WriteOutput("No games are available. \nClosing application.");
                userInterface.Exit();
            }
        }
        public IGame ChooseGame()
        {
            IGame? selectedGame = null;
            while (selectedGame is null)
            {
                int input = userInterface.ParseStringToInt(userInterface.HandleInput());
                foreach (var game in GamesList)
                {
                    if (GamesList.IndexOf(game) + 1 == input)
                    {
                        selectedGame = game;
                    }
                }
                userInterface.WriteOutput("Please enter a valid number.");
            }
            return selectedGame;
        }
    }

    class MockGameContextUI : IUserInterface
    {
        public void Clear()
        {
            Console.Clear();
        }
        public static bool ExitTest()
        {
            return false;
        }
        public void Exit() { }

        public string HandleInput()
        {
            return "";
        }

        public void WriteOutput(string message)
        {
            //message = "Hello world";
        }

        public int ParseStringToInt(string message)
        {
            throw new NotImplementedException();
        }
    }
}