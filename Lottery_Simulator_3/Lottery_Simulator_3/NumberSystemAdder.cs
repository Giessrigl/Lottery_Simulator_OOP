using System;
using System.Collections.Generic;

namespace Lottery_Simulator_3
{
    class NumberSystemAdder : NumberSystemsMenu, IExecuteable
    {
        public NumberSystemAdder(string title, char abbreviation, char[] uniqueChars, Lottery lotto) : base(title, abbreviation, uniqueChars, lotto) // Constructor
        {
            
        }

        public override void Execute()
        {
            int limitmin = 1;

            int offsetLeft = 3;
            int offsetTop = 4;

            int numberDraws;
            int numberAmount;
            int min;
            int max;

            int bonusNumbers;
            int bonusMin = 0;
            int bonusMax = 0;
            bool bonusPool = true;

            do
            {
                this.Render.SetConsoleSettings(90, 40);
                this.Render.DisplayHeader(this.Title, 3, 1);

                int linecount = 0;
                do
                {
                    numberDraws = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of numbers the user has to choose: ", limitmin, int.MaxValue, offsetLeft, offsetTop + linecount);
                    linecount++;
                    numberAmount = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of numbers the system draws: ", limitmin, int.MaxValue, offsetLeft, offsetTop + linecount);
                    linecount++;
                    int limit1 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the first limit of the range of numbers: ", limitmin, int.MaxValue, offsetLeft, offsetTop + linecount);
                    linecount++;
                    int limit2 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the second limit of the range of numbers: ", limitmin, int.MaxValue, offsetLeft, offsetTop + linecount);
                    linecount++;

                    min = (limit1 < limit2) ? limit1 : limit2;
                    max = (limit1 > limit2) ? limit1 : limit2;
                    if (max - min + 1 < numberAmount)
                    {
                        linecount--;
                        this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of numbers. Please press enter to continue!", offsetLeft, offsetTop + 1 + linecount);
                        linecount -= 2;
                        this.Lotto.KeyChecker.WaitForEnter();

                        this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount);
                        this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount + 1);
                        this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount + 2);
                        this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount + 3);
                        continue;
                    }
                    else
                    {
                        do
                        {
                            bonusNumbers = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the amount of bonus numbers you want to choose: ", 0, int.MaxValue, offsetLeft, offsetTop + linecount);
                            linecount++;
                            if (bonusNumbers > 0)
                            {
                                int bonusNumberLimit1 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the first limit of the range of bonus numbers: ", limitmin, int.MaxValue, offsetLeft, offsetTop + linecount);
                                linecount++;
                                int bonusNumberLimit2 = this.Lotto.ErrorChecker.EvaluateNumberErrors("Type in the second limit of the range of bonus numbers: ", limitmin, int.MaxValue, offsetLeft, offsetTop + linecount);
                                linecount++;

                                bonusMin = (bonusNumberLimit1 < bonusNumberLimit2) ? bonusNumberLimit1 : bonusNumberLimit2;
                                bonusMax = (bonusNumberLimit1 > bonusNumberLimit2) ? bonusNumberLimit1 : bonusNumberLimit2;

                                if (bonusMax - bonusMin + 1 < bonusNumbers)
                                {
                                    linecount--;
                                    this.Renderer.DisplayGeneralError("The area of the numbers is to little for the amount of numbers. Please press enter to continue!", offsetLeft, offsetTop + 1 + linecount);
                                    linecount -= 2;
                                    this.Lotto.KeyChecker.WaitForEnter();

                                    this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount);
                                    this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount + 1);
                                    this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount + 2);
                                    this.Renderer.OverwriteBlank(90, 0, offsetTop + linecount + 3);
                                    continue;
                                }
                                else
                                {
                                    this.Render.SetConsoleSettings(90, 40);
                                    this.Render.DisplayHeader(this.Title, 3, 1);
                                    this.Renderer.DisplayBonusPoolChoice(5, 4);

                                    int index = 0;
                                    do
                                    {
                                        this.Renderer.DisplayCursor(3, 4 + index);
                                        ConsoleKeyInfo userkey = Console.ReadKey(true);
                                        if (userkey.Key == ConsoleKey.UpArrow && index > 0)
                                        {
                                            index--;
                                        }
                                        else if (userkey.Key == ConsoleKey.DownArrow && index < 1)
                                        {
                                            index++;
                                        }
                                        else if (userkey.Key == ConsoleKey.Enter)
                                        {
                                            if (index < 1)
                                            {
                                                bonusPool = false;
                                                break;
                                            }
                                            else
                                            {
                                                bonusPool = true;
                                                break;
                                            }
                                        }
                                    }
                                    while (true);
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        while (true);
                    }
                    break;
                }
                while (true);

                if (!bonusPool)
                {
                    if (min == bonusMin && max == bonusMax)
                    {
                        if ((max - min + 1) < (numberAmount + bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((max - min + 1) < numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (min > bonusMin && max < bonusMax)
                    {
                        if ((bonusMax - bonusMin + 1) < (numberAmount + bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((bonusMax - bonusMin + 1) < numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (bonusMin > min && bonusMax < max)
                    {
                        if ((max - min + 1) < (numberAmount + bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((max - min + 1) < numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (bonusMin < max && bonusMax > max)
                    {
                        if ((max - min + 1) + (bonusMax - bonusMin + 1) - (max - bonusMin + 1) < (numberAmount + bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((max - min + 1) + (bonusMax - bonusMin + 1) - (max - bonusMin + 1) < numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else if (min < bonusMax && max > bonusMax)
                    {
                        if ((max - min + 1) + (bonusMax - bonusMin + 1) - (bonusMax - min + 1) < (numberAmount + bonusNumbers))
                        {
                            this.Renderer.DisplayGeneralError($"The total amount of numbers (bonus and normal together) in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                        else if ((max - min + 1) + (bonusMax - bonusMin + 1) - (bonusMax - min + 1) < numberDraws)
                        {
                            this.Renderer.DisplayGeneralError($"The amount of numbers the user has to choose in this constellation is too high. Please press enter to continue.", offsetLeft, offsetTop + 1);
                            this.Lotto.KeyChecker.WaitForEnter();
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                break;
            }
            while (true);

            this.Lotto.NumberSystems.Add(new NumberSystem(numberDraws, numberAmount, min, max, bonusNumbers, bonusMin, bonusMax, bonusPool));
        }
    }
}
