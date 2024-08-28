namespace Games
{
    public class Player : IPlayer
    {
        private string _name { get; set; }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 0 || string.IsNullOrEmpty(value))
                {
                    throw new Exception("Name cannot be empty.");
                }
                else
                {
                    _name = value;
                }
            }
        }

        public int NumberOfRoundsPlayed { get; set; } = 1;

        private int _totalGuesses;

        public int TotalGuesses
        {
            get
            {
                return _totalGuesses;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Totalguesses cannot be negative.");
                else
                    _totalGuesses = value;
            }
        }

        public Player(string name, int guesses)
        {
            _name = name;
            _totalGuesses = guesses;
        }

        public void UpdatePlayerStatus(int guesses)
        {
            if (guesses < 0)
            {
                throw new Exception("Number of guesses cannot be negative.");
            }
            else
            {
                TotalGuesses += guesses;
                NumberOfRoundsPlayed++;
            }
        }
        public double CalculatePlayerAverageScore()
        {
            if (NumberOfRoundsPlayed < 0)
            {
                throw new Exception("Player cannot have played 0 rounds.");
            }
            else if (TotalGuesses < 0)
            {
                throw new Exception("Player cannot have scored less than 1 points.");
            }
            else
            {
                return TotalGuesses / NumberOfRoundsPlayed;
            }


        }
    }
}
