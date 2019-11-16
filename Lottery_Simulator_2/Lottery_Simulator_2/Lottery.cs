//-----------------------------------------------------------------------
// <copyright file="Lottery.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// The access point of the lottery simulator.
// Starts and displays the menu and switches to the different modes.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This is the main class of the lottery game.
    /// It creates all mode options.
    /// Every mode gets the classes they need from here.
    /// Gives the user the right to move between the modes.
    /// </summary>
    public class Lottery
    {
        /// <summary>
        /// Initialize the mode admin for creating the mode options.
        /// </summary>
        private ModeAdmin modeadmin;

        /// <summary>
        /// Contains all mode options.
        /// </summary>
        private Mode[] modes;

        /// <summary>
        /// Initializes a new instance of the Lottery class.
        /// </summary>
        /// <param name="amount">The amount of Numbers the user can choose.</param>
        /// <param name="limit1">The first rim of the numbers in which the user can choose.</param>
        /// <param name="limit2">The second rim of the numbers in which the user can choose.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the amount is less than or equal zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If one of the limits is less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the amount is bigger than the numbers in the range of numbers.</exception>
        public Lottery(int amount, int limit1, int limit2)
        {
            this.Amount = amount;
            this.Min = (limit1 < limit2) ? limit1 : limit2;
            this.Max = (limit1 > limit2) ? limit1 : limit2;
            this.modeadmin = new ModeAdmin();
            this.Random = new Random();
            this.NumberChecker = new CheckNumbers();
            this.Render = new Renderer(3, 1);
            this.KeyChecker = new CheckKeyInput();
            this.NumberGen = new GenerateUniqueRandomNumbers(this.Random);

            if (this.Amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The amount has to be bigger than zero.");
            }

            if (this.Min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(this.Min), "The limits have to be bigger than zero.");
            }

            if (this.Max - this.Min + 1 < this.Amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The amount of unique numbers does not fit into the range of numbers from limit1 and limit2!");
            }
        }

        /// <summary>
        /// Gets the amount of numbers the user can choose.
        /// </summary>
        /// <value>
        /// The amount of numbers the user can choose.
        /// </value>
        public int Amount
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the lower rim of the numbers in which the user can choose.
        /// </summary>
        /// <value>
        /// The lower rim of the numbers in which the user can choose.
        /// </value>
        public int Min
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the upper rim of the numbers in which the user can choose.
        /// </summary>
        /// <value>
        /// The upper rim of the numbers in which the user can choose.
        /// </value>
        public int Max
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Random property.
        /// </summary>
        /// /// <value>
        /// The Random variable.
        /// </value>
        public Random Random
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Render property.
        /// </summary>
        /// /// <value>
        /// The Renderer variable.
        /// </value>
        public Renderer Render
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the KeyChecker property.
        /// </summary>
        /// /// <value>
        /// The CheckKeyInput variable.
        /// </value>
        public CheckKeyInput KeyChecker
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the NumberChecker property.
        /// </summary>
        /// <value>
        /// The CheckNumbers variable.
        /// </value>
        public CheckNumbers NumberChecker
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the NumberGen property.
        /// </summary>
        /// /// <value>
        /// The GenerateUniqueRandomNumbers variable.
        /// </value>
        public GenerateUniqueRandomNumbers NumberGen
        {
            get;
            private set;
        }

        /// <summary>
        /// Draws the menu into the console based on the mode objects in the mode admin class.
        /// Switches to different mode classes based on the key pressed.
        /// </summary>
        public void Play()
        {
            this.modes = this.modeadmin.CreateModes(this);

            do
            {
                this.Render.DisplayMenu(this.modes);
                if (!this.modeadmin.OpenMode(this.modes))
                {
                    this.Render.DisplayUserKeyError(6 + this.modes.Length);

                    do
                    {
                        if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                        {
                            break;
                        }
                    }
                    while (true);
                }
            }
            while (true); 
        }
    }
}