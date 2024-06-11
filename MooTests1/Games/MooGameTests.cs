namespace Moo.Games.Tests
{
    [TestClass()]
    public class MooGameTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            MooGame game = new MooGame();
        }

        [TestMethod()]
        public void CreateGoalTest()
        {
            MooGame game = new MooGame();
            Assert.IsTrue(game.IsPlaying);
            Assert.IsNotNull(MooGame.CreateGoal());
        }

        [TestMethod()]
        public void DisplayTest()
        {
            MooGame game = new MooGame();
            Assert.IsTrue(game.IsPlaying);
        }
    }
}