using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PYNKYS.SCRIPTS.PRICES
{

  
    public class cLevel : MonoBehaviour
    {

        public float _startingLevel = 1.0f;

        #region PRIVATE MEMBERS
        float _level;
        const float INCREMENT = 0.1f;
        cLevelSettings _settings;
        #endregion


        #region CONSTRUCTION
        public cLevel()
        {
            _level = 1f;
            _settings = new cLevelSettings();
        }
        ~cLevel()
        {
            _settings = null;
        }
        #endregion

        public float Level
        {
            get { return _level; }
        }

        public void LevelUp()
        {
            if (_level < 10)
            {
                _level += INCREMENT;
                _level = _settings.trucateLevel(_level);
                _settings.SetLevel(_level);
            }
        }

        public void LevelDown()
        {
            if (_level > 1)
            {
                _level -= INCREMENT;
                _level = _settings.trucateLevel(_level);
                _settings.SetLevel(_level);
            }
        }

        public cLevelSettings Settings
        {
            get { return _settings; }
        }

    }




}
