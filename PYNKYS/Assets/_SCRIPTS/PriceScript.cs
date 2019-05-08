﻿using PYNKYS.SCRIPTS.PRICES;
using TMPro;
using UnityEngine;

public class PriceScript : MonoBehaviour
{
    public TMP_Text _priceTag;
    // Start is called before the first frame update
    decimal _price;
    cLevel _level;
    static decimal _amount = 0;

    private void Awake()
    {
        _amount += 1m;
        Price = _amount;
    }

    //private void OnEnable()
    //{


    //}

    public decimal Price
    {
        set
        {
            _price = value;
            _priceTag.text = $"{_price:c}";
        }
        get
        {
            return _price;
        }
    }



}
