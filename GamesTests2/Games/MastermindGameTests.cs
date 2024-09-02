using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.UI;
using Games.Statistic.PlayerDAO;

namespace GamesTests2
{
    [TestClass()]
    public class MastermindGameTests
    {
        readonly MastermindGame mockMasterMind = new(new UserInterface());


        [TestMethod()]
        public void GoalAndGuessAreEqualTest()
        {
            string mockGoal = "1223";
            string mockGuess = "1223";
            string answer = MastermindGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer == "AAAA,");
        }

        [TestMethod()]
        public void GoalAndGuessAreNotEqualTest()
        {
            string mockGoal = "1144";
            string mockGuess = "1111";
            string answer = MastermindGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer != "AAAA,");
        }


        [TestMethod()]
        public void GoalHasLengthOfFourTest()
        {
            string mockGuess = mockMasterMind.CreateGoal();
            Assert.IsTrue(mockGuess.Length == 4);
        }

        [TestMethod()]
        public void GuessHasLengthOfFourTest()
        {
            string mockGuess = "1323";
            string answer = MastermindGame.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }

        [TestMethod()]
        public void GuessContainsValueHigherThanSixTest()
        {
            string mockGuess = "1234";
            string answer = MastermindGame.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }

        [TestMethod()]
        public void GuessHasNoLettersTest()
        {
            string mockGuess = "1113";
            string answer = MastermindGame.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }


        [TestMethod()]
        public void MastermindGameIsPlayingIsTrueTest()
        {
            MastermindGame game = new(new UserInterface());
            Assert.IsTrue(game.IsPlaying);
        }

        [TestMethod()]
        public void MastermindGameTest()
        {
            Assert.IsNotNull(mockMasterMind.userInterface);
        }


        [TestMethod()]
        public void CreatePlayerTest()
        {
            MockMastermind masterMind = new(new UserInterface());
            masterMind.CreatePlayer();
            Assert.IsNotNull(masterMind.CurrentPlayer.Name);
        }

        [TestMethod()]
        public void SavePlayerdataTest()
        {
            Assert.IsTrue(mockMasterMind.PathToScore != string.Empty);
        }
    }

    internal class MockMastermind : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMastemind.txt";

        public readonly IUserInterface userInterface;
        public IPlayer CurrentPlayer;

        public MockMastermind(IUserInterface ui)
        {
            this.userInterface = ui;
        }

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
                userInterface.WriteOutput($"Correct. It took {numberOfGuesses} guesses.");
                PlayAgainRequest(numberOfGuesses);
            }
            SavePlayerdata();
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
            else
                return $"{"AAAA"[..numberExistsInRightPlace]},{"BBBB"[..numberExistsInWrongPlace]}";
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
                    string name = "John Doe";
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
        void SavePlayerdata()
        {
            IPlayerDAO playerDAO = new PlayerDAO(CurrentPlayer, PathToScore);
            playerDAO.SavePlayerdataToGameScoreTable();
        }
    }
}