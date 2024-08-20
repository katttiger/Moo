using Games;
using Games.Ui;
using Games.Statistic;

namespace GamesTests2
{
    [TestClass()]
    public class MooGameTests
    {
        MooGame mooGame = new();
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

        //Using mocking, check whether the method can identify a
        //guess that does not fit the format of the goal
        [TestMethod()]
        public void CheckIfGuessIsValidTest()
        {
        MockMooGame mockMooGame = new();
            string mockGuess = "";
            //Assert.IsFalse(mockMooGame.CheckIfGuessIsValid(mockGuess));
        }

        [TestMethod()]
        public void GoalHasALengthOfFour()
        {
            Assert.IsTrue(mooGame.CreateGoal().Length.Equals(4));
        }

        [TestMethod()]
        public void GoalHasOnlyUniqueCharacters()
        {
            string mockGoal = mooGame.CreateGoal();
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
}

public class MockMooGame : IGame
{
    public bool IsPlaying { get; set; } = true;
    public string PathToScore { get; set; } = "ResultMooGame.txt";
    readonly UserInterface Ui = new();
    public Player Player;
    public void Display()
    {
        CreatePlayer();

        Ui.WriteOutput("Values allowed: 0-9.\n" +
               "B: Right number and place.\n" +
               "C: Right number, wrong place");

        while (IsPlaying)
        {
            Ui.WriteOutput("New game: \n");
            int numberOfGuesses = GameLogic();

            Ui.WriteOutput(
                $"\n Correct. It took {numberOfGuesses} guesses. " +
                "\n Press any button to start a new game." +
                "\n Press n to exit.");
            string? answerToPlayAgain = Ui.HandleInput();

            if (answerToPlayAgain != null && answerToPlayAgain != "" && answerToPlayAgain.Contains('n'))
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

        string bullsAndCows = string.Empty;

        while (!bullsAndCows.Equals("BBBB,"))
        {
            string guess = Ui.HandleInput() ?? "";
            bullsAndCows = CheckIfGuessIsValid(goal, guess);
            numberOfGuesses++;
            Ui.WriteOutput($"{bullsAndCows} \n");
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
            catch (Exception exception)
    {
        string goal = CreateGoal();
        int numberOfGuesses = 0;

        //Comment out or remove next line to play real game
        Ui.WriteOutput($"For practice, number is: {goal} \n");

        string bullsAndCows = string.Empty;

                throw new Exception("Name must have at least 1 character.");
            }
        }
        return numberOfGuesses;
    }
    public void ExitGame()
    {
        IsPlaying = false;
        PlayerDAO playerDAO = new(Player, PathToScore);
        playerDAO.SavePlayerData();
        playerDAO.ShowTopList();
    }

}
