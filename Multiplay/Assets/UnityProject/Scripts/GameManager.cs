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
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static GameManager m_instance; // �̱����� �Ҵ�� static ����
    public GameObject ballPrefab;
    public GameObject playerPrefab;
    public static int blueScore = 0;
    public static int redScore = 0;
    public TMP_Text blueScoreText;
    public TMP_Text redScoreText;
    public Transform ballPosition;
    private void Awake()
    {
        SetScoreText();
        if(PhotonNetwork.IsMasterClient)
        {
        PhotonNetwork.Instantiate(ballPrefab.name, ballPosition.position, Quaternion.identity);

        }

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
    void Start()
    {
        Vector3 randomSpawnPos = Random.insideUnitSphere*5f;
        randomSpawnPos.y=1f;

        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
        

    }
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


    }
}
