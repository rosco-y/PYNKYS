using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class cScrollingReceipt : MonoBehaviour
{

    public Transform _content;
    List<decimal> _priceList;
    decimal _totalPrice;
    public cReceiptPrice _pricePrefab;
    public GameObject _cmdContinueButton;


    private void Awake()
    {
        _cmdContinueButton.SetActive(true);
        DisplayReceipt();
    }

    public List<decimal> PriceList
    {
        set
        {
            _priceList = value;
            _totalPrice = 0;
            foreach (decimal price in _priceList)
            {
                _totalPrice += price;
            }
        }
        get
        {
            return _priceList;
        }
    }

    void AddLineItem(string item)
    {
        cReceiptPrice lineItem = Instantiate(_pricePrefab);
        lineItem.LineItem = item;
        lineItem.transform.parent = _content;
        return;
    }

    void DisplayReceipt()
    {
        decimal totalPrice = 0;
        foreach (decimal price in PriceList)
        {
            if (price > 0)
            {
                totalPrice += price;
                AddLineItem($"{price:C}");
            }
        }
        AddLineItem("=========");
        AddLineItem($"{totalPrice}");

    }



}
