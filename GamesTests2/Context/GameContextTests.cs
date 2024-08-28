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

    class MockGameContext(IUserInterface userInterface)
    {
        public IGame Game;
        private readonly IUserInterface Ui;
        public readonly List<IGame> ListOfGames = [];
        public bool gameHasBeenSet = false;

        public void AddGameToList()
        {
            ListOfGames.AddRange(
            [
                new MooGame(),
                new MastermindGame()
            ]);
        }

        public void SetGame(IGame game)
        {

            Game = game;
            gameHasBeenSet = true;
        }

        public void Run()
        {
            while (Game.isPlaying)
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

    class MockUI : IUserInterface
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