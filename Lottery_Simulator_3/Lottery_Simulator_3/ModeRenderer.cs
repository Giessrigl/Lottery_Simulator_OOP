using System;
using System.Collections.Generic;


namespace Lottery_Simulator_3
{
    public class ModeRenderer : DefaultRenderer
    {
        public void DisplayEvaluationNumber(int number, int offsetLeft, int offsetTop)
        {
            this.OverwriteBlank(90, offsetLeft, offsetTop);
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write($"{number}");
        }

        /// <summary>
        /// Displays the chosen numbers, random numbers, how many numbers are equal (starts at 3) and if the bonus number is hit.
        /// </summary>
        /// <param name="chosenNumbers">The numbers the user has chosen.</param>
        /// <param name="randomNumbers">The random generated numbers.</param>
        /// <param name="equalNumbers">The amount of equal numbers between the chosen and random numbers.</param>
        /// <param name="bonusNumbers">True if the bonus number is hit.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        /// <exception cref="ArgumentNullException">The chosen numbers array of the chosen numbers array is null.</exception>
        public void DisplayEvaluation(int bonusNumbers, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("Your chosen numbers: ");

            Console.SetCursorPosition(offsetLeft, offsetTop + 2);
            Console.Write("Randomnumbers: ");
            if (bonusNumbers > 0)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + 4);
                Console.Write("Random bonus number(s): ");
            }
        }

        /// <summary>
        /// Displays the evaluation of how many numbers are hit (starts to write at 3 equal numbers) and if the bonus number is hit or not.
        /// </summary>
        /// <param name="equalNumbers">The amount of numbers that are in common.</param>
        /// <param name="bonusNumber">True if the bonus number is hit.</param>
        /// <param name="maxNumbers">The amount of the maximum numbers that can be equal.</param>
        public void DisplayOutcomeOwnPool(int equalNumbers, int equalBonusNumbers, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);

            if (equalNumbers > 2 && equalBonusNumbers > 1)
            {
                Console.Write($"You hit {equalNumbers} numbers and {equalBonusNumbers} bonus numbers!");
            }
            else if (equalNumbers > 2 && equalBonusNumbers == 1)
            {
                Console.Write($"You hit {equalNumbers} numbers and the bonus number!");
            }
            else if (equalNumbers > 2)
            {
                Console.Write($"You hit {equalNumbers} numbers!");
            }
            else if (equalBonusNumbers > 1)
            {
                Console.Write($"You hit {equalBonusNumbers} bonus numbers!");
            }
            else if (equalBonusNumbers == 1)
            {
                Console.Write("You hit the bonus number!");
            }
            else
            {
                Console.Write("You drew a blank.");
            }
        }

        /// <summary>
        /// Displays the evaluation of how many numbers are hit (starts to write at 3 equal numbers) and if the bonus number is hit or not.
        /// </summary>
        /// <param name="equalNumbers">The amount of numbers that are in common.</param>
        /// <param name="bonusNumber">True if the bonus number is hit.</param>
        /// <param name="maxNumbers">The amount of the maximum numbers that can be equal.</param>
        public void DisplayOutcomeIdentPool(int equalNumbers, int equalBonusNumbers, int maxNumbers, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);

            if (equalNumbers >= maxNumbers)
            {
                Console.Write("You hit the jackpot!");
            }
            else if (equalNumbers > 2 && equalNumbers < maxNumbers && equalBonusNumbers > 2)
            {
                Console.Write($"You hit {equalNumbers} numbers and {equalBonusNumbers} bonus numbers!");
            }
            else if (equalNumbers > 2 && equalNumbers < maxNumbers && equalBonusNumbers == 1)
            {
                Console.Write($"You hit {equalNumbers} numbers and the bonus number!");
            }
            else if (equalNumbers > 2 && equalNumbers < maxNumbers)
            {
                Console.Write($"You hit {equalNumbers} numbers!");
            }
            else if (equalBonusNumbers > 0)
            {
                Console.Write($"You hit {equalBonusNumbers} bonus number(s)!");
            }
            else
            {
                Console.Write("You drew a blank.");
            }
        }

        /// <summary>
        /// Writes the user's numbers into the console window and leaves the draws until jackpot line with three spots.
        /// </summary>
        /// <param name="chosenNumbers">The numbers the user has chosen.</param>
        /// <exception cref="ArgumentNullException">The chosen numbers array is null.</exception>
        public void ShowJackpotDisplay(int[] chosenNumbers, int offsetLeft, int offsetTop)
        {
            if (chosenNumbers == null)
            {
                throw new ArgumentNullException(nameof(chosenNumbers));
            }

            string values = string.Join(", ", chosenNumbers);

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("Your numbers: ");

            Console.SetCursorPosition(offsetLeft, offsetTop + 1);
            this.WriteInColor(values, ConsoleColor.DarkYellow);

            Console.SetCursorPosition(offsetLeft, offsetTop + 2);
            Console.Write("Draws until jackpot: ");
            this.WriteInColor("...", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Displays how many iterations it needed to be a jackpot.
        /// </summary>
        /// <param name="iterations">The amount of iterations.</param>
        public void DisplayJackpotIterations(int iterations, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("    ");
            Console.SetCursorPosition(offsetLeft + 1, offsetTop);
            this.WriteInColor($"{iterations}", ConsoleColor.DarkYellow);
        }

        public void DrawFrequencyCell(string content, int offsetLeft, int offsetTop)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write($"{content} |                |");
        }

        public void DisplayFrequencyStatus(int iterations, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write($"Iteration:     / {iterations}");
        }

        /// <summary>
        /// Updates the status of the current iteration and the percentage of the whole process.
        /// </summary>
        /// <param name="draw">The amount of draws until now.</param>
        /// <param name="percentage">The percentage of the whole process.</param>
        public void UpdateFrequencyStatus(int draw, double percentage, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write($"{draw}");

            Console.SetCursorPosition(offsetLeft + 11, offsetTop);
            Console.Write($"({percentage} %)");
        }

        /// <summary>
        /// Turns every bar of the frequency grid into a blank bar.
        /// </summary>
        public void DisplayFrequenciesAsBlank(int cellAmount, int offsetLeft, int offsetTop)
        {
            for (int i = 0; i < ((cellAmount + 1) / 3) + 1; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.OverwriteBlank(16, offsetLeft + (23 * j), offsetTop + i);
                }
            }
        }

        /// <summary>
        /// Displays how often the number has been drawn until now into the bar.
        /// </summary>
        /// <param name="frequencies">The amount of how often a number has been drawn relative to the other numbers.</param>
        /// <param name="highestFrequence">The highest frequency possible.</param>
        /// <exception cref="ArgumentNullException">The frequencies array is null.</exception>
        public void DisplayFrequencyEvaluation(int cellAmount, int[] frequencies, int highestFrequence, int offsetLeft, int offsetTop)
        {
            if (frequencies == null)
            {
                throw new ArgumentNullException(nameof(frequencies));
            }

            int fullColumns = (cellAmount) / 3;
            int halfColumns = (cellAmount) % 3;

            int index = 0;
            for (int i = 0; i < fullColumns; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    int numberFrequence = (frequencies[index] * 16) / highestFrequence;
                    Console.SetCursorPosition(offsetLeft + (23 * k), offsetTop + i);
                    if (numberFrequence > 0)
                    {
                        for (int j = 0; j < numberFrequence; j++)
                        {
                            Console.Write("+");
                        }
                    }
                    
                    if (index < cellAmount - 1)
                    {
                        index++;
                    }
                }
            }
            for (int i = 0; i < halfColumns; i++)
            {
                int numberFrequence = (frequencies[index] * 16) / highestFrequence;
                Console.SetCursorPosition(offsetLeft + (23 * i), offsetTop + fullColumns);
                if (numberFrequence > 0)
                {
                    for (int j = 0; j < numberFrequence; j++)
                    {
                        Console.Write("+");
                    }
                }

                if (index < cellAmount - 1)
                {
                    index++;
                }
            }
        }
    }
}
