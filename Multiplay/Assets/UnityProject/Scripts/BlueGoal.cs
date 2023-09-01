using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGoal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ball"))
        {
            if(PhotonNetwork.IsMasterClient)
            {

                GameManager.instance.RedScoreUp();
            PhotonNetwork.Destroy(other.gameObject);
            GameManager.instance.BallRespawn();
            }
        }
    }
}
