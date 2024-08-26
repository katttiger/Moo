using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.Ui;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class GameContextTests
    {
        private readonly MockUI mockUI = new();
        private MockGameContext mockGameContext;
        private GameContext gameContext;


        [TestInitialize()]
        public void Initialize()
        {
            UserInterface ui = new();

            GameContext gameCtxt = new(ui);
            this.gameContext = gameCtxt;

            MockGameContext mockContxt = new(ui);
            this.mockGameContext = mockContxt;

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
        public void RunTest()
        {
            Assert.IsFalse(mockUI.ExitTest());
        }

        [TestMethod()]
        public void RunTest2()
        {
            Assert.Fail();
        }
    }
}

class MockGameContext(IUI ui)
{
    public IGame Game;
    private readonly IUI Ui = ui;
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
    public bool ExitTest()
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