using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveItemTrigger : MonoBehaviour
{

    public GameObject _scrollingReceipt;
    public GameObject _cmdContinueButton;
    public TMP_InputField _userInput;
    public ItemPlacer _itemPlacer;
    List<decimal> _priceList;
    decimal _totalPrice;


    private void Start()
    {

        _priceList = new List<decimal>();
        _totalPrice = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        PriceScript retireItem = other.GetComponent<PriceScript>();
        _totalPrice += retireItem.Price;        
        _priceList.Add(retireItem.Price);
        if (retireItem.LastItem)
        {
            _userInput.gameObject.SetActive(true);
        }
        _itemPlacer.PutItemBackInPool(retireItem);
        _itemPlacer.PlaceItem();
    }

    

    decimal Total
    {
        get
        {
            decimal total = 0;
            foreach (decimal price in _priceList)
            {
                total += price;
            }
            return total;
        }
    }

    public void CheckUserAnswer(string placeHolder)
    {
        decimal outValue;

        if (decimal.TryParse(_userInput.text, out outValue))
        {
            if (outValue != Total)
            {
                cScrollingReceipt receipt = _scrollingReceipt.GetComponent<cScrollingReceipt>();
                receipt.PriceList = _priceList; // pass this around as it will be needed!
                receipt.gameObject.SetActive(true);
                _cmdContinueButton.SetActive(true);
            }
            else
            {
                _userInput.text = "";
                _userInput.gameObject.SetActive(false);
                // user succeeded: play next level.
            }
        }
            
    }

}
