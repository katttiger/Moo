using Moo.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Statistic
{
    public class DataMethods : IDataMethods
    {

        public static void Create(string data, string pathToData)
        {

            StreamWriter writer = new(pathToData, append: true);
            writer.Write(data + writer.NewLine);
            writer.Close();
        }
        public static List<PlayerData> Read(string pathToData)
        {
            throw new NotImplementedException();
        }
        public static void Update(string data, string pathToData)
        {
            throw new NotImplementedException();

        }
        public static void Delete(string data, string pathToData)
        {
            throw new NotImplementedException();
        }

    }
}
