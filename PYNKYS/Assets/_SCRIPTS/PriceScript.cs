using PYNKYS.SCRIPTS.PRICES;
using TMPro;
using UnityEngine;

public class PriceScript : MonoBehaviour
{
    public TMP_Text _priceTag;
    // Start is called before the first frame update
    decimal _price;
    public bool LastItem { get; set; } = false;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
       
    }

    public decimal Price
    {
        set
        {
            _price = value;
            _priceTag.text = $"{_price:c}";
        }
        get
        {
            return _price;
        }
    }

    



}
