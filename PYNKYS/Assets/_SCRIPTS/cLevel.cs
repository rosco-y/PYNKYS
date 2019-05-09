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
                _level += INCREMENT;
                _settings.SetLevel(_level);
            }
        }

        public static void LevelDown()
        {
            if (_level > 1)
            {
                _level -= INCREMENT;
                _settings.SetLevel(_level);
            }
        }

        static public cLevelSettings Settings
        {
            get { return _settings; }
        }

    }




}
