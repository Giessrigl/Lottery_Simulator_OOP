//-----------------------------------------------------------------------
// <copyright file="DetermineFrequencies.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Displays how often a number is drawn from an amount of draws the user has to specify.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class calculates how often a number is drawn based on the amount of draws and gives them to the renderer. 
    /// </summary>
    public class DetermineFrequencies : Mode
    {
        /// <summary>
        /// Initializes a new instance of the DetermineFrequencies class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>
        public DetermineFrequencies(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : 
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// Asks the user how many draws the program should do.Hands it over to the DetermineSingleFrequency method.
        /// </summary>
        public override void Execute()
        {
            const int MaxIterations = 1000;

            this.Lotto.Render.SetConsoleSettings(92, 35);
            this.Lotto.Render.DisplayHeader(this.Title);

            int stopatline = Console.WindowHeight - 5;
            int lineCount = 0;
            int iterations = 0;
            do
            {
                if (lineCount >= stopatline)
                {
                    break;
                }

                string input = this.Lotto.Render.DisplayIterationsRequest(lineCount);
                lineCount++;

                if (input.Length >= int.MaxValue.ToString().Length)
                {
                    this.Lotto.Render.DisplayInputLengthError(lineCount);
                    lineCount++;
                    continue;
                }

                if (!int.TryParse(input, out iterations))
                {
                    this.Lotto.Render.DisplayUserNumberError(lineCount);
                    lineCount++;
                    continue;
                }

                if (!this.Lotto.NumberChecker.IsInRange(iterations, 1, MaxIterations))
                {
                    this.Lotto.Render.DisplayUserRangeError(1, MaxIterations, lineCount);
                    lineCount++;
                    continue;
                }
                else
                {
                    break;
                }
            }
            while (true);

            if (!(lineCount >= stopatline))
            {
                this.DetermineSingleFrequency(iterations);
                this.Lotto.Render.DisplayReturnIfEnter();
            }
            else
            {
                this.Lotto.Render.ShowErrorQuantityError();
            }

            this.Lotto.KeyChecker.WaitForEnter();
        }

        /// <summary>
        /// Iterates through the draws and updates the console on how often a number is drawn and at which iteration the user is.
        /// </summary>
        /// <param name="iterations">The amount of draws the user has picked.</param>
        private void DetermineSingleFrequency(int iterations)
        {
            this.Lotto.Render.SetConsoleSettings(92, 35);
            this.Lotto.Render.DisplayHeader(this.Title);
            this.Lotto.Render.DisplayFrequencyGrid(iterations);

            int currentIteration = 1;
            int[] frequencies = new int[45];
            int highestFrequence = 0;

            do
            {
                double percentage = Math.Round(((double)currentIteration / (double)iterations) * 100, 2);

                this.Lotto.Render.UpdateFrequencyStatus(currentIteration, percentage);

                int[] iterationNumbers = this.Lotto.NumberGen.Generate(6, this.Lotto.Min, this.Lotto.Max);
                for (int i = 0; i < iterationNumbers.Length; i++)
                {
                    frequencies[iterationNumbers[i] - 1]++;
                }

                this.Lotto.Render.DisplayFrequenciesAsBlank();

                for (int j = 0; j < 45; j++)
                {
                    if (frequencies[j] > highestFrequence)
                    {
                        highestFrequence = frequencies[j];
                    }
                    else if (highestFrequence == currentIteration)
                    {
                        break;
                    }
                }

                this.Lotto.Render.DisplayFrequencyEvaluation(frequencies, highestFrequence);

                currentIteration++;
            }
            while (currentIteration <= iterations);
        }
    }
}
