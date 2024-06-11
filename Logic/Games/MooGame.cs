using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Moo.Players;

namespace Moo.Games
{
    public class MooGame : IGame
    {
        public int Bulls { get; private set; }
        public int Cows { get; set; }
        public string Goal { get; private set; }
        public IPLayer Player { get; set; }
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
            string name = Console.ReadLine();
            Player = new PlayerData(name, 1, 1, true);

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
                    string guess = Console.ReadLine();
                    bullsAndCows = CheckIfGuessIsValid(goal, guess);
                    numberOfGuesses++;
                    Console.WriteLine($"{bullsAndCows} \n");
                }

                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(name + "#&#" + numberOfGuesses);
                output.Close();
                //print the high score
                ShowTopList();

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
        public static List<PlayerData> GetTopList(StreamReader input)
        {
            //what is "line" used for?
            List<PlayerData> results = new List<PlayerData>();
            string line = string.Empty;
            //Why is it important to declare line all the time?
            while ((line = input.ReadLine()) != null)
            {
                string[] playerNameAndScore = line.Split(new string[] { "#&#" }, StringSplitOptions.None);
                string name = playerNameAndScore[0];
                int guesses = Convert.ToInt32(playerNameAndScore[1]);

                //TODO: if the player is actively gaming, do not add another playerdata.
                PlayerData playerData = new PlayerData(name, 1, guesses);
                int indexOfPlayerData = results.IndexOf(playerData);

                //TODO: Else is never hit.
                //&& !results.Contains(playerData))
                if (indexOfPlayerData < 0)
                {
                    results.Add(playerData);
                }
                //pos === -1 => does not hit else. Score is not updates when you play more rounds.
                else
                {
                    results[indexOfPlayerData].UpdatePlayerScore(guesses);
                }
            }
            return results;
        }
        public static void ShowTopList()
        {
            //Fetches input
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = GetTopList(input);

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
}
