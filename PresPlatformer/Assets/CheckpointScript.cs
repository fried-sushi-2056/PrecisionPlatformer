using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Transform tf;
    public PlayerMovementScript player;
    private float xPos;
    private float yPos;

    void Start()
    {
        xPos = tf.position.x;
        yPos = tf.position.y;
    }
    
    public void OnTriggerEnter2D()
    {
        player.Checkpoint(xPos, yPos);
    }

    public float ReturnXPos()
    {
        return xPos;
    }

    public float ReturnYPos()
    {
        return yPos;
    }
}
