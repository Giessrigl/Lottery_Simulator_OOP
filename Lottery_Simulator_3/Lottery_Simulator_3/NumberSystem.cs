//-----------------------------------------------------------------------
// <copyright file="NumberSystem.cs" company="FH Wiener Neustadt">
//     Copyright (c) FH Wiener Neustadt. All rights reserved.
// </copyright>
// <author>Christian Giessrigl</author>
// <summary>
// This is a file for the NumberSystem class.
// </summary>
//-----------------------------------------------------------------------
namespace Lottery_Simulator_3
{
    /// <summary>
    /// This is a class for a number system.
    /// </summary>
    public class NumberSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberSystem"/> class.
        /// </summary>
        /// <param name="numberAmount">The amount of numbers the user has to choose.</param>
        /// <param name="numberDraws">The amount of numbers the system has to draw.</param>
        /// <param name="limit1">The first range of the numbers the user has to choose and the system has to draw.</param>
        /// <param name="limit2">The second range of the numbers the user has to choose and the system has to draw.</param>
        /// <param name="bonusNumberAmount">The number of bonus numbers the system has to draw.</param>
        /// <param name="bonusNumberlimit1">The first limit of the bonus numbers the system has to draw.</param>
        /// <param name="bonusNumberlimit2">The second limit of the bonus numbers the system has to draw.</param>
        /// <param name="bonusPool">Whether the bonus numbers are drawn from an own pool (true) or from the same pool as the other numbers (false).</param>
        public NumberSystem(int numberAmount, int numberDraws, int limit1, int limit2, int bonusNumberAmount, int bonusNumberlimit1, int bonusNumberlimit2, bool bonusPool)
        {
            this.NumberAmount = numberAmount;
            this.NumberDraws = numberDraws;
            this.Min = (limit1 < limit2) ? limit1 : limit2;
            this.Max = (limit1 > limit2) ? limit1 : limit2;
            this.BonusNumberAmount = bonusNumberAmount;
            this.BonusNumberMin = (bonusNumberlimit1 < bonusNumberlimit2) ? bonusNumberlimit1 : bonusNumberlimit2;
            this.BonusNumberMax = (bonusNumberlimit1 > bonusNumberlimit2) ? bonusNumberlimit1 : bonusNumberlimit2;
            this.BonusPool = bonusPool;
        }

        /// <summary>
        /// Gets or sets the amount of numbers the user can choose.
        /// </summary>
        /// <value>
        /// The amount of numbers the user can choose.
        /// </value>
        public int NumberAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the amount of random numbers the system draws.
        /// </summary>
        /// <value>
        /// The amount of random numbers the system draws.
        /// </value>
        public int NumberDraws
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lower rim of the numbers in which the user can choose and the system draws.
        /// </summary>
        /// <value>
        /// The lower rim of the numbers in which the user can choose and the system can draw.
        /// </value>
        public int Min
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the upper rim of the numbers in which the user can choose and the system draws.
        /// </summary>
        /// <value>
        /// The upper rim of the numbers in which the user can choose and the system can draw.
        /// </value>
        public int Max
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the amount of bonus numbers the user can hit and the system draws.
        /// </summary>
        /// <value>
        /// The amount of numbers the user can hit and the system draws.
        /// </value>
        public int BonusNumberAmount { get; set; }

        /// <summary>
        /// Gets or sets the lower rim of the bonus numbers in which the user can choose and the system draws.
        /// </summary>
        /// <value>
        /// The lower rim of the bonus numbers in which the user can choose and the system can draw.
        /// </value>
        public int BonusNumberMin { get; set; }

        /// <summary>
        /// Gets or sets the upper rim of the bonus numbers in which the user can choose and the system draws.
        /// </summary>
        /// <value>
        /// The upper rim of the bonus numbers in which the user can choose and the system can draw.
        /// </value>
        public int BonusNumberMax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the bonus numbers are in an own pool (true) or in the same pool as the random numbers (false).
        /// </summary>
        /// <value>
        /// Whether the bonus numbers are in an own pool (true) or in the same pool as the random numbers (false).
        /// </value>
        public bool BonusPool { get; set; }
    }
}
