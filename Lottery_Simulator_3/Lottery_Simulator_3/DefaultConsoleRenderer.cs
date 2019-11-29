//-----------------------------------------------------------------------
// <copyright file="DefaultConsoleRenderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the DefaultConsoleRenderer class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;
    using System.Collections.Generic;
    
    /// <summary>
    /// This class contains all important methods that all modes need for displaying something on the console.
    /// </summary>
    public class DefaultConsoleRenderer
    {
        /// <summary>
        /// Writes the text with the appropriate foreground color and/or background color.
        /// </summary>
        /// <param name="text">The text that should be written.</param>
        /// <param name="foreground">The color of the foreground.</param>
        /// <param name="background">The color of the background.</param>
        /// <exception cref="ArgumentNullException">The text is null.</exception>
        public void WriteInColor(string text, ConsoleColor foreground = ConsoleColor.White, ConsoleColor background = ConsoleColor.Black)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
            Console.Write($"{text}");
            this.ResetColors();
        }

        /// <summary>
        /// Writes a text into the console.
        /// </summary>
        /// <param name="title">The text that should be written.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        /// <exception cref="ArgumentNullException">The title is null.</exception>
        public void DisplayHeader(string title, int offsetLeft, int offsetTop)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor($"{title}", ConsoleColor.Cyan);

            Console.SetCursorPosition(offsetLeft, offsetTop + 1);
            for (int i = 0; i < title.Length; i++)
            {
                this.WriteInColor("=", ConsoleColor.Cyan);
            }
        }

        /// <summary>
        /// Ensures the console has at least the required size.
        /// Sets the cursor to invisible and clears the console.
        /// </summary>
        /// <param name="requiredWidth">The required window width.</param>
        /// <param name="requiredHeight">The required window height.</param>
        public void SetConsoleSettings(int requiredWidth, int requiredHeight)
        {
            Console.WindowWidth = requiredWidth;
            Console.BufferWidth = Console.WindowWidth;

            Console.WindowHeight = requiredHeight;
            Console.BufferHeight = Console.WindowHeight;

            Console.Clear();
            Console.CursorVisible = false;
        }

        /// <summary>
        /// Displays an error message to the user in red foreground color.
        /// </summary>
        /// <param name="text">The error message for the user.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        /// <exception cref="ArgumentNullException">The text is null.</exception>
        public void DisplayGeneralError(string text, int offsetLeft, int offsetTop)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor(text, ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user for numbers that are not in a range.
        /// </summary>
        /// <param name="limit1">The first limit of the range.</param>
        /// <param name="limit2">The second limit of the range.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayUserRangeError(int limit1, int limit2, int offsetLeft, int offsetTop)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor($"Type in a number between {min} and {max}!", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user for numbers that are not between one of the two ranges.
        /// </summary>
        /// <param name="range1Limit1">The first limit of the first range.</param>
        /// <param name="range1Limit2">The second limit of the first range.</param>
        /// <param name="range2Limit1">The first limit of the second range.</param>
        /// <param name="range2Limit2">The second limit of the second range.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayUserRangesError(int range1Limit1, int range1Limit2, int range2Limit1, int range2Limit2, int offsetLeft, int offsetTop)
        {
            int min1 = (range1Limit1 < range1Limit2) ? range1Limit1 : range1Limit2;
            int max1 = (range1Limit1 > range1Limit2) ? range1Limit1 : range1Limit2;

            int min2 = (range2Limit1 < range2Limit2) ? range2Limit1 : range2Limit2;
            int max2 = (range2Limit1 > range2Limit2) ? range2Limit1 : range2Limit2;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor($"Type in a number between {min1} and {max1} or {min2} and {max2}!", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes a message to the user that he/she has to press enter to continue.
        /// </summary>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayReturnIfEnter(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("Please press enter to return to the main menu.");
        }

        /// <summary>
        /// Displays a menu in the console window.
        /// </summary>
        /// <param name="menuOptions">The modes that are used in this menu.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        /// <exception cref="ArgumentNullException">The mode options array is null.</exception>
        public void DisplayMenu(List<Mode> menuOptions, int offsetLeft, int offsetTop)
        {
            if (menuOptions == null)
            {
                throw new ArgumentNullException(nameof(menuOptions));
            }

            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i);
                this.WriteInColor($"{menuOptions[i].Abbreviation}", ConsoleColor.DarkYellow);
            }

            for (int j = 0; j < menuOptions.Count; j++)
            {
                Console.SetCursorPosition(offsetLeft + 3, offsetTop + j);
                Console.Write($"... {menuOptions[j].Title}");
            }

            Console.SetCursorPosition(offsetLeft, offsetTop + 1 + menuOptions.Count);
            Console.Write("Please enter your Desicion: ");
        }

        /// <summary>
        /// Gets an answer from the user after displaying a text.
        /// </summary>
        /// <param name="text">The ''Question'' the user gets displayed.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        /// <exception cref="ArgumentNullException">The text is null.</exception>
        /// <returns>The users answer.</returns>
        public string GetAnswer(string text, int offsetLeft, int offsetTop)
        {
            if (text == null)
            {
                throw new ArgumentNullException();
            }

            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write(text);

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            string answer = Console.ReadLine();
            this.ResetColors();
            return answer;
        }

        /// <summary>
        /// Resets the foreground and background color to default.
        /// </summary>
        public void ResetColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Writes spaces into the console.
        /// </summary>
        /// <param name="charAmount">The amount of spaces that should be written.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void OverwriteBlank(int charAmount, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            for (int i = 0; i < charAmount; i++)
            {
                Console.Write(" ");
            }
        }

        /// <summary>
        /// Asks the user if he really wants to quit.
        /// </summary>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayExitRequest(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor("Do you really want to quit? (J/N): ", ConsoleColor.Red);
        }

        /// <summary>
        /// Colors the background of the specified position in gray to simulate a ''cursor''.
        /// Deletes everything that is displayed 2 rows over and beneath the position.
        /// </summary>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayVerticalCursor(int offsetLeft, int offsetTop)
        {
            for (int i = -2; i < 3; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i);
                Console.Write(" ");
            }
            
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor(" ", ConsoleColor.White, ConsoleColor.Gray);
        }

        /// <summary>
        /// Displays a message to the console that the user can use the arrow keys to move around.
        /// </summary>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayMoveMessage(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor("Use the arrow keys to move through the display.", ConsoleColor.Green);
        }
    }
}
