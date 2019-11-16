//-----------------------------------------------------------------------
// <copyright file="JackpotSimulation.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Calculates how many draws it takes to get all numbers correct.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class calculates how long it takes to get a jackpot based on the numbers the user wants to check.
    /// </summary>
    public class JackpotSimulation : Mode
    {
        /// <summary>
        /// The numbers the user has chosen.
        /// </summary>
        private int[] chosenNumbers;

        /// <summary>
        /// Initializes a new instance of the JackpotSimulation class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>
        public JackpotSimulation(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : 
        base(title, abbreviation, uniqueChars, lotto)
        {
        }
        
        /// <summary>
        /// After getting the numbers of the user, it iterates with random generated numbers and compares the every time with the user's numbers.
        /// If all numbers are equal, the iterating stops.
        /// </summary>
        public override void Execute()
        {
            const int Stopatline = 30;

            this.chosenNumbers = new int[this.Lotto.Amount];

            // get user numbers
            this.Lotto.Render.SetConsoleSettings();
            this.Lotto.Render.DisplayHeader(this.Title);

            int lineCount = 0;
            int number = 0;

            for (int i = 0; i < this.chosenNumbers.Length; i++)
            {
                if (lineCount >= Stopatline)
                {
                    break;
                }

                do
                {
                    if (lineCount >= Stopatline)
                    {
                        break;
                    }

                    string input = this.Lotto.Render.DisplayInputRequest(i + 1, lineCount);
                    lineCount++;

                    if (!int.TryParse(input, out number))
                    {
                        this.Lotto.Render.DisplayUserNumberError(lineCount);
                        lineCount++;
                        continue;
                    }

                    if (!this.Lotto.NumberChecker.IsInRange(number, this.Lotto.Min, this.Lotto.Max))
                    {
                        this.Lotto.Render.DisplayUserRangeError(this.Lotto.Min, this.Lotto.Max, lineCount);
                        lineCount++;
                        continue;
                    }

                    if (!this.Lotto.NumberChecker.IsUnique(number, this.chosenNumbers))
                    {
                        this.Lotto.Render.DisplayUserUniqueError(lineCount);
                        lineCount++;
                        continue;
                    }
                    else
                    {
                        this.chosenNumbers[i] = number;
                        break;
                    }
                }
                while (true);
            }

            if (!(lineCount >= Stopatline))
            {
                int iterations = this.DetermineJackpot();
                this.Lotto.Render.DisplayJackpotIterations(iterations);
                this.Lotto.Render.DisplayReturnIfEnter();
            }
            else
            {
                this.Lotto.Render.ShowErrorQuantityError();
            }

            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Generates random numbers and compares them to the user's chosen numbers, if all are equal, the iterating stops.
        /// </summary>
        /// <returns>Whether all user numbers and all random numbers are equal (true) or not (false).</returns>
        private bool CheckJackpot()
        {
            int[] randomNumbers = this.Lotto.NumberGen.Generate(this.Lotto.Amount, this.Lotto.Min, this.Lotto.Max);
            int equalNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, randomNumbers);

            return equalNumbers >= this.Lotto.Amount;
        }

        /// <summary>
        /// Checks if there is a jackpot, if not it counts the iterations up.
        /// </summary>
        /// <returns>The current amount of iterations.</returns>
        private int DetermineJackpot()
        {
            int iterations = 1;

            this.Lotto.Render.SetConsoleSettings();
            this.Lotto.Render.DisplayHeader(this.Title);
            this.Lotto.Render.ShowJackpotDisplay(this.chosenNumbers);
            
            do
            {
                if (!this.CheckJackpot())
                {
                    iterations++;
                }
                else
                {
                    break;
                }
            }
            while (true);

            return iterations;
        }
    }
}
