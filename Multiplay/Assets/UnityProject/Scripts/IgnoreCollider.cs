using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollider : MonoBehaviour
{
    BoxCollider myCollider;
    // Start is called before the first frame update
    void Start()
    {
        myCollider=GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            Physics.IgnoreCollision(myCollider, collision.collider);
        }
    }

}
