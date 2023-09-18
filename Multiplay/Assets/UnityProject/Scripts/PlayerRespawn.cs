using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPosition;
    PlayerHealth health;
    Animator animator;
    bool goalRespawn=false;
    // Start is called before the first frame update
    void Start()
    {
        respawnPosition=transform.position;
        health =GetComponent<PlayerHealth>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      if(GameManager.instance.isGoal==true)
        {
            if(goalRespawn==false)
            {
                goalRespawn=true;
                StartCoroutine(RespawnTimer(3));

            }
        }
    }
    public void ChangeRespawnPosition(Vector3 pos)
    {
        respawnPosition = pos;
    }
    public void Respawn()
    {

        StartCoroutine(RespawnTimer(3));
    }

   
    private IEnumerator RespawnTimer(int time)
    {
        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        Vector3 newPosition=randomSpawnPos + respawnPosition;
        newPosition.y = 1;
        yield return new WaitForSeconds(time);
        PhotonView photonView = GetComponent<PhotonView>();
        if(goalRespawn==true)
        {
            goalRespawn=false;
        }
        if(photonView != null )
        {
            photonView.RPC("RespawnPlayer", RpcTarget.All, newPosition);
        }
    }
    [PunRPC]
    void RespawnPlayer(Vector3 position)
    {
        transform.position = position;
        health.currentHealth=health.maxHealth;
        health.isDie=false;
        animator.Play("Blend Tree");
    }

    
}
