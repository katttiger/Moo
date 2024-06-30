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
    public class MasterMind : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public List<string> ListOfGuesses = new();
        public string PathToScore { get; set; }

        GameContext context = new GameContext();
        UI Ui = new UI();
        public static string CreateGoal()
        {
            Random randomGenerator = new Random();
            string goal = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                string random = randomGenerator.Next(6).ToString();
                while (goal.Contains(random))
                {
                    random = randomGenerator.Next(6).ToString();
                }
                goal += random;
            }
            return goal;
        }
        public void DisplayListOfGuesses()
        {

        }
        public static string CheckGuess(string goal, string guess)
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
                    return CheckGuess(goal, guess);
                }
            }
        }
        public void Display()
        {
            Ui.WriteOutput("You have choosen mastermind.");
            Ui.WriteOutput("Number of guesses: 8");
            Ui.WriteOutput("Any number may occur more than once. ");
            Ui.WriteOutput("Values allowed: 1, 2, 3, 4, 5, 6.");

            string goal = CreateGoal();
            string guess = string.Empty;
            int triesLeft = 8;

            while (goal != guess)
            {
                Ui.Clear();
                DisplayListOfGuesses();
                Ui.WriteOutput($"Tries left: {triesLeft}");
                Ui.WriteOutput("Enter guess: ");
                CheckGuess(goal, guess);
                ListOfGuesses.Add(guess);
                triesLeft--;
            }

            Ui.Exit();
        }

    }
}
