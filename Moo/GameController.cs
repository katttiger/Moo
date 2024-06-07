using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Moo
{
    public class GameController
    {
        private UI Ui;
        private Game Game;
        public GameController(UI ui, Game game)
        {
            this.Ui = ui;
            Game = game;
        }

        public void Run()
        {
            string goal = string.Empty;
            while (true)
            {
                Ui.Clear();
                Display(goal);
            }
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
                Game guessResult = new Game();
                return Game.CheckBullsAndCows(goal, guess);
            }
        }

        private void Display(string goal)
        {
            Console.WriteLine("Enter your user name:\n");
            string name = Console.ReadLine();

            goal = Game.CreateGoal();
            Console.WriteLine("New game:");

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
            GameHighscore.ShowTopList();


            //Clearer instructions (y || n || other)
            Console.WriteLine($"\nCorrect. It took {numberOfGuesses} guesses. " +
                "\n Press any button to start a new game." +
                "\n Press n to exit.");
            string? answer = Console.ReadLine();
            if (answer != null && answer != "" && answer.Substring(0, 1) == "n")
            {
                Exit();
            }
            Clear();

        }
        void Exit()
        {
            System.Environment.Exit(0);
        }
        void Clear()
        {
            Console.Clear();
        }
    }

}
