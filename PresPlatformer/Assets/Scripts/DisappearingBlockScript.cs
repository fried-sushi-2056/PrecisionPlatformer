using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingBlockScript : MonoBehaviour
{
    public Collider2D blockCollider;
    public GameObject block;

    public void StartDisappearing(){
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        Color c = GetComponent<Renderer>().material.color;
        for (float green = 1f; green >= 0; green -= 0.1f)
        {
            c.g = green;
            GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.1f);
        }
    }
}