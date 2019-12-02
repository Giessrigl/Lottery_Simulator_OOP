//-----------------------------------------------------------------------
// <copyright file="NumberSystemDeletion.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the NumberSystemDeletion class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the NumberSystemDeletion Mode.
    /// It lets the user delete a number system he/she does not need anymore.
    /// Except the standard number system: 6 out of 45.
    /// </summary>
    public class NumberSystemDeletion : Mode, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberSystemDeletion"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public NumberSystemDeletion(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
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
        /// This method lets the user delete one of the number systems.
        /// </summary>
        public override void Execute()
        {
            if (this.Lotto.NumberSystems.Count > 1)
            {
                this.Render.SetConsoleSettings(150, this.Lotto.NumberSystems.Count + 10);
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
                    else if (userkey.Key == ConsoleKey.Enter && index > 0)
                    {
                        this.Lotto.NumberSystems.RemoveAt(index);
                        break;
                    }
                }
                while (true);
            }
            else
            {
                this.Render.DisplayGeneralError("There is only one system available. It can not be deleted.", 3, this.Lotto.Modes.Count + 6);
                this.Render.DisplayGeneralError("Press enter to continue.", 3, this.Lotto.Modes.Count + 7);
                this.Lotto.KeyChecker.WaitForEnter();
            }
        }
    }
}
