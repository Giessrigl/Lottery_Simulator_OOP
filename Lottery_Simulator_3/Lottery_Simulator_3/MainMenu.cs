//-----------------------------------------------------------------------
// <copyright file="MainMenu.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This file is for the main menu class.
// It initializes the mode objects from one of the menus.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System.Collections.Generic;

    /// <summary>
    /// This class is the manager of the mode objects that are used in one menu.
    /// </summary>
    public class MainMenu : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public MainMenu(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
        }

        /// <summary>
        /// This method sets this menu as the current menu and displays it in the console.
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Modes = this.CreateModes(this.Lotto);
            this.Lotto.CurrentMenu = this;

            this.Render.SetConsoleSettings(90, 35);
            this.Render.DisplayHeader(this.Title, 3, 1);
            this.Render.DisplayMenu(this.Lotto.Modes, 3, 4);
        }

        /// <summary>
        /// Initializes the Mode objects and puts them into an List of modes.
        /// </summary>
        /// <param name="lotto">The Lottery variable that should used.</param>
        /// <returns>The list of the initialized mode objects.</returns>
        private List<Mode> CreateModes(Lottery lotto)
        {
            List<Mode> modeOptions = new List<Mode>();
            char[] uniqueChars = new char[modeOptions.Count];

            modeOptions.Add(new ManualTip("Manual Tip", 'M', uniqueChars, lotto));
            modeOptions.Add(new QuickTip("Quick Tip", 'Q', uniqueChars, lotto));
            modeOptions.Add(new GraphicalTip("Graphical Tip", 'G', uniqueChars, lotto));
            modeOptions.Add(new JackpotSimulation("Jackpot Simulation", 'S', uniqueChars, lotto));
            modeOptions.Add(new FrequencyDetermination("Determine Frequencies", 'H', uniqueChars, lotto));
            modeOptions.Add(new OptionsMenu("Options menu", 'O', uniqueChars, lotto));
            modeOptions.Add(new ApplicationExit("Application Extermination", 'B', uniqueChars, lotto));

            return modeOptions;
        }
    }
}
