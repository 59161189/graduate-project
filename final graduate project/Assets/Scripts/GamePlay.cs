using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SWNetwork;

public class GamePlay : MonoBehaviour
{
    [SerializeField]
    protected Text localPlayer_name;
    [SerializeField]
    protected Text remotePlayer_name;
    [SerializeField]
    protected Player localPlayer;
    [SerializeField]
    protected Player remotePlayer;
    [SerializeField]
    protected Player currentTurnPlayer;
    [SerializeField]
    protected Player currentTurnTargetPlayer;

    /*HUD*/
    public Text PlayerTurnText;
    public Text check;
    public Slider localHealthbar;
    public Slider remoteHealthbar;
    public Slider localManaBar;
    public Slider remoteManaBar;
    public GameObject mainphaseBtnDis;
    public GameObject battphaseBtn;
    public GameObject battphaseBtnDis;
    public GameObject endphaseBtn;
    public GameObject endphaseBtnDis;

    public Netcode netCode;

    RemoteEventAgent remoteEventAgent;

    public enum GameState
    {
        Idle,
        GameStarted,
        TurnStarted,
        SummonPhase,
        AttackPhase,
        WaitingOpponent,
        OpponentAttack,
        EndPhase,
        GameFinished
    };

    [SerializeField]
    protected GameState gameState = GameState.Idle;

    public void Awake()
    {
        Debug.Log("Awaked");
    }

    // Start is called before the first frame update
    protected void Start()
    {
        netCode = FindObjectOfType<Netcode>();
        remoteEventAgent = gameObject.GetComponent<RemoteEventAgent>();
        //ตั้งค่าชื่อผู้เล่น
        NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) =>
        {
            if (successful)
            {
                foreach (SWPlayer swPlayer in reply.players)
                {
                    string playerName = swPlayer.GetCustomDataString();
                    string playerId = swPlayer.id;

                    if (playerId.Equals(NetworkClient.Instance.PlayerId))
                    {
                        Debug.Log("local player name : " + playerName);
                        localPlayer.PlayerId = playerId;
                        localPlayer.PlayerName = playerName;
                        localPlayer_name.text = playerName;
                    }
                    else
                    {
                        remotePlayer.PlayerId = playerId;
                        remotePlayer.PlayerName = playerName;
                        Debug.Log("remote player name : " + playerName);
                        remotePlayer_name.text = playerName;
                    }
                }
                //netCode.EnableRoomRemoteEventAgent();
            }
            else
            {
                Debug.Log("Failed to get players in room.");
            }
        });

        gameState = GameState.GameStarted;
        //GameFlow();
        OnGameStarted();
    }

    public void GameFlow()
    {
        switch (gameState)
        {
            case GameState.Idle:
                {
                    Debug.Log("IDLE");
                    break;
                }
            case GameState.GameStarted:
                {
                    Debug.Log("GameStarted");
                    OnGameStarted();
                    break;
                }
            case GameState.TurnStarted:
                {
                    Debug.Log("TurnStarted");
                    OnTurnStarted();
                    break;
                }
            case GameState.SummonPhase:
                {
                    Debug.Log("SummonPhase");
                    OnSummonPhase();
                    break;
                }
            case GameState.AttackPhase:
                {
                    Debug.Log("AttackPhase");
                    OnAttackPhase();
                    break;
                }
            case GameState.WaitingOpponent:
                {
                    Debug.Log("Waiting Opponent");
                    OnWaitingOpponent();
                    break;
                }
            case GameState.OpponentAttack:
                {
                    Debug.Log("Opponent Attack");
                    OnOpponentAttack();
                    break;
                }
            case GameState.EndPhase:
                {
                    Debug.Log("EndPhase");
                    OnEndPhase();
                    break;
                }
            case GameState.GameFinished:
                {
                    Debug.Log("GameFinished");
                    OnGameFinished();
                    break;
                }
        }
    }

    // ก่อนเริ่มเกมให้จัดการอะไรบ้าง
    protected void OnGameStarted()
    {
        // host เริ่มก่อน
        Debug.Log("OnGameStart");
        if (NetworkClient.Instance.IsHost)
        {
            //SwitchTurn();
            gameState = GameState.TurnStarted;
            Debug.Log("I am host and the turn started");
        }
        //GameFlow();
    }

    // เริ่มเทิร์นผู้เล่น
    protected void OnTurnStarted()
    {
        //netCode.NotifyOtherPlayersGameStateChanged();
        gameState = GameState.SummonPhase;
        //GameFlow();
    }

    public void OnSummonPhase()
    {
        /*เซตมานา*/
        /*จ่ายคอส*/
        /*เช็คมานา*/
        gameState = GameState.AttackPhase;
        //check.SetActive(true);
        //check.text = "summon phase";
        showBtn("main");
        /*GameFlow();*/
    }

    public void OnAttackPhase()
    {
        gameState = GameState.EndPhase;
        //check.SetActive(true);
        showBtn("battle");
        netCode.NotifyOtherPlayersGameStateChanged();
        //GameFlow();
    }

    public void OnWaitingOpponent()
    {
        //hideAllButton();
    }

    public void hideAllButton()
    {
        showBtn("none");
    }

    public void OnEndPhase()
    {
        hideAllButton();
        gameState = GameState.WaitingOpponent;
        netCode.NotifyOtherPlayersGameStateChanged();
        //GameFlow();
    }

    public void OnGameFinished()
    {
        // เช็คผู้ชนะ
    }

    public void dealDamageToLocalPlayer(int damage)
    {
        localHealthbar.value -= damage;
    }

    public void dealDamageToRemotePlayer(int damage)
    {
        remoteHealthbar.value -= damage;
    }

    public void SwitchTurn()
    {
        netCode.NotifyOtherPlayersTurnSwitched();
        if (currentTurnPlayer == null)
        {
            currentTurnPlayer = localPlayer;
            currentTurnTargetPlayer = remotePlayer;
            return;
        }

        if (currentTurnPlayer == localPlayer)
        {
            currentTurnPlayer = remotePlayer;
            currentTurnTargetPlayer = localPlayer;
            PlayerTurnText.text = "Opponent's turn";
            Debug.Log(currentTurnPlayer.PlayerName);
        }
        else
        {
            currentTurnPlayer = localPlayer;
            currentTurnTargetPlayer = remotePlayer;
            PlayerTurnText.text = "Your turn";
            Debug.Log(currentTurnPlayer.PlayerName);
        }
    }

    public void showBtn(string cases)
    {
        switch (cases)
        {
            case "main":
                {
                    mainphaseBtnDis.SetActive(false);
                    battphaseBtn.SetActive(false);
                    battphaseBtnDis.SetActive(true);
                    endphaseBtn.SetActive(false);
                    endphaseBtnDis.SetActive(true);
                    break;
                }
            case "battle":
                {
                    mainphaseBtnDis.SetActive(true);
                    battphaseBtn.SetActive(true);
                    battphaseBtnDis.SetActive(false);
                    endphaseBtn.SetActive(false);
                    endphaseBtnDis.SetActive(true);
                    break;
                }
            case "end":
                {
                    mainphaseBtnDis.SetActive(true);
                    battphaseBtn.SetActive(true);
                    battphaseBtnDis.SetActive(false);
                    endphaseBtn.SetActive(false);
                    endphaseBtnDis.SetActive(true);
                    break;
                }
            case "none":
                {
                    mainphaseBtnDis.SetActive(false);
                    battphaseBtn.SetActive(false);
                    battphaseBtnDis.SetActive(false);
                    endphaseBtn.SetActive(false);
                    endphaseBtnDis.SetActive(false);
                    break;
                }
        }
    }

    //Netcode 
    public void OnGameStateChanged()
    {
        GameFlow();
    }

    public void OnOpponentAttack()
    {
        gameState = GameState.TurnStarted;
        GameFlow();
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene("LobbyScene");
    }

    public void OnAttacked(int damage)
    {
        netCode.NotifyOtherPlayerAttacked();
        if (NetworkClient.Instance.IsHost)
            localHealthbar.value -= damage;
    }

    public void MessagePlayer()
    {
        NetworkClient.Lobby.MessagePlayer(remotePlayer.PlayerId, "Hello", (bool successful, SWLobbyError error) => {
            if (successful)
            {
                Debug.Log("Sent player message");
            }
            else
            {
                Debug.Log("Failed to send player message " + error);
            }
        });
    }
}
