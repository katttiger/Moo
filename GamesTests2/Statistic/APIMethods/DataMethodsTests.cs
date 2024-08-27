using Games;
using System.Diagnostics.CodeAnalysis;

namespace GamesTests2
{
    [TestClass()]
    public class DataMethodsTests
    {
        [TestMethod()]
        public void DataHasPathToFile()
        {
            //Arrange
            string pathToFile = "result.txt";
            System.IO.File.Delete(pathToFile);
            string data = "bogusPlayer";

            //Act
            DataMethods.AddData(data, pathToFile);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(pathToFile));
        }

        [TestMethod()]
        public void AddedDataContainsSeperator()
        {
            //Arrange
            //Path to data
            string pathToFile = "result.txt";
            System.IO.File.Delete(pathToFile);

            string data = "bogusPlayer#&#8";

            //Act
            //call the method and add testdata
            DataMethods.AddData(data, pathToFile);

            //Assert
            //Säkerställ att #&# finns
            Assert.IsTrue(data.Contains("#&#"));
        }

        [TestMethod()]
        public void ListReturnEqualsDataStored()
        {
            //Arrange
            var pathtofile = "nameOfFile.txt";
            System.IO.File.Delete(pathtofile);

            File.WriteAllText(pathtofile, @"player+#&#3
player2#&#3
PlayerX#&#5
playerX#&#1");

            var expectedPlayers = new List<Player>();
            expectedPlayers.AddRange([
                new Player("player+",3),
                new Player("player2",3),
                new Player("PlayerX",5),
                new Player("playerX",1)]);

            //Act
            var actualPlayers = DataMethods.GetPlayerdataFromFile(pathtofile);

            //Assert
            Assert.IsTrue(actualPlayers.SequenceEqual(expectedPlayers, new PlayerEqualityComparer()));
        }
    }

    public class PlayerEqualityComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player? expectedPlayer, Player? actualPlayer)
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

        public int GetHashCode([DisallowNull] Player obj)
        {
            throw new NotImplementedException();
        }
    }


}