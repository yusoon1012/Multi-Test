using UnityEngine;
using Photon.Pun;


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
