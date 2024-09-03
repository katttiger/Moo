using Games.Statistic.PlayerDAO;
using Games.UI;

namespace Games
{
    public class MooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMooGame.txt";

        public readonly IUserInterface userInterface;
        private Player CurrentPlayer;

        public MooGame(IUserInterface ui)
        {
            this.userInterface = ui;
        }

        public void Display()
        {
            CreatePlayer();
            while (IsPlaying)
            {
                userInterface.WriteOutput("New game: \n");
                int numberOfGuesses = GameLogic();
                userInterface.WriteOutput($"Correct. It took {numberOfGuesses} guesses.");
                PlayAgainRequest(numberOfGuesses);
            }
            SavePlayerdata();
        }


        public int GameLogic()
        {
            string goal = CreateGoal();
            int numberOfGuesses = 0;
            string bullsAndCows = string.Empty;

            userInterface.WriteOutput("Values allowed: 0-9.\n" +
                   "B: Right number and place.\n" +
                   "C: Right number, wrong place");

            //Comment out or remove next line to hide answer
            userInterface.WriteOutput($"\nFor practice, number is: {goal} \n");

            while (!bullsAndCows.Equals("BBBB,"))
            {
                string guess = userInterface.HandleInput();
                string compare = CheckIfGuessIsValid(guess);

                if (string.IsNullOrEmpty(compare))
                {
                    bullsAndCows = CompareGuessWithGoal(goal, guess);
                    numberOfGuesses++;
                    userInterface.WriteOutput($"{bullsAndCows} \n");
                }
                else
                {
                    userInterface.WriteOutput($"{compare}\n");
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
        public string CheckIfGuessIsValid(string guess)
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
        public string CompareGuessWithGoal(string goal, string guess)
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

            if (bulls == 4)
            {
                return $"{"BBBB"[..bulls]},";
            }
            return $"{"BBBB"[..bulls]},{"CCCC"[..cows]}";
        }


        public void PlayAgainRequest(int numberOfGuesses)
        {
            userInterface.WriteOutput(
                $"\n Press any button to start a new game." +
                "\n Press n to exit.");
            string? answer = userInterface.HandleInput();

            if (!string.IsNullOrEmpty(answer) || answer.Contains('n'))
            {
                CurrentPlayer.UpdatePlayerScore(numberOfGuesses);
                IsPlaying = false;
            }
            else
            {
                CurrentPlayer.UpdatePlayerScoreAndRounds(numberOfGuesses);
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
                    CurrentPlayer = new(name, 0);
                    nameIsAccepted = true;
                }
            }
        }
        public void SavePlayerdata()
        {
            PlayerDAO playerDAO = new(CurrentPlayer, PathToScore);
            playerDAO.SavePlayerdataToGameScoreTable();
        }
    }
}