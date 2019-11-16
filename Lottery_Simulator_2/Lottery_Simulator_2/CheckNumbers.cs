//-----------------------------------------------------------------------
// <copyright file="CheckNumbers.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Determines the validity of a number or compares numbers with other numbers
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This is a class to check or compare numbers.
    /// </summary>
    public class CheckNumbers
    {
        /// <summary>
        /// Compares the user's chosen numbers with the random generated numbers.
        /// </summary>
        /// <param name="chosenNumbers">The user's chosen numbers.</param>
        /// <param name="randomNumbers">The generator's random numbers.</param>
        /// <returns>The amount of equal numbers.</returns>
        public int CompareNumbers(int[] chosenNumbers, int[] randomNumbers)
        {
            int equalNumbers = 0;

            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                for (int j = 0; j < chosenNumbers.Length; j++)
                {
                    if (chosenNumbers[i] == randomNumbers[j])
                    {
                        equalNumbers += 1;
                        break;
                    }
                }
            }

            return equalNumbers;
        }

        /// <summary>
        /// Compares the user's chosen numbers with the last random generated number in the array.
        /// </summary>
        /// <param name="chosenNumbers">The user's chosen numbers.</param>
        /// <param name="randomNumbers">The generator's random numbers.</param>
        /// <returns>If one of the chosen numbers is the same as the last number in the random numbers (true).</returns>
        public bool CompareBonus(int[] chosenNumbers, int[] randomNumbers)
        {
            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                if (chosenNumbers[i] == randomNumbers[randomNumbers.Length - 1])
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the number is in between the two limits.
        /// </summary>
        /// <param name="number">The number that should be checked.</param>
        /// <param name="limit1">The first limit of numbers the user can use.</param>
        /// <param name="limit2">The second limit of numbers the user can use.</param>
        /// <returns>Whether the number is in range (true) or not in range (false) of the two limits.</returns>
        public bool IsInRange(int number, int limit1, int limit2)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;

            return !(number < min || number > max);
        }

        /// <summary>
        /// Checks already chosen numbers contains the number.
        /// </summary>
        /// <param name="number">The number that has to be checked if it is already in the chosen numbers.</param>
        /// <param name="chosenNumbers">The already chosen numbers.</param>
        /// <returns>Whether the number is already in the array of chosen numbers (false) or not in the array (true).</returns>
        public bool IsUnique(int number, int[] chosenNumbers)
        {
            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                if (number == chosenNumbers[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
