using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelScript : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player")){
            gameManager.LoadNextScene();
        }
    }
}
