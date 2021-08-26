using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TicTacToeEngine
{
    public class GameStateEngine
    {
        private const char O = 'O';
        private const char X = 'X';

        private IDisplay _display;
        private ScenarioEngine _scenarioEngine;

        public GameStateEngine(IDisplay display)
        {
            _display = display;
            _scenarioEngine = new ScenarioEngine();
        }

        public void Instruct(GameState gameState, string command)
        {
            if (gameState.Player1Winner.HasValue)
            {
                _display.GameOverMessage(gameState.Player1Winner);
                return;
            }
            var c = command.ToLower();
            if (c == Commands.New.ToString().ToLower())
            {
                Start(string.Empty);
                return;
            }
            if (c == Commands.Quit.ToString().ToLower())
            {
                gameState.Quit = true;
                return;
            }
            var square = 0;
            int.TryParse(c, out square);
            if (square > 10 || square < 0)
            {
                _display.MoveErrorMessage(c, gameState.Player1Turn);
                return;
            }
            gameState.InstructionHistory += c;
            AlterGameState(gameState, square);
            var isGameOver = IsGameOver(gameState);
            if (isGameOver)
            {
                _display.GameOverMessage(gameState.Player1Winner);
                return;
            }
            var nextPersonTurn = GetPlayerTurn(gameState);
            if (nextPersonTurn == gameState.Player1Turn)
            {
                //there was an attempt to make an invalid move. no need to update anything
                return;
            }
            //normal move
            gameState.Player1Turn = nextPersonTurn;
            DrawBoard(gameState);
        }

        public string RequestCommand()
        {
            return _display.RequestCommand();
        }

        public GameState Start(string gameStateString = "012345678")
        {
            var gameState = setupGame(gameStateString);
            DrawBoard(gameState);
            return gameState;
        }

        private void AlterGameState(GameState gameState, int square)
        {
            gameState.GameNotation = gameState.GameNotation.Replace(square.ToString()[0], gameState.Player1Turn ? X : O);
        }

        private bool CanWin(char playerChar, string scenarioText)
        {
            return !(scenarioText.Contains(X) && scenarioText.Contains(O));
        }

        private bool DoesWin(char playerChar, string scenarioText)
        {
            return scenarioText.Count(a => a == playerChar) == 3;
        }

        private void DrawBoard(GameState gameState)
        {
            _display.Draw(gameState.GameNotation);
            _display.MoveInstructions(gameState.Player1Turn);
        }

        private bool GetPlayerTurn(GameState gameState)
        {
            var xCount = gameState.GameNotation.Where(a => a == X).Count();
            var oCount = gameState.GameNotation.Where(a => a == O).Count();
            if (xCount == 0 && oCount == 0)
            {
                return true;
            }
            if (xCount > oCount)
            {
                return false;
            }
            if (xCount == oCount)
            {
                return true;
            }
            if (xCount < oCount)
            {
                _display.IncorrectGameStateMessage();
                return false;
            }
            //should not occur
            return true;
        }

        private bool IsGameOver(GameState gameState)
        {
            // examine all 8 conditions for sameness
            var playerChar = gameState.Player1Turn ? X : O;
            var scenarioDictionary = new Dictionary<Scenarios, bool>();
            foreach (var scenario in _scenarioEngine.Scenarios)
            {
                var scenarioText = _scenarioEngine.GetScenario(gameState, scenario);
                var doesWin = DoesWin(playerChar, scenarioText);
                if (doesWin)
                { // if it is p1 turn, p1 wins, else p2 wins
                    gameState.Player1Winner = gameState.Player1Turn;
                    return true;
                }
                var canWin = CanWin(playerChar, scenarioText);
                scenarioDictionary.Add(scenario, canWin);
            }
            if (!scenarioDictionary.Any(a => a.Value == true))
            {
                gameState.CatWins = true;
                return true;
            }
            if (scenarioDictionary.Where(a => a.Value == true).Count() == 1)
            {
                var scenarioText = _scenarioEngine.GetScenario(gameState, scenarioDictionary.Where(a => a.Value == true).First().Key);
                var isLastMove = (scenarioText.Count(a => a == O) + scenarioText.Count(a => a == X)) == 2;
                if (!isLastMove)
                {
                    gameState.CatWins = true;
                    return true;
                }
            }
            return false;
        }

        private bool IsValidGameState(string gameState)
        {
            var pattern = "[0-9xoXO]{9}";
            var regEx = new Regex(pattern);
            return regEx.IsMatch(gameState);
        }

        private GameState setupGame(string gameStateString)
        {
            if (string.IsNullOrEmpty(gameStateString))
            {
                throw new ArgumentException("Game state is empty!");
            }
            if (!IsValidGameState(gameStateString))
            {
                _display.InvalidGameState(gameStateString);
                throw new ArgumentException("Game state is invalid!");
            }
            var gameState = new GameState();
            gameState.GameNotation = gameStateString;
            gameState.Player1Turn = GetPlayerTurn(gameState);
            return gameState;
        }
    }
}