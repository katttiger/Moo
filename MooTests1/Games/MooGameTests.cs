using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Games;
using Moo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Games.Tests
{
    [TestClass()]
    public class MooGameTests
    {
        [TestInitialize()]
        public void Initialize()
        {
            new MooGame();
        }

        [TestMethod()]
        public void CreateGoalTest()
        {
            MooGame game = new MooGame();
            Assert.IsTrue(game.IsPlaying);
            Assert.IsNotNull(MooGame.CreateGoal());
        }

        [TestMethod()]
        public void CheckBullsAndCowsTest()
        {
            MooGame game = new MooGame();
            
        }

        [TestMethod()]
        public void DisplayTest()
        {
            MooGame game = new MooGame();
            Assert.IsTrue(game.IsPlaying);
        }
    }
}