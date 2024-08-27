using Games;

namespace GamesTests2
{
    [TestClass()]
    public class PlayerDAOTests
    {
        [TestMethod()]
        public void PlayerIsNotNullOrEmptyTest()
        {
            var player = new Player("John Doe", 1);

            Exception? actualException = null;
            try
            {
                player.Name = string.Empty;
            }
            catch (Exception exception)
            {
                actualException = exception;
            }
            Assert.IsNotNull(actualException);
        }

        [TestMethod()]
        public void PlayerToBeConvertedIsNotNullTest()
        {
            var player = new Player("John Doe", 1);
            Assert.IsTrue(player != null);
        }
        [TestMethod()]
        public void PlayerToBeConvertedHasANameTest()
        {
            var player = new Player("John Doe", 1);
            Assert.IsTrue(player.Name != string.Empty);
        }

        [TestMethod()]
        public void PlayerToBeConvertedHasScoredMoreThanOnePointTest()
        {
            var player = new Player("John Doe", 3);
            Assert.IsTrue(player.TotalGuesses > 0);
        }
    }
}