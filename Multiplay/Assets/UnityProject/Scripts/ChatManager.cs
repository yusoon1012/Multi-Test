using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;


public class ChatManager : MonoBehaviourPunCallbacks
{
    public static ChatManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<ChatManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }
    private static ChatManager m_instance;
    public List<string> chatList = new List<string>();
    public GameObject chatPrefab;
    public GameObject scroll;
    public GameObject scoreBoard;
    public GameObject inputField;
    public Transform chatContent;
    public TMP_InputField chatField;
    public TMP_Text chattingMember;
    public TMP_Text chatLog;
    string chatters;
    public ScrollRect chatRect;
   public bool chatOpen = false;
    bool scoreBoardOn = false;

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
            if(chatOpen==false)
            {
                chatOpen = true;
            inputField.SetActive(true);
                if (scroll.activeSelf==false)
                {
                scroll.SetActive(true);

                }

            }
            else
            {
                chatOpen = false;
                inputField.SetActive(false);
            }
                SendButtonOnClicked();
               chatField.ActivateInputField();
                //chatField.Select();
            

           
        }

        if(Input.GetKey(KeyCode.Tab))
        {
           scoreBoard.SetActive(true);
        }
        else
        {
            scoreBoard.SetActive(false);
        }



    }

    public void JoinNameInfoUpdate(string name)
    {
        photonView.RPC("JoinInfo", RpcTarget.All, name);
    }

    [PunRPC]
    public void JoinInfo(string name)
    {
        if (scroll.activeSelf==false)
        {
            scroll.SetActive(true);

        }
       
        chatLog.text +="\n"+"<color=#FFFF00>"+name+" joined game"+"</color>";
      
        StopAllCoroutines();
        chatRect.verticalNormalizedPosition = 0.0f; // 스크롤을 아래로 이동
        StartCoroutine(ChatVisibleRoutine());
    }

    public void SendButtonOnClicked()
    {
        if (chatField.text.Equals(""))
        {

            StartCoroutine(ChatVisibleRoutine());


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

            chatters+=string.Format("{0}       {1}", player.NickName,player.GetScore()+"\n");
           

        }

        
        chattingMember.text=chatters;
        chatRect.verticalNormalizedPosition = 0.0f; // 스크롤을 아래로 이동

    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        if(scroll.activeSelf==false)
        {
        scroll.SetActive(true);

        }
        StopAllCoroutines();
            
        chatLog.text +="\n"+msg;
        chatRect.verticalNormalizedPosition = 0.0f; // 스크롤을 아래로 이동
        StartCoroutine(ChatVisibleRoutine());
    }

    private IEnumerator ChatVisibleRoutine()
    {
        yield return new WaitForSeconds(10);
        scroll.SetActive(false);
    }

}
