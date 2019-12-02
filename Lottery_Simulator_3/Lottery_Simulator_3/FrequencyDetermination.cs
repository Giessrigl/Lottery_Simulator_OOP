//-----------------------------------------------------------------------
// <copyright file="FrequencyDetermination.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the FrequencyDetermination class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the FrequencyDetermination mode.
    /// It displays the user the frequency of every number in the current system that has been drawn through an amount of iterations the user specifies.
    /// </summary>
    public class FrequencyDetermination : Mode, IEvaluable, IExecuteable
    {
        /// <summary>
        /// The number of iterations.
        /// </summary>
        private int iterations;

        /// <summary>
        /// The range of all random numbers that can be drawn.
        /// </summary>
        private int numbersAmount;
        
        /// <summary>
        /// All random numbers that can be drawn.
        /// </summary>
        private int[] allNumbers;

        /// <summary>
        /// The frequency of every number.
        /// </summary>
        private int[] frequenciesNumbers;

        /// <summary>
        /// The indentation from the left rim of the console.
        /// </summary>
        private int offsetLeft = 3;

        /// <summary>
        /// The indentation from the top rim of the console.
        /// </summary>
        private int offsetTop = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrequencyDetermination"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public FrequencyDetermination(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// This method contains the steps that have to be made to display the frequency of every number from the current number system.
        /// </summary>
        public override void Execute()
        {
            this.Lotto.Renderer.SetConsoleSettings(65, 20);
            this.Lotto.Renderer.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);

            this.PerformPreEvaluation();
            this.PerformEvaluation();
        }
        
        /// <summary>
        /// Examines all the calculations that have to be done before evaluating.
        /// </summary>
        public void PerformPreEvaluation()
        {
            this.iterations = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of iterations: ", 1, int.MaxValue, this.offsetLeft, this.offsetTop);
            this.Render.OverwriteBlank(55, 0, this.offsetTop);

            this.numbersAmount = this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1;
            this.allNumbers = new int[this.numbersAmount];
            this.frequenciesNumbers = new int[this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1];

            for (int i = this.Lotto.ActualSystem.Min; i <= this.Lotto.ActualSystem.Max; i++)
            {
                this.allNumbers[i - this.Lotto.ActualSystem.Min] = i;
            }
        }

        /// <summary>
        /// Examines all calculations that have to be done to display the evaluation and displays the evaluation.
        /// </summary>
        public void PerformEvaluation()
        {
            double currentIteration = 1.0;
            int highestNumberFrequence = 0;
            int numberFrequence;
            int[] iterationBonusNumbers = new int[0];

            do
            {
                double percentage = Math.Round((currentIteration / this.iterations) * 100, 2);

                this.Render.DisplayFrequencyStatus(int.Parse(currentIteration.ToString()), this.iterations, percentage, this.offsetLeft, this.offsetTop);

                int[] iterationNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberDraws, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                if (this.Lotto.ActualSystem.BonusPool && this.Lotto.ActualSystem.BonusNumberAmount > 0)
                {
                    iterationBonusNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                }
                
                for (int i = 0; i < iterationNumbers.Length; i++)
                {
                    this.frequenciesNumbers[iterationNumbers[i] - this.Lotto.ActualSystem.Min]++;
                }

                if (this.Lotto.ActualSystem.BonusPool)
                {
                    for (int i = 0; i < iterationBonusNumbers.Length; i++)
                    {
                        if (iterationBonusNumbers[i] - 1 > this.Lotto.ActualSystem.Min && iterationBonusNumbers[i] - 1 < this.Lotto.ActualSystem.Max)
                        {
                            this.frequenciesNumbers[iterationBonusNumbers[i] - this.Lotto.ActualSystem.Min]++;
                        }
                    }
                }
                
                for (int j = 0; j < this.numbersAmount; j++)
                {
                    if (this.frequenciesNumbers[j] > highestNumberFrequence)
                    {
                        highestNumberFrequence = this.frequenciesNumbers[j];
                    }
                    else if (highestNumberFrequence == currentIteration)
                    {
                        break;
                    }
                }

                numberFrequence = this.frequenciesNumbers[0] * 16 / highestNumberFrequence;
                this.Render.DisplayFrequencyCell(this.allNumbers[0].ToString(), numberFrequence, this.offsetLeft, this.offsetTop + 2);
                currentIteration++;
            }
            while (currentIteration <= this.iterations);

            this.Render.DisplayMoveMessage(this.offsetLeft, this.offsetTop - 1);
            this.Render.DisplayReturnIfEnter(this.offsetLeft, Console.WindowHeight - 2);

            int numberIndex = 0;
            do
            {
                ConsoleKeyInfo userKey = Console.ReadKey(true);
                if (userKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                switch (userKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        if (numberIndex > 0)
                        {
                            numberIndex--;
                            numberFrequence = (this.frequenciesNumbers[numberIndex] * 16) / highestNumberFrequence;
                            this.Render.DisplayFrequencyCell(this.allNumbers[numberIndex].ToString(), numberFrequence, this.offsetLeft, this.offsetTop + 2);
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (numberIndex < this.numbersAmount - 1)
                        {
                            numberIndex++;
                            numberFrequence = (this.frequenciesNumbers[numberIndex] * 16) / highestNumberFrequence;
                            this.Render.DisplayFrequencyCell(this.allNumbers[numberIndex].ToString(), numberFrequence, this.offsetLeft, this.offsetTop + 2);
                        }

                        break;
                    case ConsoleKey.UpArrow:
                        if (numberIndex >= 10 && this.allNumbers.Length > 10)
                        {
                            numberIndex -= 10;
                            numberFrequence = (this.frequenciesNumbers[numberIndex] * 16) / highestNumberFrequence;
                            this.Render.DisplayFrequencyCell(this.allNumbers[numberIndex].ToString(), numberFrequence, this.offsetLeft, this.offsetTop + 2);
                        }

                        break;
                    case ConsoleKey.DownArrow:
                        if (numberIndex < this.numbersAmount - 10)
                        {
                            numberIndex += 10;
                            numberFrequence = (this.frequenciesNumbers[numberIndex] * 16) / highestNumberFrequence;
                            this.Render.DisplayFrequencyCell(this.allNumbers[numberIndex].ToString(), numberFrequence, this.offsetLeft, this.offsetTop + 2);
                        }

                        break;
                }
            }
            while (true);
        }
    }
}
