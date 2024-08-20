using Games.Ui;

namespace Games
{
    public class MooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMooGame.txt";
        readonly UserInterface Ui = new();
        private Player Player;
        public void Display()
        {
            CreatePlayer();

            Ui.WriteOutput("Values allowed: 0-9.\n" +
                   "B: Right number and place.\n" +
                   "C: Right number, wrong place");

            while (IsPlaying)
            {
                Ui.WriteOutput("New game: \n");
                int numberOfGuesses = GameLogic();

                Ui.WriteOutput(
                    $"\n Correct. It took {numberOfGuesses} guesses. " +
                    "\n Press any button to start a new game." +
                    "\n Press n to exit.");
                string? answerToPlayAgain = Ui.HandleInput();


                if (answerToPlayAgain != null && answerToPlayAgain != "" && answerToPlayAgain.Contains('n'))
                {
                    //Infoga test: testa om det blir negativa tal
                    Player.TotalGuesses += numberOfGuesses;
                    ExitGame();
                }
                Player.UpdatePlayerStatus(numberOfGuesses);
            }
        }
        public int GameLogic()
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

        public string CreateGoal()
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
        public static string CheckIfGuessIsValid(string goal, string guess)
        {
            if (guess.Any(char.IsLetter))
            {
                return "Your guess must only contain numerical digits.";
            }
            else if (guess.Length != 4)
            {
                return "Your guess must contain 4 numerical digits.";
            }
            else
            {
                return CompareGuessWithGoal(goal, guess);
            }
        }
        public static string CompareGuessWithGoal(string goal, string guess)
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

        public void CreatePlayer()
        {
            bool nameIsAccepted = false;
            while (!nameIsAccepted)
            {
                Ui.WriteOutput("Enter your user name:\n");
                string name = Ui.HandleInput() ?? "";
                if (name.Length < 1)
                {
                    Ui.WriteOutput("You name must have at least 1 character.");
                }
                else
                {
                    Player = new(name, 0);
                    nameIsAccepted = true;
                }
            }
        }
        public void ExitGame()
        {
            IsPlaying = false;
            PlayerDAO playerDAO = new(Player, PathToScore);
            playerDAO.SavePlayerData();
            playerDAO.ShowTopList();
        }
    }
}
