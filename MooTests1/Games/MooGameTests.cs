namespace Moo.Games.Tests
{
    [TestClass()]
    public class MooGameTests
    {
        MooGame game = new();

        [TestMethod()]
        public void CreateGoalTest()
        {
            Assert.IsTrue(game.IsPlaying);
            Assert.IsNotNull(MooGame.CreateGoal());
        }

        [TestMethod()]
        public void DisplayTest()
        {
            Assert.IsTrue(game.IsPlaying);
        }
    }
}