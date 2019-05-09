using System;
using System.Collections.Generic;
using System.Text;

namespace PYNKYS.SCRIPTS.PRICES
{
    public class cLevelSettings
    {
        public int Dollars { get; set; }
        public int HalfDollars { get; set; }
        public int Quarters { get; set; }
        public int Dimes { get; set; }
        public int Nickels { get; set; }
        public int Pennies { get; set; }

        public cLevelSettings()
        {
            levelOne(1);
        }

        #region LEVEL SETUP METHODS

        void levelOne(int? iDollars = null)
        {
            if (iDollars != null)
                Dollars = (int)iDollars;
            HalfDollars = 0;
            Quarters = 0;
            Dimes = 0;
            Nickels = 0;
            Pennies = 0;
        }
        void levelTwo(int? iDollars = null)
        {
            levelOne();
            if (iDollars != null)
                Dollars = (int)iDollars;
            HalfDollars = 1;
        }
        void levelThree(int? iDollars = null)
        {
            levelTwo();
            if (iDollars != null)
                Dollars = (int)iDollars;
            Quarters = 1;
        }
        void levelFour(int? iDollars = null)
        {
            levelThree();
            if (iDollars != null)
                Dollars = (int)iDollars;
            Dimes = 4;
        }
        void levelFive(int? iDollars = null)
        {
            levelFour();
            if (iDollars != null)
                Dollars = (int)iDollars;
            Nickels = 1;
        }
        void levelSix(int? iDollars = null)
        {
            levelFive();
            if (iDollars != null)
                Dollars = (int)iDollars;
            Pennies = 4;

        }
        void levelSeven(int? iDollars = null)
        {
            levelSix();
            if (iDollars != null)
                Dollars = (int)iDollars;
        }
        void levelEight(int? iDollars = null)
        {
            levelSeven();
            if (iDollars != null)
                Dollars = (int)iDollars;
        }
        void levelNine(int? iDollars = null)
        {
            levelEight();
            if (iDollars != null)
                Dollars = (int)iDollars;
        }



        #endregion

        public void SetLevel(float level)
        {

            /// level = trucateLevel(level); trucate float to
            /// 1 digit precision.
            level = trucateLevel(level);

            string sLevel = level.ToString();
            int iLevel = int.Parse(sLevel.Substring(0, 1));
            int iDollars = 0;
            if (sLevel.Contains("."))
            {
                int iDot = sLevel.IndexOf('.') + 1;
                iDollars = int.Parse(sLevel.Substring(iDot, 1)) + 1;
            }
            if (iDollars < 1)
                iDollars = 1; /// 1.0, 2.0, 3.0,...,7.0, 8.0, 9.0 all start at 1 dollar
                              /// (with the exception that 7, 8 and 9 all have additional 
                              /// 5, 10, or 15 dollars added to them.)



            switch (iLevel)
            {
                case 1:
                    levelOne(iDollars); // Whole Dollar Amounts: $1.00, $2.00, $3.00, ... $9.00
                    break;
                /// LevelTwo is LevelOne + 1/2 Dollar 
                /// (random, so $0.00, $0.50, $1.00, $1.50,...$9.50)
                case 2:
                    levelTwo(iDollars);
                    break;
                case 3:
                    /// LevelThree is LevelTwo + $0.25 cents 
                    /// (With random use of HalfDollar, this gives:
                    /// $0.25, $0.50, $0.75, $1.00, $1.25,...$9.75.
                    levelThree(iDollars);
                    break;
                /// LevelFour is LevelThree + 0, 1,2, 3, or 4 Dimes.  This gives:
                /// $0.00, $0.10, $0.20, $0.25, $0.30, $0.35, $0.40, $0.45, $0.50,...$9.75, $9.85, $9.95, $10.05
                /// (note there are gaps as there are no nickels yet.)
                case 4:
                    levelFour(iDollars);
                    break;
                /// LevelFive is LevelFour + 0 or 1 Nickel, which gives:
                /// $0.00, $0.05, $0.10, $0.15, $0.20,...$9.95, $10.00, $10.05, $10.10
                /// (Now the amounts graduate evenly by .05 cents from $0.00 to $10.10)
                case 5:
                    levelFive(iDollars);
                    break;
                /// LevelSix is LevelFive with 0 to 4 Pennies. Which gives:
                /// $0.00, $0.01, $0.02,...$10.05, $10.06, $10.07, $10.08 and $10.09
                /// (We have potential to create any US Currency amount for $0.00 to $10.09.)
                case 6:
                    levelSix(iDollars);
                    break;
                /// There is nothing more we can do with change (half-dollars, quarters, dimes, nickels or pennies)
                /// so now we just make it possible to have larger dollar amounts.
                case 7:
                    levelSeven(iDollars + 5);
                    break;
                case 8:
                    levelEight(iDollars + 10);
                    break;
                case 9:
                    levelNine(iDollars + 15);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Truncate trailing precision on a float
        /// down to 1 digit of precision.
        /// (9.93030400 becomes 9.9) (No rounding.)
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public float trucateLevel(float level)
        {
            // truncate trailing precision.
            level = float.Parse(level.ToString("0.#"));
            return level;
        }
    }

}
