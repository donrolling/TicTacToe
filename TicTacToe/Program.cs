using System;
using System.Threading;
using TicTacToeDisplay;
using TicTacToeEngine;

namespace TicTacToeConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gameStateEngine = new GameStateEngine(new ConsoleDisplay());
            var gameState = gameStateEngine.Start();
            while (!gameState.Quit)
            {
                var command = gameStateEngine.RequestCommand();
                gameStateEngine.Instruct(gameState, command);
            }
            Console.WriteLine("Goodbye!");
            Thread.Sleep(3000);
        }
    }
}