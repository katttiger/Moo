using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Moo.Context;
using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;

namespace Moo.Games
{
    public class MooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
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
            Console.WriteLine("Enter your user name:\n");
            string name = Console.ReadLine() ?? "";

            Console.WriteLine("New game: \n");

            while (IsPlaying)
            {
                string goal = CreateGoal();

                //comment out or remove next line to play real games!
                Console.WriteLine($"For practice, number is: {goal} \n");

                string bullsAndCows = string.Empty;
                int numberOfGuesses = 1;

                while (!bullsAndCows.Contains("BBBB,"))
                {
                    string guess = Console.ReadLine() ?? "";
                    bullsAndCows = CheckIfGuessIsValid(goal, guess);
                    numberOfGuesses++;
                    Console.WriteLine($"{bullsAndCows} \n");
                }

                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(name + "#&#" + numberOfGuesses);
                output.Close();
                //print the high score
                GameController gameController = new();
                Score.GetTopList();
                gameController.ShowTopList();   

                //Clearer instructions (y || n || other)
                Console.WriteLine(
                    $"\n Correct. It took {numberOfGuesses} guesses. " +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answer = Console.ReadLine();
                if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
                {
                    IsPlaying = false;
                }
            }
        }
    }
}
