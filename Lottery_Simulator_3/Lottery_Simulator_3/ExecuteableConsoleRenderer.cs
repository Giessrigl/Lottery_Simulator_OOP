//-----------------------------------------------------------------------
// <copyright file="ExecuteableConsoleRenderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the ExecuteableConsoleRenderer class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This class contains all important methods that the executable modes need for displaying their intermediate steps and their evaluation. 
    /// </summary>
    public class ExecuteableConsoleRenderer : DefaultConsoleRenderer
    {
        /// <summary>
        /// Displays a number on the console.
        /// </summary>
        /// <param name="number">The number should be displayed.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayEvaluationNumber(int number, int offsetLeft, int offsetTop)
        {
            this.OverwriteBlank(55, 0, offsetTop);
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor($"{number}", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Displays the evaluation text without the numbers that are drawn or chosen.
        /// </summary>
        /// <param name="bonusNumbers">The amount of bonus numbers.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
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
        /// <param name="equalBonusNumbers">The amount of bonus numbers that are in common.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayOutcomeOwnPool(int equalNumbers, int equalBonusNumbers, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);

            if (equalNumbers > 2 && equalBonusNumbers == 1)
            {
                Console.Write($"You hit {equalNumbers} numbers and a bonus number!");
            }
            else if (equalNumbers > 2 && equalBonusNumbers > 1)
            {
                Console.Write($"You hit {equalNumbers} numbers and {equalBonusNumbers} bonus numbers!");
            }
            else if (equalNumbers > 2)
            {
                Console.Write($"You hit {equalNumbers} numbers!");
            }
            else if (equalBonusNumbers == 1)
            {
                Console.Write("You hit a bonus number!");
            }
            else if (equalBonusNumbers > 1)
            {
                Console.Write($"You hit {equalBonusNumbers} bonus numbers!");
            }
            else
            {
                Console.Write("You drew a blank.");
            }
        }

        /// <summary>
        /// Displays the evaluation of how many numbers are hit (starts to write at 3 equal numbers) and how many bonus numbers are hit.
        /// </summary>
        /// <param name="equalNumbers">The amount of numbers that are in common.</param>
        /// <param name="equalBonusNumbers">The amount of bonus numbers that are in common.</param>
        /// <param name="maxNumbers">The amount of the maximum numbers that can be equal.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
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
                Console.Write($"You hit {equalNumbers} numbers and a bonus number!");
            }
            else if (equalNumbers > 2 && equalNumbers < maxNumbers)
            {
                Console.Write($"You hit {equalNumbers} numbers!");
            }
            else if (equalBonusNumbers == 1)
            {
                Console.Write("You hit a bonus number!");
            }
            else if (equalBonusNumbers > 1)
            {
                Console.Write($"You hit {equalBonusNumbers} bonus numbers!");
            }
            else
            {
                Console.Write("You drew a blank.");
            }
        }

        /// <summary>
        /// Writes the user's numbers into the console window and leaves the draws until jackpot line with three spots.
        /// </summary>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        /// <exception cref="ArgumentNullException">The chosen numbers array is null.</exception>
        public void ShowJackpotDisplay(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("Your numbers: ");

            Console.SetCursorPosition(offsetLeft, offsetTop + 2);
            Console.Write("Draws until jackpot: ");
            this.WriteInColor("...", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Displays how many iterations it needed to be a jackpot.
        /// </summary>
        /// <param name="iterations">The amount of iterations.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayJackpotIterations(int iterations, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("    ");
            Console.SetCursorPosition(offsetLeft + 1, offsetTop);
            this.WriteInColor($"{iterations}", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Displays a content with a bar for the frequency and the frequency of the content in the console.
        /// </summary>
        /// <param name="content">The content of the cell.</param>
        /// <param name="frequence">The frequency of the content.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayFrequencyCell(string content, int frequence, int offsetLeft, int offsetTop)
        {
            if (content == null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            this.OverwriteBlank(55, offsetLeft, offsetTop);

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write($"{content} |                |");

            Console.SetCursorPosition(offsetLeft + content.Length + 2, offsetTop);
            for (int i = 0; i < frequence; i++)
            {
                Console.Write("+");
            }
        }

        /// <summary>
        /// Updates the status of the current iteration and the percentage of the whole process.
        /// </summary>
        /// <param name="draw">The amount of draws until now.</param>
        /// <param name="iterations">The amount of iterations that have to be done.</param>
        /// <param name="percentage">The percentage of the whole process.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayFrequencyStatus(int draw, int iterations, double percentage, int offsetLeft, int offsetTop)
        {
            this.OverwriteBlank(55, 0, offsetTop);
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("Iterations: ");
            Console.Write($"{draw} / {iterations}");
            Console.Write($" ({percentage}%)");
        }

        /// <summary>
        /// Displays a rectangle with a number as content (depending how long the number is) in the console window.
        /// </summary>
        /// <param name="number">The content of the cell.</param>
        /// <param name="backgroundColor">The background color of the cell.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayGraphicalCell(int number, ConsoleColor backgroundColor, int offsetLeft, int offsetTop)
        {
            this.OverwriteBlank(55, 0, offsetTop);
            this.OverwriteBlank(55, 0, offsetTop + 1);
            this.OverwriteBlank(55, 0, offsetTop + 2);

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("+");
            for (int i = 0; i < number.ToString().Length + 2; i++)
            {
                Console.Write("-");
            }

            Console.Write("+");

            Console.SetCursorPosition(offsetLeft, offsetTop + 1);
            Console.Write("|");
            this.WriteInColor($" {number} ", ConsoleColor.White, backgroundColor);
            Console.Write("|");

            Console.SetCursorPosition(offsetLeft, offsetTop + 2);
            Console.Write("+");
            for (int i = 0; i < number.ToString().Length + 2; i++)
            {
                Console.Write("-");
            }

            Console.Write("+");
        }
    }
}
