using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    public PriceScript _itemPrefab;
    Vector3 _itemPosition;

    Queue<PriceScript> _items;

    private void OnEnable()
    {
        _items = new Queue<PriceScript>();
        _itemPosition = new Vector3(-1.17199f, 1.231899f, 0);
        PlaceItem();
    }

    /// <summary>
    /// Preload items into the Item Queue, so they can be used
    /// and reused without overuse of Instantiate, and so that
    /// the GC is taxed with cleaning up piles of garbage.
    /// </summary>
    /// <param name="count"></param>
    void loadItems(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PriceScript newItem = Instantiate(_itemPrefab);
            newItem.transform.position = _itemPosition;
            newItem.transform.rotation = Quaternion.identity;
            newItem.gameObject.SetActive(false);
            _items.Enqueue(newItem);
        }
    }


    /// <summary>
    /// Place an Item it's desired location and roation, and then
    /// set it Active, so it can be priced and begin moving.
    /// </summary>
    public void PlaceItem()
    {
        if (_items.Count == 0)
            loadItems(1);
        PriceScript queItem = _items.Dequeue();
        queItem.transform.position = _itemPosition;
        queItem.transform.rotation = Quaternion.identity;
        queItem.gameObject.SetActive(true);
    }
}
