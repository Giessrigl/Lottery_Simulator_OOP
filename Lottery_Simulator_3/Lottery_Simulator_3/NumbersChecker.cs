//-----------------------------------------------------------------------
// <copyright file="CheckNumbers.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Determines the validity of a number or compares numbers with other numbers
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This is a class to check or compare numbers.
    /// </summary>
    public class NumbersChecker
    {

        /// <summary>
        /// Compares the user's chosen numbers with the random generated numbers.
        /// </summary>
        /// <param name="chosenNumbers">The user's chosen numbers.</param>
        /// <param name="randomNumbers">The generator's random numbers.</param>
        /// <exception cref="ArgumentNullException">The chosen numbers array or the random numbers array is null.</exception>
        /// <returns>The amount of equal numbers.</returns>
        public int CompareNumbers(int[] chosenNumbers, int[] randomNumbers)
        {
            if (chosenNumbers == null)
            {
                throw new ArgumentNullException(nameof(chosenNumbers));
            }

            if (randomNumbers == null)
            {
                throw new ArgumentNullException(nameof(randomNumbers));
            }

            int equalNumbers = 0;

            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                for (int j = 0; j < randomNumbers.Length; j++)
                {
                    if (chosenNumbers[i] == randomNumbers[j])
                    {
                        equalNumbers ++;
                        break;
                    }
                }
            }

            return equalNumbers;
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

        public bool IsInBothRanges(int number, int range1Limit1, int range1Limit2, int range2Limit1, int range2Limit2)
        {
            int min1 = (range1Limit1 < range1Limit2) ? range1Limit1 : range1Limit2;
            int max1 = (range1Limit1 > range1Limit2) ? range1Limit1 : range1Limit2;

            int min2 = (range2Limit1 < range2Limit2) ? range2Limit1 : range2Limit2;
            int max2 = (range2Limit1 > range2Limit2) ? range2Limit1 : range2Limit2;

            if (number >= min1 && number <= max1)
            {
                return true;
            }
            else if (number >= min2 && number <= max2)
            {
                return true;
            }
            return false;
        }

        public bool IsMaxTwice(int number, int[] chosenNumbers)
        {
            int count = 0;

            for (int i = 0; i < chosenNumbers.Length; i++)
            {
                if (chosenNumbers[i] == number)
                {
                    count ++;
                }
            }

            if (count > 2)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks already chosen numbers contains the number.
        /// </summary>
        /// <param name="number">The number that has to be checked if it is already in the chosen numbers.</param>
        /// <param name="chosenNumbers">The already chosen numbers.</param>
        /// <exception cref="ArgumentNullException">The chosen numbers array is null.</exception>
        /// <returns>Whether the number is already in the array of chosen numbers (false) or not in the array (true).</returns>
        public bool IsUnique(int number, int[] chosenNumbers)
        {
            if (chosenNumbers == null)
            {
                throw new ArgumentNullException(nameof(chosenNumbers));
            }

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
