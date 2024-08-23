using Microsoft.VisualStudio.TestTools.UnitTesting;
using Games;
using Games.Ui;

namespace GamesTests2
{
    [TestClass()]
    public class MasterMindTests
    {
        MockMastermind mockMastermind = new();
        [TestMethod()]
        public void CreateGoalTest()
        {
            string mockGuess = mockMastermind.CreateGoal();
            Assert.IsTrue(mockGuess.Length == 4);
        }
        public void GoalAndGuessAreEqual()
        {
            string mockGoal = "1234";
            string mockGuess = "1234";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer == "AAAA,");
        }
        public void GoalHasALengthOfFour()
        {
            Assert.IsTrue(mockMastermind.CreateGoal().Length.Equals(4));
        }
        [TestMethod()]
        public void GoalAndGuessAreNotEqual()
        {
            string mockGoal = "1234";
            string mockGuess = "4752";
            string answer = MooGame.CompareGuessWithGoal(mockGoal, mockGuess);
            Assert.IsTrue(answer != "AAAA,");
        }
        [TestMethod()]
        public void DisplayTest()
        {
            Assert.IsTrue(mockMastermind.IsPlaying);
        }

        [TestMethod()]
        public void PlayAgainRequestHandlerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GameLogicTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CreatePlayerTest()
        {
            MockMastermind mockGame = new MockMastermind();
            mockGame.CreatePlayer();
            Assert.IsNotNull(mockGame.Player.Name);
        }

    }
}

class MockMastermind : IGame
{
    public bool IsPlaying { get; set; } = true;
    public string PathToScore { get; set; } = "ResultMastemind.txt";
    readonly UserInterface Ui = new();
    public Player Player;
    public void Display()
    {
        CreatePlayer();

        Ui.WriteOutput("Values allowed: 0-6.\n" +
                "A: Right number and place.\n" +
                "B: Right number, wrong place");

        while (IsPlaying)
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
            Player.UpdatePlayerStatus(numberOfGuesses);
        }
    }
    public int GameLogic()
    {

        string goal = CreateGoal();
        int numberOfGuesses = 0;
        //Comment out or remove next line to play real game
        Ui.WriteOutput($"For practice, number is: {goal} \n");

        string mastermindCompare = string.Empty;

        while (!mastermindCompare.Equals("XXXX"))
        {
            string guess = Ui.HandleInput();
            mastermindCompare = CheckIfGuessIsValid(goal, guess);
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
    private static string CheckIfGuessIsValid(string goal, string guess)
    {
        if (guess.Any(char.IsLetter) || guess.Length != 4)
        {
            return "Your guess must only contain 4 numerical digits.";
        }
        else if (guess.Contains('7') || guess.Contains('8') || guess.Contains('9'))
        {
            return "You may only use numbers 0-6";
        }
        else
        {
            return CompareGuessWithGoal(goal, guess);
        }
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
        IsPlaying = false;
        PlayerDAO playerDAO = new(Player, PathToScore);
        playerDAO.SavePlayerData();
        playerDAO.ShowTopListThisGame(PathToScore);
    }
}