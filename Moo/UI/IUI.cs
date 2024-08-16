namespace Games.UI
{
    public interface IUI
    {
        //Göra det vi vill göra mot konsolen
        //Input | Output
        string HandleInput();
        void WriteOutput(string message);
        void Exit();
        void Clear();
        public int ParseStringToInt(string message);
    }
}
