//-----------------------------------------------------------------------
// <copyright file="GenerateUniqueRandomNumbers.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Creates a bunch of random numbers based on the range limits and the amount of numbers which should be generated.
// Checks after every generated number if the number has already been generated and ignores it if so.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This class is for generating an array of unique numbers.
    /// </summary>
    public class GenerateUniqueRandomNumbers
    {
        /// <summary>
        /// Initializes the random variable.
        /// </summary>
        private Random random;

        /// <summary>
        /// Initializes a new instance of the GenerateUniqueRandomNumbers class.
        /// </summary>
        /// <param name="random">The used random variable for the methods.</param>
        public GenerateUniqueRandomNumbers(Random random)
        {
            this.random = random;
        }

        /// <summary>
        /// Generates the random numbers based on the amount of numbers and their range limits.
        /// </summary>
        /// <param name="amount">The amount of numbers which should be generated.</param>
        /// <param name="limit1">The first range of the numbers.</param>
        /// <param name="limit2">The second range of the numbers.</param>
        /// <returns>An array of the generated numbers.</returns>
        public int[] Generate(int amount, int limit1, int limit2)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;

            if (max - min + 1 < amount)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The amount does not fit into the range of numbers from limit1 and limit2!");
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
