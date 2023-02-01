using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class OnlineGameManager : MonoBehaviourPunCallbacks, IPunTurnManagerCallbacks
{
    PunTurnManager punTurnManager = default;

    static RoomOptions RoomOPS = new RoomOptions()
    {
        MaxPlayers = 1,
        IsOpen = true,
    };

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = false;

        this.punTurnManager = GetComponent<PunTurnManager>();
        this.punTurnManager.enabled = true;
        this.punTurnManager.TurnDuration = 2.0f;
        this.punTurnManager.TurnManagerListener = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Success to Connect the master server!");
        PhotonNetwork.JoinOrCreateRoom("Room", RoomOPS, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("I joined into this room!");

        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("We got enough members for game play.");
                photonView.RPC(nameof(GameStart), RpcTarget.All);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom: " + newPlayer.NickName);

        if (PhotonNetwork.CurrentRoom.PlayerCount >= PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("We got enough members for game play.");
                this.photonView.RPC(nameof(GameStart), RpcTarget.All);
            }
        }
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log($"Disconnect from the server: {cause.ToString()}");
    }

    [PunRPC]
    void GameStart()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("I'm the master client!");
            this.punTurnManager.BeginTurn();
        }
    }


    // This function is callbacked when we disconnect from the server.


    // Update is called once per frame
    void Update()
    {
        if(this.photonView.IsMine != true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) == true) {
            Debug.Log("Action!");
            this.punTurnManager.SendMove(1, true);
        }
    }


    void IPunTurnManagerCallbacks.OnTurnBegins(int turn)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //this.BeginMyTurn();
        }
    }


    void IPunTurnManagerCallbacks.OnPlayerMove(Photon.Realtime.Player player, int turn, object move)
    {
        //this.ShareMove(move);
    }

    void IPunTurnManagerCallbacks.OnPlayerFinished(Photon.Realtime.Player player, int turn, object move)
    {
        if (!PhotonNetwork.IsMasterClient && PhotonNetwork.LocalPlayer.ActorNumber == player.ActorNumber + 1)
        {
            //this.BeginMyTurn();
        }
    }

    void IPunTurnManagerCallbacks.OnTurnCompleted(int turn)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Turn End!");
            punTurnManager.BeginTurn();
        }
    }

    void IPunTurnManagerCallbacks.OnTurnTimeEnds(int turn)
    {
        //punTurnManager.BeginTurn();
    }
}
