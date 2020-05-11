using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWNetwork;
//using SWNetwork.RPC;

public class Card : MonoBehaviour
{
    [SerializeField]
    int atk, def;
    string card_id;

    public Text cardName;

    public void genId()
    {
        if (string.IsNullOrEmpty(card_id))
        {
            card_id = "JACK";
            cardName.text = card_id;
        }
        else
        {
            card_id = "xxxxxx";
            cardName.text = card_id;
        }
    }

    public string getId()
    {
        return card_id;
    }
}
