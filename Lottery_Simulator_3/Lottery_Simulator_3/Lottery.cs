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
namespace Lottery_Simulator_3
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This is the main class of the lottery game.
    /// It creates all mode options.
    /// Every mode gets the classes they need from here.
    /// Gives the user the right to move between the modes.
    /// </summary>
    public class Lottery
    {

        /// <summary>
        /// Contains all mode options.
        /// </summary>
        public List<Mode> modes { get; set; }

        /// <summary>
        /// Initializes a new instance of the Lottery class.
        /// </summary>
        /// <param name="amount">The amount of Numbers the user can choose.</param>
        /// <param name="limit1">The first rim of the numbers in which the user can choose.</param>
        /// <param name="limit2">The second rim of the numbers in which the user can choose.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the amount is less than or equal zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If one of the limits is less than zero.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If the amount is bigger than the numbers in the range of numbers.</exception>
        public Lottery()
        {
            this.ActualSystem = new CurrentSystem(6, 6, 1, 45, 1, 1, 45, false);
            this.NumberSystems = new List<NumberSystem>();
            this.NumberSystems.Add(ActualSystem);

            this.CurrentMenu = new MainMenu("Main Menu", 'M', new char[0], this);
            this.Random = new Random();
            this.NumberChecker = new NumbersChecker();
            this.Render = new DefaultRenderer();
            this.KeyChecker = new KeyInputChecker();
            this.NumberGen = new RandomNumbersGenerator(this.Random);
            this.ErrorChecker = new ErrorCheck(this);

        }

        public NumberSystem ActualSystem
        { 
            get; 
            set; 
        }

        public List<NumberSystem> NumberSystems
        {
            get;
            set;
        }

        public ErrorCheck ErrorChecker
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
        public DefaultRenderer Render
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the KeyChecker property.
        /// </summary>
        /// /// <value>
        /// The CheckKeyInput variable.
        /// </value>
        public KeyInputChecker KeyChecker
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
        public NumbersChecker NumberChecker
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
        public RandomNumbersGenerator NumberGen
        {
            get;
            private set;
        }

        public Mode CurrentMenu
        {
            get;
            set;
        }

        /// <summary>
        /// Draws the menu into the console based on the mode objects in the mode admin class.
        /// Switches to different mode classes based on the key pressed.
        /// </summary>
        public void Play()
        {
            do
            {
                this.CurrentMenu.Execute();
                this.OpenMenu();
            }
            while (true);
        }

        public void OpenMenu()
        {
            do
            {
                ConsoleKeyInfo userKey = Console.ReadKey(true);
                foreach (Mode mode in this.modes)
                {
                    if (char.ToUpper(userKey.KeyChar) == mode.Abbreviation)
                    {
                        mode.Execute();
                        return;
                    }
                }
                this.Render.DisplayGeneralError("Wrong Input! Please press enter to continue.", 3, 6 + this.modes.Count);
                this.KeyChecker.WaitForEnter();
                this.Render.OverwriteBlank(90, 3, 6 + this.modes.Count);
            }
            while (true);
        }
    }
}
