using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    interface IEvaluationDependent
    {
        void PerformPreEvaluation();

        void PerformEvaluation();
    }
}
