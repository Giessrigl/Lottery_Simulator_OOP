//-----------------------------------------------------------------------
// <copyright file="ApplicationExit.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the ApplicationExit class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for quitting the application.
    /// </summary>
    public class ApplicationExit : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationExit"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public ApplicationExit(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// This method asks the user if he/she wants to quit the game.
        /// After the user pressed the wright button, the application stops.
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Renderer.SetConsoleSettings(55, 20);
            this.Lotto.Renderer.DisplayHeader(this.Title, 3, 1);

            this.Lotto.Renderer.DisplayExitRequest(3, 4);

            if (this.Lotto.KeyChecker.WaitForYesNo())
            {
                Environment.Exit(0);
            }
        }
    }
}
