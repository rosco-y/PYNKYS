using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItemTrigger : MonoBehaviour
{
    public ItemPlacer _itemPlacer;

    private void OnTriggerEnter(Collider other)
    {
        _itemPlacer.PlaceItem();
    }
}
