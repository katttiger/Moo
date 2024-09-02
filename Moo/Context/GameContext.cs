using Games.Statistic;
using Games.UI;

namespace Games
{
    public class GameContext
    {
        private IGame? Game;
        private IUserInterface UserInterface { get; set; }
        private readonly GameLobby gamelobby = new(new UserInterface());
        public GameContext(IUserInterface userinterface)
        {
            this.UserInterface = userinterface;
        }
        public void Run()
        {
            gamelobby.PrintMenuOfGames();
            Game = gamelobby.ChooseGame();

            while (Game.IsPlaying)
            {
                UserInterface.Clear();
                Game.Display();
            }

            if (string.IsNullOrEmpty(Game.PathToScore))
            {
                UserInterface.WriteOutput("List of scores cannot be found.");
            }
            else
            {
                PlayerscorePresenter.ShowTopListForGame(Game.PathToScore);
            }
            UserInterface.Exit();
        }
    }

    public class GameLobby
    {
        public readonly List<IGame> GamesList;
        readonly IUserInterface userInterface;

        public GameLobby(IUserInterface ui)
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
                    userInterface.WriteOutput($"{GamesList.IndexOf(game) + 1})"
                        + $" {game.ToString()[6..^4]}");
                }
            }
            else
            {
                userInterface.WriteOutput("No games are available. \nClosing application.");
                userInterface.Exit();
            }
        }
        public IGame ChooseGame()
        {
            IGame? selectedGame = null;
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
