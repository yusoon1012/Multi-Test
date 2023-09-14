using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviourPunCallbacks,IPunObservable
{
    public static GameManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<GameManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static GameManager m_instance; // 싱글톤이 할당될 static 변수
    public GameObject ballPrefab;
    public GameObject bluePlayerPrefab;
    public GameObject redPlayerPrefab;
    public static int blueScore = 0;
    public static int redScore = 0;
    public TMP_Text blueScoreText;
    public TMP_Text redScoreText;

    public TMP_Text blueIdxText;
    public TMP_Text redIdxText;
    public TMP_Text playerIdxText;
    public int playerCount;
    public Transform ballPosition;
    public int playerIdx=0;
    public int blueteamIdx = 0;
    public int redteamIdx = 0;
    public Transform[] blueTeamSpawnPoint;
    public Transform[] redTeamSpawnPoint;
    private int currentBlueSpawnIndex = 0;
    private int currentRedSpawnIndex = 0;
    Transform blueSpawnPoint;
    Transform RedSpawnPoint;

    private void Awake()
    {
        SetScoreText();
        playerCount  = PhotonNetwork.PlayerList.Length;
        Debug.LogFormat("PlayerCount : {0}", playerCount);
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(ballPrefab.name, ballPosition.position, Quaternion.identity);


        }

        if (playerCount%2==0)
        {
            //PhotonNetwork.Instantiate(bluePlayerPrefab.name, blueTeamSpawnPoint[blueteamIdx].position, Quaternion.identity);
            //photonView.RPC("AddBluePlayerIdx", RpcTarget.AllBuffered);
            if (playerCount==2)
            {
                blueSpawnPoint = blueTeamSpawnPoint[0];

            }
            else if(playerCount==4)
            {
                blueSpawnPoint = blueTeamSpawnPoint[1];

            }
            else if (playerCount==6)
            {
                blueSpawnPoint = blueTeamSpawnPoint[2];

            }
            PhotonNetwork.Instantiate(bluePlayerPrefab.name, blueSpawnPoint.position, Quaternion.identity);
          


        }
        else
        {
            //PhotonNetwork.Instantiate(redPlayerPrefab.name, redTeamSpawnPoint[redteamIdx].position, Quaternion.identity);
            //photonView.RPC("AddRedPlayerIdx", RpcTarget.AllBuffered);
            // 레드 팀 플레이어 스폰
            if (playerCount==1)
            {
                RedSpawnPoint= redTeamSpawnPoint[0];

            }
            else if(playerCount==3) 
            {
                RedSpawnPoint= redTeamSpawnPoint[1];
            }
            else if(playerCount==5)
            {
                RedSpawnPoint= redTeamSpawnPoint[2];

            }

            PhotonNetwork.Instantiate(redPlayerPrefab.name, RedSpawnPoint.position, Quaternion.identity);
          

           


        }
       

        Debug.Log(playerIdx);
        

    }
    void Start()
    {

        Vector3 randomSpawnPos = Random.insideUnitSphere*5f;
        randomSpawnPos.y=1f;
        
       

        



    }

 
    
   
    [PunRPC]
    void AddPlayer()
    {
        playerIdx+=1;

    }
    [PunRPC]
    void AddBluePlayerIdx()
    {
        blueteamIdx+=1;


    }
    [PunRPC]
    void AddRedPlayerIdx()
    {
       
        redteamIdx+=1;


    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        
    }
    [PunRPC]
    void SetScoreTextRPC(int redScore, int blueScore)
    {
        redScoreText.text = redScore.ToString();
        blueScoreText.text = blueScore.ToString();
    }
    // Start is called before the first frame update
   
    void SetScoreText()
    {
        photonView.RPC("SetScoreTextRPC", RpcTarget.All, redScore, blueScore);
    }
    public void BallRespawn()
    {
        photonView.RPC("BallSpawn", RpcTarget.MasterClient);
    }
    [PunRPC]
    void BallSpawn()
    {
        if(PhotonNetwork.IsMasterClient)
        {
        PhotonNetwork.Instantiate(ballPrefab.name, ballPosition.position, Quaternion.identity);

        }

    }
    [PunRPC]
    void AddRedScore()
    {
        redScore+=1;
    }
    public void RedScoreUp()
    {
        photonView.RPC("AddRedScore", RpcTarget.AllBuffered);
    }
    [PunRPC]
    void AddBlueScore()
    {
        blueScore+=1;
    }
    public void BlueScoreUp()
    {
        photonView.RPC("AddBlueScore", RpcTarget.AllBuffered);

    }


    // Update is called once per frame

    void Update()
    {
        SetScoreText();
        blueIdxText.text=string.Format("BlueIdx : {0}",blueteamIdx);
        redIdxText.text=string.Format("RedIdx : {0}",redteamIdx);
        playerIdxText.text=string.Format("PlayerCount : {0}",playerCount);
       
    }
}
