using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class RigidBodyView : MonoBehaviourPun, IPunObservable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    [PunRPC]
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 로컬 플레이어의 데이터를 전송
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
            stream.SendNext(rb.rotation);
            stream.SendNext(rb.angularVelocity);
        }
        else
        {
            // 원격 플레이어의 데이터를 수신
            rb.position = (Vector3)stream.ReceiveNext();
            rb.velocity = (Vector3)stream.ReceiveNext();
            rb.rotation = (Quaternion)stream.ReceiveNext();
            rb.angularVelocity = (Vector3)stream.ReceiveNext();
        }
    }
}
