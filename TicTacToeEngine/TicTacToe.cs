
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TicTacToeEngine {
	public class TicTacToe {
		private IDisplay Display;

		public string GameNotation { get; private set; } = "012345678";
		public string InstructionHistory { get; private set; }
		public bool Quit { get; private set; } = false;
		public bool Player1Turn { get; private set; } = true;
		public bool GameOver { get; private set; } = false;
		public bool? Player1Winner { get; private set; }
		public bool CatWins { get; private set; } = false;
		
		private const char X = 'X';
		private const char O = 'O';

		public TicTacToe(IDisplay display) {
			this.Display = display;
		}

		public void Start(string gameState) {
			this.setupGame(gameState);
			this.drawBoard();
        }

		public void Instruct(string command){
			if (this.Player1Winner.HasValue) {
				this.Display.GameOverMessage(this.Player1Winner);
				return;
			}
			var c = command.ToLower();
			if (c == Commands.New.ToString().ToLower()) {
				this.Start(string.Empty);
				return;
			}
			if (c == Commands.Quit.ToString().ToLower()) {
				this.Quit = true;
				return;
			}
			var square = 0;
			int.TryParse(c, out square);
			if (square > 10 || square < 0) {
				this.Display.MoveErrorMessage(c, this.Player1Turn);
				return;
			}
			this.InstructionHistory += c;
            this.alterGameState(square);
			var isGameOver = this.isGameOver();
			if (isGameOver) {
				this.Display.GameOverMessage(this.Player1Winner);
				return;
			}
			var nextPersonTurn = this.getPlayerTurn();
			if (nextPersonTurn == this.Player1Turn) {
				//there was an attempt to make an invalid move. no need to update anything
				return;
			}
			//normal move
			this.Player1Turn = nextPersonTurn;
			this.drawBoard();
		}

		private bool isGameOver() {
			//examine all 8 conditions for sameness
			var playerChar = this.Player1Turn ? X : O;
			var scenarios = Enum.GetValues(typeof(Scenarios)).Cast<Scenarios>();
			var scenarioDictionary = new Dictionary<Scenarios, bool>();
			foreach (var scenario in scenarios) {
				var scenarioText = this.getScenario(scenario);
				var doesWin = this.doesWin(playerChar, scenarioText);
				if (doesWin) {//if it is p1 turn, p1 wins, else p2 wins
					this.Player1Winner = this.Player1Turn;
					return true;
				}
				var _canWin = this.canWin(playerChar, scenarioText);
				scenarioDictionary.Add(scenario, _canWin);
            }
			if (!scenarioDictionary.Any(a => a.Value == true)) {
				this.CatWins = true;
				return true;
			}
			if (scenarioDictionary.Where(a => a.Value == true).Count() == 1) {
				var scenarioText = this.getScenario(scenarioDictionary.Where(a => a.Value == true).First().Key);
				var isLastMove = (scenarioText.Count(a => a == O) + scenarioText.Count(a => a == X)) == 2;
				if (!isLastMove) {
					this.CatWins = true;
					return true;
				}
			}
			return false;
		}

		private bool doesWin(char playerChar, string scenarioText) {
			return scenarioText.Where(a => a == playerChar).Count() == 3;
		}

		private bool canWin(char playerChar, string scenarioText) {
			return !(scenarioText.Contains(X) && scenarioText.Contains(O));
		}

		private string getScenario(Scenarios scenario) {
			var charArray = this.GameNotation.ToCharArray();
            switch (scenario) {
				case Scenarios.VertOne:
					return string.Concat(charArray[0], charArray[3], charArray[6]);
				case Scenarios.VertTwo:
					return string.Concat(charArray[1], charArray[4], charArray[7]);
				case Scenarios.VertThree:
					return string.Concat(charArray[2], charArray[5], charArray[8]);
				case Scenarios.HorizOne:
					return string.Concat(charArray[0], charArray[1], charArray[2]);
				case Scenarios.HorizTwo:
					return string.Concat(charArray[3], charArray[4], charArray[5]);
				case Scenarios.HorizThree:
					return string.Concat(charArray[6], charArray[7], charArray[8]);
				case Scenarios.DiagOne:
					return string.Concat(charArray[0], charArray[4], charArray[8]);
				case Scenarios.DiagTwo:
					return string.Concat(charArray[6], charArray[4], charArray[3]);
				default:
					return string.Empty;
			}
		}

		public string RequestCommand() {
			var command = Console.ReadLine();
			return command;
		}

		private void setupGame(string gameState) {
			if (string.IsNullOrEmpty(gameState)) {
				return;
			}
			if (!this.isGameStateValid(gameState)) {
				this.Display.InvalidGameState(gameState);
				return;
			}
			this.GameNotation = gameState;
			this.Player1Turn = this.getPlayerTurn();
		}

		private bool getPlayerTurn() {
			var xCount = this.GameNotation.Where(a => a == X).Count();
			var oCount = this.GameNotation.Where(a => a == O).Count();
			if (xCount == 0 && oCount == 0) {
				return true;
			}
			if (xCount > oCount) {
				return false;
			}
			if (xCount == oCount) {
				return true;
			}
			if (xCount < oCount) {
				this.Display.IncorrectGameStateMessage();
				return false;
			}
			//should not occur
			return true;
		}

		private bool isGameStateValid(string gameState) {
			var pattern = "[0-9xoXO]{9}";
			var regEx = new Regex(pattern);
			return regEx.IsMatch(gameState);
        }

		private void drawBoard() {
			this.Display.Draw(this.GameNotation);
			this.Display.MoveInstructions(this.Player1Turn);
		}

		private void alterGameState(int square) {
			this.GameNotation = this.GameNotation.Replace(square.ToString()[0], this.Player1Turn ? X : O);
		}
	}
}