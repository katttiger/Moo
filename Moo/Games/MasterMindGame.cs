using Games.Statistic.PlayerDAO;
using Games.UI;

namespace Games
{
    public class MastermindGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMastemind.txt";

        public readonly IUserInterface userInterface;
        private Player CurrentPlayer;

        public MastermindGame(IUserInterface ui)
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

                if (numberOfGuesses < 8)
                {
                    userInterface.WriteOutput($"\nCorrect. It took {numberOfGuesses} guesses.");
                }
                else
                {
                    userInterface.WriteOutput($"\nYou have run out of guesses.");
                }

                PlayAgainRequest(numberOfGuesses);
            }
            SavePlayerdata();
        }


        public int GameLogic()
        {
            string goal = CreateGoal();
            int numberOfGuesses = 0;
            string answer = string.Empty;

            userInterface.WriteOutput("Values allowed: 0-6.\n" +
                   "A: Right number and place.\n" +
                   "B: Right number, wrong place.\n" +
                   "Duplicated values may occur.");

            //Comment out or remove next line to hide answer
            userInterface.WriteOutput($"\n For practice, number is: {goal} \n");

            for (int i = 8; !answer.Contains("AAAA,"); i--)
            {
                if (i == 0)
                    break;
                else
                {

                    userInterface.WriteOutput($"\nTries left: {i}.");

                    string guess = userInterface.HandleInput();
                    string compare = CheckIfGuessIsValid(guess);
                    if (string.IsNullOrEmpty(compare))
                    {
                        answer = CompareGuessWithGoal(guess, goal);
                        userInterface.WriteOutput($"{answer}");
                    }
                    else
                    {
                        userInterface.WriteOutput($"{compare} \n");
                        i++;
                    }
                }
                numberOfGuesses = (8 - i) + 1;
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
        public string CheckIfGuessIsValid(string guess)
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
        public string CompareGuessWithGoal(string guess, string goal)
        {
            int rightNumberAndPlace = 0;
            int rightNumberWrongPlace = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (goal[i] == guess[j])
                    {
                        if (i == j)
                        {
                            rightNumberAndPlace++;
                        }
                        else
                        {
                            rightNumberWrongPlace++;
                        }
                    }
                }
            }

            if (rightNumberAndPlace == 4)
            {
                return $"AAAA,";
            }
            else
                return $"A:{rightNumberAndPlace}, B:{rightNumberWrongPlace}";
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
                try
                {
                    string name = userInterface.HandleInput();
                    if (name.Length < 1)
                    {
                        userInterface.WriteOutput("You name must have at least 1 character.");
                    }
                    else
                    {
                        CurrentPlayer = new Player(name, 0);
                        nameIsAccepted = true;
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Name must have at least 1 character.");
                }
            }
        }
        public void SavePlayerdata()
        {
            IPlayerDAO playerDAO = new PlayerDAO(CurrentPlayer, PathToScore);
            playerDAO.SavePlayerdataToGameScoreTable();
        }
    }
}
