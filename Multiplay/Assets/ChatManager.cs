using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string>();
    public GameObject chatPrefab;
    public Transform chatContent;
    public TMP_InputField chatField;
    public TMP_Text chattingMember;
    public TMP_Text chatLog;
    string chatters;
    public ScrollRect chatRect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ChatUpdate();
        if (Input.GetKeyDown(KeyCode.Return))
        {
          

                SendButtonOnClicked();
               // chatField.ActivateInputField();
                chatField.Select();
            

           
        }


    }

    public void SendButtonOnClicked()
    {
        if (chatField.text.Equals(""))
        {
            

          
            return;
        }
        string chatMessage = string.Format("{0} : {1}", PhotonNetwork.LocalPlayer.NickName, chatField.text);
        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, chatMessage);
        ReceiveMsg(chatMessage);
        
       
        chatField.text="";
        
    }
    void ChatUpdate()
    {
        chatters="Player List\n";
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            chatters+=player.NickName+"\n";

        }
        chattingMember.text=chatters;
        chatRect.verticalNormalizedPosition = 0.0f; // 스크롤을 아래로 이동

    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text +="\n"+msg;
        chatRect.verticalNormalizedPosition = 0.0f; // 스크롤을 아래로 이동
       
    }

}
