using Games;
using Games.Ui;


namespace GamesTests2
{
    [TestClass()]
    public class MooGameTests
    {
        readonly MockMooGame mockMooGame = new();
        readonly MooGame mooGame = new();

        [TestMethod()]
        public void GoalAndGuessAreEqualTest()
        {
            string mockGoal = "1234";
            string mockGuess = "1234";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer == "BBBB,");
        }
        [TestMethod()]
        public void GoalAndGuessAreNotEqualTest()
        {
            string mockGoal = "1234";
            string mockGuess = "4752";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer != "BBBB,");
        }


        [TestMethod()]
        public void GoalHasALengthOfFourTest()
        {
            Assert.IsTrue(mockMooGame.CreateGoal().Length.Equals(4));
        }
        [TestMethod()]
        public void GoalHasOnlyUniqueCharactersTest()
        {
            string mockGoal = mockMooGame.CreateGoal();
            bool hasDuplicates = false;
            hasDuplicates = mockGoal
                    .GroupBy(x => x)
                    .Any(g => g.Count() > 1);
            Assert.IsFalse(hasDuplicates);
        }


        [TestMethod()]
        public void GuessHasLengthOfFourTest()
        {
            string mockGuess = "1234";
            string answer = MooGame.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }
        [TestMethod()]
        public void GuessHasNoLettersTest()
        {
            string mockGuess = "1234";
            string answer = MooGame.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }


        [TestMethod()]
        public void CreatePlayerTest()
        {
            MockMooGame mockGame = new();
            mockGame.CreatePlayer();
            Assert.IsNotNull(mockGame.Player.Name);
        }

        [TestMethod()]
        public void ExitGameTest()
        {
            Assert.IsTrue(mockMooGame.PathToScore != string.Empty);
        }
    }
    public class MockMooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMooGame.txt";
        readonly UserInterface Ui = new();
        public Player Player;
        readonly UserInterface userInterface = new();
        public void Display()
        {
            CreatePlayer();
            while (IsPlaying)
            {
                userInterface.WriteOutput("New game: \n");
                int numberOfGuesses = GameLogic();
                MockPlayAgainRequestHandler(numberOfGuesses);
                Player.UpdatePlayerStatus(numberOfGuesses);
            }
        }
        public int GameLogic()
        {
            string goal = CreateGoal();
            int numberOfGuesses = 0;
            string bullsAndCows = string.Empty;

            userInterface.WriteOutput("Values allowed: 0-9.\n" +
                   "B: Right number and place.\n" +
                   "C: Right number, wrong place");

            //Comment out or remove next line to play real game
            userInterface.WriteOutput($"For practice, number is: {goal} \n");

            while (!bullsAndCows.Equals("BBBB,"))
            {
                string guess = userInterface.HandleInput();
                bullsAndCows = CheckIfGuessIsValid(goal, guess);
                numberOfGuesses++;
                userInterface.WriteOutput($"{bullsAndCows} \n");
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
            return goal[..4];
        }
        public string CheckIfGuessIsValid(string goal, string guess)
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
                    return CompareGuessWithGoal(goal, guess);
                }
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
            return $"{"BBBB"[..bulls]},{"CCCC"[..cows]}";
        }

        public void CreatePlayer()
        {
            bool nameIsAccepted = false;
            while (!nameIsAccepted)
            {
                Ui.WriteOutput("Enter your user name:\n");
                try
                {
                    string name = "John Doe";
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
                catch (Exception)
                {
                    throw new Exception("Name must have at least 1 character.");
                }
            }
        }
        public void MockPlayAgainRequestHandler(int numberOfGuesses)
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
        public void ExitGame()
        {
            IsPlaying = false;
            PlayerDAO playerDAO = new(Player, PathToScore);
            playerDAO.SavePlayerdataToGameScoreTable();
        }
    }
}


