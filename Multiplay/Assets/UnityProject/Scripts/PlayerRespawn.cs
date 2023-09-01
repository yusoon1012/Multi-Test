using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeRespawnPosition(Vector3 pos)
    {
        respawnPosition = pos;
    }
    public void Respawn()
    {

        StartCoroutine(RespawnTimer());
    }

   
    private IEnumerator RespawnTimer()
    {
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        Vector3 newPosition=randomSpawnPos + respawnPosition;
        newPosition.y = 1;
        yield return new WaitForSeconds(3);
        PhotonView photonView = GetComponent<PhotonView>();
        if(photonView != null )
        {
            photonView.RPC("RespawnPlayer", RpcTarget.All, newPosition);
        }
    }
    [PunRPC]
    void RespawnPlayer(Vector3 position)
    {
        transform.position = position;
    }

    
}
