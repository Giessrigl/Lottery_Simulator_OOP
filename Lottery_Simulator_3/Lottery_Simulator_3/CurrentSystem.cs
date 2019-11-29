//-----------------------------------------------------------------------
// <copyright file="CurrentSystem.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the CurrentSystem class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    /// <summary>
    /// This is a class for getting and setting the current chosen number system.
    /// </summary>
    public class CurrentSystem : NumberSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentSystem"/> class.
        /// </summary>
        /// <param name="numberAmount">The amount of numbers the user has to choose.</param>
        /// <param name="numberDraws">The amount of numbers the system has to draw.</param>
        /// <param name="limit1">The first range of the numbers the user has to choose and the system has to draw.</param>
        /// <param name="limit2">The second range of the numbers the user has to choose and the system has to draw.</param>
        /// <param name="bonusNumberAmount">The number of bonus numbers the system has to draw.</param>
        /// <param name="bonusNumberlimit1">The first limit of the bonus numbers the system has to draw.</param>
        /// <param name="bonusNumberlimit2">The second limit of the bonus numbers the system has to draw.</param>
        /// <param name="bonusPool">Whether the bonus numbers are drawn from an own pool (true) or from the same pool as the other numbers (false).</param>
        public CurrentSystem(int numberAmount, int numberDraws, int limit1, int limit2, int bonusNumberAmount, int bonusNumberlimit1, int bonusNumberlimit2, bool bonusPool) 
            : base(numberAmount, numberDraws, limit1, limit2, bonusNumberAmount, bonusNumberlimit1, bonusNumberlimit2, bonusPool)
        {
        }
    }
}
