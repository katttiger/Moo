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
        MockGameController mockGameController;
        GameContext gameContext;
        MockUI mockUI = new();
        IGame game;

        [TestInitialize()]
        public void Intialize()
        {
            mockUI = new MockUI();
            GameContext gameContext = new GameContext();
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

class MockGameController : GameContext
{
    public IGame Game;
    private readonly UI UI = new();
    private readonly List<IGame> Games = [];
    public PlayerDAO PlayerDAO = new("mockResults");

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