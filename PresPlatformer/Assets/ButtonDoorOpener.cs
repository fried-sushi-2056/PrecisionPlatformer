using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoorOpener : MonoBehaviour
{
    public GameObject door;

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Check") || collision.CompareTag("Player")){
            Destroy(door);
        }
    }
}
