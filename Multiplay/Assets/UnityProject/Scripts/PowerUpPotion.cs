using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PowerUpPotion : MonoBehaviour
{
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView=GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PhotonView targetView=other.GetComponent<PhotonView>();
            int viewId = targetView.ViewID;
            photonView.RPC("ScaleUp",RpcTarget.AllBuffered,viewId);
        }
    }
    [PunRPC]
    private void ScaleUp(int id_)
    {
        PhotonView targetView = PhotonView.Find(id_);
        Vector3 upScale = new Vector3(3, 3, 3);
        Transform targetTransform = targetView.transform;
        targetTransform.localScale = upScale;
    }
}
