using Games;
using Games.UI;

namespace GamesTests2
{
    [TestClass()]
    public class GameLobbyTests
    {
        readonly MockGameLobby mockGameLobby = new(new UserInterface());
        [TestMethod()]
        public void GameLobbyTest()
        {
            Assert.IsNotNull(mockGameLobby.userInterface);
        }

        [TestMethod()]
        public void PrintMenuOfGamesTest()
        {
            Assert.IsTrue(mockGameLobby.GamesList.Count > 0);
        }

        [TestMethod()]
        public void ChooseGameTest()
        {
            Assert.IsTrue(mockGameLobby.GamesList.Count > 0);
        }
    }

    public class MockGameLobby
    {
        public readonly List<IGame> GamesList;
        public IUserInterface userInterface;

        public MockGameLobby(IUserInterface ui)
        {
            GamesList =
            [
                    new MooGame(ui),
                    new MastermindGame(ui),
            ];

            userInterface = ui;
        }

        public void PrintMenuOfGames()
        {
            if (GamesList.Count > 0)
            {
                userInterface.WriteOutput("Menu of games:");
                foreach (IGame game in GamesList)
                {
                    userInterface.WriteOutput($"{GamesList.IndexOf(game) + 1})" +
                        $" {game.ToString()[6..^4]}");
                }
            }
            else
            {
                //throw == redundant?
                throw new Exception("The list of games is empty.");
            }
        }
        public IGame ChooseGame()
        {
            IGame selectedGame = null;
            while (selectedGame is null)
            {
                int input = userInterface.ParseStringToInt(userInterface.HandleInput());
                foreach (var game in GamesList)
                {
                    if (GamesList.IndexOf(game) + 1 == input)
                    {
                        selectedGame = game;
                    }
                }
                userInterface.WriteOutput("Please enter a valid number.");
            }
            return selectedGame;
        }
    }
}