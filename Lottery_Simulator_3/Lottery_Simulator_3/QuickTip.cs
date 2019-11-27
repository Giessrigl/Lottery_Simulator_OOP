using System;
using System.Collections.Generic;

namespace Lottery_Simulator_3
{
    public class QuickTip : Mode , IEvaluationDependent, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the ManualTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public QuickTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        private int[] chosenNumbers;
        private int equalNumbers;
        private int equalBonusNumbers;
        private int[] randomNumbers;
        private int[] randomBonusNumbers;

        private int OffsetLeft = 3;

        private int OffsetTop = 4;

        public override void Execute()
        {
            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, 3, 1);
            PerformPreEvaluation();

            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, OffsetLeft, 1);
            PerformEvaluation();
        }

        public void PerformPreEvaluation()
        {
            // generate uniques everytime for the user, doesnt matter if bonus pool or not!
            chosenNumbers = this.Lotto.NumberGen.GenerateUniquesInRanges(this.Lotto.ActualSystem.NumberDraws, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
            
        }

        public void PerformEvaluation()
        {
            randomNumbers = new int[this.Lotto.ActualSystem.NumberAmount];
            randomBonusNumbers = new int[this.Lotto.ActualSystem.BonusNumberAmount];
            int[] allRandomNumbers;


            if (this.Lotto.ActualSystem.BonusPool)
            {
                randomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                {
                    randomBonusNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                }
            }
            else
            {
                if (this.Lotto.ActualSystem.Min >= this.Lotto.ActualSystem.BonusNumberMin && this.Lotto.ActualSystem.Max <= this.Lotto.ActualSystem.BonusNumberMax)
                {
                    allRandomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax);
                    for (int i = 0; i < this.Lotto.ActualSystem.NumberAmount; i++)
                    {
                        randomNumbers[i] = allRandomNumbers[i];
                    }
                    if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                    {
                        for (int i = this.Lotto.ActualSystem.NumberAmount; i < this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount; i++)
                        {
                            randomBonusNumbers[i - this.Lotto.ActualSystem.NumberAmount] = allRandomNumbers[i];
                        }
                    }
                }
                else if (this.Lotto.ActualSystem.BonusNumberMin >= this.Lotto.ActualSystem.Min && this.Lotto.ActualSystem.BonusNumberMax <= this.Lotto.ActualSystem.Max)
                {
                    allRandomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                    for (int i = 0; i < this.Lotto.ActualSystem.NumberAmount; i++)
                    {
                        randomNumbers[i] = allRandomNumbers[i];
                    }
                    if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
                    {
                        for (int i = this.Lotto.ActualSystem.NumberAmount; i < this.Lotto.ActualSystem.NumberAmount + this.Lotto.ActualSystem.BonusNumberAmount; i++)
                        {
                            randomBonusNumbers[i - this.Lotto.ActualSystem.NumberAmount] = allRandomNumbers[i];
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
                                randomNumbers[i] = allRandomNumbers[i];
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
                                    randomBonusNumbers[i] = allRandomNumbers[i];
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }

            equalNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, randomNumbers);
            if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
            {
                equalBonusNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, randomBonusNumbers);
            }

            int chosenIndex = 0;
            int randomIndex = 0;
            int bonusIndex = 0;

            int cursorTop = 0;

            this.Render.DisplayEvaluation(this.Lotto.ActualSystem.BonusNumberAmount, 4, 4);
            this.Render.DisplayEvaluationNumber(chosenNumbers[0], 5, 5);
            this.Render.DisplayEvaluationNumber(randomNumbers[0], 5, 7);
            if (this.Lotto.ActualSystem.BonusNumberAmount > 0)
            {
                this.Render.DisplayEvaluationNumber(randomBonusNumbers[0], 5, 9);
            }

            if (this.Lotto.ActualSystem.BonusPool)
            {
                this.Render.DisplayOutcomeOwnPool(equalNumbers, equalBonusNumbers, 5, 11);
            }
            else
            {
                this.Render.DisplayOutcomeIdentPool(equalNumbers, equalBonusNumbers, this.Lotto.ActualSystem.NumberDraws, 5, 11);
            }
            this.Render.DisplayReturnIfEnter(OffsetLeft, Console.WindowHeight - 2);
            do
            {
                this.Render.DisplayCursor(3, 5 + cursorTop);
                ConsoleKeyInfo userKey = Console.ReadKey(true);
                if (userKey.Key == ConsoleKey.Enter)
                {
                    break;
                }

                switch (userKey.Key)
                {
                    case (ConsoleKey.UpArrow):
                        if (cursorTop > 0)
                        {
                            cursorTop -= 2;
                        }
                        break;
                    case (ConsoleKey.DownArrow):
                        if (cursorTop < 4)
                        {
                            cursorTop += 2;
                        }
                        break;
                    case (ConsoleKey.LeftArrow):
                        if (cursorTop == 0)
                        {
                            if (chosenIndex > 0)
                            {
                                chosenIndex--;
                                this.Render.DisplayEvaluationNumber(chosenNumbers[chosenIndex], 5, 5);
                            }
                        }
                        else if (cursorTop == 2)
                        {
                            if (randomIndex > 0)
                            {
                                randomIndex--;
                                this.Render.DisplayEvaluationNumber(randomNumbers[randomIndex], 5, 7);
                            }
                        }
                        else if (cursorTop == 4)
                        {
                            if (bonusIndex > 0 && this.Lotto.ActualSystem.BonusNumberAmount > 0)
                            {
                                bonusIndex--;
                                this.Render.DisplayEvaluationNumber(randomBonusNumbers[bonusIndex], 5, 9);
                            }
                        }
                        break;
                    case (ConsoleKey.RightArrow):
                        if (cursorTop == 0)
                        {
                            if (chosenIndex < chosenNumbers.Length - 1)
                            {
                                chosenIndex++;
                                this.Render.DisplayEvaluationNumber(chosenNumbers[chosenIndex], 5, 5);
                            }
                        }
                        else if (cursorTop == 2)
                        {
                            if (randomIndex < randomNumbers.Length - 1)
                            {
                                randomIndex++;
                                this.Render.DisplayEvaluationNumber(randomNumbers[randomIndex], 5, 7);
                            }
                        }
                        else if (cursorTop == 4)
                        {
                            if (bonusIndex < randomBonusNumbers.Length - 1 && this.Lotto.ActualSystem.BonusNumberAmount > 0)
                            {
                                bonusIndex++;
                                this.Render.DisplayEvaluationNumber(randomBonusNumbers[bonusIndex], 5, 9);
                            }
                        }
                        break;
                }
            }
            while (true);
        }
    }
}
