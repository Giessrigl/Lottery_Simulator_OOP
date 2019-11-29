//-----------------------------------------------------------------------
// <copyright file="ErrorChecker.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the ErrorCheck class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for methods that check the users inputs on errors.
    /// </summary>
    public class ErrorChecker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorChecker"/> class.
        /// </summary>
        /// <param name="lotto">The used Lottery variable.</param>
        public ErrorChecker(Lottery lotto)
        {
            this.Lotto = lotto;
        }

        /// <summary>
        /// Gets or sets the used Lottery variable.
        /// </summary>
        private Lottery Lotto
        {
            get;
            set;
        }

        /// <summary>
        /// Asks the user about an input and checks the users input if it is a Number, if the number is between a range 
        /// and if the number can be put inside an Integer32. As long as the input does not match the criteria, it keeps asking.
        /// </summary>
        /// <param name="question">The message the user gets displayed for choosing an input.</param>
        /// <param name="limit1">The first limit of the number range.</param>
        /// <param name="limit2">The second limit of the number range.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console, where the error messages for the user should be written.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console, where the error messages for the user should be written.</param>
        /// <returns>The number the user wants to use that has all the requirements.</returns>
        public int EvaluateNumberErrors(string question, int limit1, int limit2, int offsetLeft, int offsetTop)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;
            int number;

            do
            {
                string input = this.Lotto.Renderer.GetAnswer(question, offsetLeft, offsetTop);
                this.Lotto.Renderer.OverwriteBlank(150, 0, offsetTop + 1);

                if (input.Length > int.MaxValue.ToString().Length)
                {
                    this.Lotto.Renderer.OverwriteBlank(input.Length + 1, offsetLeft + question.Length, offsetTop);
                    this.Lotto.Renderer.DisplayGeneralError("Your input is too long!", offsetLeft, offsetTop + 1);
                    continue;
                }

                if (!int.TryParse(input, out number))
                {
                    this.Lotto.Renderer.OverwriteBlank(input.Length + 1, offsetLeft + question.Length, offsetTop);
                    this.Lotto.Renderer.DisplayGeneralError("Type in a positive, whole number!", offsetLeft, offsetTop + 1);
                    continue;
                }

                if (!this.Lotto.NumberChecker.IsInRange(number, min, max))
                {
                    this.Lotto.Renderer.OverwriteBlank(input.Length + 1, offsetLeft + question.Length, offsetTop);
                    this.Lotto.Renderer.DisplayUserRangeError(min, max, offsetLeft, offsetTop + 1);
                    continue;
                }
                else
                {
                    break;
                }
            }
            while (true);

            return number;
        }
    }
}
