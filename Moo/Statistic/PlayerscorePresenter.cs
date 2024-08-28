using Games.Ui;

namespace Games.Statistic
{
    public class PlayerscorePresenter
    {
        public static List<IPlayer> GetPlayerData(string pathToData)
        {
            return DataMethods.GetPlayerdataFromFile(pathToData);
        }
        public static void ShowTopListForGame(string pathToData)
        {
            UserInterface userInterface = new();
            List<IPlayer> results = GetPlayerData(pathToData);
            results.Sort((p1, p2) => p1.TotalGuesses.CompareTo(p2.TotalGuesses));

            userInterface.WriteOutput("Player      games average");
            foreach (Player player in results)
            {
                userInterface.WriteOutput(
                    string.Format("{0,-9}{1,5:D}{2,9:F2}",
                    player.Name, player.NumberOfRoundsPlayed,
                    player.TotalGuesses));
            }
        }
    }
}
