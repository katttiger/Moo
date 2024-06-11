﻿using Moo.Context;
using Moo.Interfaces;
using Moo.Statistic;

namespace Moo.Games
{
    public class MooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;

        GameController gameController = new();
        UI Ui = new UI();

        public static string CreateGoal()
        {
            Random randomGenerator = new Random();
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

            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
        }
        public static string CheckIfGuessIsValid(string goal, string guess)
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
                MooGame guessResult = new MooGame();
                return CheckBullsAndCows(goal, guess);
            }
        }
        public void Display()
        {
            Ui.WriteOutput("Enter your user name:\n");
            string name = Ui.HandleInput() ?? "";

            Ui.WriteOutput("New game: \n");

            while (IsPlaying)
            {
                string goal = CreateGoal();

                //comment out or remove next line to play real games!
                Ui.WriteOutput($"For practice, number is: {goal} \n");

                string bullsAndCows = string.Empty;
                int numberOfGuesses = 0;

                while (!bullsAndCows.Contains("BBBB,"))
                {
                    string guess =Ui.HandleInput() ?? "";
                    bullsAndCows = CheckIfGuessIsValid(goal, guess);
                    numberOfGuesses++;
                    Ui.WriteOutput($"{bullsAndCows} \n");
                }
                PlayerDAO.AddDataToScoreboard(name, numberOfGuesses);
                gameController.ShowTopList(Ui);

                Ui.WriteOutput(
                    $"\n Correct. It took {numberOfGuesses} guesses. " +
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
