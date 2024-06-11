using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Players;
using Moo.Statistic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic.Tests
{
    [TestClass()]
    public class ScoreTests
    {
        [TestInitialize]
        public void Inititialize()
        {
            //StreamReader streamReader = new StreamReader("result.txt");
            List<IPLayer> players = new List<IPLayer>();
            //Score score = new(streamReader, players);
        }

        [TestMethod()]
        public void GetTopListTest()
        {
            //StreamReader reader = new StreamReader("result.txt");
        }
    }

    class MockReader
    {
        StreamReader reader;
        public MockReader() { }
    }
}