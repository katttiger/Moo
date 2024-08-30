using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class UserInterfaceTests
    {

        UserInterface mockUI = new();

        //[TestMethod()]
        //public void ExitTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void ClearTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void HandleInputTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void WriteOutputTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public void ParseStringToIntTest()
        {
            string mockMessage = "ABCD";
            var value = mockUI.ParseStringToInt(mockMessage);
            Assert.IsNotNull(value);
        }

    }

}