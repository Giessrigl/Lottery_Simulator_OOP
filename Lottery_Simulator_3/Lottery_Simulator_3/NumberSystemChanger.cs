//-----------------------------------------------------------------------
// <copyright file="NumberSystemChanger.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the NumberSystemChanger class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;
    using System.Linq;

    /// <summary>
    /// This is a class for the NumberSystemChanger Mode.
    /// It lets the user change the properties of one of the current available number systems.
    /// Except the standard number system: 6 out of 45.
    /// </summary>
    public class NumberSystemChanger : Mode, IExecuteable
    {
        /// <summary>
        /// The amount of numbers the system draws.
        /// </summary>
        private int numberDraws;

        /// <summary>
        /// The amount of numbers the user has to choose.
        /// </summary>
        private int numberAmount;

        /// <summary>
        /// The minimum limit of the numbers the user choose and the system draws.
        /// </summary>
        private int min;

        /// <summary>
        /// The maximum limit of the numbers the user choose and the system draws.
        /// </summary>
        private int max;

        /// <summary>
        /// The indentation from the left rim of the console, where everything is written.
        /// </summary>
        private int offsetLeft = 3;

        /// <summary>
        /// The indentation from the top rim of the console, where everything is written.
        /// </summary>
        private int offsetTop = 5;

        /// <summary>
        /// The amount of bonus numbers the system draws and the user can hit.
        /// </summary>
        private int bonusNumbers;

        /// <summary>
        /// The minimum limit of the bonus numbers the user choose and the system draws.
        /// </summary>
        private int bonusMin = 0;

        /// <summary>
        /// The maximum limit of the numbers the user choose and the system draws.
        /// </summary>
        private int bonusMax = 0;

        /// <summary>
        /// Whether the bonus numbers are in an own pool (true) or in the same pool as the random numbers (false).
        /// </summary>
        private bool bonusPool = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumberSystemChanger"/> class.
        /// </summary>
        /// <param name="title">The title of the object.</param>
        /// <param name="abbreviation">The key to get the user to the objects execute method.</param>
        /// <param name="uniqueChars">The keys that are already used to prevent not being able to call the second mode with the same abbreviation.</param>
        /// <param name="lotto">The Lottery variable that should be used.</param>
        public NumberSystemChanger(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
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
        /// Lets the user change an existing number system.
        /// </summary>
        public override void Execute()
        {
            const int Limitmin = 0;
            const int Limitmax = int.MaxValue;
            const int DrawAmount = 1000;

            int options;
            if (this.Lotto.NumberSystems.Count > 1)
            {
                this.Render.SetConsoleSettings(150, this.Lotto.NumberSystems.Count + 10);
                this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);
                this.Renderer.DisplayNumberSystems(this.Lotto.NumberSystems, this.offsetLeft + 2, this.offsetTop);

                options = 0;
                do
                {
                    this.Renderer.DisplayVerticalCursor(this.offsetLeft, options + this.offsetTop);

                    ConsoleKeyInfo userkey = Console.ReadKey(true);

                    if (userkey.Key == ConsoleKey.UpArrow && options > 0)
                    {
                        options--;
                    }
                    else if (userkey.Key == ConsoleKey.DownArrow && options < this.Lotto.NumberSystems.Count - 1)
                    {
                        options++;
                    }
                    else if (userkey.Key == ConsoleKey.Enter && options > 0)
                    {
                        break;
                    }
                }
                while (true);

                do
                {
                    this.Render.SetConsoleSettings(150, 40);
                    this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);

                    do
                    {
                        this.numberAmount = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of numbers the user has to choose: ", 1, DrawAmount, this.offsetLeft, this.offsetTop);

                        this.numberDraws = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of numbers the system draws: ", 1, DrawAmount, this.offsetLeft, this.offsetTop + 1);

                        int limit1 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the first limit of the range of numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + 2);

                        int limit2 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the second limit of the range of numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + 3);

                        this.min = (limit1 < limit2) ? limit1 : limit2;
                        this.max = (limit1 > limit2) ? limit1 : limit2;
                        if (this.max - this.min + 1 < this.numberDraws)
                        {
                            this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of system numbers. Please press enter to continue!", this.offsetLeft, this.offsetTop + 4);
                            this.Lotto.KeyChecker.WaitForEnter();

                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 1);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 2);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 3);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 4);
                            continue;
                        }
                        else if (this.max - this.min + 1 < this.numberAmount)
                        {
                            this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of user numbers. Please press enter to continue!", this.offsetLeft, this.offsetTop + 4);
                            this.Lotto.KeyChecker.WaitForEnter();

                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 1);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 2);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 3);
                            this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 4);
                            continue;
                        }
                        else
                        {
                            do
                            {
                                this.bonusNumbers = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of bonus numbers the system draws: ", 0, DrawAmount, this.offsetLeft, this.offsetTop + 5);

                                if (this.bonusNumbers > 0)
                                {
                                    int bonusNumberLimit1 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the first limit of the range of bonus numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + 6);

                                    int bonusNumberLimit2 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the second limit of the range of bonus numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + 7);

                                    this.bonusMin = (bonusNumberLimit1 < bonusNumberLimit2) ? bonusNumberLimit1 : bonusNumberLimit2;
                                    this.bonusMax = (bonusNumberLimit1 > bonusNumberLimit2) ? bonusNumberLimit1 : bonusNumberLimit2;

                                    if (this.bonusMax - this.bonusMin + 1 < this.bonusNumbers)
                                    {
                                        this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of bonus numbers. Please press enter to continue!", this.offsetLeft, this.offsetTop + 8);

                                        this.Lotto.KeyChecker.WaitForEnter();

                                        this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 5);
                                        this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 6);
                                        this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 7);
                                        this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 8);
                                        continue;
                                    }
                                    else
                                    {
                                        this.Render.SetConsoleSettings(150, 40);
                                        this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);
                                        this.Renderer.DisplayBonusPoolChoice(this.offsetLeft + 2, this.offsetTop);

                                        int index = 0;
                                        do
                                        {
                                            this.Renderer.DisplayVerticalCursor(this.offsetLeft, this.offsetTop + index);
                                            ConsoleKeyInfo userkey = Console.ReadKey(true);
                                            if (userkey.Key == ConsoleKey.UpArrow && index > 0)
                                            {
                                                index--;
                                            }
                                            else if (userkey.Key == ConsoleKey.DownArrow && index < 1)
                                            {
                                                index++;
                                            }
                                            else if (userkey.Key == ConsoleKey.Enter)
                                            {
                                                if (index < 1)
                                                {
                                                    this.bonusPool = false;
                                                    break;
                                                }
                                                else
                                                {
                                                    this.bonusPool = true;
                                                    break;
                                                }
                                            }
                                        }
                                        while (true);
                                    }
                                }

                                break;
                            }
                            while (true);
                        }

                        break;
                    }
                    while (true);

                    this.Renderer.OverwriteBlank(150, 0, this.offsetTop);
                    this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 1);
                    this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 2);
                    this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 3);
                    this.Renderer.OverwriteBlank(150, 0, this.offsetTop + 4);

                    if (!this.bonusPool)
                    {
                        if (this.min == this.bonusMin && this.max == this.bonusMax)
                        {
                            if ((this.max - this.min + 1) < (this.numberDraws + this.bonusNumbers))
                            {
                                this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                            else if ((this.max - this.min + 1) < this.numberAmount)
                            {
                                this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                        }
                        else if (this.min > this.bonusMin && this.max < this.bonusMax)
                        {
                            if ((this.bonusMax - this.bonusMin + 1) < (this.numberDraws + this.bonusNumbers))
                            {
                                this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                            else if ((this.bonusMax - this.bonusMin + 1) < this.numberAmount)
                            {
                                this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                        }
                        else if (this.bonusMin > this.min && this.bonusMax < this.max)
                        {
                            if ((this.max - this.min + 1) < (this.numberDraws + this.bonusNumbers))
                            {
                                this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                            else if ((this.max - this.min + 1) < this.numberAmount)
                            {
                                this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                        }
                        else if (this.bonusMin < this.max && this.bonusMax > this.max)
                        {
                            if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.max - this.bonusMin + 1) < (this.numberDraws + this.bonusNumbers))
                            {
                                this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                            else if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.max - this.bonusMin + 1) < this.numberAmount)
                            {
                                this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                        }
                        else if (this.min < this.bonusMax && this.max > this.bonusMax)
                        {
                            if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.bonusMax - this.min + 1) < (this.numberDraws + this.bonusNumbers))
                            {
                                this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                            else if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.bonusMax - this.min + 1) < this.numberAmount)
                            {
                                this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                                this.Lotto.KeyChecker.WaitForEnter();
                                continue;
                            }
                        }
                    }

                    if (this.max - this.min + 1 > 100000)
                    {
                        this.Renderer.DisplayGeneralError($"The limits of the numbers are too far apart to be executed correctly. Max range is 100000. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                        this.Lotto.KeyChecker.WaitForEnter();
                        continue;
                    }
                    else if (this.bonusMax - this.bonusMin + 1 > 100000)
                    {
                        this.Renderer.DisplayGeneralError($"The limits of the bonus numbers are too far apart to be executed correctly. Max range is 100000. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                        this.Lotto.KeyChecker.WaitForEnter();
                        continue;
                    }

                    if (this.bonusNumbers > 0)
                    {
                        if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) < this.numberAmount)
                        {
                            this.Renderer.DisplayGeneralError($"The numbers of the user are too much in this constellation. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                            this.Lotto.KeyChecker.WaitForEnter();
                            continue;
                        }
                    }
                    else if (this.bonusNumbers <= 0)
                    {
                        if ((this.max - this.min + 1) < this.numberAmount)
                        {
                            this.Renderer.DisplayGeneralError($"The numbers of the user are too much in this constellation. Please press enter to continue.", this.offsetLeft, this.offsetTop);
                            this.Lotto.KeyChecker.WaitForEnter();
                            continue;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                while (true);

                this.Lotto.NumberSystems.ElementAt(options).NumberAmount = this.numberAmount;
                this.Lotto.NumberSystems.ElementAt(options).NumberDraws = this.numberDraws;
                this.Lotto.NumberSystems.ElementAt(options).Min = this.min;
                this.Lotto.NumberSystems.ElementAt(options).Max = this.max;
                this.Lotto.NumberSystems.ElementAt(options).BonusNumberAmount = this.bonusNumbers;
                this.Lotto.NumberSystems.ElementAt(options).BonusNumberMin = this.bonusMin;
                this.Lotto.NumberSystems.ElementAt(options).BonusNumberMax = this.bonusMax;
                this.Lotto.NumberSystems.ElementAt(options).BonusPool = this.bonusPool;
            }
            else
            {
                this.Render.DisplayGeneralError("The standard system is the only system available.", 3, this.Lotto.Modes.Count + 6);
                this.Render.DisplayGeneralError("Please press enter to continue.", 3, this.Lotto.Modes.Count + 7);
                this.Lotto.KeyChecker.WaitForEnter();
            }
        }
    }
}
