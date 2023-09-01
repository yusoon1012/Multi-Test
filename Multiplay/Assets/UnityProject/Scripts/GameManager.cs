using UnityEngine;
using Photon.Pun;


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
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomSpawnPos = Random.insideUnitSphere*5f;
        randomSpawnPos.y=1f;

        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);
        

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 randomSpawnPos = Random.insideUnitSphere*5f;
        randomSpawnPos.y=1f;
        if (PhotonNetwork.IsMasterClient)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                PhotonNetwork.Instantiate(ballPrefab.name, randomSpawnPos, Quaternion.identity);

            }
        }
       
    }
}
