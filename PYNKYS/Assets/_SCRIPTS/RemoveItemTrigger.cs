using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;
using UnityEngine.UI;

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
        //_itemPlacer.PutItemBackInPool(retireItem);
        //_itemPlacer.PlaceItem();
        objectPooler.Instance.SpawnFromPool("ITEM");
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

    public void CheckUserAnswer(string fieldValue)
    {
        decimal outValue;

        if (decimal.TryParse(fieldValue, out outValue))
        {
            if (outValue != Total)
            {
                

                cLevel.PlayingLevel = false;
                cScrollingReceipt receipt = _scrollingReceipt.GetComponent<cScrollingReceipt>();
                receipt.PriceList = _priceList; // pass this around as it will be needed!
                receipt.gameObject.SetActive(true);
                SetColor(Color.red);
                _cmdContinueButton.GetComponent<cCmdContinue>().SetItemPlacer(_itemPlacer);
                _cmdContinueButton.SetActive(true);

            }
            else
            {
                _userInput.text = "";
                _userInput.gameObject.SetActive(false);
                cLevel.PlayingLevel = true;                
            }
        }
            
    }

    /// <summary>
    /// Change the background color of the Input Field
    /// </summary>
    /// <param name="color"></param>
    void SetColor(Color color)
    {
        ColorBlock cb = _userInput.colors;
        cb.normalColor = color;
        _userInput.colors = cb;
    }

}
