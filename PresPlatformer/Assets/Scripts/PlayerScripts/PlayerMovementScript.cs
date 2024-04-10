using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private bool isWallSliding;

    private float vertical;
    private float ladderSpeed = 8f;


    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool onGround;

    [SerializeField] public float currentGroundSpeedX;
    [SerializeField] public float currentGroundSpeedY;
    [SerializeField] public float currentWallFriction;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private bool onWall;

    [SerializeField] private bool onLadder;
    [SerializeField] private bool isClimbing;
    




    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

<<<<<<< Updated upstream
=======
        if (Input.GetButtonDown("Jump") && onWall)
        {
            if(wallDirection == "Left")
            {
                wallJumpSpeed = walljumpPower;
            }

            if(wallDirection == "Right")
            {
                wallJumpSpeed = -walljumpPower;
            }
        }

        if(wallJumpSpeed != 0f && canSlowWall)
        {
            StartCoroutine(SlowWallSpeed());
        }


>>>>>>> Stashed changes
        Flip();
        //WallSlide();
    }

    private void FixedUpdate()
    {
        if (onWall && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y * currentWallFriction);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }


        //Adds velocity upwards when the player is on Ladder
        
        if(isClimbing){
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical*ladderSpeed);
            print("walling");
        }
        else{
            rb.gravityScale = 4f;
            print("notwalling");
        }

        

    }

    public void touchGround()
    {
        onGround = true;
    }

    public void leaveGround()
    {
        onGround = false;
    }


    public void touchWall(float friction)
    {
        currentWallFriction = friction;
        onWall = true;
    }

    public void leaveWall()
    {
        currentWallFriction = 1;
        onWall = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    /*
    //Check if player is in contact with wall
    private bool IsWalled(){
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    
    private void WallSlide(){
        if(onWall && !onGround && horizontal != 0f){
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else{
            isWallSliding = false;
        }
    }
    */


//Checks if player is on/off the ladder
public void TouchLadder(){
    onLadder = true;
}

public void OffLadder(){
    onLadder = false;
}

//Checks if the player is Climbing a ladder
public void CurClimbing(){
    isClimbing = true;
}

public void NotClimbing(){
    isClimbing = false;
}


}


