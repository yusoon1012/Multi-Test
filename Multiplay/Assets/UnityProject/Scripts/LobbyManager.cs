using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0.0";
    public TMP_Text connectionInfoText;
    public Button joinButton;
    public TMP_InputField nickNameField;
    public string nickName; 
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
        joinButton.interactable = false;
        connectionInfoText.text="connecting to master server...";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        joinButton.interactable=true;
        connectionInfoText.text="Online : Succesfully connection MasterServer";
       
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        joinButton.interactable=false;
        connectionInfoText.text="Offline : MasterServer Connection \n fail Reconnect...";
    }
    public void Connect()
    {
        joinButton.interactable=false;

        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text="Join Room...";
            if(string.IsNullOrEmpty(nickNameField.text))
            {
                nickName = "Guest";
                nickNameField.text=nickName;
            }
            PlayerPrefs.SetString("NICKNAME", nickNameField.text);
            PhotonNetwork.NickName = nickNameField.text;
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            connectionInfoText.text="Offline : MasterServer Connection \n fail Reconnect...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message);
        connectionInfoText.text="New Room Create...";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers=20 });

    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
      
        
        connectionInfoText.text="Room Joined";
        
        PhotonNetwork.LoadLevel("PlayScene");

    }
}

