using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PYNKYS.SCRIPTS.PRICES
{

  
    public static class cLevel 
    {

        public static float _startingLevel = 1.0f;

        #region PRIVATE MEMBERS
        static float _level = 1f;
        const float INCREMENT = 0.1f;
        static cLevelSettings _settings = new cLevelSettings();
        #endregion


        public static void LevelUp()
        {
            if (_level < 10)
            {
                _level = trucateLevel(_level + INCREMENT);
                _settings.SetLevel(_level); // truncated and agreed upon
            }
        }

        public static void LevelDown()
        {
            if (_level > 1)
            {
                _level = trucateLevel(_level - INCREMENT);
                _settings.SetLevel(_level); // truncated and agreed upon
            }
        }

        static public cLevelSettings Settings
        {
            get { return _settings; }
        }

        public static float Level
        {
            get
            {
                return _level;
            }
        }
        /// <summary>
        /// Truncate the float representation of the level to 
        /// 1 digit precision without rounding.
        /// </summary>
        /// <param name="level">the level that needs to be guaranteed no more than
        /// n digits of precision.</param>
        /// <param name="precision">
        /// How many trailing digits of precion are desired.
        /// for cLevel, the precision is always 1, but it's just as easy to 
        /// copy it generically, as I got this from:
        /// https://stackoverflow.com/questions/3143657/truncate-two-decimal-places-without-rounding
        /// </param>
        /// <returns></returns>
        static float trucateLevel(float level, int precision = 1)
        {
            string sNum = level.ToString();
            string sLevel = sNum.Substring(0, 1);
            string fullNum = string.Empty;
            if (sNum.Contains("."))
            {
                string sDecimal = sNum.Substring(sNum.IndexOf("."));
                if (sDecimal.Length >= 2)
                    sDecimal = sDecimal.Substring(1, 1); // 1 digit following the decimal

                fullNum = sLevel + "." + sDecimal;
            }
            else // is a level with no precision
                fullNum = sNum;
            float newLevel = float.Parse(fullNum);
            return newLevel;
        }
    }




}
