using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PYNKYS.SCRIPTS.PRICES;

public class cCmdContinue : MonoBehaviour
{
    public GameObject _userPrompt;
    public GameObject _scrollingReceipt;
    public GameObject _cmdContinueButton;

    ItemPlacer _reStarter;

    public void SetItemPlacer(ItemPlacer placer)
    {
        _reStarter = placer;
    }

    public void cmdContinue_Click(string placeHolder)
    {
        _userPrompt.SetActive(false);
        _scrollingReceipt.SetActive(false);
        _cmdContinueButton.SetActive(false);
        ///
        /// TODO: START NEXT LEVEL FROM HERE.
        /// (I think it will be something like 
        /// reload the pool and let'er go.)
        /// 
        cLevel.PlayingLevel = true;
        if (_reStarter != null)
            _reStarter.PlaceItem();
        else
        {
            throw new System.Exception("Must set Itemplacer \"reStarter\"");
        }
    }
}
