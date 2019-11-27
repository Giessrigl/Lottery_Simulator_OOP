using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    class CurrentNumberSystems : NumberSystemsMenu, IExecuteable
    {
        public CurrentNumberSystems(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
        }

        public override void Execute()
        {
            this.Renderer.SetConsoleSettings(90, 35);
            this.Renderer.DisplayHeader(this.Title, 3, 1);

            this.Renderer.DisplayNumberSystems(this.Lotto.NumberSystems, 5, 4);

            this.Lotto.KeyChecker.WaitForEnter();
        }
    }
}
