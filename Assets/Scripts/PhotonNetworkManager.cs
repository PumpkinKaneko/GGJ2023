using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonNetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject camera_ = null;


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // This function is callbacked when we are success to connect the master server.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Success to Connect the master server!");
        //PhotonNetwork.JoinRandomRoom();
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
    }

    // This function is callbacked when we disconnect from the server.
    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log($"Disconnect from the server: {cause.ToString()}");
    }

/*
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 8;
        PhotonNetwork.CreateRoom(null, roomOptions); 
    }
*/

    public override void OnJoinedRoom()
    {
        float rangeX = Random.Range(-10.0f, 10.0f);
        float rangeZ = Random.Range(-10.0f, 10.0f);
        GameObject player = PhotonNetwork.Instantiate(
            "Player",
            new Vector3(rangeX, 1f, rangeZ),
            Quaternion.identity
        );

        camera_.transform.parent = player.transform;
        camera_.transform.localPosition = new Vector3(0, 2, -5);
    }
}
