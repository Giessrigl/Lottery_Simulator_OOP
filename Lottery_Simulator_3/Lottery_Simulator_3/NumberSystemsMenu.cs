//-----------------------------------------------------------------------
// <copyright file="NumberSystemsMenu.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the NumberSystemsMenu mode.
// It initializes the mode objects from one of the menus.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System.Collections.Generic;

    /// <summary>
    /// This class is the manager of the mode objects that are used in one of the menus.
    /// </summary>
    public class NumberSystemsMenu : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberSystemsMenu"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public NumberSystemsMenu(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto)
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
        /// Initializes the Mode objects and puts them into a List of modes.
        /// </summary>
        /// <returns>The List of the initialized mode objects.</returns>
        private List<Mode> CreateOptions()
        {
            List<Mode> options = new List<Mode>();
            char[] uniqueChars = new char[options.Count];

            options.Add(new CurrentNumberSystems("List current available number systems", 'P', uniqueChars, this.Lotto));
            options.Add(new NumberSystemAdder("Add number system", 'A', uniqueChars, this.Lotto));
            options.Add(new NumberSystemChanger("Change number system", 'C', uniqueChars, this.Lotto));
            options.Add(new NumberSystemDeletion("Delete number system", 'L', uniqueChars, this.Lotto));
            options.Add(new OptionsMenu("Options menu", 'Z', uniqueChars, this.Lotto));

            return options;
        }
    }
}
