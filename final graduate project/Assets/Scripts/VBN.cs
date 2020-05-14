using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class VBN : MonoBehaviour, iv
{

    public GameObject vbn;

    // Start is called before the first frame update
    void Start()
    {
        vbn = GameObject.Find("AttackBtn");
        vbn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {

    }
}