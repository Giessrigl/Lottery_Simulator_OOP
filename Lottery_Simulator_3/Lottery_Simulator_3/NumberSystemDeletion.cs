using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    class NumberSystemDeletion : NumberSystemsMenu, IExecuteable
    {
        public NumberSystemDeletion(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
        }

        public override void Execute()
        {
            if (this.Lotto.NumberSystems.Count > 1)
            {
                this.Render.SetConsoleSettings(90, 35);
                this.Render.DisplayHeader(this.Title, 3, 1);
                this.Renderer.DisplayNumberSystems(this.Lotto.NumberSystems, 5, 4);

                int index = 0;
                do
                {
                    this.Renderer.DisplayCursor(3, 4 + index);

                    ConsoleKeyInfo userkey = Console.ReadKey(true);

                    if (userkey.Key == ConsoleKey.UpArrow && index > 0)
                    {
                        index--;
                    }
                    else if (userkey.Key == ConsoleKey.DownArrow && index < this.Lotto.NumberSystems.Count - 1)
                    {
                        index++;
                    }
                    else if (userkey.Key == ConsoleKey.Enter && index > 0)
                    {
                        this.Lotto.NumberSystems.RemoveAt(index);
                        break;
                    }
                }
                while (true);
            }
        }
    }
}
