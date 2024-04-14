using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Transform player;
    public Transform barrel;

    public float bulletSpeed = 10;
    public bool canShoot = true;

    void Update(){
        if(Mathf.Abs(bulletSpawnPoint.position.x-player.position.x) < 10 && Mathf.Abs(bulletSpawnPoint.position.y-player.position.y) < 10 && canShoot){
            canShoot = false;
            Fire();
        }
    }

    void Fire(){
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(1,0,0) * bulletSpeed;
        barrel.rotation = bulletSpawnPoint.rotation;
        print(barrel.rotation);
        StartCoroutine(Reload());
    }

    IEnumerator Reload(){
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
}
