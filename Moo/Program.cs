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
                string goal = CreateGoal();

                Console.WriteLine("New game:\n");

                //comment out or remove next line to play real games!
                Console.WriteLine("For practice, number is: " + goal + "\n");

                string guess = Console.ReadLine();
                int numberOfGuess = 1;
                string bullsAndCows = CheckBullsAndCows(goal, guess);
                Console.WriteLine(bullsAndCows + "\n");

                while (bullsAndCows != "BBBB,")
                {
                    numberOfGuess++;
                    guess = Console.ReadLine();
                    //Console.WriteLine(guess + "\n");
                    bullsAndCows = CheckBullsAndCows(goal, guess);
                    Console.WriteLine(bullsAndCows + "\n");
                }

                StreamWriter output = new StreamWriter("result.txt", append: true);
                output.WriteLine(name + "#&#" + numberOfGuess);
                output.Close();
                ShowTopList();
                //Clearer instructions (y/n and catch)
                Console.WriteLine("Correct, it took " + numberOfGuess + " guesses. \nContinue?");
                string answer = Console.ReadLine();
                if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
                {
                    playOn = false;
                }
            }
        }

        static string CreateGoal()
        {
            Random NumberGenerator = new Random();
            string goal = "";
            for (int i = 0; i < 4; i++)
            {
                int random = NumberGenerator.Next(10);
                string randomDigit = "" + random;
                while (goal.Contains(randomDigit))
                {
                //append?
                    random = NumberGenerator.Next(10);
                    randomDigit = "" + random;
                }
                goal = goal + randomDigit;
            }
            return goal;
        }

        static string CheckBullsAndCows(string goal, string guess)
        {
            int cows = 0, bulls = 0;
            //If player entered more than four characters?
            //If player entered less than four characters?
            guess += "    ";     // if player entered less than 4 chars => increased looptime.
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

        static void ShowTopList()
        {
            StreamReader input = new StreamReader("result.txt");
            List<PlayerData> results = new List<PlayerData>();
            string line;
            while ((line = input.ReadLine()) != null)
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
                else
                {
                    results[pos].UpdatePlayerScore(guesses);
                }
            }
            results.Sort((p1, p2) => p1.CalculatePlayerAverageScore().CompareTo(p2.CalculatePlayerAverageScore()));
            Console.WriteLine("Player   games average");
            foreach (PlayerData p in results)
            {
                Console.WriteLine(string.Format("{0,-9}{1,5:D}{2,9:F2}", p.Name, p.NumberOfGamesPlayed, p.CalculatePlayerAverageScore()));
            }
            input.Close();
        }
    }

    class PlayerData
    {
        //Creates a new player even if a player has the same name.
        public string Name { get; private set; }

        //Shouldn't it be updated when you have played a new round?
        //NumberOfRoundsPlayed
        public int NumberOfGamesPlayed { get; private set; }
        int totalGuesses;

        public PlayerData(string name, int guesses)
        {
            this.Name = name;
            NumberOfGamesPlayed = 1;
            totalGuesses = guesses;
        }

        public void UpdatePlayerScore(int guesses)
        {
            totalGuesses += guesses;
            NumberOfGamesPlayed++;
        }
        public double CalculatePlayerAverageScore()
        {
            return (double)totalGuesses / NumberOfGamesPlayed;
        }
    }
}