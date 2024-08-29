namespace Games.UI
{
    public class UserInterface : IUserInterface
    {
        public void Exit()
        {
            Environment.Exit(0);
        }
        public void Clear()
        {
            Console.Clear();
        }
        public string HandleInput()
        {
            return Console.ReadLine() ?? "";
        }
        public void WriteOutput(string message)
        {
            Console.WriteLine(message);
        }
        public int ParseStringToInt(string message)
        {
            bool conversionWasSuccesful = int.TryParse(message, out int value);
            if (conversionWasSuccesful)
            {
                return value;
            }
            else
            {
                return 0;
            }
        }
    }
}
