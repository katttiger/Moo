using Games.Games;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Moo.Games.Tests
{
    [TestClass()]
    public class MooGameTests
    {
        MooGame game = new();
        MockGuess mockGuess = new MockGuess();

        [TestMethod()]
        public void CreateGoalTest()
        {
            Assert.IsNotNull(MooGame.CreateGoal());
        }

        [TestMethod()]
        public void DisplayTest()
        {
            Assert.IsTrue(game.IsPlaying);
        }

        [TestMethod()]
        public void CheckIfGuessIsValidTest()
        {
        }
    }

    public class MockGuess
    {
        string guess = string.Empty;
    }

}