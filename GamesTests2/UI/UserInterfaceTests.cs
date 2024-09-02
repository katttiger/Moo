using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class UserInterfaceTests
    {
        UserInterface mockUI = new();

        [TestMethod()]
        public void ParseStringToIntTest()
        {
            string mockMessage = "ABCD";
            var value = mockUI.ParseStringToInt(mockMessage);
            Assert.IsNotNull(value);
        }
    }
}