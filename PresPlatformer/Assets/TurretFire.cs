using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public Transform player;

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
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        StartCoroutine(Reload());
        print(bulletSpawnPoint.up);
    }

    IEnumerator Reload(){
        yield return new WaitForSeconds(2f);
        canShoot = true;
    }
}
