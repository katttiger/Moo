using Games;
using System.Diagnostics.CodeAnalysis;

namespace GamesTests2
{
    [TestClass()]
    public class APIEndpointsTests
    {
        [TestMethod()]
        public void DataHasPathToFileTest()
        {
            string pathToFile = "result.txt";
            System.IO.File.Delete(pathToFile);
            string data = "bogusPlayer";

            APIEndpoints.AddData(data, pathToFile);

            Assert.IsFalse(string.IsNullOrEmpty(pathToFile));
        }

        [TestMethod()]
        public void AddedDataContainsSeperatorTest()
        {
            string pathToFile = "result.txt";
            System.IO.File.Delete(pathToFile);

            string data = "bogusPlayer#&#8";

            APIEndpoints.AddData(data, pathToFile);

            Assert.IsTrue(data.Contains("#&#"));
        }

        [TestMethod()]
        public void ListReturnedEqualsDataStoredTest()
        {
            var pathtofile = "nameOfFile.txt";
            System.IO.File.Delete(pathtofile);

            File.WriteAllText(pathtofile, @"player+#&#3
player2#&#3
PlayerX#&#5
playerX#&#1");

            //Note: Before running, add these inputs to the file that contains the results from the game.
            //Otherwise the test will not work.
            var expectedPlayers = new List<Player>();
            expectedPlayers.AddRange([
                new Player("player+",3),
                new Player("player2",3),
                new Player("PlayerX",5),
                new Player("playerX",1)]);

            var actualPlayers = APIEndpoints.GetPlayerdataFromFile(pathtofile);

            Assert.IsTrue(actualPlayers.SequenceEqual(expectedPlayers, new PlayerEqualityComparer()));
        }
    }

    //Note: Without this method, TestMethod ListReturnEqualDataStoredTest will not work.
    internal class PlayerEqualityComparer : IEqualityComparer<IPlayer>
    {
        public bool Equals(IPlayer? expectedPlayer, IPlayer? actualPlayer)
        {
            if (ReferenceEquals(expectedPlayer, actualPlayer))
            {
                return true;
            }
            if (expectedPlayer is null)
            {
                return false;
            }
            if (actualPlayer is null)
            {
                return false;
            }
            if (expectedPlayer.GetType() != actualPlayer.GetType())
            {
                return false;
            }
            return expectedPlayer.Name == actualPlayer.Name
                && expectedPlayer.TotalGuesses == actualPlayer.TotalGuesses;
        }

        public int GetHashCode([DisallowNull] IPlayer obj)
        {
            throw new NotImplementedException();
        }
    }
}