//-----------------------------------------------------------------------
// <copyright file="CurrentSystemSetter.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the CurrenSystemSetter class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;
    using System.Linq;

    /// <summary>
    /// This is a class for the CurrentSystemSetter Mode.
    /// It lets the user decide which number system he/she wants to use.
    /// </summary>
    public class CurrentSystemSetter : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSystemSetter"/> class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public CurrentSystemSetter(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto)
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
        /// This method lets the user decide which number system he/she wants to use.
        /// </summary>
        public override void Execute()
        {
            if (this.Lotto.NumberSystems.Count > 1)
            {
                this.Render.SetConsoleSettings(150, 35);
                this.Render.DisplayHeader(this.Title, 3, 1);
                this.Renderer.DisplayNumberSystems(this.Lotto.NumberSystems, 5, 5);

                int index = 0;
                do
                {
                    this.Renderer.DisplayVerticalCursor(3, 5 + index);
                    ConsoleKeyInfo userkey = Console.ReadKey(true);
                    if (userkey.Key == ConsoleKey.UpArrow && index > 0)
                    {
                        index--;
                    }
                    else if (userkey.Key == ConsoleKey.DownArrow && index < this.Lotto.NumberSystems.Count - 1)
                    {
                        index++;
                    }
                    else if (userkey.Key == ConsoleKey.Enter)
                    {
                        this.Lotto.ActualSystem = this.Lotto.NumberSystems.ElementAt(index);
                        break;
                    }
                }
                while (true);
            }
        }
    }
}
