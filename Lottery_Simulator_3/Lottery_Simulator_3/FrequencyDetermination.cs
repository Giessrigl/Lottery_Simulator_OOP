using System;
using System.Collections.Generic;


namespace Lottery_Simulator_3
{
    public class FrequencyDetermination : Mode, IEvaluationDependent, IExecuteable
    { 
        /// <summary>
        /// Initializes a new instance of the ManualTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public FrequencyDetermination(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        private int iterations;

        private int offsetLeft = 3;
        private int offsetTop = 6;

        public override void Execute()
        {
            this.Lotto.Render.SetConsoleSettings(90, 45);
            this.Lotto.Render.DisplayHeader(this.Title, offsetLeft, 1);

            PerformPreEvaluation();
            PerformEvaluation();

            this.Render.DisplayReturnIfEnter(offsetLeft, Console.WindowHeight - 2);
            this.Lotto.KeyChecker.WaitForEnter();
        }

        public void PerformPreEvaluation()
        {
            iterations = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of iterations: ", 1, int.MaxValue, offsetLeft, 4);
            this.Render.OverwriteBlank(90, 0, 4);

            this.Render.DisplayFrequencyStatus(iterations, offsetLeft, 4);
        }

        public void PerformEvaluation()
        {
            int cellAmount;
            if (this.Lotto.ActualSystem.Min >= this.Lotto.ActualSystem.BonusNumberMin && this.Lotto.ActualSystem.Max <= this.Lotto.ActualSystem.BonusNumberMax)
            {
                cellAmount = (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1);
            }
            else if (this.Lotto.ActualSystem.BonusNumberMin >= this.Lotto.ActualSystem.Min && this.Lotto.ActualSystem.BonusNumberMax <= this.Lotto.ActualSystem.Max)
            {
                cellAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1);
            }
            else if (this.Lotto.ActualSystem.BonusNumberMin < this.Lotto.ActualSystem.Max && this.Lotto.ActualSystem.BonusNumberMax > this.Lotto.ActualSystem.Max)
            {
                cellAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1) - (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.BonusNumberMin + 1);
            }
            else if (this.Lotto.ActualSystem.Min < this.Lotto.ActualSystem.BonusNumberMax && this.Lotto.ActualSystem.Max > this.Lotto.ActualSystem.BonusNumberMax)
            {
                cellAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1) - (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.Min + 1);
            }
            else
            {
                cellAmount = (this.Lotto.ActualSystem.Max - this.Lotto.ActualSystem.Min + 1) + (this.Lotto.ActualSystem.BonusNumberMax - this.Lotto.ActualSystem.BonusNumberMin + 1);
            }

            double currentIteration = 1.0;
            int[] frequencies = new int[cellAmount];
            int highestFrequence = 0;

            do
            {
                double percentage = Math.Round((currentIteration / iterations) * 100, 2);

                this.Render.UpdateFrequencyStatus(int.Parse(currentIteration.ToString()), percentage, offsetLeft + 11, 4);

                int[] iterationNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                for (int i = 0; i < iterationNumbers.Length; i++)
                {
                    frequencies[iterationNumbers[i] - 1]++;
                }

                this.Render.DisplayFrequenciesAsBlank(cellAmount, offsetLeft + 5, offsetTop);

                for (int j = 0; j < cellAmount; j++)
                {
                    if (frequencies[j] > highestFrequence)
                    {
                        highestFrequence = frequencies[j];
                    }
                    else if (highestFrequence == currentIteration)
                    {
                        break;
                    }
                }

                this.Render.DisplayFrequencyEvaluation(cellAmount, frequencies, highestFrequence, offsetLeft + 5, offsetTop);

                currentIteration++;
            }
            while (currentIteration <= iterations);
        }

            
        

        
    }
}
