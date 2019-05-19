using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;
using UnityEngine.EventSystems;

public class ItemPlacer : MonoBehaviour
{
    const int NUMITEMSPERTABLETWIDTH = 8;
    const int ITEMSPERLEVEL = 21;
    public TMP_Text _totalPriceTag;
    public PriceScript _itemPrefab;
    Vector3 _itemPosition;
    cCurrencyValue _randomCurrencyValueGenerator;
    Queue<PriceScript> _items;
    decimal _totalPrice = 0m;
    bool _playingLevel = true;
    public TMP_InputField _userInputField;
    string _userAnswerSuccess = "";
    /// <summary>
    /// ITEMWIDT, _itemNo; used to space items evenly across the belt.
    /// </summary>
    const float ITEMWIDTH = 0.0763F;
    int _itemNo = 0;


    /// <summary>
    /// OnEnable is fired whenever a GameObject is SetActive(true).
    /// </summary>
    private void OnEnable()
    {

        _randomCurrencyValueGenerator = new cCurrencyValue();
        _items = new Queue<PriceScript>();
        

        loadItems(ITEMSPERLEVEL);
        Reset();
        
    }

    public void Reset()
    {
        _itemNo = 0;
        _totalPrice = 0;
        adjustCurrencyGenerator();
        _playingLevel = true;
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
            PriceScript newItem = Instantiate(_itemPrefab);
            newItem.ItemNo = i + 1;
            if (preLoading)
            {
                newItem.LastItem = i == count - 1;
            }
            setPosition(newItem);
            newItem.gameObject.SetActive(false);
            _items.Enqueue(newItem);
        }
    }


    
    void setPosition(PriceScript item)
    {
        // start placement on far side of belt, and evenly place them across belt.
        _itemPosition = new Vector3(-2.482f, 1.2319f, 0.266F - (ITEMWIDTH * ((_itemNo++) % NUMITEMSPERTABLETWIDTH)));
        item.transform.position = _itemPosition;

        item.transform.rotation = Quaternion.identity;
    }

    bool _priceWasZeroLastTime = false;
    /// <summary>
    /// Place an Item it's desired location and roation, and then
    /// set it Active, so it can be priced and begin moving.
    /// </summary>
    
    public void PlaceItem()
    {
        if (!_playingLevel)
            return;  

        //_totalPriceTag.text = $"Level {cLevel.Level} Item: {_items.Count}";
        
        PriceScript deQueItem = _items.Dequeue();

       
        decimal price = 0;
        // ensure that zero amounts aren't too common.
        // dont' even allow two of them in a row.
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
        _totalPriceTag.text = $"{_userAnswerSuccess} Level: {cLevel.Level} Item# {deQueItem.ItemNo} = {_totalPrice:C}";

        setPosition(deQueItem);
        
        deQueItem.gameObject.SetActive(true);

        if (deQueItem.LastItem)
        {
            _playingLevel = false;            
        }
    }


    public void CheckUserInput(string textValue)
    {
        decimal userValue;
        if (decimal.TryParse(_userInputField.text, out userValue))

        {
            _userInputField.gameObject.SetActive(false);
            
            if(userValue == _totalPrice)
            {
                cLevel.LevelUp();
                _userAnswerSuccess = "Success!!";
            }
            else
            {
                cLevel.LevelDown();
                _userAnswerSuccess = "Oooops.";
            }

            _totalPriceTag.text = $"{_userAnswerSuccess} Level: {cLevel.Level} Total Price = {_totalPrice}";
            Reset();
        }
        else
        {
            // illegal value, alert user so they can try again.
        }
    }

    ///// <summary>
    ///// GetUserInput
    ///// </summary>
    ///// <returns></returns>
    //public bool GetUserInput()
    //{
    //    decimal userInput;

    //    bool success = false;
    //    if (decimal.TryParse(_userInputField.text, out userInput))
    //    {
    //        success = userInput == _totalPrice;
    //    }
    //    return success;
    //}

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
        

    public void EnQueueItem(PriceScript enQueueThisItem)
    {
        if (enQueueThisItem.LastItem)
        {
            _userInputField.text = string.Empty;
            _userInputField.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(_userInputField.gameObject, null);
        }
        enQueueThisItem.gameObject.SetActive(false);
        setPosition(enQueueThisItem);
        _items.Enqueue(enQueueThisItem);
    }
}
