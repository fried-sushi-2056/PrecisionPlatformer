using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public Transform checkpoint;
    public PlayerMovementScript player;
    private float xPos;
    private float yPos;
    public GameObject glowPrefab;
    private bool canGlow = true;

    void Start()
    {
        xPos = checkpoint.position.x;
        yPos = checkpoint.position.y;
    }
    
    public void OnTriggerEnter2D()
    {
        if(canGlow){
            player.Checkpoint(xPos, yPos);
            Glow();
            canGlow = false;
        }
    }

    public void Glow(){
        var glow = Instantiate(glowPrefab, checkpoint.position, checkpoint.rotation);
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
