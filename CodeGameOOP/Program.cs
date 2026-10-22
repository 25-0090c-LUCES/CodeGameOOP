using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGameOOP
{//git
    public class GuessAttempt
    {
        public string Guess { get; set; }
        public int CorrectDigits { get; set; }
        public int CorrectPositions { get; set; }
        public string RandomNums;
        public GuessAttempt(string g, int digits, int positions)
        {
            this.Guess = g;
            this.CorrectDigits = digits;
            this.CorrectPositions = positions;

        }
    }

    public class CodeBreakerGame
    {
        private string secretCode;
        private GuessAttempt[] history;

        public int AttemptsUsed { get; private set; }
        public bool IsGameOver { get; private set; }
        public bool IsGameWon { get; private set; }


        public CodeBreakerGame()
        {

            this.history = new GuessAttempt[6];
            this.AttemptsUsed = 0;
            this.IsGameOver = false;
            this.IsGameWon = false;
            GenerateSecretCode();
            RevealSecretCode();
            ShowHistory();


        }


        private void GenerateSecretCode()
        {
            Random RNG = new Random();
            Random RNG2 = new Random();
            Random RNG3 = new Random();
            int Rn1;
            int Rn2;
            int Rn3;
            Rn1 = RNG.Next(1, 7);
            while (true)
            {
                Rn2 = RNG2.Next(1, 7);
                if (Rn1 == Rn2)
                {
                    Rn2 = RNG2.Next(1, 7);
                }
                else
                {
                    break;
                }
            }
            while (true)
            {
                Rn3 = RNG3.Next(1, 7);

                if (Rn1 == Rn3 || Rn2 == Rn3)
                {
                    Rn3 = RNG3.Next(1, 7);
                }
                else
                {
                    break;
                }
            }
            secretCode = $"{Rn1}{Rn2}{Rn3}";

        }

        public bool IsValidGuess(string playerGuess)
        {

            if (playerGuess.Length != 3)
            {
                Console.WriteLine("Invalid input.");
                return false;
            }
            else if (playerGuess[0] < '1' || playerGuess[0] > '6' || playerGuess[1] < '1' || playerGuess[1] > '6' || playerGuess[2] < '1' || playerGuess[2] > '6')
            {
                return false;
            }
            else if (playerGuess[0] == playerGuess[1] || playerGuess[0] == playerGuess[2] || playerGuess[1] == playerGuess[2])
            {
                return false;
            }
            return true;


        }

        public GuessAttempt ProcessGuess(string playerGuess)
        {
            int maxattempts = 6;
            int digits = 0;
            int positions = 0;
            for (int i = 0; i < playerGuess.Length; i++)
            {

                if (secretCode[i] == playerGuess[i])
                {

                    positions++;
                }
               
                    if (secretCode.Contains( playerGuess[i]))
                    {
                        digits++;
                    }
                
            }

            AttemptsUsed++;


            if (positions == 3)
            {
               
                IsGameWon = true;
                IsGameOver = true;


            }
            if (AttemptsUsed == maxattempts)
            {
               
                IsGameOver = true;
            }

            GuessAttempt countg = new GuessAttempt(playerGuess, digits, positions);
            history[AttemptsUsed-1]=countg;
            return countg;
        }

        public void ShowHistory()
        {
            Console.WriteLine("\n--- Guess Feedback & History ---");
            for (int i = 0; i < AttemptsUsed; i++)
            {
                Console.WriteLine($"Attempt {i + 1}: {history[i].Guess} | Correct Digits: {history[i].CorrectDigits} | Correct Positions: {history[i].CorrectPositions}");
            }
            Console.WriteLine("--------------------------------\n");
        }

        public string RevealSecretCode()
        {
            return secretCode;
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            CodeBreakerGame game = new CodeBreakerGame();

            while (!game.IsGameOver)
            {
                Console.WriteLine("Attempts used:" + game.AttemptsUsed);
                Console.WriteLine("Enter Guess: ");
                string playerGuess = Console.ReadLine();
                if (!game.IsValidGuess(playerGuess))
                {
                    Console.WriteLine("Invalid");
                    continue;
                }
                else
                {
                    game.ProcessGuess(playerGuess);
                }
                game.ShowHistory();

                if (game.IsGameWon)
                {
                    Console.WriteLine("Woah Amazing! You Guessed the correct number");

                }
                else if (game.IsGameOver == true)
                {
                    Console.WriteLine("\nLimit of Attempts Reached. The correct number was: " + game.RevealSecretCode());
                }

            }

        }
    }

}

