using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToeEngine
{
    public class ScenarioEngine
    {
        public List<Scenarios> Scenarios;

        public ScenarioEngine()
        {
            Scenarios = Enum.GetValues(typeof(Scenarios)).Cast<Scenarios>().ToList();
        }

        public string GetScenario(GameState gameState, Scenarios scenario)
        {
            var charArray = gameState.GameNotation.ToCharArray();
            switch (scenario)
            {
                case TicTacToeEngine.Scenarios.VertOne:
                    return string.Concat(charArray[0], charArray[3], charArray[6]);

                case TicTacToeEngine.Scenarios.VertTwo:
                    return string.Concat(charArray[1], charArray[4], charArray[7]);

                case TicTacToeEngine.Scenarios.VertThree:
                    return string.Concat(charArray[2], charArray[5], charArray[8]);

                case TicTacToeEngine.Scenarios.HorizOne:
                    return string.Concat(charArray[0], charArray[1], charArray[2]);

                case TicTacToeEngine.Scenarios.HorizTwo:
                    return string.Concat(charArray[3], charArray[4], charArray[5]);

                case TicTacToeEngine.Scenarios.HorizThree:
                    return string.Concat(charArray[6], charArray[7], charArray[8]);

                case TicTacToeEngine.Scenarios.DiagOne:
                    return string.Concat(charArray[0], charArray[4], charArray[8]);

                case TicTacToeEngine.Scenarios.DiagTwo:
                    return string.Concat(charArray[6], charArray[4], charArray[3]);

                default:
                    return string.Empty;
            }
        }
    }
}