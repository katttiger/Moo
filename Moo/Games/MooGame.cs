﻿using Moo.Context;
using Moo.Interfaces;
using Moo.Statistic;

namespace Moo.Games
{
    public class MooGame : IGame
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
                string random = randomGenerator.Next(10).ToString();
                while (goal.Contains(random))
                {
                    random = randomGenerator.Next(10).ToString();
                }
                goal += random;
            }
            return goal;
        }
        public static string CheckBullsAndCows(string goal, string guess)
        {
            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            bulls++;
                        }
                        else
                        {
                            cows++;
                        }
                    }
                }
            }
            return $"{"BBBB"[..bulls]},{"CCCC"[..cows]}";
        }
        private static string CheckIfGuessIsValid(string goal, string guess)
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
            Ui.WriteOutput("Enter your user name:\n");
            string name = Ui.HandleInput() ?? "";
            Ui.WriteOutput("New game: \n");
            int numberOfGuesses = 0;

            while (IsPlaying)
            {
                string goal = CreateGoal();

                //Comment out or remove next line to play real game
                Ui.WriteOutput($"For practice, number is: {goal} \n");

                string bullsAndCows = string.Empty;

                while (!bullsAndCows.Equals("BBBB,"))
                {
                    string guess = Ui.HandleInput() ?? "";
                    bullsAndCows = CheckIfGuessIsValid(goal, guess);
                    numberOfGuesses++;
                    Ui.WriteOutput($"{bullsAndCows} \n");
                }

                //output.WriteLine(name + "#&#" + nGuess)
                string result = $"{name}#&#{numberOfGuesses}";
                PlayerDAO.AddPlayerdataToScoreboard(result, "result.txt");
                PlayerDAO.GetTopList("result.txt");
                context.ShowTopList(Ui);

                Ui.WriteOutput(
                    $"\n Correct. It took {numberOfGuesses} guesses. " +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answer = Ui.HandleInput();
                if (answer != null && answer != "" && answer.Contains('n'))
                {
                    IsPlaying = false;
                }
            }
        }
    }
}
