//-----------------------------------------------------------------------
// <copyright file="OptionsMenu.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the OptionsMenu class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System.Collections.Generic;

    /// <summary>
    /// This class is for the OptionsMenu mode.
    /// It gives the user the opportunity to choose the current number system or manage the current available number systems.
    /// </summary>
    public class OptionsMenu : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsMenu"/> class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public OptionsMenu(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
            this.Renderer = new DefaultConsoleRenderer();
        }

        /// <summary>
        /// Gets or sets the Renderer for this class.
        /// </summary>
        private DefaultConsoleRenderer Renderer
        {
            get;
            set;
        }

        /// <summary>
        /// This method sets this menu as the current menu and displays it in the console.
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Modes = this.CreateOptions();
            this.Lotto.CurrentMenu = this;

            this.Renderer.SetConsoleSettings(65, this.Lotto.Modes.Count + 15);
            this.Renderer.DisplayHeader(this.Title, 3, 1);
            this.Renderer.DisplayMenu(this.Lotto.Modes, 3, 4);
        }

        /// <summary>
        /// Initializes the Mode objects and puts them into an List of modes.
        /// </summary>
        /// <returns>The list of the initialized mode objects.</returns>
        private List<Mode> CreateOptions()
        {
            List<Mode> options = new List<Mode>();
            char[] uniqueChars = new char[options.Count];

            options.Add(new CurrentSystemSetter("Set current number system", 'A', uniqueChars, this.Lotto));
            options.Add(new NumberSystemsMenu("Number systems menu", 'V', uniqueChars, this.Lotto));
            options.Add(new MainMenu("Main menu", 'Z', uniqueChars, this.Lotto));

            return options;
        }
    }
}
