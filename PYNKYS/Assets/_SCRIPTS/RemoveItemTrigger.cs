using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RemoveItemTrigger : MonoBehaviour
{

    
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
        _priceList.Add(_totalPrice);
        _itemPlacer.EnQueueItem(retireItem);
        //_itemPlacer.PlaceItem();
    }

}
