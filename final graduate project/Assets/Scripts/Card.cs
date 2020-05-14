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
    int card_id;

    public Text cardName;

    public int genId(GameObject card)
    {
        this.card_id = card.GetInstanceID();
        return this.card_id;

        /*if (string.IsNullOrEmpty(card_id))
        {
            card_id = "JACK";
            cardName.text = card_id;
        }
        else
        {
            card_id = "xxxxxx";
            cardName.text = card_id;
        }*/
    }

    public int getId()
    {
        return card_id;
    }
}
