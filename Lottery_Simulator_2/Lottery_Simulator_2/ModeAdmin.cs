//-----------------------------------------------------------------------
// <copyright file="ModeAdmin.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Manages the mode objects, initializes them and chooses which mode to call after pressing a defined key in the main menu.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class is the manager of the mode objects that are used in this simulator.
    /// </summary>
    public class ModeAdmin
    {
        /// <summary>
        /// Initializes the Mode objects and puts them into an array of modes.
        /// </summary>
        /// <param name="lotto">The Lottery variable that should used.</param>
        /// <returns>The array of the initialized mode objects.</returns>
        public Mode[] CreateModes(Lottery lotto)
        {
            Mode[] modeOptions = new Mode[6];
            char[] uniqueChars = new char[modeOptions.Length];

            modeOptions[0] = new ManualTip("Manual Tip", 'M', uniqueChars, lotto);
            modeOptions[1] = new QuickTip("Quick Tip", 'Q', uniqueChars, lotto);
            modeOptions[2] = new GraphicalTip("Graphical Tip", 'G', uniqueChars, lotto);
            modeOptions[3] = new JackpotSimulation("Jackpot Simulation", 'S', uniqueChars, lotto);
            modeOptions[4] = new DetermineFrequencies("Determine Frequencies", 'H', uniqueChars, lotto);
            modeOptions[5] = new ExitApp("Application Extermination", 'B', uniqueChars, lotto);

            return modeOptions;
        }

        /// <summary>
        /// Calls the mode objects execute method based on the abbreviation of the mode and the key pressed. 
        /// </summary>
        /// <param name="modes">The array of mode objects initialized in the mode admin.</param>
        /// <returns>True if the modes uniqueChars contain the key the user pressed.</returns>
        public bool OpenMode(Mode[] modes)
        {
            ConsoleKeyInfo userKey = Console.ReadKey(true);
            for (int i = 0; i < modes.Length; i++)
            {
                if (char.ToUpper(userKey.KeyChar) == modes[i].Abbreviation)
                {
                    modes[i].Execute();
                    return true;
                }
            }

            return false;
        }
    }
}