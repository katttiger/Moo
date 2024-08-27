using Games;
using Games.Statistic;
using Games.Ui;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class GameContextTests
    {
        private MockGameContext mockGameContext;
        private GameContext gameContext;

        [TestInitialize()]
        public void Initialize()
        {
            UserInterface ui = new();

            GameContext gameCtxt = new(ui);
            gameContext = gameCtxt;

            MockGameContext mockContxt = new(ui);
            mockGameContext = mockContxt;

            gameContext.AddGameToList();
        }

        [TestMethod()]
        public void PrintMenuOfGamesTest()
        {
            mockGameContext.AddGameToList();
            Assert.IsNotNull(mockGameContext.ListOfGames);
        }

        [TestMethod()]
        public void ChooseGameTest()
        {
            gameContext.AddGameToList();
            Assert.IsTrue(gameContext.GamesList.Count > 0);
        }

        [TestMethod()]
        public void SetGameTest()
        {
            Assert.IsFalse(mockGameContext.gameHasBeenSet);
        }

        [TestMethod()]
        public void RunGameTest()
        {
            Assert.IsFalse(MockUI.ExitTest());
        }

        [TestMethod()]
        public void RunGameHasBeenSetTest()
        {
            IGame game = new MooGame();
            mockGameContext.SetGame(game);
            Assert.IsTrue(mockGameContext.gameHasBeenSet);
        }

        [TestMethod()]
        public void PresentScoreTest()
        {
            MooGame game = new();
            mockGameContext.SetGame(game);
            Assert.IsFalse(string.IsNullOrEmpty(mockGameContext.Game.PathToScore));
        }

    }

    class MockGameContext(IUI userInterface)
    {
        public IGame Game;
        private readonly IUI Ui;
        public readonly List<IGame> ListOfGames = [];
        public bool gameHasBeenSet = false;

        public void AddGameToList()
        {
            ListOfGames.AddRange(
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
                userInterface.Clear();
                Game.Display();
            }

            if (string.IsNullOrEmpty(Game.PathToScore))
            {
                throw new Exception("List of scores cannot be found.");
            }
            else
            {
                //Show toplist
                PlayerscorePresenter.ShowTopListForGame(Game.PathToScore);
            }

            userInterface.Exit();
        }

        public void PrintMenuOfGames()
        {
            AddGameToList();
            if (ListOfGames.Count > 0)
            {

                Ui.WriteOutput("Menu of games:");

                foreach (var game in ListOfGames)
                {
                    Ui.WriteOutput($"{ListOfGames.IndexOf(game) + 1}) {game.ToString().AsSpan(10)}");
                }
            }
            else
            {
                throw new Exception("Message");
            }
        }

        public void ChooseGame(int mockInput)
        {
            while (!gameHasBeenSet)
            {
                foreach (var game in ListOfGames)
                {
                    if (ListOfGames.IndexOf(game) + 1 == mockInput)
                    {
                        SetGame(game);
                        gameHasBeenSet = true;
                    }
                }
                Ui.WriteOutput("Please enter a valid number.");
            }
        }
    }

    class MockUI : IUI
    {
        public void Clear()
        {
            Console.Clear();
        }
        public static bool ExitTest()
        {
            return false;
        }
        public void Exit()
        { }

        public string HandleInput()
        {
            return "";
        }

        public void WriteOutput(string message)
        {
            message = "Hello world";
        }

        public int ParseStringToInt(string message)
        {
            throw new NotImplementedException();
        }
    }
}