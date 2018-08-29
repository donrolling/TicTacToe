namespace TicTacToeEngine {
	public interface IDisplay {
		void Draw(string gameState);
		void IncorrectGameStateMessage();
		void InvalidGameState(string gameState);
		void MoveErrorMessage(string command, bool player1Turn);
		void MoveInstructions(bool player1Turn);
		void GameOverMessage(bool? player1Winner);
	}
}