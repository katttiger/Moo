using Games.Ui;

namespace Games
{
    public class MooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMooGame.txt";
        public UserInterface userInterface = new();
        private Player Player;
        public void Display()
        {
            CreatePlayer();

            userInterface.WriteOutput("Values allowed: 0-9.\n" +
                   "B: Right number and place.\n" +
                   "C: Right number, wrong place");

            while (IsPlaying)
            {
                userInterface.WriteOutput("New game: \n");
                int numberOfGuesses = GameLogic();
                PlayAgainRequestHandler(numberOfGuesses);
                Player.UpdatePlayerStatus(numberOfGuesses);
            }
        }

        public int GameLogic()
        {
            string goal = CreateGoal();
            int numberOfGuesses = 0;
            string bullsAndCows = string.Empty;


            //Comment out or remove next line to play real game
            userInterface.WriteOutput($"For practice, number is: {goal} \n");

            while (!bullsAndCows.Equals("BBBB,"))
            {
                string guess = userInterface.HandleInput();
                string compare = CheckIfGuessIsValid(guess);
                if (compare == string.Empty)
                {
                    bullsAndCows = CheckIfGuessIsValid(guess);
                    numberOfGuesses++;
                    userInterface.WriteOutput($"{bullsAndCows} \n");
                }
                else
                {
                    userInterface.WriteOutput($"{compare}");
                }
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
        public static string CheckIfGuessIsValid(string guess)
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
                return string.Empty;
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

        public void PlayAgainRequestHandler(int numberOfGuesses)
        {
            userInterface.WriteOutput(
                $"\n Correct. It took {numberOfGuesses} guesses. " +
                "\n Press any button to start a new game." +
                "\n Press n to exit.");
            string? answer = userInterface.HandleInput();
            if (!string.IsNullOrEmpty(answer) || answer.Contains('n'))
            {
                IsPlaying = false;
                Player.TotalGuesses += numberOfGuesses;
                ExitGame();
            }
        }

        public void CreatePlayer()
        {
            bool nameIsAccepted = false;
            while (!nameIsAccepted)
            {
                userInterface.WriteOutput("Enter your user name:\n");
                string name = userInterface.HandleInput() ?? "";
                if (name.Length < 1)
                {
                    userInterface.WriteOutput("You name must have at least 1 character.");
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
            playerDAO.ShowTopListGame(PathToScore);
        }
    }
}