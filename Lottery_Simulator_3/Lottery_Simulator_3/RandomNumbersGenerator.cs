﻿//-----------------------------------------------------------------------
// <copyright file="RandomNumbersGenerator.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Creates a bunch of random numbers based on the range limits and the amount of numbers which should be generated.
// Checks after every generated number if the number has already been generated and ignores it if so.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    using System;

    /// <summary>
    /// This class is for generating arrays of numbers.
    /// </summary>
    public class RandomNumbersGenerator
    {
        /// <summary>
        /// Initializes the random variable.
        /// </summary>
        private Random random;

        /// <summary>
        /// Initializes a new instance of the RandomNumbersGenerator class.
        /// </summary>
        /// <param name="random">The used random variable for the methods.</param>
        public RandomNumbersGenerator(Random random)
        {
            this.random = random;
        }

        /// <summary>
        /// Generates unique random numbers in a range of numbers based on the amount of numbers and their range limits.
        /// </summary>
        /// <param name="amount">The amount of numbers which should be generated.</param>
        /// <param name="limit1">The first range of the numbers.</param>
        /// <param name="limit2">The second range of the numbers.</param>
        /// <returns>An array of the generated numbers.</returns>
        public int[] GenerateUniquesInRange(int amount, int limit1, int limit2)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;

            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The amount has to be at least 1.");
            }

            int[] randomNumbers = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                do
                {
                    int number = this.random.Next(min, max + 1);
                    if (this.CheckUniquness(number, randomNumbers))
                    {
                        randomNumbers[i] = number;
                        break;
                    }
                }
                while (true);
            }

            return randomNumbers;
        }

        /// <summary>
        /// Generates random numbers of either one range of numbers or the another range of numbers based on the amount of numbers and their range limits.
        /// </summary>
        /// <param name="range1amount">The amount of numbers of the first range which should be generated.</param>
        /// <param name="range2amount">The amount of numbers of the second range which should be generated.</param>
        /// <param name="range1Limit1">The first limit of the first range of the numbers.</param>
        /// <param name="range1Limit2">The second limit of the first range of the numbers.</param>
        /// <param name="range2Limit1">The first limit of the second range of the numbers.</param>
        /// <param name="range2Limit2">The second limit of the second range of the numbers.</param>
        /// <returns>An array of the generated numbers.</returns>
        public int[] GenerateUniquesInRanges(int range1amount, int range2amount, int range1Limit1, int range1Limit2, int range2Limit1, int range2Limit2)
        {
            int min1 = (range1Limit1 < range1Limit2) ? range1Limit1 : range1Limit2;
            int max1 = (range1Limit1 > range1Limit2) ? range1Limit1 : range1Limit2;

            int min2 = (range2Limit1 < range2Limit2) ? range2Limit1 : range2Limit2;
            int max2 = (range2Limit1 > range2Limit2) ? range2Limit1 : range2Limit2;

            if (range1amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(range1amount), "The amount has to be at least 1.");
            }

            if (range2amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(range2amount), "The amount has to be at least 1.");
            }

            int[] numbers = new int[range1amount + range2amount];
            int num = 0;
            int count;

            int range1count = 0;
            int range2count = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                do
                {
                    count = 0;
                    if (range1count < range1amount)
                    {
                        num = this.random.Next(min1, max1 + 1);
                        range1count++;

                        foreach (int number in numbers)
                        {
                            if (number == num)
                            {
                                count++;
                                range1count--;
                            }
                        }
                    }
                    else if (range2count < range2amount)
                    {
                        num = this.random.Next(min2, max2 + 1);
                        range2count++;
                        foreach (int number in numbers)
                        {
                            if (number == num)
                            {
                                count++;
                                range2count--;
                            }
                        }
                    }
                }
                while (count > 0);

                numbers[i] = num;
            }

            return numbers;
        }

        /// <summary>
        /// Checks if the generated number is already in the random numbers.
        /// </summary>
        /// <param name="number">The generated number.</param>
        /// <param name="randomnumbers">The array of the unique generated numbers.</param>
        /// <returns>False if the number is already in the array of generated numbers.</returns>
        private bool CheckUniquness(int number, int[] randomnumbers)
        {
            for (int i = 0; i < randomnumbers.Length; i++)
            {
                if (number == randomnumbers[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
