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
    public class GameControllerTests
    {
        [TestInitialize()]
        public void Intialize()
        {
        }

        [TestMethod()]
        public void GameControllerTest()
        {
            MockGameController mockGameController = new(new MooGame(), new UI());
            Assert.IsNotNull(mockGameController.UI);
            Assert.IsNotNull(mockGameController.Game);
        }
        [TestMethod()]
        public void RunTest()
        {
            MockUI mockUI = new MockUI();
            Assert.IsFalse(mockUI.Exit());
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

class MockUI
{
    public void Clear()
    {

    }

    public bool Exit()
    {
        return false;
    }

    public string HandleInput()
    {
        return "";
    }

    public void WriteOutput(string message)
    {
        message = "Hello world";
    }
}