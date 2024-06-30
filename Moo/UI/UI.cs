using Moo.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Interfaces
{
    public class UI : IUI
    {
        public void Exit()
        {
            Environment.Exit(0);
        }
        public void Clear()
        {
            Console.Clear();
        }
        public string HandleInput()
        {
            //Can be made prettier
            return Console.ReadLine() ?? "";
        }
        public void WriteOutput(string message)
        {
            Console.WriteLine(message);
        }
        public void WriteArray(string message)
        {
            Console.Write(message);
        }
    }
}
