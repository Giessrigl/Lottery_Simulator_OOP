//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a program for playing lotto in different representations.
// This is the second homework of the first semester at the FH Wiener Neustadt.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    /// <summary>
    /// Leads the user to the lottery simulator.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the lottery simulator with the amount of numbers the user can choose and the two limits of the number range.
        /// </summary>
        public static void Main()
        {
            const int NumberAmount = 6;
            const int Limit1 = 1;
            const int Limit2 = 45;

            Lottery lotto = new Lottery(NumberAmount, Limit1, Limit2);
            
            lotto.Play();
        }
    }
}
