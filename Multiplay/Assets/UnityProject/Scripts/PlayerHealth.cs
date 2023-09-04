using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    public bool isDie = false;
    public Slider healthSlider;
    PlayerRespawn respawn;

   

    // Start is called before the first frame update
    void Start()
    {
        currentHealth=maxHealth;
        respawn=GetComponent<PlayerRespawn>();
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = currentHealth/maxHealth;
        if(currentHealth<=0)
        {
            if(isDie==false)
            {
                isDie=true;
            respawn.Respawn();

            }
        }
    }
}
