using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;
using UnityEngine.EventSystems;

public class ItemPlacer : MonoBehaviour
{
    const int NUMITEMSPERTABLETWIDTH = 8;
    public int _ItemsPerLevel = 10;
    cCurrencyValue _randomCurrencyValueGenerator;
    decimal _totalPrice = 0m;
    List<decimal> _prices;
    public TMP_InputField _userInputField;
    string _userAnswerSuccess = "";
    Vector3 _itemPosition;
    /// <summary>
    /// ITEMWIDTH, _itemNo; used to space items evenly across the belt.
    /// </summary>
    const float ITEMWIDTH = 0.0763F;
    int _itemNo = 0;


    /// <summary>
    /// OnEnable is fired whenever a GameObject is SetActive(true).
    /// </summary>
    private void OnEnable()
    {
        _randomCurrencyValueGenerator = new cCurrencyValue();
        _prices = new List<decimal>();
        Reset();
    }

    public void Reset()
    {
        _itemNo = 0;
        _totalPrice = 0;
        _prices.Clear();
        adjustCurrencyGenerator();
        cLevel.PlayingLevel = true;
        PlaceItem();
    }


    /// <summary>
    /// Preload items into the Item Queue, so they can be used
    /// and reused without overuse of Instantiate, and so that
    /// the GC is taxed with cleaning up piles of garbage.
    /// </summary>
    /// <param name="count">num items to add to the queue
    /// </param>
    bool preLoading = false;
    void loadItems(int count)
    {
        preLoading = count > 1;

        for (int i = 0; i < count; i++)
        {
            PriceScript newItem = SpawnFromPool();
            newItem.ItemNo = i + 1;
            if (preLoading)
            {
                newItem.LastItem = i == count - 1;
            }
            setPosition(newItem);
            newItem.gameObject.SetActive(false);
            // _items.Enqueue(newItem);
        }
    }


    /// <summary>
    /// Set item at start of belt, offset by the item's width so they don't 
    /// collide in the recepticle bin.
    /// </summary>
    /// <param name="item"></param>
    void setPosition(PriceScript item)
    {
        // start placement on far side of belt, and evenly place them across belt.
        _itemPosition = new Vector3(-2.482f, 1.2319f, 0.266F - (ITEMWIDTH * ((_itemNo++) % NUMITEMSPERTABLETWIDTH)));
        item.transform.position = _itemPosition;

        item.transform.rotation = Quaternion.identity;
    }

    bool _priceWasZeroLastTime = false;


    PriceScript SpawnFromPool()
    {
        GameObject obj = objectPooler.Instance.SpawnFromPool("ITEM");
        PriceScript priceObj = obj.GetComponent<PriceScript>();
        return priceObj;
    }


    /// <summary>
    /// Place an Item it's desired location and roation, and then
    /// price it and set it Active so it will begin moving.
    /// </summary>
    public void PlaceItem()
    {
        if (!cLevel.PlayingLevel)
        {
            return;
        }



        PriceScript deQueItem = SpawnFromPool();

        // ensure that zero amounts aren't too common.
        // dont' even allow two of them in a row.
        decimal price = 0;        
        if (_priceWasZeroLastTime)
        {
            while (price == 0)
            {
                price = _randomCurrencyValueGenerator.Next();
            }
        }
        else
            price = _randomCurrencyValueGenerator.Next();

        deQueItem.Price = price;
        _priceWasZeroLastTime = price == 0;

        _totalPrice += price;
        _prices.Add(price);

        setPosition(deQueItem);
        
        deQueItem.gameObject.SetActive(true);

        if (deQueItem.LastItem)
        {
            cLevel.PlayingLevel = false;            
        }
    }


    void adjustCurrencyGenerator()
    {
        cLevelSettings level = cLevel.Settings;
        _randomCurrencyValueGenerator.Dollars.Max = level.Dollars;
        _randomCurrencyValueGenerator.HalfDollars.Max = level.HalfDollars;
        _randomCurrencyValueGenerator.Quarters.Max = level.Quarters;
        _randomCurrencyValueGenerator.Dimes.Max = level.Dimes;
        _randomCurrencyValueGenerator.Nickels.Max = level.Nickels;
        _randomCurrencyValueGenerator.Pennies.Max = level.Pennies;
    }
        

    //public void PutItemBackInPool(PriceScript poolItem)
    //{
    //    poolItem.gameObject.SetActive(false);
    //    setPosition(poolItem);
    //    //_items.Enqueue(poolItem);
    //}
}
