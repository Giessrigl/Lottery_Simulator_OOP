//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a program for playing lotto in different representations.
// This is the third homework of the first semester at the FH Wiener Neustadt.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    /// <summary>
    /// Leads the user to the lottery simulator.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starts the lottery simulation.
        /// </summary>
        public static void Main()
        {
            Lottery lotto = new Lottery();

            lotto.Play();
        }
    }
}