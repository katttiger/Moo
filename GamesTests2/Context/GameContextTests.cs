using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.Context;
using Games.Games;
using Games.UI;
using Games;

namespace GamesTests2
{
    [TestClass()]
    public class GameContextTests
    {
        readonly MockGameContext mockGameContext;
        private readonly MockUI mockUI = new();
        GameContext gameContext;
        readonly IGame? MockIGame = new MooGame();
        List<IGame> games = new List<IGame>();


        [TestInitialize()]
        public void Initialize()
        {
            gameContext.AddGameToList();
        }

        [TestMethod()]
        public void PrintMenuOfGamesTest()
        {
            mockGameContext.AddGameToList();
            Assert.IsNotNull(gameContext.GamesList);
            //Assert.IsTrue(mockGameContext.ListOfGames.Count > 0);
        }

        [TestMethod()]
        public void ChooseGameTest()
        {
            mockGameContext.AddGameToList();
            Assert.IsTrue(mockGameContext.ListOfGames.Count > 0);
        }

        [TestMethod()]
        public void SetGameTest()
        {
            Assert.IsFalse(mockGameContext.gameHasBeenSet);
        }

        [TestMethod()]
        public void RunGameTest()
        {
            Assert.IsFalse(mockUI.ExitTest());
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