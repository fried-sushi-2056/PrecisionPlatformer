using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float checkpointX = 0;
    public float checkpointY = 5;


    [SerializeField] public bool usingControler = false;


    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 0.3f;
    [SerializeField] private float jumpingPower = 20f;
    [SerializeField] private float walljumpPower = 20f;
    [SerializeField] private bool isFacingRight = true;
    [SerializeField] private bool onGround;
    [SerializeField] private float airSlow = 0.1f;
    [SerializeField] private float capLeftRight = 9;

    [SerializeField] private float deadzone = 0.2f;

    [SerializeField] public float currentGroundSpeedX;
    [SerializeField] public float currentGroundSpeedY;
    [SerializeField] public float currentGroundFriction = 0;

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

    private bool canRight = true;
    private bool canLeft = true;
    private bool canFriction = true;
    private bool canWallJump = true;

    private bool rightDirectionPressed = false;
    private bool leftDirectionPressed = false;



    void Update()
    {



        if ((rightDirectionPressed && horizontal < capLeftRight) && canRight && !leftDirectionPressed)
        {
            StartCoroutine(SpeedChange(speed, canRight, 1));
        }
        if ((leftDirectionPressed && horizontal > -1 * capLeftRight) && canLeft && !rightDirectionPressed)
        {
            StartCoroutine(SpeedChange(-1*speed, canLeft, 1));
        }

        if ((horizontal > 0 && onGround && canFriction && (!rightDirectionPressed || (leftDirectionPressed && rightDirectionPressed))) || horizontal > capLeftRight)
        {
            StartCoroutine(SpeedChange(-1*currentGroundFriction, canFriction,1));
        }

        if ((horizontal < 0 && onGround && canFriction && (!leftDirectionPressed || (leftDirectionPressed && rightDirectionPressed))) || horizontal < -1*capLeftRight)
        {
            StartCoroutine(SpeedChange(currentGroundFriction, canFriction,1));
        }

        if (horizontal > 0 && !onGround && canFriction)
        {
            StartCoroutine(SpeedChange(-1*airSlow, canFriction, 1));
        }

        if (horizontal < 0 && !onGround && canFriction)
        {
            StartCoroutine(SpeedChange(airSlow, canFriction, 1));
        }

        if (Input.GetKeyDown("d") || Input.GetKeyDown("right") || (Input.GetAxis("Horizontal") > deadzone && usingControler))
        {
            rightDirectionPressed = true;
        }
        if (Input.GetKeyUp("d") || Input.GetKeyUp("right") || (Input.GetAxis("Horizontal")! > deadzone && usingControler))
        {
            rightDirectionPressed = false;
        }

        if (Input.GetKeyDown("a") || Input.GetKeyDown("left") || (Input.GetAxis("Horizontal") < -1 * deadzone && usingControler))
        {
            leftDirectionPressed = true;
        }
        if (Input.GetKeyUp("a") || Input.GetKeyUp("left") || (Input.GetAxis("Horizontal")! < -1 * deadzone && usingControler))
        {
            leftDirectionPressed = false;
        }

        if (Input.GetButtonDown("Jump") && (onGround || onWall))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetButtonDown("Jump") && onWall && !onGround)
        {
            if(wallDirection == "Left")
            {
                StartCoroutine(InstantSpeedChange(walljumpPower, canWallJump, 10));
            }

            if(wallDirection == "Right")
            {
                StartCoroutine(InstantSpeedChange(-walljumpPower, canWallJump, 10));
            }
        }

        if(wallJumpSpeed != 0f && canSlowWall)
        {
            StartCoroutine(SlowWallSpeed());
        }


        Flip();
        //WallSlide();
    }


    public void SetFloorFriction(float friction)
    {
        currentGroundFriction = friction;
    }

    IEnumerator SpeedChange(float rightSpeed, bool cooldown, int frameDelay)
    {
        cooldown = false;
        if ((horizontal == 0) || (Mathf.Sign(horizontal) == Mathf.Sign(horizontal + rightSpeed)))
        {
            horizontal += rightSpeed;
        }
        else
        {
            horizontal = 0f;
        }
        for (int i = 0; i < frameDelay; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        cooldown = true;
    }
    IEnumerator InstantSpeedChange(float rightSpeed, bool cooldown, int frameDelay)
    {
        cooldown = false;

        horizontal = rightSpeed;

        for (int i = 0; i < frameDelay; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        cooldown = true;
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
            rb.velocity = new Vector2(horizontal + currentGroundSpeedX, (rb.velocity.y + currentGroundSpeedY)* currentWallFriction);
        }
        else
        {
            rb.velocity = new Vector2(horizontal + currentGroundSpeedX, rb.velocity.y);
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
        currentGroundFriction = 0f;
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
