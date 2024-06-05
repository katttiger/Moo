using System;
using System.IO;
using System.Collections.Generic;

namespace MooGame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool playOn = true;
            Console.WriteLine("Enter your user name:\n");
            string name = Console.ReadLine();

            while (playOn)
            {
                string goal = GameEngine.CreateGoal();
                Console.WriteLine("New game:\n");

                //comment out or remove next line to play real games!
                Console.WriteLine("For practice, number is: " + goal + "\n");

                int numberOfGuesses = 1;
                string bullsAndCows = string.Empty;
                while (!bullsAndCows.Contains("BBBB,"))
                {
                    bullsAndCows = GameEngine.GuessTheNumber(goal.ToString());
                    numberOfGuesses++;
                }

                //Scoreboard => seperate part of the code?
                //Fetch the result-file
                //Creates a new player every time despite person continuing on the game.
                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(name + "#&#" + numberOfGuesses);
                output.Close();

                ShowTopList();

                //Clearer instructions (y/n and catch)
                Console.WriteLine("Correct, it took " + numberOfGuesses + " guesses. \nContinue?");
                string answer = Console.ReadLine();
                if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
                {
                    playOn = false;
                }
            }
        }

        //should be small an do one thing
        //names need to be fixed and the method shortened down a bit
        static void ShowTopList()
        {
            //Fetch input
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();
            //what is line used for?
            string line = input.ReadLine();
            while (line != null)
            {
                string[] nameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = nameAndScore[0];
                int guesses = Convert.ToInt32(nameAndScore[1]);
                PlayerData pd = new PlayerData(name, guesses);
                int pos = results.IndexOf(pd);
                if (pos < 0)
                {
                    results.Add(pd);
                }
                //pos = -1 => does not hit else
                else
                {
                    results[pos].UpdatePlayerScore(guesses);
                }
            }

            //Prints out the score
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            Console.WriteLine("Player   games average");
            foreach (PlayerData p in results)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGamesPlayed, p.CalculatePlayerAverageScore()));
            }
            input.Close();
        }
    }

    public class Game
    {
        public Game() { }
        public static string CreateGoal()
        {
            Random randomGenerator = new Random();
            string goal = string.Empty;
            for (int i = 0; i < 4; i++)
            {
                int random = randomGenerator.Next(10);
                string randomDigit = random.ToString();
                while (goal.Contains(randomDigit))
                {
                    random = randomGenerator.Next(10);
                    randomDigit = random.ToString();
                }
                goal += randomDigit;
            }
            return goal;
        }
    }

    class GuessResult
    {
        public int Bulls { get; }
        public int Cows { get; }
        public static string GuessTheNumber(string goal)
        {
            string guess = Console.ReadLine();
            string bullsAndCows = CheckIfGuessIsValid(goal, guess);
            Console.WriteLine(bullsAndCows + "\n");
            return bullsAndCows;
        }
        static string CheckIfGuessIsValid(string goal, string guess)
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
                GuessResult guessResult = new GuessResult();
                return CheckBullsAndCows(goal, guess);
            }
        }
        static string CheckBullsAndCows(string goal, string guess)
        {
            int bulls = 0;
            int cows = 0;

            //i=>length of guess (4)
            //j=>count the number in the sequence
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
            //I think this needs polishing:
            //Goal: 0377
            //Guess: 0070
            //Result: BB,CCC => maybe ask user to insert unique numbers?
            return "BBBB".Substring(0, bulls) + "," + "CCCC".Substring(0, cows);
        }
    }

    class PlayerData
    {
        public string Name { get; private set; }
        public int NumberOfGamesPlayed { get; private set; }
        int totalGuesses;

        public PlayerData(string name, int guesses)
        {
            this.Name = name;
            //Number of rounds?
            NumberOfGamesPlayed = 1;
            totalGuesses = guesses;
        }

        public void UpdatePlayerScore(int guesses)
        {
            totalGuesses += guesses;
            //Number of rounds?
            NumberOfGamesPlayed++;
        }
        public double CalculatePlayerAverageScore()
        {
            return (double)totalGuesses / NumberOfGamesPlayed;
        }
    }
}