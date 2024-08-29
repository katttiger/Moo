using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class MastermindGameTests
    {
        readonly MastermindGame masterMind = new(new UserInterface());


        [TestMethod()]
        public void GoalAndGuessAreEqualTest()
        {
            string mockGoal = "1234";
            string mockGuess = "1234";
            string answer = MastermindGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer == "AAAA,");
        }

        [TestMethod()]
        public void GoalAndGuessAreNotEqualTest()
        {
            string mockGoal = "1234";
            string mockGuess = "4752";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer != "AAAA,");
        }

        [TestMethod()]
        public void GoalHasLengthOfFourTest()
        {
            string mockGuess = masterMind.CreateGoal();
            Assert.IsTrue(mockGuess.Length == 4);
        }


        [TestMethod()]
        public void GuessHasLengthFourTest()
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
        public void CreatePlayerTest()
        {
            MockMastermind masterMind = new();
            masterMind.CreatePlayer();
            Assert.IsNotNull(masterMind.Player.Name);
        }

        [TestMethod()]
        public void MastermindGameTest()
        {
            Assert.IsTrue(masterMind.isPlaying);
        }

        [TestMethod()]
        public void MastermindGameTest1()
        {
            Assert.IsNotNull(masterMind.userInterface);
        }
    }

    class MockMastermind : IGame
    {
        public bool isPlaying { get; set; } = true;
        public string PathToScore { get; set; } = "ResultMastemind.txt";
        readonly UserInterface Ui = new();
        public Player Player;
        public void Display()
        {
            CreatePlayer();

            Ui.WriteOutput("Values allowed: 0-6.\n" +
                    "A: Right number and place.\n" +
                    "B: Right number, wrong place");

            while (isPlaying)
            {
                Ui.WriteOutput("New game: \n");
                int numberOfGuesses = GameLogic();

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
                Player.UpdatePlayerScoreAndRounds(numberOfGuesses);
            }
        }
        public int GameLogic()
        {
            string goal = CreateGoal();
            int numberOfGuesses = 0;

            //Comment out or remove next line to play real game
            Ui.WriteOutput($"For practice, number is: {goal} \n");

            string mastermindCompare = string.Empty;

            while (!mastermindCompare.Equals("AAAA,"))
            {
                string guess = Ui.HandleInput();
                mastermindCompare = CheckIfGuessIsValid(guess);
                numberOfGuesses++;
                Ui.WriteOutput($"{mastermindCompare} \n");
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
        public static string CompareGuessWithGoal(string goal, string guess)
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
        void ExitGame()
        {
            isPlaying = false;
            PlayerDAO playerDAO = new(Player, PathToScore);
            playerDAO.SavePlayerdataToGameScoreTable();
        }
    }
}