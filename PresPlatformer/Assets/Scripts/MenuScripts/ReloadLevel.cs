using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    public PlayerMovementScript player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player")){
            player.ReloadCheckpoint();
        }
    }
}
