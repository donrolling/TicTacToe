using TicTacToeEngine;

namespace Test
{
    /// <summary>
    /// This class intentionally does nothing. This make it easier to test with.
    /// </summary>
    public class MockDisplay : IDisplay
    {
        public void GameOverMessage(bool? player1Winner)
        {
        }

        public void Draw(string gameState)
        {
        }

        public void IncorrectGameStateMessage()
        {
        }

        public void InvalidGameState(string gameState)
        {
        }

        public void MoveErrorMessage(string command, bool player1Turn)
        {
        }

        public void MoveInstructions(bool player1Turn)
        {
        }

        public string RequestCommand()
        {
            return "";
        }
    }
}