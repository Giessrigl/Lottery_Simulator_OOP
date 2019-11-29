//-----------------------------------------------------------------------
// <copyright file="OptionsConsoleRenderer.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the OptionsConsoleRenderer class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// This class contains all important methods that the option modes need for displaying their intermediate steps and their evaluation. 
    /// </summary>
    public class OptionsConsoleRenderer : DefaultConsoleRenderer
    {
        /// <summary>
        /// Displays a list of number systems into the console.
        /// </summary>
        /// <param name="numberSystems">The list of number systems.</param>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayNumberSystems(List<NumberSystem> numberSystems, int offsetLeft, int offsetTop)
        {
            if (numberSystems == null)
            {
                throw new ArgumentNullException(nameof(numberSystems));
            }

            for (int i = 0; i < numberSystems.Count; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i);
                this.WriteInColor(
                    $"{numberSystems.ElementAt(i).NumberAmount} number(s) from " +
                    $"{numberSystems.ElementAt(i).Min} to " +
                    $"{numberSystems.ElementAt(i).Max} and " +
                    $"{numberSystems.ElementAt(i).BonusNumberAmount} bonus number(s) from " +
                    $"{numberSystems.ElementAt(i).BonusNumberMin} to " +
                    $"{numberSystems.ElementAt(i).BonusNumberMax}.  ", 
                    ConsoleColor.DarkYellow);

                if (numberSystems.ElementAt(i).BonusPool)
                {
                    this.WriteInColor("Bonus numbers from own pool.", ConsoleColor.DarkYellow);
                }
                else
                {
                    this.WriteInColor("Bonus numbers from the same pool.", ConsoleColor.DarkYellow);
                }
            }
        }

        /// <summary>
        /// Displays the choices of whether the user wants the bonus numbers in an own pool or in the same pool as the random drawn numbers.
        /// </summary>
        /// <param name="offsetLeft">The indentation from the left rim of the console.</param>
        /// <param name="offsetTop">The indentation from the top rim of the console.</param>
        public void DisplayBonusPoolChoice(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor("Get bonus numbers from the same pool as the other numbers.");

            Console.SetCursorPosition(offsetLeft, offsetTop + 1);
            this.WriteInColor("Get bonus numbers from their own pool.");
        }
    }
}
