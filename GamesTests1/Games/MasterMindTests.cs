using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Games.Tests
{
    [TestClass()]
    public class MasterMindTests
    {
        readonly string mockGoal = MockMastermind.CreateGoal();

        [TestMethod()]
        public void CreateGoalTest()
        {
            Assert.IsFalse(mockGoal.Equals(string.Empty));
        }
    }
}

class MockMastermind : MasterMind
{ }