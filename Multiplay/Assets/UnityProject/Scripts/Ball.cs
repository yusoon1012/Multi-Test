using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour, IPunObservable
{
    PhotonView photonView;
    private Rigidbody rb;
    Renderer ballColor;
    public string bluePlayerName;
    public string redPlayerName;
    public int teamNumber;
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
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag.Equals("Player"))
        //{
        //    this.photonView.TransferOwnership(collision.gameObject.GetComponent<PhotonView>().Owner);
        //}

        PhotonView playerView=collision.gameObject.GetComponent<PhotonView>();
        if (playerView != null)
        {
           
            if(playerView.Owner.ActorNumber%2==0)
            {
                bluePlayerName=playerView.Owner.NickName;
            }
            else
            {
                redPlayerName=playerView.Owner.NickName;
            }
            teamNumber=playerView.Owner.ActorNumber;

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
