using Moo.Context;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;
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

        readonly GameContext context = new();
        readonly UI Ui = new();
        public static string CreateGoal()
        {
            Random randomGenerator = new();
            string goal = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                string random = randomGenerator.Next(6).ToString();
                goal += random;
            }
            return goal;
        }

        //TODO: Improve. The method does not take into consideration whether the
        //digit has already been accounted for when there are duplicates in the 
        //goal.
        public static string CheckGuess(string goal, string guess)
        {
            StringBuilder stringBuilder = new();
            int rightNumberAndPlace = 0;
            int rightNumberWrongPlace = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            stringBuilder.Append('X');
                            rightNumberAndPlace++;
                        }
                        else
                        {
                            stringBuilder.Append('Y');
                            rightNumberWrongPlace++;
                        }
                    }
                }
            }
            return $"{"XXXX"[..rightNumberAndPlace]}\n{"YYYY"[..rightNumberWrongPlace]}";
            //return stringBuilder.ToString();
        }
        private static string CheckIfGuessIsValid(string goal, string guess)
        {
            if (guess.Any(char.IsLetter) || guess.Length != 4)
            {
                return "Your guess must only contain 4 numerical digits.";
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

            PlayerData playerData = new(name, 0);

            Ui.WriteOutput(
                "Values allowed: 0-6.");
            Ui.WriteOutput("New game: \n");
            int numberOfGuesses = 0;

            while (IsPlaying)
            {
                string goal = CreateGoal();

                //Comment out or remove next line to play real game
                Ui.WriteOutput($"For practice, number is: {goal} \n");

                string mastermindCompare = string.Empty;

                while (!mastermindCompare.Equals("XXXX"))
                {
                    string guess = Ui.HandleInput();
                    mastermindCompare = CheckIfGuessIsValid(goal, guess);
                    numberOfGuesses++;
                    Ui.WriteOutput($"{mastermindCompare} \n");
                }
                string result = $"{name}#&#{numberOfGuesses}";

                playerData.TotalGuesses = numberOfGuesses;
                PlayerDAO.AddPlayerdataToScoreboard(result, "result.txt");
                PlayerDAO.AddPlayerdataToScoreboard(result, "MastermindResult.txt");
                PlayerDAO.GetTopList("MastermindResult.txt");
                context.ShowTopList(Ui);


                Ui.WriteOutput(
                    $"\n Correct. It took {numberOfGuesses} guesses" +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answer = Ui.HandleInput();
                if (answer != null && answer != string.Empty && answer.Contains('n'))
                {
                    IsPlaying = false;
                }

            }
        }
    }
}
