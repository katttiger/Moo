using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;

namespace Moo.Games
{
    public class MooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMooGame";
        readonly UI Ui = new();
        private PlayerData Player;

        private static string CreateGoal()
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
        private static string CheckBullsAndCows(string goal, string guess)
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

        private int PlayGame()
        {
            string goal = CreateGoal();
            int numberOfGuesses = 0;

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
            return numberOfGuesses;
        }
        void ExitGame()
        {
            IsPlaying = false;
            string result = $"{Player.Name}#&#{Player.CalculatePlayerAverageScore()}";
            SaveResultToDatabase(result);
            PlayerDataContext.ShowTopList(Ui);
        }

        void CreatePlayer()
        {
            Ui.WriteOutput("Enter your user name:\n");
            string name = Ui.HandleInput() ?? "";
            Player = new(name, 0);
        }
        private void SaveResultToDatabase(string result)
        {
            PlayerDAO playerDAO = new PlayerDAO(Player, PathToScore);
            playerDAO.Save(Player);
        }

        public void Display()
        {
            CreatePlayer();

            Ui.WriteOutput("Values allowed: 0-9.\n" +
                   "B: Right number and place.\n" +
                   "C: Right number, wrong place");

            while (IsPlaying)
            {
                Ui.WriteOutput("New game: \n");
                int numberOfGuesses = PlayGame();

                Ui.WriteOutput(
                    $"\n Correct. It took {numberOfGuesses} guesses. " +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answerToPlayAgain = Ui.HandleInput();

                if (answerToPlayAgain != null && answerToPlayAgain != "" && answerToPlayAgain.Contains('n'))
                {
                    Player.TotalGuesses += numberOfGuesses;
                    ExitGame();
                }
                Player.UpdatePlayerStatus(numberOfGuesses);
            }
        }
    }
}
