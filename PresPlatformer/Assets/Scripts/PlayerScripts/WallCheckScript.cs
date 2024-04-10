using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheckScript : MonoBehaviour
{
    [SerializeField] private Collider2D wallCheck;
    [SerializeField] private GameObject parent;
    [SerializeField] private bool leftWall;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D wallCheck)
    {
        // check if collider has 'Ground' tag
        if (wallCheck.CompareTag("Wall"))
        {
            parent.GetComponent<PlayerMovementScript>().touchWall(wallCheck.gameObject.GetComponent<WallFriction>().GetFriction());
        }
    }


    // detection of ground collider
    void OnTriggerExit2D(Collider2D wallCheck)
    {
        //check if collider has 'Ground' tag
        if (wallCheck.CompareTag("Wall"))
        {
            parent.GetComponent<PlayerMovementScript>().leaveWall();
        }
    }
}
