using Games.Games;

namespace Games.Games.Tests
{
    [TestClass()]
    public class MasterMindTests
    {
        readonly string mockGoal = MockMastermind.CreateGoal();

        [TestMethod()]
        public void CreateGoalTest()
        {
            Assert.IsFalse(mockGoal.Equals(string.Empty));
        }

        [TestMethod()]
        public void CreateGoalTest1()
        {
            Assert.Fail();
        }
    }
}

class MockMastermind : MasterMind
{ }