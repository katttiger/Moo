using Moo.Context;
using Moo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Moo.Games
{
    class MasterMind : IGame
    {
        public bool IsPlaying { get; set; } = true;

        public string PathToScore { get; set; }

        GameController gameController = new GameController();
        UI Ui = new UI(); 
        public static string CreateGoal()
        {
            Random randomGenerator = new Random();
            randomGenerator.Next(1,6);
            return "";

            //string goal = string.Empty;
            //for (int i = 0; i < 4; i++)
            //{
            //    string random = randomGenerator.Next(10).ToString();
            //    while (goal.Contains(random))
            //    {
            //        random = randomGenerator.Next(10).ToString();
            //    }
            //    goal += random;
            //}
            //return goal;
        }
        public static string CheckBullsAndCows(string goal, string guess)
        {
            throw new NotImplementedException();
        }
        public static string CheckIfGuessIsValid(string goal, string guess)
        {
            if (guess.Any(char.IsLetter))
            {
                return "Your guess must only contain numerical digits.";
            }
            else
            {
                if (guess.Length > 4)
                {
                    return "Your guess has more than 4 digits.";
                }
                else if (guess.Length < 4)
                {
                    return "Your guess has less than 4 digits.";
                }
                else
                {
                    return CheckBullsAndCows(goal, guess);
                }
            }
        }
        public void Display()
        {
            Ui.WriteOutput("You have choosen mastermind.");
            Ui.Exit();
        }

    }
}
