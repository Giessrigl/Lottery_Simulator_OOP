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
    /// Every mode gets the classes they need from here.
    /// Gives the user the ability to move between the modes.
    /// </summary>
    public class Lottery
    {
        /// <summary>
        /// Initializes a new instance of the Lottery class.
        /// </summary>
        public Lottery()
        {
            this.ActualSystem = new CurrentSystem(6, 6, 1, 45, 1, 1, 45, false);
            this.NumberSystems = new List<NumberSystem>();
            this.NumberSystems.Add(this.ActualSystem);

            this.CurrentMenu = new MainMenu("Main Menu", 'M', new char[0], this);
            this.Random = new Random();
            this.NumberChecker = new NumbersChecker();
            this.Renderer = new DefaultConsoleRenderer();
            this.KeyChecker = new KeyInputChecker();
            this.NumberGen = new RandomNumbersGenerator(this.Random);
            this.ErrorChecker = new ErrorChecker(this);
        }

        /// <summary>
        /// Gets or sets all modes listed in the menu.
        /// </summary>
        /// <value>The list of modes that are in the current menu.</value>
        public List<Mode> Modes { get; set; }

        /// <summary>
        /// Gets or sets the current used number system.
        /// </summary>
        /// <value>
        /// The current used number system.
        /// </value>
        public NumberSystem ActualSystem
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets or sets all currently available number systems.
        /// </summary>
        /// <value>
        /// The list of currently available number systems.
        /// </value>
        public List<NumberSystem> NumberSystems
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the error checker object.
        /// </summary>
        /// <value>
        /// The used error checker instance.
        /// </value>
        public ErrorChecker ErrorChecker
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the Random object.
        /// </summary>
        /// <value>
        /// The Random variable.
        /// </value>
        public Random Random
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the DefaultConsoleRenderer object.
        /// </summary>
        /// <value>
        /// The Renderer variable.
        /// </value>
        public DefaultConsoleRenderer Renderer
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the KeyInputChecker object.
        /// </summary>
        /// <value>
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
        /// Gets the RandomNumberGenerator object.
        /// </summary>
        /// /// <value>
        /// The GenerateUniqueRandomNumbers variable.
        /// </value>
        public RandomNumbersGenerator NumberGen
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the Mode object.
        /// </summary>
        /// <value>
        /// The currently used menu.
        /// </value>
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
                this.OpenMode();
            }
            while (true);
        }

        /// <summary>
        /// Opens the specified mode based on the key the user pressed.
        /// </summary>
        public void OpenMode()
        {
            do
            {
                ConsoleKeyInfo userKey = Console.ReadKey(true);
                foreach (Mode mode in this.Modes)
                {
                    if (char.ToUpper(userKey.KeyChar) == mode.Abbreviation)
                    {
                        mode.Execute();
                        return;
                    }
                }

                this.Renderer.DisplayGeneralError("Wrong Input! Please press enter to continue.", 3, 6 + this.Modes.Count);
                this.KeyChecker.WaitForEnter();
                this.Renderer.OverwriteBlank(55, 3, 6 + this.Modes.Count);
            }
            while (true);
        }
    }
}
