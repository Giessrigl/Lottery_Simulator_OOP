using System;
using System.Linq;

namespace Lottery_Simulator_3
{
    public class ManualTip : Mode, IEvaluationDependent, IExecuteable
    {
        /// <summary>
        /// The numbers the user has chosen.
        /// </summary>
        private int[] chosenNumbers;
        private int equalNumbers;
        private int equalBonusNumbers;
        private int[] randomNumbers;
        private int[] randomBonusNumbers;

        private int OffsetLeft = 3;

        private int OffsetTop = 4;

        /// <summary>
        /// Initializes a new instance of the ManualTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public ManualTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
            
        }

        public override void Execute()
        {
            this.Render.SetConsoleSettings(90, 40);
            this.Render.DisplayHeader(this.Title, OffsetLeft, 1);
            this.PerformPreEvaluation();

            this.Render.SetConsoleSettings(200, 40);
            this.Render.DisplayHeader(this.Title, OffsetLeft, 1);
            this.PerformEvaluation();
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

                    if (input.Length > int.MaxValue.ToString().Length)
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

                    if (!this.Lotto.NumberChecker.IsInBothRanges(number, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, OffsetLeft + question.Length, i + OffsetTop);
                        this.Render.DisplayUserRangesError(this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.Lotto.ActualSystem.BonusNumberMin, this.Lotto.ActualSystem.BonusNumberMax, OffsetLeft, i + OffsetTop + 1);
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

                switch(userKey.Key)
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
