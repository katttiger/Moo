using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.UI;
using Games.Statistic.PlayerDAO;

namespace GamesTests2
{
    [TestClass()]
    public class MastermindGameTests
    {
        readonly MockMastermindGame mockMastermind = new(new UserInterface());

        [TestMethod()]
        public void GoalAndGuessAreEqualTest()
        {
            string mockGoal = "1223";
            string mockGuess = "1223";
            string answer = mockMastermind.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer == "AAAA,");
        }

        [TestMethod()]
        public void GoalAndGuessAreNotEqualTest()
        {
            string mockGoal = "1144";
            string mockGuess = "1111";
            string answer = mockMastermind.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer != "AAAA,");
        }


        [TestMethod()]
        public void GoalHasLengthOfFourTest()
        {
            string mockGuess = mockMastermind.CreateGoal();
            Assert.IsTrue(mockGuess.Length == 4);
        }

        [TestMethod()]
        public void GuessHasLengthOfFourTest()
        {
            string mockGuess = "1323";
            string answer = mockMastermind.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }

        [TestMethod()]
        public void GuessContainsValueHigherThanSixTest()
        {
            string mockGuess = "1234";
            string answer = mockMastermind.CheckIfGuessIsValid(mockGuess);
            Assert.IsTrue(answer == string.Empty);
        }

        [TestMethod()]
        public void GuessHasNoLettersTest()
        {
            string mockGuess = "1113";
            string answer = mockMastermind.CheckIfGuessIsValid(mockGuess);
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
            Assert.IsNotNull(mockMastermind.userInterface);
        }


        [TestMethod()]
        public void CreatePlayerTest()
        {
            mockMastermind.CreatePlayer();
            Assert.IsFalse(string.IsNullOrEmpty(mockMastermind.CurrentPlayer.Name));
        }

        [TestMethod()]
        public void SavePlayerdataTest()
        {
            Assert.IsTrue(mockMastermind.PathToScore != string.Empty);
        }
    }

    internal class MockMastermindGame : IGame
    {
        public bool IsPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMastemind.txt";

        public readonly IUserInterface userInterface;
        public IPlayer CurrentPlayer;

        public MockMastermindGame(IUserInterface ui)
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
        public void SavePlayerdata()
        {
            IPlayerDAO playerDAO = new PlayerDAO(CurrentPlayer, PathToScore);
            playerDAO.SavePlayerdataToGameScoreTable();
        }
    }
}