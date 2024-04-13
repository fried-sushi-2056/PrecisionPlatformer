using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletCode : MonoBehaviour
{
    public PlayerMovementScript player;

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            collider.GetComponent<PlayerMovementScript>().ReloadCheckpoint();
            Destroy(gameObject);
        }
    }
}
