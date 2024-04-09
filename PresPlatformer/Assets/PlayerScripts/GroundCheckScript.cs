using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private GameObject parent;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D groundCheck)
    {
        // check if collider has 'Ground' tag
        if (groundCheck.CompareTag("Ground"))
        {
            parent.GetComponent<PlayerMovementScript>().touchGround();
        }
    }

    // detection of ground collider
    void OnTriggerExit2D(Collider2D groundCheck)
    {
        //check if collider has 'Ground' tag
        if (groundCheck.CompareTag("Ground"))
        {
            parent.GetComponent<PlayerMovementScript>().leaveGround();
        }
    }
}  
