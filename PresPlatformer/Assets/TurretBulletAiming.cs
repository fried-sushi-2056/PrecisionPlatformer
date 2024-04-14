using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletAiming : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
