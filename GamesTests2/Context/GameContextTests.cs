using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.Context;
using Games.Games;
using Games.UI;

namespace Games.Context.Tests
{
    [TestClass()]
    public class GameContextTests
    {
        readonly MockGameContext? mockGameContext;
        MockUI? mockUI = new();
        readonly IGame? MockIGame = new MooGame();

        [TestMethod()]
        public void RunTest()
        {
            Assert.IsFalse(mockUI.ExitTest());
        }
    }
}

class MockGameContext(IUI ui)
{
    public IGame Game;
    private readonly IUI Ui = ui;
    private readonly List<IGame> Games = [];
    public bool gameHasBeenSet = false;

    public void AddGameToList()
    {
        Games.AddRange(
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
        Ui.WriteOutput("Menu of games:");

        foreach (var game in Games)
        {
            Ui.WriteOutput($"{Games.IndexOf(game) + 1}) {game.ToString().AsSpan(10)}");
        }
    }

    public void ChooseGame(int mockInput)
    {
        while (!gameHasBeenSet)
        {
            foreach (var game in Games)
            {
                if (Games.IndexOf(game) + 1 == mockInput)
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