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
            // �����͸� ���� �� Rigidbody�� ���¸� ����ȭ�մϴ�.
            stream.SendNext(rb.velocity);
            stream.SendNext(rb.angularVelocity);
        }
        else
        {
            // �����͸� ���� �� Rigidbody�� ���¸� ������ȭ�մϴ�.
            rb.velocity = (Vector3)stream.ReceiveNext();
            rb.angularVelocity = (Vector3)stream.ReceiveNext();
        }
    }
}   
