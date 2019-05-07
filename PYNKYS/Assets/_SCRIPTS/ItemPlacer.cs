using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    public PriceScript _itemPrefab;
    Vector3 _itemPosition;

    private void OnEnable()
    {
        _itemPosition = new Vector3(-1.17199f, 1.231899f, 0);
        PlaceItem();
    }


    public void PlaceItem()
    {
        PriceScript newItem = Instantiate(_itemPrefab);
        newItem.transform.position = _itemPosition;
        newItem.transform.rotation = Quaternion.identity;
        newItem.gameObject.SetActive(true);
    }
}
