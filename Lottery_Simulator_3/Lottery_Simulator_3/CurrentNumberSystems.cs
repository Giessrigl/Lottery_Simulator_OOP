//-----------------------------------------------------------------------
// <copyright file="CurrentNumberSystems.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the CurrentNumberSystems class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    /// <summary>
    /// This is a class for the currentNumberSystem mode.
    /// It shows the user the current available number systems he/she has created.
    /// </summary>
    public class CurrentNumberSystems : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentNumberSystems"/> class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public CurrentNumberSystems(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
            this.Renderer = new OptionsConsoleRenderer();
        }

        /// <summary>
        /// Gets or sets the Renderer for this class.
        /// </summary>
        private OptionsConsoleRenderer Renderer
        {
            get;
            set;
        }

        /// <summary>
        /// This method shows the user the current available number systems he/she has created.
        /// </summary>
        public override void Execute()
        {
            this.Renderer.SetConsoleSettings(90, 35);
            this.Renderer.DisplayHeader(this.Title, 3, 1);

            this.Renderer.DisplayNumberSystems(this.Lotto.NumberSystems, 5, 4);

            this.Lotto.KeyChecker.WaitForEnter();
        }
    }
}
