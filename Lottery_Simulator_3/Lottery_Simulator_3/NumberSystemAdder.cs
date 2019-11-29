//-----------------------------------------------------------------------
// <copyright file="NumberSystemAdder.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the NumberSystemAdder class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the NumberSystemAdder Mode.
    /// It lets the user build a new number system.
    /// </summary>
    public class NumberSystemAdder : Mode, IExecuteable
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
        /// Initializes a new instance of the <see cref="NumberSystemAdder"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public NumberSystemAdder(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
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
        /// This method lets the user define a new number system after all inputs meet the requirements.
        /// </summary>
        public override void Execute()
        {
            const int Limitmin = 0;
            const int Limitmax = int.MaxValue;
            const int DrawAmount = 1000;

            do
            {
                this.Render.SetConsoleSettings(90, 40);
                this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);

                int linecount = 0;
                do
                {
                    this.numberDraws = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of numbers the user has to choose: ", 1, DrawAmount, this.offsetLeft, this.offsetTop + linecount);
                    linecount++;
                    this.numberAmount = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of numbers the system draws: ", 1, DrawAmount, this.offsetLeft, this.offsetTop + linecount);
                    linecount++;
                    int limit1 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the first limit of the range of numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + linecount);
                    linecount++;
                    int limit2 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the second limit of the range of numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + linecount);
                    linecount++;

                    this.min = (limit1 < limit2) ? limit1 : limit2;
                    this.max = (limit1 > limit2) ? limit1 : limit2;
                    if (this.max - this.min + 1 < this.numberAmount)
                    {
                        linecount--;
                        this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of numbers. Please press enter to continue!", this.offsetLeft, this.offsetTop + 1 + linecount);
                        linecount -= 2;
                        this.Lotto.KeyChecker.WaitForEnter();

                        this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount);
                        this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount + 1);
                        this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount + 2);
                        this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount + 3);
                        continue;
                    }
                    else
                    {
                        do
                        {
                            this.bonusNumbers = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of bonus numbers you want to choose: ", 0, DrawAmount, this.offsetLeft, this.offsetTop + linecount);
                            linecount++;
                            if (this.bonusNumbers > 0)
                            {
                                int bonusNumberLimit1 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the first limit of the range of bonus numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + linecount);
                                linecount++;
                                int bonusNumberLimit2 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the second limit of the range of bonus numbers: ", Limitmin, Limitmax, this.offsetLeft, this.offsetTop + linecount);
                                linecount++;

                                this.bonusMin = (bonusNumberLimit1 < bonusNumberLimit2) ? bonusNumberLimit1 : bonusNumberLimit2;
                                this.bonusMax = (bonusNumberLimit1 > bonusNumberLimit2) ? bonusNumberLimit1 : bonusNumberLimit2;

                                if (this.bonusMax - this.bonusMin + 1 < this.bonusNumbers)
                                {
                                    linecount--;
                                    this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of numbers. Please press enter to continue!", this.offsetLeft, this.offsetTop + 1 + linecount);
                                    linecount -= 2;
                                    this.Lotto.KeyChecker.WaitForEnter();

                                    this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount);
                                    this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount + 1);
                                    this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount + 2);
                                    this.Renderer.OverwriteBlank(90, 0, this.offsetTop + linecount + 3);
                                    continue;
                                }
                                else
                                {
                                    this.Render.SetConsoleSettings(90, 40);
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
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        while (true);
                    }

                    break;
                }
                while (true);

                if (!this.bonusPool)
                {
                    if (this.min == this.bonusMin && this.max == this.bonusMax)
                    {
                        if ((this.max - this.min + 1) < (this.numberAmount + this.bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((this.max - this.min + 1) < this.numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (this.min > this.bonusMin && this.max < this.bonusMax)
                    {
                        if ((this.bonusMax - this.bonusMin + 1) < (this.numberAmount + this.bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((this.bonusMax - this.bonusMin + 1) < this.numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (this.bonusMin > this.min && this.bonusMax < this.max)
                    {
                        if ((this.max - this.min + 1) < (this.numberAmount + this.bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((this.max - this.min + 1) < this.numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (this.bonusMin < this.max && this.bonusMax > this.max)
                    {
                        if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.max - this.bonusMin + 1) < (this.numberAmount + this.bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.max - this.bonusMin + 1) < this.numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (this.min < this.bonusMax && this.max > this.bonusMax)
                    {
                        if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.bonusMax - this.min + 1) < (this.numberAmount + this.bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((this.max - this.min + 1) + (this.bonusMax - this.bonusMin + 1) - (this.bonusMax - this.min + 1) < this.numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", this.offsetLeft, this.offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                break;
            }
            while (true);

            this.Lotto.NumberSystems.Add(new NumberSystem(this.numberDraws, this.numberAmount, this.min, this.max, this.bonusNumbers, this.bonusMin, this.bonusMax, this.bonusPool));
        }
    }
}
