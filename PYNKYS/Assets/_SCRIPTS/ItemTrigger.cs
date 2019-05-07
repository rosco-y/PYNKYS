using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{

    public ItemPlacer _itemPlacer;

    private void OnTriggerEnter(Collider other)
    {
        PriceScript ME = other.GetComponent<PriceScript>();
        _itemPlacer.EnQueueItem(ME);
        _itemPlacer.PlaceItem();
    }

}
