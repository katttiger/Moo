using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Context;
using Moo.Games;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Context.Tests
{
    [TestClass()]
    public class GameContextTests
    {
        readonly MockGameController? mockGameController;
        readonly GameContext? gameContext;
        MockUI? mockUI = new();
        readonly IGame? game;

        [TestInitialize()]
        public void Intialize()
        {
            mockUI = new MockUI();
            GameContext gameContext;
        }

        [TestMethod()]
        public void RunTest()
        {
            Assert.IsFalse(mockUI.ExitTest());
        }

        [TestMethod()]
        public void ChooseGameTest()
        {
        }
    }


}

class MockGameController
{
    private IGame Game;
    private readonly IUI Ui;
    private readonly List<IGame> Games = [];
    bool gameHasBeenSet = false;
    public MockGameController(IUI ui)
    {
        this.Ui = ui;
    }

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
        gameHasBeenSet = true;
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

    public void ChooseGame()
    {
        while (!gameHasBeenSet)
        {
            string input = Ui.HandleInput();
            if (!input.Any(char.IsLetter))
            {
                foreach (var game in Games)
                {
                    if ((Games.IndexOf(game) + 1).ToString() == input)
                    {
                        SetGame(game);
                    }
                }
                Ui.WriteOutput("Please enter a valid number.");
            }
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

}