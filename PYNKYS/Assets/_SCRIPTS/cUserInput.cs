using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;

public class cUserInput : MonoBehaviour
{

    decimal _totalPrice;
    public GameObject _scrollingReceipt;
    public TMP_InputField _userInput;

    public void CheckUserInput(string placeHolder)
    {
        decimal outValue;
        if (decimal.TryParse(_userInput.text, out outValue))
        {
            if (outValue != _totalPrice)
            {
                cLevel.LevelDown();
                _scrollingReceipt.SetActive(true);
            }
        }
        else
        {
            // success
            cLevel.LevelUp();
        }
    }

    public decimal TotalPrice
    {
        set { _totalPrice = value; }
    }
}
