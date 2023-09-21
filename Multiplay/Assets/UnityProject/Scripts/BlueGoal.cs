using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
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
                Ball ball=other.GetComponent<Ball>();
                if(ball!=null)
                {
                    GameManager.instance.GoalTextUpdate(1, ball.redPlayerName);
                    for(int i=0;i<PhotonNetwork.PlayerList.Length;i++)
                    {
                        Player player = PhotonNetwork.PlayerList[i];
                        
                        if(player.NickName==ball.redPlayerName)
                        {
                            string name = ball.redPlayerName;
                            
                            player.AddScore(100);
                        }
                    }
                    
                }
                GameManager.instance.RedScoreUp();
            PhotonNetwork.Destroy(other.gameObject);
            GameManager.instance.BallRespawn(3);
            }
        }
    }
}
