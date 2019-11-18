//-----------------------------------------------------------------------
// <copyright file="CheckKeyInput.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// Takes a Key input and compares it with various keys.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_2
{
    using System;

    /// <summary>
    /// This is a class to compare the key the user pressed with various keys.
    /// </summary>
    public class CheckKeyInput
    {
        /// <summary>
        /// Checks if the pressed key is either the J key or the N key. The ReadKey() is implemented.
        /// </summary>
        /// <returns>True if the J - key has been pressed and False if the N - key has been pressed.</returns>
        public bool WaitForYesNo()
        {
            do
            {
                ConsoleKeyInfo userKey = Console.ReadKey(true);

                if (userKey.Key == ConsoleKey.J)
                {
                    return true;
                }
                else if (userKey.Key == ConsoleKey.N)
                {
                    return false;
                }
            }
            while (true);
        }

        /// <summary>
        /// Continues if the pressed key is the enter key. The ReadKey() is implemented.
        /// </summary>
        public void WaitForEnter()
        {
            do
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    break;
                }
            }
            while (true);
        }
    }
}
