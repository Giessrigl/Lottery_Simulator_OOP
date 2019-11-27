using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    public class ErrorCheck
    {
        public ErrorCheck(Lottery lotto)
        {
            this.Lotto = lotto;
        }

        private Lottery Lotto
        {
            get;
            set;
        }

        public int EvaluateNumberErrors(string question, int limit1, int limit2, int offsetLeft, int offsetTop)
        {
            int min = (limit1 < limit2) ? limit1 : limit2;
            int max = (limit1 > limit2) ? limit1 : limit2;
            int number;

            do
            {
                string input = this.Lotto.Render.GetAnswer(question, offsetLeft, offsetTop);
                this.Lotto.Render.OverwriteBlank(150, 0, offsetTop + 1);

                if (input.Length > int.MaxValue.ToString().Length)
                {
                    this.Lotto.Render.OverwriteBlank(input.Length + 1, offsetLeft + question.Length, offsetTop);
                    this.Lotto.Render.DisplayGeneralError("Your input is too long!", offsetLeft, offsetTop + 1);
                    continue;
                }

                if (!int.TryParse(input, out number))
                {
                    this.Lotto.Render.OverwriteBlank(input.Length + 1, offsetLeft + question.Length, offsetTop);
                    this.Lotto.Render.DisplayGeneralError("Type in a positive, whole number!", offsetLeft, offsetTop + 1);
                    continue;
                }

                if (!this.Lotto.NumberChecker.IsInRange(number, min, max))
                {
                    this.Lotto.Render.OverwriteBlank(input.Length + 1, offsetLeft + question.Length, offsetTop);
                    this.Lotto.Render.DisplayUserRangeError(min, max, offsetLeft, offsetTop + 1);
                    continue;
                }
                else
                {
                    break;
                }
            }
            while (true);

            return number;
        }
    }
}
