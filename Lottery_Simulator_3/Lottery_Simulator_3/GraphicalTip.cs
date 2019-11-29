//-----------------------------------------------------------------------
// <copyright file="GraphicalTip.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the GraphicalTip class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the GraphicalTip mode.
    /// It displays the user all numbers he can choose in cells and can switch through the cells with the arrow keys.
    /// After choosing the specified amount of numbers from the current system, it displays the evaluation of how many numbers are in common with the drawn numbers.
    /// </summary>
    public class GraphicalTip : Mode, IEvaluable, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalTip"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public GraphicalTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// This method displays  .....
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Renderer.SetConsoleSettings(90, 40);
            this.Lotto.Renderer.DisplayHeader(this.Title, 3, 1);
            Console.ReadKey();

            // PerformPreEvaluation();
            // PerformPastEvaluation(); 
        }

        /// <summary>
        /// Examines all the calculations that have to be done before evaluating.
        /// </summary>
        public void PerformPreEvaluation()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Examines all calculations that have to be done to display the evaluation and displays the evaluation.
        /// </summary>
        public void PerformEvaluation()
        {
            throw new NotImplementedException();
        }
    }
}
