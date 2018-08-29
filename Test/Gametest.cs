using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeEngine;

namespace Test {
	[TestClass]
	public class Gametest {
		[TestMethod]
		public void Given_ValidMoves_GameStateProgresses() {
			var game = new TicTacToe(new MockDisplay());
			game.Start(string.Empty);

			Assert.IsTrue(game.Player1Turn);
			game.Instruct("4");
			Assert.IsFalse(game.Player1Turn);
			game.Instruct("7");

			Assert.IsTrue(game.GameNotation == "0123X56O8");

			Assert.IsTrue(game.Player1Turn);
			game.Instruct("0");
			Assert.IsFalse(game.Player1Turn);
			game.Instruct("8");

			Assert.IsTrue(game.GameNotation == "X123X56OO");
		}

		[TestMethod]
		public void Given_ValidMoves_CatWins() {
			var game = new TicTacToe(new MockDisplay());
			game.Start(string.Empty);
			var moves = "0436217";
			var toggle = true;
			foreach (var c in moves) {
				Assert.IsTrue(game.Player1Turn == toggle);
				game.Instruct(c.ToString());
				toggle = !toggle;
			}
			Assert.IsFalse(game.Player1Winner.HasValue);
			Assert.IsTrue(game.CatWins);
        }

		[TestMethod]
		public void Given_ValidMoves_XWins() {
			var game = new TicTacToe(new MockDisplay());
			game.Start(string.Empty);
			var moves = "03142";
			var toggle = true;
			foreach (var c in moves) {
				Assert.IsTrue(game.Player1Turn == toggle);
				game.Instruct(c.ToString());
				toggle = !toggle;
			}
			Assert.IsTrue(game.Player1Winner.Value);
		}

		[TestMethod]
		public void Given_ValidMoves_OWins() {
			var game = new TicTacToe(new MockDisplay());
			game.Start(string.Empty);
			var moves = "036425";
            var toggle = true;
			foreach (var c in moves) {
				Assert.IsTrue(game.Player1Turn == toggle);
				game.Instruct(c.ToString());
				toggle = !toggle;
			}
			Assert.IsFalse(game.Player1Winner.Value);
		}
	}
}
