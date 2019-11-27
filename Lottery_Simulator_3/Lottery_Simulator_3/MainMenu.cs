//-----------------------------------------------------------------------
// <copyright file="ModeAdmin.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Manages the mode objects, initializes them and chooses which mode to call after pressing a defined key in the main menu.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This class is the manager of the mode objects that are used in this simulator.
    /// </summary>
    public class MainMenu : Mode, IExecuteable
    {
        public MainMenu(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
        }

        /// <summary>
        /// Initializes the Mode objects and puts them into an array of modes.
        /// </summary>
        /// <param name="lotto">The Lottery variable that should used.</param>
        /// <returns>The array of the initialized mode objects.</returns>
        public List<Mode> CreateModes(Lottery lotto)
        {
            List<Mode> modeOptions = new List<Mode>();
            char[] uniqueChars = new char[modeOptions.Count];

            modeOptions.Add(new ManualTip("Manual Tip", 'M', uniqueChars, lotto));
            modeOptions.Add(new QuickTip("Quick Tip", 'Q', uniqueChars, lotto));
            modeOptions.Add(new GraphicalTip("Graphical Tip", 'G', uniqueChars, lotto));
            modeOptions.Add(new JackpotSimulation("Jackpot Simulation", 'S', uniqueChars, lotto));
            modeOptions.Add(new FrequencyDetermination("Determine Frequencies", 'H', uniqueChars, lotto));
            modeOptions.Add(new OptionsMenu("Options menu", 'O', uniqueChars, lotto));
            modeOptions.Add(new ExitApp("Application Extermination", 'B', uniqueChars, lotto));

            return modeOptions;
        }

        public override void Execute()
        {
            this.Lotto.modes = CreateModes(this.Lotto);
            this.Lotto.CurrentMenu = this;

            this.Render.SetConsoleSettings(90, 35);
            this.Render.DisplayHeader(this.Title, 3, 1);
            this.Render.DisplayMenu(this.Lotto.modes, 3, 4);
        }
    }
}
