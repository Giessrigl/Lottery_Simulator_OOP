//-----------------------------------------------------------------------
// <copyright file="ExitApp.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Stops the application after asking the user.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This is a class to stop the application if the user wants to.
    /// </summary>
    public class ExitApp : Mode
    {
        /// <summary>
        /// Initializes a new instance of the ExitApp class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public ExitApp(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
        }

        /// <summary>
        /// The method that calculates the modes function and calls the rendering methods to display it in the console.
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Render.SetConsoleSettings(92, 35);
            this.Lotto.Render.DisplayHeader(this.Title);
            this.Lotto.Render.DisplayExitRequest();

            if (this.Lotto.KeyChecker.WaitForYesNo())
            {
                Environment.Exit(0);
            }
        }
    }
}