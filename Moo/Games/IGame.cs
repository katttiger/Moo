namespace Games.Games
{
    public interface IGame
    {
        public bool IsPlaying { get; }
        string PathToScore { get; set; }
        public void Display();
        public int GameLogic();
        public string CreateGoal();
        void CreatePlayer();
    }
}
