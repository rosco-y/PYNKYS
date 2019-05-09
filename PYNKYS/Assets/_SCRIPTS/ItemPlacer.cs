using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;

public class ItemPlacer : MonoBehaviour
{
    const int ITEMSPERLEVEL = 10;
    public TMP_Text _totalPriceTag;
    public PriceScript _itemPrefab;
    Vector3 _itemPosition;
    public int ItemCount { get; set; }
    Queue<PriceScript> _items;


    /// <summary>
    /// OnEnable is fired whenever a GameObject is SetActive(true).
    /// </summary>
    private void OnEnable()
    {

        _items = new Queue<PriceScript>();
        _itemPosition = new Vector3(-1.17199f, 1.231899f, 0);
        PlaceItem();
    }

    public void Reset()
    {                     
        ItemCount = 0;
    }


    /// <summary>
    /// Preload items into the Item Queue, so they can be used
    /// and reused without overuse of Instantiate, and so that
    /// the GC is taxed with cleaning up piles of garbage.
    /// </summary>
    /// <param name="count">num items to add to the queue
    /// </param>
    void loadItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PriceScript newItem = Instantiate(_itemPrefab);
            setPosition(newItem);
            newItem.gameObject.SetActive(false);
            _items.Enqueue(newItem);
        }
    }


    void setPosition(PriceScript item)
    {
        item.transform.position = _itemPosition;
        item.transform.rotation = Quaternion.identity;
    }

    /// <summary>
    /// Place an Item it's desired location and roation, and then
    /// set it Active, so it can be priced and begin moving.
    /// </summary>
    public void PlaceItem()
    {
        if (_items.Count == 0)
            loadItems(1);  // 1 at  time until there are enough.

        _totalPriceTag.text = $"Level {cLevel.Level}-Item {++ItemCount}";
        if (ItemCount == ITEMSPERLEVEL)
        {
            cLevel.LevelUp();
            ItemCount = 0;
        }

        PriceScript queItem = _items.Dequeue();
        setPosition(queItem);
        queItem.gameObject.SetActive(true);
       
    }

    public void EnQueueItem(PriceScript enQueueThisItem)
    {
        enQueueThisItem.gameObject.SetActive(false);
        setPosition(enQueueThisItem);
        _items.Enqueue(enQueueThisItem);
    }
}
