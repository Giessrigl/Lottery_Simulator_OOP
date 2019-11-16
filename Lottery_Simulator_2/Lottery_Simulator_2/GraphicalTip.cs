//-----------------------------------------------------------------------
// <copyright file="GraphicalTip.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Creates a grid of cells with numbers as content and allows the user to move around inside the grid and select his/her numbers through the keyboard.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class calculates a grid and shows the user in which cell he/she is. After selecting all of his/her numbers, the user can see an evaluation how many of his/her numbers are equal to random generated ones.
    /// </summary>
    public class GraphicalTip : Mode
    {
        /// <summary>
        /// Each cell content is written in here.
        /// </summary>
        private int[] cellNumbers;

        /// <summary>
        /// The numbers the user has already chosen.
        /// </summary>
        private int[] chosenNumbers;

        /// <summary>
        /// Has the same size as the grid (rows and columns)
        /// True if the cell has been selected, false if not.
        /// </summary>
        private bool[] cellsChosen;

        /// <summary>
        /// How many numbers the user has already chosen.
        /// </summary>
        private int selectedNumbers;

        /// <summary>
        /// The rows of the grid.
        /// </summary>
        private int rows;

        /// <summary>
        /// The columns of the grid.
        /// </summary>
        private int columns;

        /// <summary>
        /// Initializes a new instance of the GraphicalTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>
        public GraphicalTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : 
        base(title, abbreviation, uniqueChars, lotto)
        {  
        }

        /// <summary>
        /// Calculates the columns and rows, displays a grid, based on the rows and columns, with numbers as content, based on the limits of numbers.
        /// Allows the user to move around inside the grid and select his/her numbers.
        /// After he/she is finished, the evaluation will be displayed next to the grid.
        /// </summary>
        public override void Execute()
        {
            // Determines the height and width of the Grid and Evaluates which numbers to place inside the grid.
            int valueAmount = this.Lotto.Max - this.Lotto.Min + 1;
            this.columns = this.CalculateColumns(valueAmount);
            this.rows = valueAmount / this.columns;

            try
            {
                this.cellNumbers = this.EvaluateCellNumbers(this.Lotto.Min, this.Lotto.Max);
            }
            catch
            {
                throw new IndexOutOfRangeException();
            }

            // Displays the grid.
            this.Lotto.Render.SetConsoleSettings();
            this.Lotto.Render.DisplayHeader(this.Title);
            this.Lotto.Render.DisplayGraphicalGrid(this.rows, this.columns, this.cellNumbers);

            // Does the selecting part
            try
            {
                this.SelectNumbers();
            }
            catch
            {
                throw new ArgumentOutOfRangeException();
            }

            // Evaluates the outcome.
            int[] randomNumbers = this.Lotto.NumberGen.Generate(this.Lotto.Amount + 1, this.Lotto.Min, this.Lotto.Max);
            int equalnumbers = this.Lotto.NumberChecker.CompareNumbers(this.chosenNumbers, randomNumbers);
            bool bonusNumber = this.Lotto.NumberChecker.CompareBonus(this.chosenNumbers, randomNumbers);

            // the 4 is just to not stick on the grid with the evaluation.
            int offsetLeft = 4 + (this.rows * 5);
            int offsetTop = 2;

            this.Lotto.Render.DisplayGridRandomNumbers(this.rows, randomNumbers);
            this.Lotto.Render.DisplayGridUserNumbers(this.rows, this.chosenNumbers);
            this.Lotto.Render.DisplayGridEqualNumbers(this.rows, this.chosenNumbers, randomNumbers);
            this.Lotto.Render.DisplayEvaluation(this.chosenNumbers, randomNumbers, equalnumbers, bonusNumber, offsetLeft, offsetTop);
            this.Lotto.Render.DisplayReturnIfEnter();

            // press enter
            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Calculates the columns of the grid based on how many cells there will be.
        /// </summary>
        /// <param name="cellAmount">The amount of cells.</param>
        /// <returns>The amount of columns of the grid.</returns>
        private int CalculateColumns(int cellAmount)
        {
            int columns = 0; 

            for (int i = 10; i > 1; i--)
            {
                if (cellAmount % i == 0)
                {
                    columns = cellAmount / i;
                    break;
                }
                else if (i < 4)
                {
                    throw new ArgumentOutOfRangeException(nameof(cellAmount));
                }
            }

            return columns;
        }

        /// <summary>
        /// Calculates all cell content numbers and puts them in an array.
        /// </summary>
        /// <param name="min">The lower limit of numbers.</param>
        /// <param name="max">The upper limit of numbers.</param>
        /// <returns>The array of cell contents.</returns>
        private int[] EvaluateCellNumbers(int min, int max)
        {
            int cells = this.rows * this.columns;
            int[] cellNumbers = new int[cells];

            for (int i = min; i <= max; i++)
            {
                cellNumbers[i - 1] += i;
            }

            return cellNumbers;
        }

        /// <summary>
        /// Calculates the movements of the user and which numbers he has selected.
        /// </summary>
        private void SelectNumbers()
        {
            this.cellsChosen = new bool[this.rows * this.columns];
            this.chosenNumbers = new int[this.Lotto.Amount];
            this.selectedNumbers = 0;

            int cursorRow = 0;
            int cursorColumn = 0;

            do
            {
                this.Lotto.Render.DisplayCursor(cursorRow, cursorColumn, this.rows);

                ConsoleKey userKey = Console.ReadKey(true).Key;

                this.Lotto.Render.DisplayCellStatus(this.rows, cursorRow, cursorColumn, this.cellsChosen);

                if (userKey == ConsoleKey.Enter && this.selectedNumbers >= this.Lotto.Amount)
                {
                    this.DetermineSelectedNumbers();
                    break;
                }

                int index = (cursorColumn * this.rows) + cursorRow;
                switch (userKey)
                {
                    case ConsoleKey.Spacebar:
                        this.ChangeSelectedNumbers(index);
                        break;

                    case ConsoleKey.UpArrow:
                        if (cursorColumn >= 1)
                        {
                            cursorColumn--;
                        }

                        break;

                    case ConsoleKey.DownArrow:
                        if (cursorColumn <= this.columns - 2)
                        {
                            cursorColumn++;
                        }

                        break;

                    case ConsoleKey.LeftArrow:
                        if (cursorRow >= 1)
                        {
                            cursorRow--;
                        }

                        break;

                    case ConsoleKey.RightArrow:
                        if (cursorRow <= this.rows - 2)
                        {
                            cursorRow++;
                        }

                        break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Changes the status of the boolean array of chosen cells and changes the amount of already selected numbers.
        /// </summary>
        /// <param name="index">The array index of the current cell.</param>
        private void ChangeSelectedNumbers(int index)
        {
            if (!this.cellsChosen[index] && this.selectedNumbers < this.chosenNumbers.Length)
            {
                this.selectedNumbers++;
                this.cellsChosen[index] = true;
            }
            else if (this.cellsChosen[index])
            {
                this.selectedNumbers--;
                this.cellsChosen[index] = false;
            }
        }

        /// <summary>
        /// Calculates which cell numbers are selected based on the boolean array of chosen cells.
        /// </summary>
        private void DetermineSelectedNumbers()
        {
            this.chosenNumbers = new int[this.selectedNumbers];
            int index = 0;

            for (int i = 0; i < this.cellsChosen.Length; i++)
            {
                if (this.cellsChosen[i])
                {
                    this.chosenNumbers[index] = 1 + i;
                    index++;
                }

                if (index >= this.selectedNumbers)
                {
                    break;
                }
            }
        }
    }
}
