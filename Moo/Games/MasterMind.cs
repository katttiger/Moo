using Moo.Interfaces;
using Moo.Players;
using Moo.Statistic;

namespace Moo.Games
{
    public class MasterMind : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMastemind";
        readonly UI Ui = new();
        private PlayerData Player;

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
        public static string CompareGuessWithAnswer(string goal, string guess)
        {
            int numberExistsInRightPlace = 0;
            int numberExistsInWrongPlace = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            numberExistsInRightPlace++;
                        }
                        else
                        {
                            numberExistsInWrongPlace++;
                        }
                    }
                }
            }
            return $"{"AAAA"[..numberExistsInRightPlace]},{"BBBB"[..numberExistsInWrongPlace]}";
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
                return CompareGuessWithAnswer(goal, guess);
            }
        }

        private int PlayGame()
        {

            string goal = CreateGoal();
            int numberOfGuesses = 0;
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

            Ui.WriteOutput("Values allowed: 0-6.\n" +
                    "A: Right number and place.\n" +
                    "B: Right number, wrong place");

            while (IsPlaying)
            {
                Ui.WriteOutput("New game: \n");
                int numberOfGuesses = PlayGame();

                Ui.WriteOutput(
                    $"\n Correct. It took {numberOfGuesses} guesses. " +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answer = Ui.HandleInput();

                if (answer != null && answer != "" && answer.Contains('n'))
                {
                    Player.TotalGuesses += numberOfGuesses;
                    ExitGame();
                }
                Player.UpdatePlayerScore(numberOfGuesses);
            }
        }
    }
}
