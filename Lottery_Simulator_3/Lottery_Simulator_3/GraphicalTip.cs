using System;
using System.Collections.Generic;

namespace Lottery_Simulator_3
{
    public class GraphicalTip : Mode, IEvaluationDependent, IExecuteable
    {
        /// <summary>
        /// Initializes a new instance of the ManualTip class.
        /// </summary>
        /// <param name="title">The name/title of the Mode.</param>
        /// <param name="abbreviation">The specified key, the user has to press to get lead to this mode.</param>
        /// <param name="uniqueChars">The already used keys to prevent to not get to this mode.</param>
        /// <param name="lotto">The Lottery variable with all necessary classes this mode needs.</param>m>
        public GraphicalTip(string title, char abbreviation, char[] uniqueChars, Lottery lotto) :
        base(title, abbreviation, uniqueChars, lotto)
        {
        }

        public override void Execute()
        {
            this.Lotto.Render.SetConsoleSettings(90, 40);
            this.Lotto.Render.DisplayHeader(this.Title, 3, 1);
            Console.ReadKey();

            // PerformPreEvaluation();
            // PerformPastEvaluation(); 
        }

        public void PerformPreEvaluation()
        {
            throw new NotImplementedException();
        }

        public void PerformEvaluation()
        {
            throw new NotImplementedException();
        }

       
    }
}
