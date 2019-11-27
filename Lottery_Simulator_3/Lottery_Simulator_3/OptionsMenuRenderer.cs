using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    public class OptionsMenuRenderer : DefaultRenderer
    {
        public void DisplayNumberSystems(List<NumberSystem> numberSystems, int offsetLeft, int offsetTop)
        {
            if (numberSystems == null)
            {
                throw new ArgumentNullException(nameof(numberSystems));
            }

            for (int i = 0; i < numberSystems.Count; i++)
            {
                Console.SetCursorPosition(offsetLeft, offsetTop + i);
                this.WriteInColor($"{numberSystems.ElementAt(i).NumberAmount} number(s) from {numberSystems.ElementAt(i).Min} to " +
                    $"{numberSystems.ElementAt(i).Max} and {numberSystems.ElementAt(i).BonusNumberAmount} bonus number(s) from " +
                    $"{numberSystems.ElementAt(i).BonusNumberMin} to {numberSystems.ElementAt(i).BonusNumberMax}.  ", ConsoleColor.DarkYellow);

                if (numberSystems.ElementAt(i).BonusPool)
                {
                    this.WriteInColor("Bonus numbers from own pool." , ConsoleColor.DarkYellow);
                }
                else
                {
                    this.WriteInColor("Bonus numbers from the same pool.", ConsoleColor.DarkYellow);
                }
            }
        }

        public void DisplayBonusPoolChoice(int offsetLeft, int offsetTop)
        {
            Console.SetCursorPosition(offsetLeft, offsetTop);
            this.WriteInColor("Get bonus numbers from the same pool as the other numbers.");

            Console.SetCursorPosition(offsetLeft, offsetTop + 1);
            this.WriteInColor("Get bonus numbers from their own pool.");
        }
    }
}
