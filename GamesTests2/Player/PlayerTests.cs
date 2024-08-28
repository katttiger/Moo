using Games;

namespace GamesTests2
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void CalculatePlayerAverageScoreTest()
        {
            var player = new Player("", 1);
            var result = player.TotalGuesses / player.NumberOfRoundsPlayed;
            Assert.IsTrue(result > 0);
        }

        [TestMethod()]
        public void CannotHaveNegativeTotalGuessesTest()
        {
            var player = new Player("John Doe", 6);
            Exception? actualException = null;
            try
            {
                player.TotalGuesses = -10;
            }
            catch (Exception exception)
            {
                actualException = exception;
            }
            Assert.IsNotNull(actualException);
        }

        [TestMethod()]
        public void UpdatePlayerStatusIsNotNegativeTest()
        {
            var player = new Player("John Doe", 1);
            Assert.IsTrue(player.TotalGuesses > 0);
        }

        [TestMethod()]
        public void PlayerHasNotPlayedZeroRounds()
        {
            var player = new Player("John Doe", 1);
            Assert.IsTrue(player.NumberOfRoundsPlayed > 0);
        }
    }
}