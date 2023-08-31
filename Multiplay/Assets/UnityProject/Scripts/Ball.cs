using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            this.photonView.TransferOwnership(collision.gameObject.GetComponent<PhotonView>().Owner);
        }
    }
}   
