using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Context;
using Moo.Games;
using Moo.Interfaces;
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
        MockUI mockUI = new();
        IGame game;

        [TestInitialize()]
        public void Intialize()
        {
            mockUI = new MockUI();
        }

        [TestMethod()]
        public void GameControllerTest()
        {
            MockGameController mockGameController = new(game, mockUI);
            Assert.IsNotNull(mockGameController.UI);
            Assert.IsNotNull(mockGameController.Game);
        }
        [TestMethod()]
        public void RunTest()
        {
            Assert.IsFalse(mockUI.ExitTest());
        }

        [TestMethod()]
        public void RunGameTest()
        {

        }
    }
}

class MockGameController
{
    public IGame Game { get; set; }
    public IUI UI { get; set; }
    public MockGameController(IGame game, IUI ui)
    {
        Game = game;
        UI = ui;
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