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
            StreamReader streamreader = new StreamReader("result.txt");
            List<IPLayer> playerList = new List<IPLayer>();
            Score score = new(streamreader, playerList);

        }


        [TestMethod()]
        public void GetTopListTest()
        {

        }
    }
}