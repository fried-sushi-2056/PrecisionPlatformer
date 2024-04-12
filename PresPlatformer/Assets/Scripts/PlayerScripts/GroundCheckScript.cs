using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    [SerializeField] private Collider2D groundCheck;
    [SerializeField] private GameObject parent;
    public bool onGround;
    public Collider2D currentFloorCollider;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D groundCheck)
    {
        
        // check if collider has 'Ground' tag
        if (groundCheck.CompareTag("Ground"))
        {
            currentFloorCollider = groundCheck;
            onGround = true;
            UpdatePlatformVelocity(groundCheck);


        }

        else if (groundCheck.CompareTag("Disappearing")){
            currentFloorCollider = groundCheck;
            onGround = true;
            currentFloor.gameObject.GetComponent<DisappearingBlockScript>().StartDisappearing();
        }
    }
    
    void UpdatePlatformVelocity(Collider2D currentFloor)//gets friction value and added speed value from what you're currently
    {
        parent.GetComponent<PlayerMovementScript>().SetFloorFriction(currentFloor.gameObject.GetComponent<Rigidbody2D>().sharedMaterial.friction);
        parent.GetComponent<PlayerMovementScript>().touchGround(((Vector2)currentFloor.gameObject.GetComponent<Rigidbody2D>().velocity).x, ((Vector2)currentFloor.gameObject.GetComponent<Rigidbody2D>().velocity).y);
    }

    // detection of ground collider
    void OnTriggerExit2D(Collider2D groundCheck)
    {
        //check if collider has 'Ground' tag
        if (groundCheck.CompareTag("Ground"))
        {
            onGround = false;
            parent.GetComponent<PlayerMovementScript>().leaveGround();
        }

        else if (groundCheck.CompareTag("Disappearing"))
        {
            onGround = false;
            parent.GetComponent<PlayerMovementScript>().leaveGround();
        }
    }
    private void Update()
    {
        if(onGround) { UpdatePlatformVelocity(currentFloorCollider); }
    }
}
