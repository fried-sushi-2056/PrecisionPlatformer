using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevel : MonoBehaviour
{
    public GameManager gm;

    void OnTriggerEnter2D()
    {
        gm.ReloadLevel();
    }
}
