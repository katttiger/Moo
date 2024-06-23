using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Statistic.PlayerDAO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Moo.Statistic.PlayerDAO.Tests
{
    [TestClass()]
    public class FakePlayerDAOTests
    {
        string _textFileContent = string.Empty;
        string seperator = "#&#";

        [TestInitialize]
        public void Initialize()
        {
        }

        [TestMethod()]
        public void FakePlayerDAOTest()
        {
        }

        [TestMethod()]
        public void GetPlayerDatasTest()
        {
        }

        [TestMethod()]
        public void SaveTest(string name, int totalGuesses)
        {
            Assert.IsNotNull(name);
            Assert.IsNotNull(totalGuesses);

            //_txtFileContent += name + Seperator + totalGuesses
            //+ Environment.NewLine;
            //Debug.WriteLine("Saved");
        }
    }
}