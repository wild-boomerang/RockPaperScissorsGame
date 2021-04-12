using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace RockPaperScissorsGame
{
    class Game
    {
        private readonly string[] moves;
        private int userMove;
        private int computerMove;
        private readonly Dictionary<Winner, string> winnerMessages;
        private enum Winner
        {
            Computer,
            User,
            Draw
        }
        private enum ArgumentTypeError
        {
            SmallLength,
            EvenLength,
            RepeatingElement
        }

        public Game(string[] moves)
        {
            this.moves = moves;
            winnerMessages = new Dictionary<Winner, string>()
            {
                [Winner.Computer] = "Computer wins!",
                [Winner.User] = "You win!",
                [Winner.Draw] = "Draw!"
            };
        }
        
        public void Run()
        {
            if (ValidateMoves())
            {
                GenerateHmacAndSecretKey(out string secretKey, out string hmac);
                Console.WriteLine($"HMAC:\n{hmac}");
                
                userMove = UserMove();
                if (userMove != 0)
                {
                    userMove--;
                    PrintGameResult(secretKey);
                }
            }
        }

        private void GenerateHmacAndSecretKey(out string secretKey, out string hmac)
        {
            secretKey = HmacHelper.GenerateSecretKey(16);
            computerMove = GetComputerMove();
            hmac = HmacHelper.GenerateHmac(secretKey, moves[computerMove]);
        }

        private void PrintGameResult(string secretKey)
        {
            Console.WriteLine($"Your move: {moves[userMove]}");
            Console.WriteLine($"Computer move: {moves[computerMove]}");

            PrintWinner(FindWinner());

            Console.WriteLine($"HMAC key: {secretKey}");
        }

        private void PrintWinner(Winner winner)
        {
            Console.WriteLine(winnerMessages[winner]);
        }

        private int GetComputerMove()
        {
            return RandomNumberGenerator.GetInt32(moves.Length);
        }

        private Winner FindWinner()
        {            
            int half = moves.Length / 2;
            if (userMove == computerMove)
            {
                return Winner.Draw;
            }
            else if (Math.Abs(userMove - computerMove) <= half) // the greatest wins
            {
                return (Math.Max(userMove, computerMove) == userMove) ? Winner.User : Winner.Computer;
            }
            else // the smallest wins
            {
                return (Math.Min(userMove, computerMove) == userMove) ? Winner.User : Winner.Computer;
            }
        }

        private int UserMove()
        {
            int move = -1;
            while (move == -1)
            {
                PrintAvailableMoves();
                move = CatchUserInput();
            }
            return move;
        }

        private int CatchUserInput()
        {
            Console.Write("Enter your move: ");
            if (Int32.TryParse(Console.ReadLine(), out int move))
            {
                return (move <= moves.Length && move >= 0) ? move : -1;
            }
            else
            {
                return -1;
            }
        }

        private void PrintAvailableMoves()
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Length; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }
            Console.WriteLine("0 - exit");
        }

        private bool ValidateMoves()
        {
            Dictionary<ArgumentTypeError, string> errors = new Dictionary<ArgumentTypeError, string>();
            if (moves.Length < 3)
            {
                errors[ArgumentTypeError.SmallLength] = "Arguments count must be 3 or greater.";
            }
            if (moves.Length % 2 == 0)
            {
                errors[ArgumentTypeError.EvenLength] = "Arguments count must be odd.";
            }
            foreach (string arg in moves)
            {
                if (Array.FindAll(moves, value => value == arg).Length > 1)
                {
                    errors[ArgumentTypeError.RepeatingElement] = "Arguments must not be repeated.";
                    break;
                }
            }
            PrintErrors(errors);
            return (errors.Count == 0);
        }
        
        private void PrintErrors(Dictionary<ArgumentTypeError, string> errors)
        {
            if (errors.Count == 0)
            {
                return;
            }
            Console.WriteLine("\nError in arguments:");
            foreach (var message in errors.Values)
            {
                Console.WriteLine(" *" + message);
            }
            Console.WriteLine("\nAn example of correct arguments input:\n" +
                              "<filepath> rock paper scissors lizard Spock");
        }
    }
}
