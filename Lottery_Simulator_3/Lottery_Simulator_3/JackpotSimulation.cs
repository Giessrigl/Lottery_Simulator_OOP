//-----------------------------------------------------------------------
// <copyright file="JackpotSimulation.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the JackpotSimulation class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class for the JackpotSimulation mode.
    /// It iterates through drawn numbers until all are in common with the users chosen numbers, then displays the user the amount of iterations it had to do.
    /// </summary>
    public class JackpotSimulation : Mode, IEvaluable, IExecuteable
    {
        /// <summary>
        /// The indentation from the left rim of the console, where everything will be written.
        /// </summary>
        private int offsetLeft = 3;

        /// <summary>
        /// The indentation from the top rim of the console, where everything will be written.
        /// </summary>
        private int offsetTop = 5;

        /// <summary>
        /// The users chosen Numbers.
        /// </summary>
        private int[] chosenNumbers;

        /// <summary>
        /// The random drawn numbers.
        /// </summary>
        private int[] randomNumbers;

        /// <summary>
        /// The amount of chosen numbers that are equal with the random numbers.
        /// </summary>
        private int equalNumbers;

        /// <summary>
        /// Initializes a new instance of the <see cref="JackpotSimulation"/> class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public JackpotSimulation(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        /// <summary>
        /// This method contains the steps that have to be made to display the iterations until the jackpot with the numbers from the current number system.
        /// </summary>
        public override void Execute()
        {
            this.Render.SetConsoleSettings(55, 20);
            this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);
          
            this.PerformPreEvaluation();

            this.Render.SetConsoleSettings(55, 20);
            this.Render.DisplayHeader(this.Title, this.offsetLeft, this.offsetTop - 4);
            
            this.PerformEvaluation();
        }

        /// <summary>
        /// Examines all the calculations that have to be done before evaluating.
        /// </summary>
        public void PerformPreEvaluation()
        {
            this.chosenNumbers = new int[this.Lotto.ActualSystem.NumberAmount];
            int number;

            // choose the normal numbers
            for (int i = 0; i < this.chosenNumbers.Length; i++)
            {
                this.Render.OverwriteBlank(55, 0, this.offsetTop);
                string question = $"Please enter the {i + 1}. number: ";
                do
                {
                    string input = this.Lotto.Renderer.GetAnswer(question, this.offsetLeft, this.offsetTop);
                    this.Render.OverwriteBlank(55, 0, this.offsetTop + 1);

                    if (input.Length >= int.MaxValue.ToString().Length)
                    {
                        this.Render.OverwriteBlank(input.Length + 1, this.offsetLeft + question.Length, this.offsetTop);
                        this.Render.DisplayGeneralError("Your input is too long!", this.offsetLeft, this.offsetTop + 1);
                        continue;
                    }

                    if (!int.TryParse(input, out number))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, this.offsetLeft + question.Length, this.offsetTop);
                        this.Render.DisplayGeneralError("Type in a positive, whole number!", this.offsetLeft, this.offsetTop + 1);
                        continue;
                    }

                    if (!this.Lotto.NumberChecker.IsInRange(number, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, this.offsetLeft + question.Length, this.offsetTop);
                        this.Render.DisplayUserRangeError(this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max, this.offsetLeft, this.offsetTop + 1);
                        continue;
                    }

                    if (!this.Lotto.NumberChecker.IsUnique(number, this.chosenNumbers))
                    {
                        this.Render.OverwriteBlank(input.Length + 1, this.offsetLeft + question.Length, this.offsetTop);
                        this.Render.DisplayGeneralError("The number must not be used twice!", this.offsetLeft, this.offsetTop + 1);
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

        /// <summary>
        /// Examines all calculations that have to be done to display the evaluation and displays the evaluation.
        /// </summary>
        public void PerformEvaluation()
        {
            int iterations = 0;
            this.Render.ShowJackpotDisplay(this.offsetLeft, this.offsetTop);
            this.Render.DisplayEvaluationNumber(this.chosenNumbers[0], this.offsetLeft + 1, this.offsetTop + 1);
            
            do
            {
                this.randomNumbers = this.Lotto.NumberGen.GenerateUniquesInRange(this.Lotto.ActualSystem.NumberDraws, this.Lotto.ActualSystem.Min, this.Lotto.ActualSystem.Max);
                this.equalNumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, this.randomNumbers);
                iterations++;
            }
            while (this.equalNumbers < this.Lotto.ActualSystem.NumberAmount);
            
            this.Render.DisplayMoveMessage(this.offsetLeft, this.offsetTop - 1);
            this.Render.DisplayJackpotIterations(iterations, 23, this.offsetTop + 2);
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
                            this.Render.DisplayEvaluationNumber(this.chosenNumbers[numberIndex], this.offsetLeft + 1, this.offsetTop + 1);
                        }

                        break;
                    case ConsoleKey.RightArrow:
                        if (numberIndex < this.chosenNumbers.Length - 1)
                        {
                            numberIndex++;
                            this.Render.DisplayEvaluationNumber(this.chosenNumbers[numberIndex], this.offsetLeft + 1, this.offsetTop + 1);
                        }

                        break;
                }
            }
            while (true);
        }
    }
}
