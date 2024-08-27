using Games.Ui;

namespace Games
{
    public class MasterMindGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMastemind.txt";
        readonly UserInterface userInterface = new();
        private Player Player;

        public void Display()
        {
            CreatePlayer();

            userInterface.WriteOutput("Values allowed: 0-6.\n" +
                               "A: Right number and place.\n" +
                               "B: Right number, wrong place");

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
            string AsAndBs = string.Empty;

            //Comment out or remove next line to play real game
            userInterface.WriteOutput($"For practice, number is: {goal} \n");

            for (int i = 8; !AsAndBs.Contains("AAAA,"); i--)
            {
                userInterface.WriteOutput($"\nTries left: {i}.");

                string guess = userInterface.HandleInput();
                string compare = CheckIfGuessIsValid(guess);
                if (compare == string.Empty)
                {
                    AsAndBs = CompareGuessWithGoal(guess, goal);
                    userInterface.WriteOutput($"{AsAndBs}");
                }
                else
                {
                    userInterface.WriteOutput($"{compare} \n");
                }
                numberOfGuesses = (8 - i);
            }
            return numberOfGuesses;
        }

        public string CreateGoal()
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
        public static string CheckIfGuessIsValid(string guess)
        {
            foreach (char c in guess)
            {
                if (c > '6')
                {
                    return "You may only use numbers 0-6";
                }
            }
            if (guess.Any(char.IsLetter) || guess.Length != 4)
            {
                return "Your guess must only contain 4 numerical digits.";
            }
            return string.Empty;
        }
        public static string CompareGuessWithGoal(string guess, string goal)
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
            if (numberExistsInRightPlace == 4)
                return $"{"AAAA"[..numberExistsInRightPlace]},";
            return $"{"AAAA"[..numberExistsInRightPlace]},{"BBBB"[..numberExistsInWrongPlace]}";
        }

        public void PlayAgainRequestHandler(int numberOfGuesses)
        {
            userInterface.WriteOutput(
                $"\n Correct. It took {numberOfGuesses} guesses. " +
                "\n Press any button to start a new game." +
                "\n Press n to exit.");
            string? answer = userInterface.HandleInput();
            if (string.IsNullOrEmpty(answer) || answer.Contains('n'))
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
                try
                {
                    string name = "John Doe";
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
                catch (Exception)
                {
                    throw new Exception("Name must have at least 1 character.");
                }
            }
        }
        void ExitGame()
        {
            IsPlaying = false;
            PlayerDAO playerDAO = new(Player, PathToScore);
            playerDAO.SavePlayerData();
            playerDAO.ShowTopListGame(PathToScore);
        }
    }
}
