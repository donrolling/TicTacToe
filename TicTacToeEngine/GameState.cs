namespace TicTacToeEngine
{
    public class GameState
    {
        public bool CatWins { get; set; }
        public string GameNotation { get; set; }
        public bool GameOver { get { return Player1Winner.HasValue || CatWins; } }
        public string InstructionHistory { get; set; }
        public bool Player1Turn { get; set; } = true;
        public bool? Player1Winner { get; set; }

        private bool _quit;
        public bool Quit
        {
            get { return _quit || GameOver; }
            set { _quit = value; }
        }

    }
}