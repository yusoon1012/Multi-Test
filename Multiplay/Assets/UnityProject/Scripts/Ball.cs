using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour, IPunObservable
{
    PhotonView photonView;
    private Rigidbody rb;
    Renderer ballColor;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ballColor=GetComponent<Renderer>();


    }
    // Start is called before the first frame update
    void Start()
    {
        photonView=GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine == false)
        {
            ballColor.material.color= Color.red;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            this.photonView.TransferOwnership(collision.gameObject.GetComponent<PhotonView>().Owner);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 데이터를 보낼 때 Rigidbody의 상태를 직렬화합니다.
            stream.SendNext(rb.velocity);
            stream.SendNext(rb.angularVelocity);
        }
        else
        {
            // 데이터를 받을 때 Rigidbody의 상태를 역직렬화합니다.
            rb.velocity = (Vector3)stream.ReceiveNext();
            rb.angularVelocity = (Vector3)stream.ReceiveNext();
        }
    }
}   
