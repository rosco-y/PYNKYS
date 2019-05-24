using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cReceiptPrice : MonoBehaviour
{

    string _lineItem;

    public string LineItem
    {
        set { _lineItem = value; }
        get { return _lineItem; }
    }
}
