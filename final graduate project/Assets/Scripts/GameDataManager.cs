using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public int host_ID, remote_ID;
    public string host_Name, remote_Name;
    public string currentTurn;
    public int host_HP, remote_HP, host_Mana, remote_Mana;

    public GameDataManager(int hostID, int remoteID, string hostName, string remoteName)
    {
        setHostID(hostID);
        setRemoteID(remoteID);
        setHostName(hostName);
        setRemoteName(remoteName);
        setcurrentTurn(hostName);
    }

    public void setHostID(int id)
    {
        host_ID = id;
    }

    public void setRemoteID(int id)
    {
        remote_ID = id;
    }

    public void setHostName(string name)
    {
        host_Name = name;
    }

    public void setRemoteName(string name)
    {
        remote_Name = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
