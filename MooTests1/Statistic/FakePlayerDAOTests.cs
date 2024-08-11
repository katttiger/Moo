using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moo.Players;
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
        readonly string seperator = "#&#";

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
        public void SaveTest()
        {
            //_txtfilecontent += name + seperator + totalguesses
            //+ environment.newline;
            //debug.writeline("saved");
        }
    }
    //used for testing
    public class FakePlayerDAO : IPlayerDAO
    {
        string _txtFileContent = string.Empty;
        public FakePlayerDAO(string initialFileContent)
        {
            _txtFileContent = initialFileContent;
        }

        public PlayerData PlayerData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //Return player data
        public List<PlayerData> GetPlayerDatas()
        {
            List<PlayerData> results = new List<PlayerData>();
            string line;
            string[] lines = _txtFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                Debug.WriteLine(lines[i]);
                line = lines[i];
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new(name, guesses);
                int pos = results.IndexOf(pd);
                if (pos < 0)
                {
                    results.Add(pd);
                }
                else
                {
                    results[pos].UpdatePlayerScore(guesses);
                }
            }
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            return results;
        }

        public List<PlayerData> GetPlayerDatas(string fileName)
        {
            throw new NotImplementedException();
        }

        //Save to memory
        public void Save(string name, int totalGuesses)
        {
            _txtFileContent += name + "#&#" + totalGuesses + Environment.NewLine;
            Debug.WriteLine("Saved");
        }

        public void Save(PlayerData playerdata)
        {
            throw new NotImplementedException();
        }
    }
}
