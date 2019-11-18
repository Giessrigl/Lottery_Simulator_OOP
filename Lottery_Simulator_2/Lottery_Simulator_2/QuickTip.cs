//-----------------------------------------------------------------------
// <copyright file="QuickTip.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Generates the users numbers and random generated ones and compares them.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class compares the random generated numbers for the user with random generated numbers for the lottery and hands them over to the renderer. 
    /// </summary>
    public class QuickTip : Mode
    {
        /// <summary>
        /// Initializes a new instance of the QuickTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>
        public QuickTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : 
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// Compares the random generated numbers for the user with random generated numbers for the lottery and hands them over to the renderer. 
        /// </summary>
        public override void Execute()
        {
            // generating both number arrays
            int[] chosenNumbers = this.Lotto.NumberGen.Generate(this.Lotto.Amount, this.Lotto.Min, this.Lotto.Max);
            int[] randomNumbers = this.Lotto.NumberGen.Generate(this.Lotto.Amount + 1, this.Lotto.Min, this.Lotto.Max);

            // evaluation
            int equalNumbers = this.Lotto.NumberChecker.CompareNumbers(chosenNumbers, randomNumbers);
            bool bonusNumber = this.Lotto.NumberChecker.CompareBonus(chosenNumbers, randomNumbers);

            // evaluation display
            this.Lotto.Render.SetConsoleSettings(92, 35);
            this.Lotto.Render.DisplayHeader(this.Title);
            this.Lotto.Render.DisplayEvaluation(chosenNumbers, randomNumbers, equalNumbers, bonusNumber);
            this.Lotto.Render.DisplayReturnIfEnter();

            // press enter
            this.Lotto.KeyChecker.WaitForEnter();
        }
    }
}
