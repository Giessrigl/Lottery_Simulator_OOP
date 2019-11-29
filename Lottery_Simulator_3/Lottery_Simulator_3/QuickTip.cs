//-----------------------------------------------------------------------
// <copyright file="QuickTip.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the QuickTip class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the quick tip mode.
    /// It gets the numbers for the user randomly, compares them to the drawn random numbers and displays the evaluation in the console to the user. 
    /// </summary>
    public class QuickTip : Mode, IEvaluable, IExecuteable
    {
        /// <summary>
        /// The numbers the user has chosen.
        /// </summary>
        private int[] chosenNumbers;

        /// <summary>
        /// The drawn random numbers.
        /// </summary>
        private int[] randomNumbers;

        /// <summary>
        /// The drawn bonus numbers.
        /// </summary>
        private int[] randomBonusNumbers;

        /// <summary>
        /// The amount of equal chosen and random numbers.
        /// </summary>
        private int equalNumbers;

        /// <summary>
        /// The amount of equal chosen and bonus numbers.
        /// </summary>
        private int equalBonusNumbers;

        /// <summary>
        /// The indentation from the left rim of the console, where everything will be displayed.
        /// </summary>
        private int offsetLeft = 3;

        /// <summary>
        /// The indentation from the top rim of the console, where everything will be displayed.
        /// </summary>
        private int offsetTop = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickTip"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public QuickTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// This method contains the steps that have to be made to display the evaluation of the users chosen numbers and all the drawn numbers.
        /// </summary>
        public override void Execute()
        {
            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);
            this.PerformPreEvaluation();

            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);
            this.PerformEvaluation();
        }

        /// <summary>
        /// Examines all the calculations that have to be done before evaluating.
        /// </summary>
        public void PerformPreEvaluation()
        {
            this.chosenNumbers = this.Lotto.NumberGen.GenerateUniquesInRanges(this.Lotto.ActualSystem.NumberDraws, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);

            this.randomNumbers = new int[this.Lotto.ActualSystem.NumberAmount];
            this.randomBonusNumbers = new int[this.Lotto.ActualSystem.BonusNumberAmount];
            int[] allRandomNumbers;

            if (this.Lotto.ActualSystem.BonusPool)
            {
                this.randomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                {
                    this.randomBonusNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                }
            }
            else
            {
                if (this.Lotto.ActualSystem.Min >= this.Lotto.ActualSystem.BonusNumberMin && this.Lotto.ActualSystem.Max <= this.Lotto.ActualSystem.BonusNumberMax)
                {
                    allRandomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                    for (int i = 0; i < this.Lotto.ActualSystem.NumberAmount; i++)
                    {
                        this.randomNumbers[i] = allRandomNumbers[i];
                    }

                    if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                    {
                        for (int i = this.Lotto.ActualSystem.NumberAmount; i < this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount; i++)
                        {
                            this.randomBonusNumbers[i - this.Lotto.ActualSystem.NumberAmount] = allRandomNumbers[i];
                        }
                    }
                }
                else if (this.Lotto.ActualSystem.BonusNumberMin >= this.Lotto.ActualSystem.Min && this.Lotto.ActualSystem.BonusNumberMax <= this.Lotto.ActualSystem.Max)
                {
                    allRandomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                    for (int i = 0; i < this.Lotto.ActualSystem.NumberAmount; i++)
                    {
                        this.randomNumbers[i] = allRandomNumbers[i];
                    }

                    if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                    {
                        for (int i = this.Lotto.ActualSystem.NumberAmount; i < this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount; i++)
                        {
                            this.randomBonusNumbers[i - this.Lotto.ActualSystem.NumberAmount] = allRandomNumbers[i];
                        }
                    }
                }
                else
                {
                    allRandomNumbers = this.Lotto.NumberGen.GenerateUniquesInRanges(this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                    for (int i = 0; i < allRandomNumbers.Length; i++)
                    {
                        if (allRandomNumbers[i] > this.Lotto.ActualSystem.Min && allRandomNumbers[i] < this.Lotto.ActualSystem.Max)
                        {
                            try
                            {
                                this.randomNumbers[i] = allRandomNumbers[i];
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                            {
                                try
                                {
                                    this.randomBonusNumbers[i] = allRandomNumbers[i];
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }

            this.equalNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, this.randomNumbers);
            if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
            {
                this.equalBonusNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, this.randomBonusNumbers);
            }
        }

        /// <summary>
        /// Examines all calculations that have to be done to display the evaluation and displays the evaluation.
        /// </summary>
        public void PerformEvaluation()
        {
            int chosenIndex = 0;
            int randomIndex = 0;
            int bonusIndex = 0;

            int cursorTop = 0;

            this.Render.DisplayMoveMessage(this.offsetLeft + 1, this.offsetTop - 1);
            this.Render.DisplayEvaluation(this.Lotto.ActualSystem.BonusNumberAmount, this.offsetLeft + 1, this.offsetTop);
            this.Render.DisplayEvaluationNumber(this.chosenNumbers[0], this.offsetLeft + 2, this.offsetTop + 1);
            this.Render.DisplayEvaluationNumber(this.randomNumbers[0], this.offsetLeft + 2, this.offsetTop + 3);
            if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
            {
                this.Render.DisplayEvaluationNumber(this.randomBonusNumbers[0], this.offsetLeft + 2, this.offsetTop + 5);
            }

            if (this.Lotto.ActualSystem.BonusPool)
            {
                this.Render.DisplayOutcomeOwnPool(this.equalNumbers, this.equalBonusNumbers, this.offsetLeft + 2, this.offsetTop + 7);
            }
            else
            {
                this.Render.DisplayOutcomeIdentPool(this.equalNumbers, this.equalBonusNumbers, this.Lotto.ActualSystem.NumberDraws, this.offsetLeft + 2, this.offsetTop + 7);
            }

            this.Render.DisplayReturnIfEnter(this.offsetLeft, Console.WindowHeight - 2);
            do
            {
                this.Render.DisplayVerticalCursor(this.offsetLeft, this.offsetTop + 1 + cursorTop);
                ConsoleKeyInfo userKey = Console.ReadKey(true);
                if (userKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                switch (userKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (cursorTop > 0)
                        {
                            cursorTop -= 2;
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        if (cursorTop < 4)
                        {
                            cursorTop += 2;
                        }

                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorTop == 0)
                        {
                            if (chosenIndex > 0)
                            {
                                chosenIndex--;
                                this.Render.DisplayEvaluationNumber(this.chosenNumbers[chosenIndex], this.offsetLeft + 2, this.offsetTop + 1);
                            }
                        }
                        else if (cursorTop == 2)
                        {
                            if (randomIndex > 0)
                            {
                                randomIndex--;
                                this.Render.DisplayEvaluationNumber(randomNumbers[randomIndex], this.offsetLeft + 2, this.offsetTop + 3);
                            }
                        }
                        else if (cursorTop == 4)
                        {
                            if (bonusIndex > 0 && this.Lotto.ActualSystem.BonusNumberAmount > 0)
                            {
                                bonusIndex--;
                                this.Render.DisplayEvaluationNumber(this.randomBonusNumbers[bonusIndex], this.offsetLeft + 2, this.offsetTop + 5);
                            }
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (cursorTop == 0)
                        {
                            if (chosenIndex < this.chosenNumbers.Length - 1)
                            {
                                chosenIndex++;
                                this.Render.DisplayEvaluationNumber(this.chosenNumbers[chosenIndex], this.offsetLeft + 2, this.offsetTop + 1);
                            }
                        }
                        else if (cursorTop == 2)
                        {
                            if (randomIndex < this.randomNumbers.Length - 1)
                            {
                                randomIndex++;
                                this.Render.DisplayEvaluationNumber(this.randomNumbers[randomIndex], this.offsetLeft + 2, this.offsetTop + 3);
                            }
                        }
                        else if (cursorTop == 4)
                        {
                            if (bonusIndex < this.randomBonusNumbers.Length - 1 && this.Lotto.ActualSystem.BonusNumberAmount > 0)
                            {
                                bonusIndex++;
                                this.Render.DisplayEvaluationNumber(randomBonusNumbers[bonusIndex], this.offsetLeft + 2, this.offsetTop + 5);
                            }
                        }

                        break;
                }
            }
            while (true);
        }
    }
}
