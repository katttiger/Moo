using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Players;
using Moo.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic.Tests
{
    [TestClass()]
    public class PlayerDAOTests
    {
        MockPlayerDAO mockPlayerDAO = new();
        [TestMethod()]
        public void AddDataToScoreboardTest()
        {
            var testdirectory = "path";
            mockPlayerDAO.DataReader = new(testdirectory);
            //Values are null
            Assert.IsNotNull(mockPlayerDAO.DataReader);
            Assert.IsNotNull(mockPlayerDAO.DataWriter);
        }
    }
    public class MockPlayerDAO
    {
        public StreamReader DataReader { get; set; }
        public StreamWriter DataWriter { get; set; }
        public List<IPLayer> PlayerList { get; set; }
    }

}

