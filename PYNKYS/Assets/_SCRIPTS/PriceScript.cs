using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;

public class PriceScript : MonoBehaviour
{
    public TMP_Text _priceTag;
    // Start is called before the first frame update
    decimal _price;
    cLevel _level;
    static decimal _amount = 0;

    private void OnEnable()
    {
        _amount += 1.11m;
        Price = _amount;
    }

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

    void setPrice()
    {
       
    }


}
