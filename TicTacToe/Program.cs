using System;
using System.Threading;
using TicTacToeDisplay;

namespace TicTacToeEngine {
	class Program {
		static void Main(string[] args) {
			var game = new TicTacToe(new ConsoleDisplay());
			game.Start(string.Empty);
			while (!game.Quit) {
				var command = game.RequestCommand();
				game.Instruct(command);
			}
			Console.WriteLine("Goodbye!");
			Thread.Sleep(5000);
		}
	}
}
