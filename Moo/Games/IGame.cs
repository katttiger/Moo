namespace Games
{
    public interface IGame
    {
        public bool isPlaying { get; }
        string PathToScore { get; set; }
        public void Display();
        public int GameLogic();
        public string CreateGoal();
        void CreatePlayer();
    }
}
