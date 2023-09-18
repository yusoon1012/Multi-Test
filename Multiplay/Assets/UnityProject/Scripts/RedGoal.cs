using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;

public class RedGoal : MonoBehaviour
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
        if(other.tag.Equals("Ball"))
        {
            if(PhotonNetwork.IsMasterClient)
            {
                Ball ball = other.GetComponent<Ball>();
                if (ball!=null)
                {
                    GameManager.instance.GoalTextUpdate(0, ball.bluePlayerName);
                }
                GameManager.instance.BlueScoreUp();
            PhotonNetwork.Destroy(other.gameObject);
            GameManager.instance.BallRespawn(3);
            }
        }
    }
  
}
