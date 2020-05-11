using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;
using UnityEngine.Events;
using System;

public class Netcode : MonoBehaviour
{
    public UnityEvent OnGameStateChanged = new UnityEvent();
    public UnityEvent OnLocalAttack = new UnityEvent();
    public UnityEvent OnLeftRoom = new UnityEvent();

    RoomPropertyAgent roomPropertyAgent;
    RoomRemoteEventAgent roomRemoteEventAgent;

    const string GAME_STATE_CHANGED = "GameStateChanged";
    const string OPPONENT_CONFIRMED = "OpponentConfirmed";
    const string LEAVE_ROOM_WORKED = "LeaveRoom";

    private void Awake()
    {
        roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();
        roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
    }

    /*public void EnableRoomRemoteEventAgent()
    {
        RoomRemoteEventAgent.Initialize();
    }*/

    public void NotifyHostPlayerOpponentConfirmed()
    {
        roomRemoteEventAgent.Invoke(OPPONENT_CONFIRMED);
    }

    public void NotifyOtherPlayersGameStateChanged()
    {
        roomRemoteEventAgent.Invoke(GAME_STATE_CHANGED);
    }

    public void NotifyOtherPlayersTurnSwitched()
    {
        roomRemoteEventAgent.Invoke("TurnSwitched");
    }

    public void NotifyOtherPlayerAttacked()
    {
        roomRemoteEventAgent.Invoke("AttackOpponent");
    }

    public void LeaveRoom()
    {
        NetworkClient.Instance.DisconnectFromRoom();
        NetworkClient.Lobby.LeaveRoom((successful, error) => {

            if (successful)
            {
                Debug.Log("Left room");
            }
            else
            {
                Debug.Log($"Failed to leave room {error}");
            }

            OnLeftRoom.Invoke();
        });
    }

    //ทดสอบการติดต่อระหว่างผู้เล่น
    public void SendText()
    {
        roomRemoteEventAgent.Invoke("SwitchTurn");
        Debug.Log("Switch Turn");
    }

    public void OnGameStateChangedRemoteEvent()
    {
        OnGameStateChanged.Invoke();
    }

    public void OnLocalAttackRemoteEvent()
    {
        OnLocalAttack.Invoke();
    }
}
