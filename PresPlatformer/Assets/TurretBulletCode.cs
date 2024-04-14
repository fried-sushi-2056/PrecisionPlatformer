using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletCode : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D bulletRB;
    public float force;

    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        bulletRB.velocity = new Vector2(direction.x, direction.y).normalized * force;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            collider.GetComponent<PlayerMovementScript>().ReloadCheckpoint();//If it hits the player kill player and destroy bullet
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Check")) {}//Ignore the ground, wall, and ladder checks on the player
        else{
            Destroy(gameObject);//if it hits a wall or something just destroy the bullet
        }
    }
}
