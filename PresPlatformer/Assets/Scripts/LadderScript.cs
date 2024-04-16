using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;

    [SerializeField] private Rigidbody2D playerRb;
    //[SerializeField] private Collider2D playerLadCol;

    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;

        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            playerRb.gravityScale = 0f;
            playerRb.velocity = new Vector2(playerRb.velocity.x, vertical * speed);
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            print("Laddering");
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            if (isClimbing)
            {
                playerRb.gravityScale = 4f;
            }
            isLadder = false;
            isClimbing = false;
            print("notLaddering");
        }
    }
}