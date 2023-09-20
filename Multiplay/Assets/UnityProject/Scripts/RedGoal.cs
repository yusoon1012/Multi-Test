using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

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
                    for (int i = 0; i<PhotonNetwork.PlayerList.Length; i++)
                    {
                        Player player = PhotonNetwork.PlayerList[i];

                        if (player.NickName==ball.bluePlayerName)
                        {
                            string name = ball.redPlayerName;
                           
                            player.AddScore(100);
                        }
                    }
                }
                GameManager.instance.BlueScoreUp();
            PhotonNetwork.Destroy(other.gameObject);
            GameManager.instance.BallRespawn(3);
            }
        }
    }
  
}
