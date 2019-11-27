using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    public class DefaultRenderer
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
            
            Console.BufferWidth = 1500;
            Console.WindowWidth = requiredWidth;
            
            Console.BufferHeight = 5000;
            Console.WindowHeight = requiredHeight;
            
            Console.Clear();
            Console.CursorVisible = false;
        }

        public void DisplayGeneralError(string text, int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor(text, ConsoleColor.Red);
        }

        /// <summary>
        /// Writes an error message to the user after the number is not in range of the two limits.
        /// </summary>
        /// <param name="min">The lower limit of the range.</param>
        /// <param name="max">The upper limit of the range.</param>
        /// <param name="offsetTop">The position from top where the whole text will be written into the console window.</param>
        public void DisplayUserRangeError(int limit1, int limit2, int offsetLeft, int offsetTop)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;

            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor($"Type in a number between {min} and {max}!", ConsoleColor.Red);
        }

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
        /// Writes an error message to the user after causing too many error messages in the console window.
        /// </summary>
        public void ShowErrorQuantityError()
        {
            this.SetConsoleSettings(92, 35);
            this.WriteInColor("You caused too many errors! Press enter to return to the main menu.", ConsoleColor.Red);
        }

        /// <summary>
        /// Writes a message to the user that he/she has to press enter to continue.
        /// </summary>
        public void DisplayReturnIfEnter(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            Console.Write("Please press enter to return to the main menu.");
        }

        /// <summary>
        /// Displays the main menu in the console window.
        /// </summary>
        /// <param name="menuOptions">The modes that are used in this lottery simulation.</param>
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
        public void DisplayExitRequest(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor("Do you really want to quit? (J/N): ", ConsoleColor.Red);
        }

        public void DisplayCursor(int offsetLeft, int offsetTop)
        {
            for (int i = -2; i < 3; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i);
                Console.Write(" ");
            }
            
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor(" ", ConsoleColor.White, ConsoleColor.Gray);
        }


    }
}
