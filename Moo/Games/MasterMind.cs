using Moo.Context;
using Moo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moo.Games
{
    public class MasterMind : IGame
    {
        public bool IsPlaying { get; set; } = true;

        GameContext context = new GameContext();
        UI Ui = new UI();
        public static string CreateGoal()
        {
            Random randomGenerator = new Random();
            string goal = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                string random = randomGenerator.Next(1, 6).ToString();
                while (goal.Contains(random))
                {
                    random = randomGenerator.Next(6).ToString();
                }
                goal += random;
            }
            return goal;
        }
        public static string CheckGuess(string goal, string guess)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            stringBuilder.Append('X');
                        }
                        else
                        {
                            stringBuilder.Append('Y');

                        }
                    }
                }
            }
            return stringBuilder.ToString();

        }
        private static string CheckIfGuessIsValid(string goal, string guess)
        {
            if (guess.Any(char.IsLetter) || guess.Length != 6)
            {
                return "Your guess must only contain 6 numerical digits.";
            }
            else if (guess.Contains('7') || guess.Contains('8') || guess.Contains('9'))
            {
                return "You may only use numbers 0-6";
            }
            else
            {
                return CheckGuess(goal, guess);
            }
        }
        public void Display()
        {
            Ui.WriteOutput("Enter your user name:\n");
            string name = Ui.HandleInput() ?? "";

            Ui.WriteOutput("Any number may occur more than once." +
                "\n Values allowed: 0, 1, 2, 3, 4, 5." +
                "\n X = Right number and position" +
                "\n Y = Right number, wrong position \n");
            Ui.WriteOutput("New game: \n");

            while (IsPlaying)
            {
                string goal = CreateGoal();
                int triesLeft = 8;
                string result = string.Empty;
                string mastermindCompare = string.Empty;

                //Comment out or remove next line to play real game
                Ui.WriteOutput($"For practice, number is: {goal} \n");

                Ui.WriteOutput("Enter guess: ");

                while (!mastermindCompare.Equals("XXXXXX".ToUpper()) || triesLeft > 0)
                {
                    Ui.WriteOutput($"Tries left: {triesLeft}");
                    string guess = Ui.HandleInput();
                    mastermindCompare = CheckIfGuessIsValid(goal, guess);
                    if (mastermindCompare.Contains('X'))
                    {
                        triesLeft--;
                    }
                    Ui.WriteOutput($"{mastermindCompare} \n");
                    if (mastermindCompare.Equals("XXXXXX".ToUpper()))
                    {
                        result = $"\n Correct. It took {8 - triesLeft} guesses.";
                        break;
                    }
                    if (triesLeft == 0)
                    {
                        result = "\n You have no tries left. You loose.";
                        break;
                    }
                }
                Ui.WriteOutput(
                    $"{result}" +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answer = Ui.HandleInput();
                if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
                {
                    IsPlaying = false;
                }
            }

        }
    }
}
