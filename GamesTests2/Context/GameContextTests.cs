using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.Statistic;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class GameContextTests
    {
        readonly MockGameContext mockGameContext = new(new UserInterface());

        [TestMethod()]
        public void RunGameTest()
        {
            Assert.IsFalse(MockGameContextUI.ExitTest());
        }

        [TestMethod()]
        public void GameContextTest()
        {
            Assert.IsNotNull(mockGameContext.UserInterface);
        }

        [TestMethod()]
        public void PresentScoreTest()
        {
            MooGame game = new(new UserInterface());
            Assert.IsFalse(string.IsNullOrEmpty(game.PathToScore));
        }
    }

    internal class MockGameContext
    {
        public IGame Game;
        public IUserInterface UserInterface { get; set; }
        public readonly GameLobby gamelobby = new(new UserInterface());
        public MockGameContext(IUserInterface userinterface)
        {
            this.UserInterface = userinterface;
        }
        public void Run()
        {
            gamelobby.PrintMenuOfGames();
            Game = gamelobby.ChooseGame();

            while (Game.IsPlaying)
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

    internal class MockGameContextUI : IUserInterface
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