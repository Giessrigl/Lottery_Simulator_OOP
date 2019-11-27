using System;
using System.Collections.Generic;


namespace Lottery_Simulator_3
{
    public class JackpotSimulation : Mode, IEvaluationDependent, IExecuteable
    {
        private int OffsetLeft = 3;

        private int OffsetTop = 4;

        private int[] chosenNumbers;
        /// <summary>
        /// Initializes a new instance of the ManualTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public JackpotSimulation(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        public override void Execute()
        {
            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, 3, 1);
          
            PerformPreEvaluation();

            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, 3, 1);
            this.Render.ShowJackpotDisplay(chosenNumbers, 3, 4);

            PerformEvaluation();

            this.Render.DisplayReturnIfEnter(OffsetLeft, Console.WindowHeight - 2);
            this.Lotto.KeyChecker.WaitForEnter();
        }

        public void PerformPreEvaluation()
        {
            this.chosenNumbers = new int[this.Lotto.ActualSystem.NumberDraws];

            // choose the normal numbers
            for (int i = 0; i < this.chosenNumbers.Length; i++)
            {
                string question = $"Please enter the {i + 1}. number: ";
                do
                {
                    string input = this.Lotto.Render.GetAnswer(question, OffsetLeft, i + OffsetTop);
                    this.Render.OverwriteBlank(90, 0, i + OffsetTop + 1);

                    if (input.Length >= int.MaxValue.ToString().Length)
                    {
                        this.Render.OverwriteBlank(input.Length + 1, OffsetLeft + question.Length, i + OffsetTop);
                        this.Render.DisplayGeneralError("Your input is too long!", OffsetLeft, i + OffsetTop + 1);
                        continue;
                    }

                    if (!int.TryParse(input, out int number))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, OffsetLeft + question.Length, i + OffsetTop);
                        this.Render.DisplayGeneralError("Type in a positive, whole number!", OffsetLeft, i + OffsetTop + 1);
                        continue;
                    }

                    if (!this.Lotto.NumberChecker.IsInRange(number, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, OffsetLeft + question.Length, i + OffsetTop);
                        this.Render.DisplayUserRangeError(this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, OffsetLeft, i + OffsetTop + 1);
                        continue;
                    }

                    if (!this.Lotto.NumberChecker.IsUnique(number, this.chosenNumbers))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, OffsetLeft + question.Length, i + OffsetTop);
                        this.Render.DisplayGeneralError("The number must not be used twice!", OffsetLeft, i + OffsetTop + 1);
                        continue;
                    }
                    else
                    {
                        this.chosenNumbers[i] = number;
                        break;
                    }
                }
                while (true);
            }
        }

        public void PerformEvaluation()
        {
            int iterations = 0;
            int equalNumbers;
            
            do
            {
                int[] randomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                equalNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, randomNumbers);
                iterations++;
            }
            while (equalNumbers < this.Lotto.ActualSystem.NumberAmount);
            
            this.Render.DisplayJackpotIterations(iterations , 23, 6);
        }
    }
}
