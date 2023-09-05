using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour,IPunObservable
{
    public float maxHealth = 10f;
    public float currentHealth;
    public bool isDie = false;
    public Slider healthSlider;
    PlayerRespawn respawn;
    PhotonView photonView;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        respawn=GetComponent<PlayerRespawn>();
        photonView=GetComponent<PhotonView>();
        animator=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
        if (currentHealth<=0)
        {
            if(isDie==false)
            {
                photonView.RPC("DieAnimation", RpcTarget.All);
                isDie=true;
            respawn.Respawn();

            }
        }
    }
    [PunRPC]
    public void UpdateHealth(float damage)
    {
       
        currentHealth-=damage;
        
        if (photonView.IsMine)
        {
            photonView.RPC("UpdateHealth", RpcTarget.OthersBuffered, damage);
        }
    }
    [PunRPC]
    public void DieAnimation()
    {
        animator.Play("f_death_A");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {


        if (stream.IsWriting)
        {
            // 로컬 플레이어의 데이터를 전송
            stream.SendNext(currentHealth);
            stream.SendNext(isDie);
        }
        else
        {
            // 원격 플레이어의 데이터를 수신
            currentHealth = (float)stream.ReceiveNext();
            isDie = (bool)stream.ReceiveNext();
        }
    }
}

