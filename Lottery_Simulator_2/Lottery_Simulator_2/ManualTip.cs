//-----------------------------------------------------------------------
// <copyright file="ManualTip.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Compares numbers the user choses with random generated numbers and calculates how many numbers are equal.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class compares numbers the user choses with random generated numbers and 
    /// calculates how many numbers are equal. Hands it all over to the Renderer to display an evaluation.
    /// </summary>
    public class ManualTip : Mode
    {
        /// <summary>
        /// The numbers the user has chosen.
        /// </summary>
        private int[] chosenNumbers;

        /// <summary>
        /// Initializes a new instance of the ManualTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public ManualTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : 
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// Asks the user about the numbers he/she want to use. Hands it over to the Evaluate method.
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
                this.Evaluate();
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
        /// Compares how many numbers of the user's numbers and the random numbers are equal. 
        /// Looks up if the bonus number (last number of the random numbers) is hit or not.
        /// Hands it to the renderer.
        /// </summary>
        private void Evaluate()
        {
            // evaluation
            int[] randomNumbers = this.Lotto.NumberGen.Generate(this.Lotto.Amount + 1, this.Lotto.Min, this.Lotto.Max);
            int equalNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, randomNumbers);
            bool bonusNumber = this.Lotto.NumberChecker.CompareBonus(this.chosenNumbers, randomNumbers);

            // evaluation display
            this.Lotto.Render.SetConsoleSettings();
            this.Lotto.Render.DisplayHeader(this.Title);
            this.Lotto.Render.DisplayEvaluation(this.chosenNumbers, randomNumbers, equalNumbers, bonusNumber);
        }
    }
}
