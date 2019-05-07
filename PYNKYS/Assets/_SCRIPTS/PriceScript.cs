using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PYNKYS.SCRIPTS.PRICES;

public class PriceScript : MonoBehaviour
{
    public TMP_Text _priceTag;
    // Start is called before the first frame update
    decimal _price;
    cLevel _level;

    private void Awake()
    {
    }

    private void OnEnable()
    {

    }


}
