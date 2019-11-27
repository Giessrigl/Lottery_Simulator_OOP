using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    public class CurrentSystem : NumberSystem
    {
        public CurrentSystem(int numberDraws, int numberAmount, int limit1, int limit2, int bonusNumberAmount, int bonusNumberlimit1, int bonusNumberlimit2, bool bonusPool) 
            : base(numberDraws, numberAmount, limit1, limit2, bonusNumberAmount, bonusNumberlimit1, bonusNumberlimit2, bonusPool)
        {
        }
    }
}
