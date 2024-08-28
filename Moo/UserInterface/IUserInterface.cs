namespace Games.UI
{
    public interface IUserInterface
    {
        string HandleInput();
        void WriteOutput(string message);
        void Exit();
        void Clear();
        public int ParseStringToInt(string message);
    }
}
