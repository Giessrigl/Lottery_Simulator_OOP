//-----------------------------------------------------------------------
// <copyright file="ExitApp.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// 
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class OptionsMenu : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the OptionsMenu class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public OptionsMenu(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
            this.Renderer = new OptionsMenuRenderer();
        }

        public OptionsMenuRenderer Renderer
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Execute()
        {
            this.Lotto.modes = this.CreateOptions();
            this.Lotto.CurrentMenu = this;

            this.Renderer.SetConsoleSettings(90, 35);
            this.Renderer.DisplayHeader(this.Title, 3, 1);
            this.Renderer.DisplayMenu(this.Lotto.modes, 3, 4);
        }

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
