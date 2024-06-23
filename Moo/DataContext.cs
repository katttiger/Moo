using Moo.Players;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic
{
    /*
     Ett sätt skulle kunna vara att skicka in filePathen
    till konstruktorn för PlayerDAO, då kan du köra tester 
    mot en annan textfil som du behöver skapa upp i varje test.
    */

    public interface IDataContext
    {
        void Save(string name, int totalGuesses);
        List<PlayerData> GetPlayerDatas();
    }
    /// <summary>
    /// This class is responsible for saving and retrieving player data.
    /// </summary>
    public class DataContext : IDataContext
    {
        #region
        private string _fileName;
        private const string SEPARATOR = "#&#";
        public DataContext(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// WIP: Should return a list of player data from the textfile
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<PlayerData> GetPlayerDatas()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// WIP. Should save the player data to the textfile
        /// </summary>
        /// <param name="name"></param>
        /// <param name="totalGuesses"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Save(string name, int totalGuesses)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// This class mocks saving and retrieving player data.
        /// Used for testing purposes.
        /// </summary>
        public class FakeDataContext : IDataContext
        {
            string _txtFileContent;
            public FakeDataContext(string initialFileContent)
            {
                _txtFileContent = initialFileContent;
            }
            /// <summary>
            /// Returns
            /// </summary>
            /// <param name="playerData"></param>
            /// <returns></returns>
            public List<PlayerData> GetPlayerDatas()
            {
                List<PlayerData> results = new List<PlayerData>();
                string line;
                string[] lines = _txtFileContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i = 0; i < lines.Length; i++)
                {
                    Debug.WriteLine(lines[i]);
                    line = lines[i];
                    string[] nameAndScore = line.Split(new string[] { SEPARATOR }, StringSplitOptions.None);
                    string name = nameAndScore[0];
                    int guesses = Convert.ToInt32(nameAndScore[1]);
                    PlayerData pd = new PlayerData(name, guesses);
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

            /// <summary>
            /// Saves to memory
            /// </summary>
            /// <param name="name"></param>
            /// <param name="totalGuesses"></param>
            public void Save(string name, int totalGuesses)
            {
                _txtFileContent += name + SEPARATOR + totalGuesses + Environment.NewLine;
                Debug.WriteLine("Saved");
            }
        }
    }
}
