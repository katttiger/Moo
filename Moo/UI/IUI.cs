using Moo.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Interfaces
{
    public interface IUI
    {
        //Göra det vi vill göra mot konsolen
        //Input | Output
        string HandleInput();
        void WriteOutput(string message);
        void Exit();
        void Clear();
    }
}
