using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeEngine;

namespace Test
{
    [TestClass]
    public class GameTests
    {
        private GameStateEngine _gameStateEngine;

        public GameTests()
        {
            _gameStateEngine = new GameStateEngine(new MockDisplay());
        }

        [TestMethod]
        public void Given_ValidMoves_GameStateProgresses()
        {
            var gameState = _gameStateEngine.Start();

            Assert.IsTrue(gameState.Player1Turn);
            _gameStateEngine.Instruct(gameState, "4");
            Assert.IsFalse(gameState.Player1Turn);
            _gameStateEngine.Instruct(gameState, "7");

            Assert.IsTrue(gameState.GameNotation == "0123X56O8");

            Assert.IsTrue(gameState.Player1Turn);
            _gameStateEngine.Instruct(gameState, "0");
            Assert.IsFalse(gameState.Player1Turn);
            _gameStateEngine.Instruct(gameState, "8");

            Assert.IsTrue(gameState.GameNotation == "X123X56OO");
        }

        [TestMethod]
        public void Given_ValidMoves_CatWins()
        {
            var gameState = _gameStateEngine.Start();
            var moves = "0436217";
            var toggle = true;
            foreach (var c in moves)
            {
                Assert.IsTrue(gameState.Player1Turn == toggle);
                _gameStateEngine.Instruct(gameState, c.ToString());
                toggle = !toggle;
            }
            Assert.IsFalse(gameState.Player1Winner.HasValue);
            Assert.IsTrue(gameState.CatWins);
            Assert.IsTrue(gameState.GameOver);
        }

        [TestMethod]
        public void Given_ValidMoves_XWins()
        {
            var gameState = _gameStateEngine.Start();
            var moves = "03142";
            var toggle = true;
            foreach (var c in moves)
            {
                Assert.IsTrue(gameState.Player1Turn == toggle);
                _gameStateEngine.Instruct(gameState, c.ToString());
                toggle = !toggle;
            }
            Assert.IsTrue(gameState.Player1Winner.Value);
            Assert.IsTrue(gameState.GameOver);
        }

        [TestMethod]
        public void Given_ValidMoves_OWins()
        {
            var gameState = _gameStateEngine.Start();
            var moves = "036425";
            var toggle = true;
            foreach (var c in moves)
            {
                Assert.IsTrue(gameState.Player1Turn == toggle);
                _gameStateEngine.Instruct(gameState, c.ToString());
                toggle = !toggle;
            }
            Assert.IsFalse(gameState.Player1Winner.Value);
            Assert.IsTrue(gameState.GameOver);
        }
    }
}