using Games.UI;

namespace Games.Ui
{
    public class UserInterface : IUI
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
                throw new Exception($"{message} could not be converted.");
        }
    }
}
