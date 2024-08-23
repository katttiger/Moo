using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.Ui;
using System.Runtime.Intrinsics.X86;

namespace Games.Tests
{
    [TestClass()]
    public class MooGameTests
    {
    }
}

namespace GamesTests2
{
    [TestClass()]
    public class MooGameTests
    {
        MockMooGame mockMooGame = new();

        [TestMethod()]
        public void GoalAndGuessAreEqual()
        {
            string mockGoal = "1234";
            string mockGuess = "1234";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer == "BBBB,");
        }

        [TestMethod()]
        public void GoalAndGuessAreNotEqual()
        {
            string mockGoal = "1234";
            string mockGuess = "4752";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer != "BBBB,");
        }

        [TestMethod()]
        public void GuessMatchesFormatOfLenthFourAndNoLetters()
        {
            string mockGuess = "1234";
            MooGame.CheckIfGuessIsValid(mockGuess, "1234");
            Assert.IsTrue(
                mockGuess.Count() == 4 && mockGuess.All(char.IsNumber)
                );
        }

        [TestMethod()]
        public void GoalHasALengthOfFour()
        {
            Assert.IsTrue(mockMooGame.CreateGoal().Length.Equals(4));
        }

        [TestMethod()]
        public void GoalHasOnlyUniqueCharacters()
        {
            string mockGoal = mockMooGame.CreateGoal();
            bool hasDuplicates = false;
            hasDuplicates = mockGoal
                    .GroupBy(x => x)
                    .Any(g => g.Count() > 1);
            Assert.IsFalse(hasDuplicates);
        }

        [TestMethod()]
        public void CreatePlayerTest()
        {
            MockMooGame mockGame = new MockMooGame();
            mockGame.CreatePlayer();
            Assert.IsNotNull(mockGame.Player.Name);
        }
    }
    public class MockMooGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMooGame.txt";
        readonly UserInterface Ui = new();
        public Player Player;
        UserInterface userInterface = new();
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
            string answer = "";
            if (string.IsNullOrEmpty(answer) || answer.Contains('n'))
            {
                IsPlaying = false;
            }
            else
            {
                IsPlaying = true;
            }
        }
        public void ExitGame()
        {
            IsPlaying = false;
            PlayerDAO playerDAO = new(Player, PathToScore);
            playerDAO.SavePlayerData();
            playerDAO.ShowTopListThisGame(PathToScore);
        }
    }
}


