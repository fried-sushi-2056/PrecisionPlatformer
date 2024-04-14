using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform player;
    public Transform bulletSpawnPoint;
    public Transform barrel;
    public float angle;

    void Update()
    {   //checks if player within 10x10 area around the turret
        if (Mathf.Abs(bulletSpawnPoint.position.x - player.position.x) < 10 && Mathf.Abs(bulletSpawnPoint.position.y - player.position.y) < 10)
        {
            CalculateAngle();
        }
    }

    public void CalculateAngle()
    {
        Vector3 direction = player.position - bulletSpawnPoint.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        barrel.rotation = Quaternion.Slerp(barrel.rotation, angleAxis, Time.deltaTime * 50);
    }


}
