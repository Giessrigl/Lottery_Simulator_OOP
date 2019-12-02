//-----------------------------------------------------------------------
// <copyright file="GraphicalTip.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the GraphicalTip class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the GraphicalTip mode.
    /// It displays the user all numbers he can choose in cells and can switch through the cells with the arrow keys.
    /// After choosing the specified amount of numbers from the current system, it displays the evaluation of how many numbers are in common with the drawn numbers.
    /// </summary>
    public class GraphicalTip : Mode, IEvaluable, IExecuteable
    {
        /// <summary>
        /// The indentation from the left rim of the console.
        /// </summary>
        private int offsetLeft = 3;

        /// <summary>
        /// The indentation from the top rim of the console.
        /// </summary>
        private int offsetTop = 5;

        /// <summary>
        /// The color for the selected cells.
        /// </summary>
        private ConsoleColor selectColor = ConsoleColor.Green;

        /// <summary>
        /// The color for the hit cells.
        /// </summary>
        private ConsoleColor hitColor = ConsoleColor.Blue;

        /// <summary>
        /// The color for the not hit but drawn cell.
        /// </summary>
        private ConsoleColor noHitColor = ConsoleColor.Red;

        /// <summary>
        /// The range of all numbers that can be chosen.
        /// </summary>
        private int allNumbersAmount;

        /// <summary>
        /// All random numbers that can be drawn.
        /// </summary>
        private int[] allNumbers;

        /// <summary>
        /// The background color of a cell, whether its selected, hit or not hit.
        /// </summary>
        private ConsoleColor[] cellColors;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicalTip"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public GraphicalTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// This method displays  .....
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Renderer.SetConsoleSettings(65, 30);
            this.Lotto.Renderer.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);

            this.PerformPreEvaluation();
            this.PerformEvaluation(); 
        }

        /// <summary>
        /// Examines all the calculations that have to be done before evaluating.
        /// </summary>
        public void PerformPreEvaluation()
        {
            if (this.Lotto.ActualSystem.BonusNumberMin >= this.Lotto.ActualSystem.Min && this.Lotto.ActualSystem.BonusNumberMax <= this.Lotto.ActualSystem.Max)
            {
                this.allNumbersAmount = this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1;
            }
            else if (this.Lotto.ActualSystem.Min >= this.Lotto.ActualSystem.BonusNumberMin && this.Lotto.ActualSystem.Max <= this.Lotto.ActualSystem.BonusNumberMax)
            {
                this.allNumbersAmount = this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1;
            }
            else if (this.Lotto.ActualSystem.BonusNumberMin < this.Lotto.ActualSystem.Max && this.Lotto.ActualSystem.BonusNumberMax > this.Lotto.ActualSystem.Max)
            {
                this.allNumbersAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1) - (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.BonusNumberMin + 1);
            }
            else if (this.Lotto.ActualSystem.Min < this.Lotto.ActualSystem.BonusNumberMax && this.Lotto.ActualSystem.Max > this.Lotto.ActualSystem.BonusNumberMax)
            {
                this.allNumbersAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1) - (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.Min + 1);
            }
            else
            {
                this.allNumbersAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1);
            }

            this.allNumbers = new int[this.allNumbersAmount];
            this.cellColors = new ConsoleColor[this.allNumbersAmount];

            if (this.Lotto.ActualSystem.BonusNumberMin >= this.Lotto.ActualSystem.Min && this.Lotto.ActualSystem.BonusNumberMax <= this.Lotto.ActualSystem.Max)
            {
                for (int i = this.Lotto.ActualSystem.Min; i <= this.Lotto.ActualSystem.Max; i++)
                {
                    this.allNumbers[i - this.Lotto.ActualSystem.Min] = i;
                }
            }
            else if (this.Lotto.ActualSystem.Min > this.Lotto.ActualSystem.BonusNumberMin && this.Lotto.ActualSystem.Max <= this.Lotto.ActualSystem.BonusNumberMax)
            {
                for (int i = this.Lotto.ActualSystem.BonusNumberMin; i <= this.Lotto.ActualSystem.BonusNumberMax; i++)
                {
                    this.allNumbers[i - this.Lotto.ActualSystem.BonusNumberMin] = i;
                }
            }
            else if (this.Lotto.ActualSystem.BonusNumberMin < this.Lotto.ActualSystem.Max && this.Lotto.ActualSystem.BonusNumberMax > this.Lotto.ActualSystem.Max)
            {
                for (int i = this.Lotto.ActualSystem.Min; i <= this.Lotto.ActualSystem.BonusNumberMax; i++)
                {
                    this.allNumbers[i - this.Lotto.ActualSystem.Min] = i;
                }
            }
            else if (this.Lotto.ActualSystem.Min < this.Lotto.ActualSystem.BonusNumberMax && this.Lotto.ActualSystem.Max > this.Lotto.ActualSystem.BonusNumberMax)
            {
                for (int i = this.Lotto.ActualSystem.BonusNumberMin; i <= this.Lotto.ActualSystem.Max; i++)
                {
                    this.allNumbers[i - this.Lotto.ActualSystem.BonusNumberMin] = i;
                }
            }
            else
            {
                for (int i = this.Lotto.ActualSystem.Min; i <= this.Lotto.ActualSystem.Max; i++)
                {
                    this.allNumbers[i - this.Lotto.ActualSystem.Min] = i;
                }

                for (int i = this.Lotto.ActualSystem.BonusNumberMin; i <= this.Lotto.ActualSystem.BonusNumberMax; i++)
                {
                    this.allNumbers[(this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + i - this.Lotto.ActualSystem.BonusNumberMin] = i;
                }
            }

            int selectedNumbers = 0;
            int index = 0;

            this.Render.DisplayMoveMessage(this.offsetLeft + 2, this.offsetTop - 1);
            do
            {
                this.Render.DisplayGraphicalCell(this.allNumbers[index], this.cellColors[index], this.offsetLeft + 2, this.offsetTop);

                ConsoleKeyInfo userKey = Console.ReadKey(true);
                if (userKey.Key == ConsoleKey.Enter && selectedNumbers >= this.Lotto.ActualSystem.NumberAmount)
                {
                    break;
                }

                switch (userKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (index > 0)
                        {
                            index--;
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (index < this.allNumbersAmount - 1)
                        {
                            index++;
                        }

                        break;
                    case ConsoleKey.UpArrow:
                        if (index >= 10 && this.allNumbers.Length > 10)
                        {
                            index -= 10;
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        if (index < this.allNumbersAmount - 10)
                        {
                            index += 10;
                        }

                        break;

                    case ConsoleKey.Spacebar:
                        if (this.cellColors[index] == ConsoleColor.Black && selectedNumbers < this.Lotto.ActualSystem.NumberAmount)
                        {
                            this.cellColors[index] = this.selectColor;
                            selectedNumbers++;
                        }
                        else if (this.cellColors[index] == this.selectColor && selectedNumbers > 0)
                        {
                            this.cellColors[index] = ConsoleColor.Black;
                            selectedNumbers--;
                        }
                        
                        break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Examines all calculations that have to be done to display the evaluation and displays the evaluation.
        /// </summary>
        public void PerformEvaluation()
        {
            int[] chosenNumbers = new int[this.Lotto.ActualSystem.NumberAmount];

            int chosenIndex = 0;
            for (int j = 0; j < this.cellColors.Length; j++)
            {
                if (this.cellColors[j] == this.selectColor)
                {
                    chosenNumbers[chosenIndex] = this.allNumbers[j];
                    chosenIndex++;
                }
            }

            int[] randomNumbers;
            int[] bonusNumbers = new int[this.Lotto.ActualSystem.BonusNumberAmount];

            if (this.Lotto.ActualSystem.BonusPool)
            {
                randomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberDraws, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                {
                    bonusNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                }
            }
            else
            {
                randomNumbers = new int[this.Lotto.ActualSystem.NumberDraws];
                
                int[] allDrawnNumbers = this.Lotto.NumberGen.GenerateUniquesInRanges(this.Lotto.ActualSystem.NumberDraws, this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);

                int randomIndex = 0;
                int bonusIndex = 0;
                for (int i = 0; i < allDrawnNumbers.Length; i++)
                {
                    if (allDrawnNumbers[i] > this.Lotto.ActualSystem.Min && allDrawnNumbers[i] < this.Lotto.ActualSystem.Max && randomIndex < this.Lotto.ActualSystem.NumberDraws)
                    {
                        randomNumbers[randomIndex] = allDrawnNumbers[i];
                        randomIndex++;
                    }
                    else
                    {
                        bonusNumbers[bonusIndex] = allDrawnNumbers[i];
                        bonusIndex++;
                    }
                }
            }

            // all random numbers will be in nohitcolor.
            for (int i = 0; i < randomNumbers.Length; i++)
            {
                this.cellColors[randomNumbers[i] - 1] = this.noHitColor;
            }

            // all bonus numbers will be in nohitcolor.
            for (int i = 0; i < bonusNumbers.Length; i++)
            {
                this.cellColors[bonusNumbers[i] - 1] = this.noHitColor;
            }

            int equalNumbers = 0;

            // all hitted random numbers will be in hitcolor.
            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                for (int j = 0; j < randomNumbers.Length; j++)
                {
                    if (chosenNumbers[i] == randomNumbers[j])
                    {
                        this.cellColors[chosenNumbers[i] - 1] = this.hitColor;
                        equalNumbers++;
                        break;
                    }
                }
            }

            int equalBonusNumbers = 0;
            if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
            {
                // all hitted bonus numbers will be in hitcolor.
                for (int i = 0; i < chosenNumbers.Length; i++)
                {
                    for (int j = 0; j < bonusNumbers.Length; j++)
                    {
                        if (chosenNumbers[i] == bonusNumbers[j])
                        {
                            this.cellColors[chosenNumbers[i] - 1] = this.hitColor;
                            equalBonusNumbers++;
                            break;
                        }
                    }
                }
            }
            
            int cursor = 0;
            int cellIndex = 0;
            int chosenNumberIndex = 0;
            int randomNumberIndex = 0;
            int bonusNumberIndex = 0;

            this.Render.DisplayReturnIfEnter(this.offsetLeft + 2, Console.WindowHeight - 2);
            this.Render.DisplayEvaluation(this.Lotto.ActualSystem.BonusNumberAmount, this.offsetLeft + 2, this.offsetTop + 4);
            if (this.Lotto.ActualSystem.BonusPool)
            {
                this.Render.DisplayOutcomeOwnPool(equalNumbers, equalBonusNumbers, this.offsetLeft + 2, this.offsetTop + 11);
            }
            else
            {
                this.Render.DisplayOutcomeIdentPool(equalNumbers, equalBonusNumbers, this.Lotto.ActualSystem.NumberAmount, this.offsetLeft + 2, this.offsetTop + 11);
            }
           
            do
            {
                this.Render.DisplayGraphicalCell(this.allNumbers[cellIndex], this.cellColors[cellIndex], this.offsetLeft + 2, this.offsetTop);
                this.Render.DisplayEvaluationNumber(chosenNumbers[chosenNumberIndex], this.offsetLeft + 3, this.offsetTop + 5);
                this.Render.DisplayEvaluationNumber(randomNumbers[randomNumberIndex], this.offsetLeft + 3, this.offsetTop + 7);
                this.Render.DisplayEvaluationNumber(bonusNumbers[bonusNumberIndex], this.offsetLeft + 3, this.offsetTop + 9);
                this.Render.DisplayVerticalCursor(this.offsetLeft, this.offsetTop + 1 + cursor);

                ConsoleKeyInfo userKey = Console.ReadKey(true);
                if (userKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                switch (userKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (cursor == 0 && cellIndex > 0)
                        {
                            cellIndex--;
                        }
                        else if (cursor == 4 && chosenNumberIndex > 0)
                        {
                            chosenNumberIndex--;
                        }
                        else if (cursor == 6 && randomNumberIndex > 0)
                        {
                            randomNumberIndex--;
                        }
                        else if (cursor > 8 && bonusNumberIndex > 0)
                        {
                            bonusNumberIndex--;
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (cursor == 0 && cellIndex < this.cellColors.Length - 1)
                        {
                            cellIndex++;
                        }
                        else if (cursor == 4 && chosenNumberIndex < chosenNumbers.Length - 1)
                        {
                            chosenNumberIndex++;
                        }
                        else if (cursor == 6 && randomNumberIndex < randomNumbers.Length - 1)
                        {
                            randomNumberIndex++;
                        }
                        else if (cursor >= 8 && bonusNumberIndex < bonusNumbers.Length - 1)
                        {
                            bonusNumberIndex++;
                        }

                        break;
                    case ConsoleKey.UpArrow:
                        if (cursor > 0)
                        {
                            if (cursor < 5)
                            {
                                cursor -= 4;
                            }
                            else
                            {
                                cursor -= 2;
                            }
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        if (cursor < 8)
                        {
                            if (cursor == 0)
                            {
                                cursor += 4;
                            }
                            else
                            {
                                cursor += 2;
                            }
                        }
                
                        break;
                }
            }
            while (true);
        }
    }
}
