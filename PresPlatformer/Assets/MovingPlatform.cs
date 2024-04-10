using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeedX;
    public float platformSpeedY;
    public float leftBound;
    public float rightBound;
    public Rigidbody2D rb;

    private bool goingRight = true;

    public float getPlatformSpeedX()
    {
        return platformSpeedX;
    }

    public float getPlatformSpeedY()
    {
        return platformSpeedY;
    }

    public void platformMove()
    {
        if(goingRight)
        {
            rb.velocity = new Vector2(platformSpeedX, platformSpeedY);
        }
        else if (!goingRight)
        {
            rb.velocity = new Vector2(-platformSpeedX, platformSpeedY);
        }
    }

    void Update()
    {
        platformMove();
        if(rb.position.x > rightBound)
        {
            goingRight = false;
        }
        else if (rb.position.x < leftBound)
        {
            goingRight = true;
        }
    }
}
