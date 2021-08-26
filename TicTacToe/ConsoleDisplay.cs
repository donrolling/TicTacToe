using System;
using System.Text;
using TicTacToeEngine;

namespace TicTacToeDisplay {
	public class ConsoleDisplay : IDisplay {
		private string padding = "  ";
		private string emptyLine = "     |     |     ";
		private string line = "|";

		public void Draw(string gameState) {
			var display = new StringBuilder("\r\n\r\n");
			for (int i = 0; i < 3; i++) {
				var a = gameState[8 - (i * 3) - 2];
				var b = gameState[8 - (i * 3) - 1];
				var c = gameState[8 - (i * 3)];
				display.AppendLine(emptyLine);
				display.AppendLine(string.Concat(padding, a, padding, line, padding, b, padding, line, padding, c, padding));
				display.AppendLine(emptyLine);
			}
			Console.Clear();
			Console.WriteLine(display);
		}

		public void IncorrectGameStateMessage() {
			Console.WriteLine("There appears to be more moves by o than by x. This cannot be allowed!");
		}

		public void InvalidGameState(string gameState) {
			Console.WriteLine($"The game state that was provided is not acceptable: { gameState }.");
			Console.WriteLine("Please use 0 as the bottom left corner and indicate x or o with those characters. Here's an example: 0123x5o78");
		}

		public void MoveInstructions(bool player1Turn) {
			Console.WriteLine($"Enter the number of the square upon which you would like to move.");
			Console.WriteLine($"'New' to start over.");
			Console.WriteLine($"'Quit' to exit.");
			if (player1Turn) {
				Console.WriteLine("X Turn");
			} else {
				Console.WriteLine("O Turn");
			}
			Console.WriteLine();
		}

		public void MoveErrorMessage(string command, bool player1Turn) {
			Console.WriteLine($"The following command was not valid: { command }.");
			this.MoveInstructions(player1Turn);
		}

		public void GameOverMessage(bool? player1Winner) {
			var winner = player1Winner.HasValue ? (player1Winner.Value ? "X" : "O") : "Cat";
            Console.WriteLine($"The game is over { winner } wins!");
		}

        public string RequestCommand()
		{
			return Console.ReadLine();
		}
    }
}