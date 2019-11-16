//-----------------------------------------------------------------------
// <copyright file="Renderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// All the methods for writing something into the console window.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class is for interacting with the console window only.
    /// It contains all writing methods for all modes.
    /// </summary>
    public class Renderer
    {
        /// <summary>
        /// The position from left where the whole program will be written into the console window.
        /// </summary>
        private readonly int left;

        /// <summary>
        /// The position from top where the whole program will be written into the console window.
        /// </summary>
        private readonly int top;

        /// <summary>
        /// Initializes a new instance of the Renderer class.
        /// </summary>
        /// <param name="offsetLeft">At which position from left the whole program should be written into the console window.</param>
        /// <param name="offsetTop">At which position from top the whole program should be written into the console window.</param>
        public Renderer(int offsetLeft, int offsetTop)
        {
            this.left = offsetLeft;
            this.top = offsetTop;
        }
        
        /// <summary>
        /// Writes the text with the appropriate foreground color and/or background color.
        /// </summary>
        /// <param name="text">The text that should be written.</param>
        /// <param name="foreground">The color of the foreground.</param>
        /// <param name="background">The color of the background.</param>
        public void WriteInColor(string text, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write($"{text}");
            this.ResetColors();
        }

        /// <summary>
        /// Writes a text with an underline made of '=' into the console.
        /// </summary>
        /// <param name="title">The text that should be written.</param>
        public void DisplayHeader(string title)
        {
            Console.SetCursorPosition(this.left, this.top);
            this.WriteInColor($"{title}", ConsoleColor.Cyan);

            Console.SetCursorPosition(this.left, this.top + 1);
            for (int i = 0; i < title.Length; i++)
            {
                this.WriteInColor("=", ConsoleColor.Cyan);
            }
        }

        /// <summary>
        /// Defines a specific window size.
        /// Sets the cursor to invisible and clears the console.
        /// </summary>
        public void SetConsoleSettings()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(120, 35);
            Console.SetBufferSize(120, 35);
        }

        /// <summary>
        /// Writes an error message to the user after pressing a false key.
        /// </summary>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayUserKeyError(int offsetTop)
        {
            Console.SetCursorPosition(this.left, this.top + offsetTop);
            this.WriteInColor("Wrong Input! Please press enter to continue.", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user after not typing in a number.
        /// </summary>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayUserNumberError(int offsetTop)
        {
            Console.SetCursorPosition(this.left, this.top + 3 + offsetTop);
            this.WriteInColor("Type in a positive, whole number!", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user after the number is not in range of the two limits.
        /// </summary>
        /// <param name="min">The lower limit of the range.</param>
        /// <param name="max">The upper limit of the range.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayUserRangeError(int min, int max, int offsetTop)
        {
            Console.SetCursorPosition(this.left, this.top + 3 + offsetTop);
            this.WriteInColor($"Type in a number between {min} and {max}!", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user after a number should be unique but isn't.
        /// </summary>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayUserUniqueError(int offsetTop)
        {
            Console.SetCursorPosition(this.left, this.top + 3 + offsetTop);
            this.WriteInColor("The number must not be used twice!", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user after causing too many error messages in the console window.
        /// </summary>
        public void ShowErrorQuantityError()
        {
            this.SetConsoleSettings();
            this.WriteInColor("You caused too many errors! Press enter to return to the main menu.", ConsoleColor.Red);
        }

        /// <summary>
        /// Displays the main menu in the console window.
        /// </summary>
        /// <param name="modeOptions">The modes that are used in this lottery simulation.</param>
        public void DisplayMenu(Mode[] modeOptions)
        {
            this.SetConsoleSettings();
            this.DisplayHeader("Welcome to the Lottery Simulator");
           
            for (int i = 0; i < modeOptions.Length; i++)
            {
                Console.SetCursorPosition(this.left + 3, this.top + 3 + i);
                this.WriteInColor($"{modeOptions[i].Abbreviation}", ConsoleColor.DarkYellow);
            }

            for (int j = 0; j < modeOptions.Length; j++)
            {
                Console.SetCursorPosition(this.left + 5, this.top + 3 + j);
                Console.Write($"... {modeOptions[j].Title}");
            }

            Console.SetCursorPosition(this.left, this.top + 5 + modeOptions.Length);
            Console.Write("Please enter your Desicion: ");
        }

        /// <summary>
        /// Asks the user to type in a number.
        /// </summary>
        /// <param name="numberCount">The enumeration for which number should be typed in.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        /// <returns>The input the user has given.</returns>
        public string DisplayInputRequest(int numberCount, int offsetTop)
        {
            Console.SetCursorPosition(this.left, this.top + 3 + offsetTop);
            this.WriteInColor($"Type in the {numberCount}. number: ", ConsoleColor.White);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string input = Console.ReadLine();
            this.ResetColors();
            return input;
        }

        /// <summary>
        /// Displays the chosen numbers, random numbers, how many numbers are equal (starts at 3) and if the bonus number is hit.
        /// </summary>
        /// <param name="chosenNumbers">The numbers the user has chosen.</param>
        /// <param name="randomNumbers">The random generated numbers.</param>
        /// <param name="equalNumbers">The amount of equal numbers between the chosen and random numbers.</param>
        /// <param name="bonusNumber">True if the bonus number is hit.</param>
        /// <param name="offsetLeft">The position from left where the whole text will be written into the console window.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayEvaluation(int[] chosenNumbers, int[] randomNumbers, int equalNumbers, bool bonusNumber, int offsetLeft = 0, int offsetTop = 0)
        {
            Console.SetCursorPosition(this.left + offsetLeft, 3 + this.top + offsetTop);
            Console.Write("Your chosen numbers: ");
            this.WriteInColor(string.Join(", ", chosenNumbers), ConsoleColor.DarkYellow);

            Console.SetCursorPosition(this.left + offsetLeft, 5 + this.top + offsetTop);
            Console.Write("Randomnumbers: ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            for (int i = 0; i < randomNumbers.Length - 1; i++)
            {
                Console.Write($"{randomNumbers[i]}, ");
            }
          
            Console.SetCursorPosition(this.left + offsetLeft, 7 + this.top + offsetTop);
            this.WriteInColor("Bonus number: ", ConsoleColor.White);
            this.WriteInColor($"{randomNumbers[chosenNumbers.Length]}", ConsoleColor.DarkYellow);
           
            Console.SetCursorPosition(this.left + offsetLeft, 9 + this.top + offsetTop);
            this.DisplayOutcome(equalNumbers, bonusNumber, chosenNumbers.Length);
        }

        /// <summary>
        /// Draws a grid with numbers as cell content inside the console window.
        /// </summary>
        /// <param name="rows">The amount of rows the grid has.</param>
        /// <param name="columns">The amount of columns the grid has.</param>
        /// <param name="cellNumbers">The numbers that are inside the grid cells.</param>
        public void DisplayGraphicalGrid(int rows, int columns, int[] cellNumbers)
        {
            int cellindex = 0;
            int cellWidth = 5;
            int cellHeight = 2;

            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    Console.SetCursorPosition(this.left + (j * cellWidth), 4 + this.top + (i * cellHeight));
                    Console.Write("+----+");
                    Console.SetCursorPosition(this.left + (j * cellWidth), 5 + this.top  + (i * cellHeight));
                    if (cellindex < 9)
                    {
                        Console.Write($"| 0{cellNumbers[cellindex]} |");
                    }
                    else
                    {
                        Console.Write($"| {cellNumbers[cellindex]} |");
                    }

                    Console.SetCursorPosition(this.left + (j * cellWidth), 6 + this.top + (i * cellHeight));
                    Console.Write("+----+");
                    cellindex++;
                }
            }
        }

        /// <summary>
        /// Displays where the cursor is now at the grid.
        /// </summary>
        /// <param name="cursorRow">The row of the grid the cursor is at now.</param>
        /// <param name="cursorColumn">The columns of the grid the cursor is at now.</param>
        /// <param name="rows">The amount of rows the grid has.</param>
        public void DisplayCursor(int cursorRow, int cursorColumn, int rows)
        {
            int cellWidth = 5;
            int cellHeight = 2;
            string cellContent;

            Console.SetCursorPosition(1 + this.left + (cursorRow * cellWidth), 5 + this.top + (cursorColumn * cellHeight));
            if (((cursorColumn * rows) + cursorRow + 1) < 10)
            {
                cellContent = $" 0{(cursorColumn * rows) + cursorRow + 1} ";
                this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Gray);
            }
            else
            {
                cellContent = $" {(cursorColumn * rows) + cursorRow + 1} ";
                this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Gray);
            }
        }

        /// <summary>
        /// Displays whether the cell, the cursor is at, is selected or not.
        /// </summary>
        /// <param name="rows">The amount of rows the grid has.</param>
        /// <param name="cursorRow">The row the cursor was until this moment.</param>
        /// <param name="cursorColumn">The column the cursor was until this moment.</param>
        /// <param name="cellsChosen">The array of the statuses of the cells, whether they are selected or not.</param>
        public void DisplayCellStatus(int rows, int cursorRow, int cursorColumn, bool[] cellsChosen)
        {
            int cellWidth = 5;
            int cellHeight = 2;

            int index = (cursorColumn * rows) + cursorRow;

            Console.SetCursorPosition(1 + this.left + (cursorRow * cellWidth), 5 + this.top + (cursorColumn * cellHeight));
            if (!cellsChosen[index])
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            if (((cursorColumn * rows) + cursorRow + 1) < 10)
            {
                Console.Write($" 0{(cursorColumn * rows) + cursorRow + 1} ");
            }
            else
            {
                Console.Write($" {(cursorColumn * rows) + cursorRow + 1} ");
            }

            this.ResetColors();
        }

        /// <summary>
        /// Displays all the numbers of the random numbers that have been drawn with a blue background.
        /// </summary>
        /// <param name="rows">The amount of rows the grid has.</param>
        /// <param name="randomNumbers">The generated random numbers.</param>
        public void DisplayGridRandomNumbers(int rows, int[] randomNumbers)
        {
            const int CellHeight = 2;
            const int CellWidth = 5;

            int cellRow;
            int cellColumn;
            string cellContent;

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                cellRow = (randomNumbers[i] - 1) % rows;
                cellColumn = (randomNumbers[i] - 1 - cellRow) / rows;
                Console.SetCursorPosition(1 + this.left + (cellRow * CellWidth), 5 + this.top + (cellColumn * CellHeight));
                if (((cellColumn * rows) + cellRow + 1) < 10)
                {
                    cellContent = $" 0{(cellColumn * rows) + cellRow + 1} ";
                    this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Blue);
                }
                else
                {
                    cellContent = $" {(cellColumn * rows) + cellRow + 1} ";
                    this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Blue);
                }
            }
        }

        /// <summary>
        /// Displays all the numbers of the user's numbers that have been selected with a red background.
        /// </summary>
        /// <param name="rows">The amount of rows the grid has.</param>
        /// <param name="chosenNumbers">The numbers the user has selected.</param>
        public void DisplayGridUserNumbers(int rows, int[] chosenNumbers)
        {
            const int CellHeight = 2;
            const int CellWidth = 5;

            int cellRow;
            int cellColumn;

            string cellContent;
            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                cellRow = (chosenNumbers[i] - 1) % rows;
                cellColumn = (chosenNumbers[i] - 1 - cellRow) / rows;
                Console.SetCursorPosition(this.left + 1 + (cellRow * CellWidth), 5 + this.top + (cellColumn * CellHeight));
                if (((cellColumn * rows) + cellRow + 1) < 10)
                {
                    cellContent = $" 0{(cellColumn * rows) + cellRow + 1} ";
                    this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Red);
                }
                else
                {
                    cellContent = $" {(cellColumn * rows) + cellRow + 1} ";
                    this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Displays all the numbers of the chosen and random ones that are equal with a green background.
        /// </summary>
        /// <param name="rows">The amount of rows the grid has.</param>
        /// <param name="chosenNumbers">The numbers the user has chosen.</param>
        /// <param name="randomNumbers">The generated random numbers.</param>
        public void DisplayGridEqualNumbers(int rows, int[] chosenNumbers, int[] randomNumbers)
        {
            const int CellHeight = 2;
            const int CellWidth = 5;

            int cellRow;
            int cellColumn;
            string cellContent;

            for (int i = 0; i < randomNumbers.Length; i++)
            {
                for (int j = 0; j < chosenNumbers.Length; j++)
                {
                    if (randomNumbers[i] == chosenNumbers[j])
                    {
                        cellRow = (randomNumbers[i] - 1) % rows;
                        cellColumn = (randomNumbers[i] - 1 - cellRow) / rows;
                        Console.SetCursorPosition(1 + this.left + (cellRow * CellWidth), 5 + this.top + (cellColumn * CellHeight));

                        if (((cellColumn * rows) + cellRow + 1) < 10)
                        {
                            cellContent = $" 0{(cellColumn * rows) + cellRow + 1} ";
                            this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Green);
                        }
                        else
                        {
                            cellContent = $" {(cellColumn * rows) + cellRow + 1} ";
                            this.WriteInColor(cellContent, ConsoleColor.Black, ConsoleColor.Green);
                        }

                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Asks the user how many iterations he would like to have.
        /// </summary>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        /// <returns>The input of the user.</returns>
        public string DisplayIterationsRequest(int offsetTop)
        {
            Console.SetCursorPosition(this.left, 3 + this.top + offsetTop);
            this.WriteInColor("Type in the amount of iterations: ", ConsoleColor.White);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string input = Console.ReadLine();
            this.ResetColors();
            return input;
        }

        /// <summary>
        /// Displays a list of the numbers 1 to 45 and a bar for the frequency next to every number.
        /// </summary>
        /// <param name="iterations">The amount of iterations the user has chosen.</param>
        public void DisplayFrequencyGrid(int iterations)
        {
            Console.SetCursorPosition(this.left, this.top + 4);
            Console.Write("Current draw: ");
            Console.Write($"      / {iterations}");

            Console.SetCursorPosition(this.left, this.top + 6);
            Console.WriteLine("Frequencies:");

            for (int i = 1; i < 24; i++)
            {
                Console.SetCursorPosition(this.left + 1, this.top + 6 + i);
                if (i < 10)
                {
                    Console.Write(" ");
                }

                Console.Write($"{i}: |                |");
            }

            for (int j = 1; j < 23; j++)
            {
                Console.SetCursorPosition(this.left + 25, this.top + 6 + j);
                Console.Write($"{j + 23}: |                |");
            }
        }

        /// <summary>
        /// Updates the status of the current iteration and the percentage of the whole process.
        /// </summary>
        /// <param name="draw">The amount of draws until now.</param>
        /// <param name="percentage">The percentage of the whole process.</param>
        public void UpdateFrequencyStatus(int draw, double percentage)
        {
            Console.SetCursorPosition(this.left + 15, this.top + 4);
            if (draw < 10)
            {
                Console.Write(" ");
            }

            Console.Write($"{draw}");

            Console.SetCursorPosition(this.left + 27, this.top + 4);
            Console.Write("                 ");
            Console.SetCursorPosition(this.left + 27, this.top + 4);
            Console.Write($"({percentage} %)");
        }

        /// <summary>
        /// Turns every bar of the frequency grid into a blank bar.
        /// </summary>
        public void DisplayFrequenciesAsBlank()
        {
            for (int i = 1; i < 46; i++)
            {
                if (i < 24)
                {
                    Console.SetCursorPosition(this.left + 6, this.top + 7 + i - 1);
                }
                else
                {
                    Console.SetCursorPosition(this.left + 30, this.top + 7 + i - 24);
                }

                for (int j = 0; j < 16; j++)
                {
                    Console.Write(" ");
                }
            }
        }

        /// <summary>
        /// Displays how often the number has been drawn until now into the bar.
        /// </summary>
        /// <param name="frequencies">The amount of how often a number has been drawn relative to the other numbers.</param>
        /// <param name="highestFrequence">The highest frequency possible.</param>
        public void DisplayFrequencyEvaluation(int[] frequencies, int highestFrequence)
        {
            for (int i = 1; i < 46; i++)
            {
                int numberFrequence = (frequencies[i - 1] * 16) / highestFrequence;

                if (i < 24)
                {
                    Console.SetCursorPosition(this.left + 6, this.top + 7 + i - 1);
                }
                else
                {
                    Console.SetCursorPosition(this.left + 30, this.top + 7 + i - 24);
                }

                if (numberFrequence > 0)
                {
                    for (int j = 0; j < numberFrequence; j++)
                    {
                        Console.Write("+");
                    }
                }
            }
        }

        /// <summary>
        /// Writes the user's numbers into the console window and leaves the draws until jackpot line with three spots.
        /// </summary>
        /// <param name="chosenNumbers">The numbers the user has chosen.</param>
        public void ShowJackpotDisplay(int[] chosenNumbers)
        {
            string values = string.Join(", ", chosenNumbers);

            Console.SetCursorPosition(this.left, this.top + 3);
            Console.Write("Your numbers: ");

            this.WriteInColor(values, ConsoleColor.DarkYellow);
            
            Console.SetCursorPosition(this.left, this.top + 4);
            Console.Write("Draws until jackpot: ");
            this.WriteInColor("...", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Displays how many iterations it needed to be a jackpot.
        /// </summary>
        /// <param name="iterations">The amount of iterations.</param>
        public void DisplayJackpotIterations(int iterations)
        {
            Console.SetCursorPosition(this.left + 20, this.top + 4);
            Console.Write("    ");
            Console.SetCursorPosition(this.left + 21, this.top + 4);
            this.WriteInColor($"{iterations}", ConsoleColor.DarkYellow);
        }

        /// <summary>
        /// Asks the user if he really wants to quit.
        /// </summary>
        public void DisplayExitRequest()
        {
            Console.SetCursorPosition(this.left, this.top + 3);
            this.WriteInColor("Do you really want to quit? (J/N): ", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes a message to the user that he/she has to press enter to continue.
        /// </summary>
        public void DisplayReturnIfEnter()
        {
            Console.SetCursorPosition(this.left, Console.WindowHeight - 3);
            Console.Write("Please press enter to return to the main menu.");
        }

        /// <summary>
        /// Displays the evaluation of how many numbers are hit (starts to write at 3 equal numbers) and if the bonus number is hit or not.
        /// </summary>
        /// <param name="equalNumbers">The amount of numbers that are in common.</param>
        /// <param name="bonusNumber">True if the bonus number is hit.</param>
        /// <param name="maxNumbers">The amount of the maximum numbers that can be equal.</param>
        private void DisplayOutcome(int equalNumbers, bool bonusNumber, int maxNumbers)
        {
            if (equalNumbers > maxNumbers)
            {
                Console.Write("You hit the Jackpot!");
            }
            else if (equalNumbers > 2 && equalNumbers < maxNumbers && bonusNumber)
            {
                Console.Write($"You hit {equalNumbers} numbers and the bonus number!");
            }
            else if (equalNumbers > 2 && equalNumbers < maxNumbers)
            {
                Console.Write($"You hit {equalNumbers} numbers!");
            }
            else if (bonusNumber)
            {
                Console.Write("You hit the bonus number!");
            }
            else
            {
                Console.Write("You drew a blank.");
            }
        }

        /// <summary>
        /// Resets the foreground and background color to default.
        /// </summary>
        private void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}