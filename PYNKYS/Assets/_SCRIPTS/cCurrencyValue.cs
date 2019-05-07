using System;
using System.Collections.Generic;
using System.Text;

namespace PYNKYS.SCRIPTS.PRICES
{
    public class cCurrencyValue
    {
        #region CONSTANTS
        const decimal DOLLAR = 1M;
        const decimal HALF_DOLLAR = .5M;
        const decimal QUARTER = .25M;
        const decimal DIME = .1M;
        const decimal NICKEL = .05M;
        const decimal PENNY = .01M;
        #endregion


        #region Member Variables
        cRange _dollars;
        cRange _halfDollars;
        cRange _quarters;
        cRange _dimes;
        cRange _nickels;
        cRange _pennies;

        decimal _amount;
        #endregion



        public cCurrencyValue()
        {
            // start at an elementary level
            _amount = 0;
            _dollars = new cRange(0, 5);
            _halfDollars = new cRange(0, 0);
            _quarters = new cRange(0, 0);
            _dimes = new cRange(0, 0);
            _nickels = new cRange(0, 0);
            _pennies = new cRange(0, 0);
        }


        #region MONEY RANGE SETTERS AND GETTERS
        /// <summary>
        /// public setters to currencies, so ranges can be adjested.
        /// public getters so current values can be used in making adjustents.
        /// </summary>
        public cRange Dollars
        {
            set
            {
                _dollars = value;
            }
            get
            {
                return _dollars;
            }

        }

        public cRange HalfDollars
        {
            set
            {
                _halfDollars = value;
            }
            get
            {
                return _halfDollars;
            }
                
        }

        public cRange Quarters
        {
            get
            {
                return _quarters;
            }
            set
            {
                _quarters = value;
            }
        }

        public cRange Dimes
        {
            get
            {
                return _dimes;
            }
            set
            {
                _dimes = value;
            }
        }

        public cRange Nickels
        {
            get
            {
                return _nickels;
            }
            set
            {
                _nickels = value;
            }
        }

        public cRange Pennies
        {
            get
            {
                return _pennies;
            }
            set
            {
                _pennies = value;
            }
        }

        #endregion

        public decimal Next()
        {
            _amount = 0m; // value member variable

            // add the (decimal)random (int) * the decimal factor
            _amount += _dollars.Next() * DOLLAR;
            _amount += _halfDollars.Next() * HALF_DOLLAR;
            _amount += _quarters.Next() * QUARTER;
            _amount += _dimes.Next() * DIME;
            _amount += _nickels.Next() * NICKEL;
            _amount += _pennies.Next() * PENNY;

            return _amount;           
        }

        public decimal Amount
        {
            get { return _amount; }
        }

    }
}
