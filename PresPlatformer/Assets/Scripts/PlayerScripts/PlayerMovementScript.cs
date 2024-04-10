using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    private bool isWallSliding;
    //private float wallSlidingSpeed = 2f;
    public float checkpointX = 0;
    public float checkpointY = 5;

    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float walljumpPower;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool onGround;

    [SerializeField] public float currentGroundSpeedX;
    [SerializeField] public float currentGroundSpeedY;
    [SerializeField] public float currentWallFriction;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private bool onWall;
    [SerializeField] private string wallDirection;

    private float wallJumpSpeed = 0;
    private bool canSlowWall = true;

    [SerializeField] private WallCheckScript leftWall;
    [SerializeField] private WallCheckScript rightWall;


    private void Start()
    {
        walljumpPower = 2.25f * speed;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && (onGround || onWall))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

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
        
        
        Flip();
        //WallSlide();
    }
    IEnumerator SlowWallSpeed()
    {
        canSlowWall = false;
        if(wallJumpSpeed < 1f)
        {
            wallJumpSpeed = 0f;
            yield return new WaitForSeconds(.1f);
            canSlowWall = true;
        }
        if (wallJumpSpeed > 1f)
        {
            wallJumpSpeed = 0f;
            yield return new WaitForSeconds(.1f);
            canSlowWall = true;
        }
        if (wallJumpSpeed > 0f)
        {
            wallJumpSpeed += -1f;
            yield return new WaitForSeconds(.1f);
            canSlowWall = true;
        }
        if (wallJumpSpeed < 0f)
        {
            wallJumpSpeed += 1f;
            yield return new WaitForSeconds(.1f);
            canSlowWall = true;
        }

    }

    private void FixedUpdate()
    {
        if (onWall && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(horizontal * speed + currentGroundSpeedX + wallJumpSpeed, (rb.velocity.y + currentGroundSpeedY)* currentWallFriction);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed + currentGroundSpeedX + wallJumpSpeed, rb.velocity.y);
        }
    }

    public void touchGround(float x, float y)
    {
        onGround = true;
        currentGroundSpeedX = x;
        currentGroundSpeedY = y;
    }

    public void leaveGround()
    {
        onGround = false;
    }


    public void touchWall(float friction, bool isLeft)
    {
        currentGroundSpeedX = 0;
        currentWallFriction = friction;
        onWall = true;
        if(isLeft)
        {
            wallDirection = "Left";
        }
        if(!isLeft)
        {
            wallDirection = "Right";
        }
    }

    public void leaveWall()
    {
        currentWallFriction = 1;
        onWall = false;
        wallDirection = "None";
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            leftWall.Flip();
            rightWall.Flip();
        }
    }

    public void Checkpoint(float x, float y)
    {
        checkpointX = x;
        checkpointY = y;
    }

    public void ReloadCheckpoint()
    {
        playerTransform.position = new Vector2(checkpointX,checkpointY);
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

}