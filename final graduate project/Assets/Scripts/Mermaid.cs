using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class Mermaid : MonoBehaviour, IVirtualButtonEventHandler
{
    VirtualButtonBehaviour[] virtualButtonBehaviours;
    string vbName;

    public GameObject virtualBtn;

    public HPBar remoteHPBar;
    public Text checkEvent;
    public Animator catAni;

    void Start()
    {
        //Register with the virtual buttons TrackableBehaviour
        virtualButtonBehaviours = GetComponentsInChildren<VirtualButtonBehaviour>();

        for (int i = 0; i < virtualButtonBehaviours.Length; i++)
            virtualButtonBehaviours[i].RegisterEventHandler(this);
        
          /*virtualBtn = GameObject.Find("Attack");
           virtualBtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
           catAni.GetComponent<Animator>();*/
    }

    

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        vbName = vb.VirtualButtonName;
        if (vbName == "AttackBtn")
        {
            Debug.Log("btn pressed.");
            remoteHPBar.hp_decrease(300);
            checkEvent.text = "attacked.";
            attack();
        }
        else if (vbName == "AttackedBtn")
        {

        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        Debug.Log("btn released.");
    }

    public void attack()
    {
        catAni.Play("CatJump 0");
    }
}
