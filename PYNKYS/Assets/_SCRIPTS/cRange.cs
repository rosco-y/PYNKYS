using System;
using System.Collections.Generic;
using System.Text;


namespace PYNKYS.SCRIPTS.PRICES
{
    public class cRange
    {
        Random _rand;
        int _min;
        int _max;
        int _cur; // current random value

        public cRange(int min, int max)
        {
            _rand = new Random();
            _min = min;
            _max = max;
        }

        /// <summary>
        /// Return a random value betwen _min and _max, inclusive.
        /// </summary>
        /// <returns>
        /// Returns a random value between and including the values
        /// _min and max.
        /// </returns>
        public decimal Next()
        {
            if (_max == 0)
                _cur = 0;
            else
            {
                _cur = _rand.Next(_min, _max + 1);
            }
            return (decimal)_cur * 1;
        }

        /// <summary>
        /// Set a new Range for this instance of cRange.
        /// </summary>
        /// <param name="min">new minimum value</param>
        /// <param name="max">new maximum value</param>
        public void SetRange(int min, int max)
        {
            _min = min;
            _max = max;
            _cur = int.MinValue; // preposterous value.
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public int Cur
        {
            get { return _cur; }
        }

    }
}
