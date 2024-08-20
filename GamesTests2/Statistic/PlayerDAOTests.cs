using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;

namespace GamesTests2
{
    [TestClass()]
    public class PlayerDAOTests
    {
        string filepathForTesting = "test.txt";

        [TestMethod()]
        public void PlayerIsNotNullOrEmptyTest()
        {
            var player = new Player("John Doe", 1);
            var playerDao = new PlayerDAO(player, filepathForTesting);

            Exception actualException = null;
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
        public void ConvertPlayerDataToStringTest()
        {
            var player = new Player("John Doe", 1);
            Assert.IsTrue(player != null);
        }
        [TestMethod()]
        public void ConvertPlayerDataToStringTest1()
        {
            var player = new Player("John Doe", 1);
            Assert.IsTrue(player.Name != string.Empty);
        }

        [TestMethod()]
        public void ConvertPlayerDataToStringTest2()
        {
            var player = new Player("John Doe", 3);
            Assert.IsTrue(player.TotalGuesses > 0);
        }


        [TestMethod()]
        public void GetPlayerDataTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ShowTopListTest()
        {
            Assert.Fail();
        }

    }
}