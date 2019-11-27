using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery_Simulator_3
{
    class NumberSystemsMenu : OptionsMenu, IExecuteable
    {
        public NumberSystemsMenu(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto)// Constructor
        {
        }

        public override void Execute()
        {
            this.Lotto.modes = this.CreateOptions();
            this.Lotto.CurrentMenu = this;

            this.Renderer.SetConsoleSettings(90, 35);
            this.Renderer.DisplayHeader(this.Title, 3, 1);
            this.Renderer.DisplayMenu(this.Lotto.modes, 3, 4);

        }

        private List<Mode> CreateOptions()
        {
            List<Mode> options = new List<Mode>();
            char[] uniqueChars = new char[options.Count];

            options.Add(new CurrentNumberSystems("List current available number systems", 'P', uniqueChars, this.Lotto));
            options.Add(new NumberSystemAdder("Add number system", 'A', uniqueChars, this.Lotto));
            options.Add(new NumberSystemChanger("Change number system", 'C', uniqueChars, this.Lotto));
            options.Add(new NumberSystemDeletion("Delete number system", 'L', uniqueChars, this.Lotto));
            options.Add(new OptionsMenu("Options menu", 'Z', uniqueChars, this.Lotto));

            return options;
        }
    }
}
