using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    public class NumberSystem
    {
        public NumberSystem(int numberDraws, int numberAmount, int limit1, int limit2, int bonusNumberAmount, int bonusNumberlimit1, int bonusNumberlimit2, bool bonusPool)
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

            //if (value <= 0)
            //{
            //        throw new ArgumentOutOfRangeException(nameof(this.Amount), "The amount has to be bigger than zero.");
            //}

        }

        public int NumberDraws
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the lower rim of the numbers in which the user can choose.
        /// </summary>
        /// <value>
        /// The lower rim of the numbers in which the user can choose.
        /// </value>
        public int Min
        {
            get;
            set;

            //if (value < 0)
            //{
            //    throw new ArgumentOutOfRangeException(nameof(this.Min), "The limits have to be bigger than zero.");
            //}

        }

        /// <summary>
        /// Gets or sets the upper rim of the numbers in which the user can choose.
        /// </summary>
        /// <value>
        /// The upper rim of the numbers in which the user can choose.
        /// </value>
        public int Max
        {
            get;
            set;
        }

        public int BonusNumberAmount { get; set; }

        public int BonusNumberMin { get; set; }

        public int BonusNumberMax { get; set; }

        public bool BonusPool { get; set; }
    }
}
